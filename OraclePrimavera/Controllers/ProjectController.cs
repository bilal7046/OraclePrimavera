using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Evaluation;
using Microsoft.CodeAnalysis;
using OraclePrimavera.Data;
using OraclePrimavera.DTOs;
using OraclePrimavera.Helper;
using OraclePrimavera.IRepository;
using OraclePrimavera.Repository;

namespace OraclePrimavera.Controllers
{
    public class ProjectController : Controller
    {
        private readonly IApiClient _apiClient;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly FileUploader _fileUploader;

        public ProjectController(IApiClient apiClient, IProjectRepository projectRepository, IMapper mapper, FileUploader fileUploader)
        {
            _apiClient = apiClient;
            _projectRepository = projectRepository;
            _mapper = mapper;
            _fileUploader = fileUploader;
        }

        public async Task<IActionResult> Index()
        {
            var projectList = await _projectRepository.GetAll();
            return View(projectList);
        }

        public async Task<IActionResult> RecordsManual()
        {
            var projectList = await _projectRepository.GetAllManual();
            return View(projectList);
        }

        public IActionResult AddProject()
        {
            return View(new ProjectRecordDTO());
        }

        [HttpPost]
        public async Task<IActionResult> AddProject(ProjectRecordDTO projectDto, string submitType)
        {
            ProjectRecord projectRecord = new();
            if (!ModelState.IsValid)
            {
                return View();
            }

            projectDto.CreationDate = DateTime.Now;
            var project = _mapper.Map<ProjectRecord>(projectDto);

            if (submitType == "FormSubmit")
            {
                projectRecord = await _projectRepository.AddProject(project);

                if (projectDto.AttachmentFile != null && projectDto.AttachmentFile.Count() > 0)
                {
                    foreach (var file in projectDto.AttachmentFile)
                    {
                        ProjectRecordFile recordFile = new ProjectRecordFile();
                        var fileResponse = await _fileUploader.UploadFile(file);

                        recordFile.ProjectRecordId = Convert.ToInt64(projectRecord.ProjectId);
                        recordFile.FileName = file.FileName;
                        recordFile.Extension = Path.GetExtension(file.FileName);
                        recordFile.Size = file.Length;
                        recordFile.MimeType = file.ContentType;
                        recordFile.FileUrl = fileResponse.url;
                        recordFile.Base64File = Utility.ConvertFileToBase64WithMimeType(file);

                        await _projectRepository.AddFile(recordFile);
                    }
                }
            }
            else
            {
                projectRecord = await _apiClient.PostAsync<ProjectRecord>("project", project);
            }

            if (projectRecord != null)
            {
                return Redirect("/project/index");
            }

            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var project = await _projectRepository.GetById(id);

            var projectDto = _mapper.Map<ProjectRecordDTO>(project);

            return View(projectDto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProjectRecordDTO projectDto, string submitType)
        {
            ProjectRecord projectRecord = new();

            if (!ModelState.IsValid)
            {
                return View();
            }

            //if (projectDto.AttachmentFile != null)
            //{
            //    projectDto.Attachment = Utility.ConvertFileToBase64WithMimeType(projectDto.AttachmentFile);
            //}

            projectDto.LastUpdateDate = DateTime.Now;
            var project = _mapper.Map<ProjectRecord>(projectDto);

            if (submitType == "FormSubmit")
            {
                var projectObj = await _projectRepository.GetById(project.ProctorNo);
                project.CreationDate = projectObj.CreationDate;

                if (string.IsNullOrEmpty(project.Attachment))
                {
                    project.Attachment = projectObj.Attachment;
                }

                projectRecord = await _projectRepository.Update(project);

                if (projectDto.AttachmentFile != null && projectDto.AttachmentFile.Count() > 0)
                {
                    foreach (var file in projectDto.AttachmentFile)
                    {
                        ProjectRecordFile recordFile = new ProjectRecordFile();
                        var fileResponse = await _fileUploader.UploadFile(file);

                        recordFile.ProjectRecordId = Convert.ToInt64(projectRecord.ProjectId);
                        recordFile.FileName = file.FileName;
                        recordFile.Extension = Path.GetExtension(file.FileName);
                        recordFile.Size = file.Length;
                        recordFile.MimeType = file.ContentType;
                        recordFile.FileUrl = fileResponse.url;
                        recordFile.Base64File = Utility.ConvertFileToBase64WithMimeType(file);

                        await _projectRepository.AddFile(recordFile);
                    }
                }
            }
            else
            {
                projectRecord = await _apiClient.PutAsync<ProjectRecord>("project", project.ProctorNo, project);
            }

            if (projectRecord != null)
            {
                return Redirect("/project/index");
            }

            return View();
        }

        public async Task<IActionResult> Detail(int id)
        {
            var project = await _projectRepository.GetById(id);
            var files = await _projectRepository.GetFiles(id);

            ViewBag.Files = files;
            return View(project);
        }
    }
}