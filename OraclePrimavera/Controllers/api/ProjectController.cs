using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

using OraclePrimavera.Data;
using OraclePrimavera.DTOs;
using OraclePrimavera.Helper;
using OraclePrimavera.IRepository;
using System.Diagnostics.Contracts;

namespace OraclePrimavera.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ProjectController(IProjectRepository projectRepository, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var data = await _projectRepository.GetProjectsWithFiles();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)//project id
        {
            var data = await _projectRepository.GetProjectWithFiles(id);
            return Ok(data);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> GetFiltered([FromQuery] string projectName,
            [FromQuery] int? procterNo,
            [FromQuery] int? projectId,
            [FromQuery] string recordNo,
            [FromQuery] DateTime? creationDate,
            [FromQuery] DateTime? lastUpdatedDate,
            [FromQuery] bool lastOneHour = false
)
        {
            int defaultProcterNo = procterNo ?? 0;
            int defaultProjectId = projectId ?? 0;

            var data = await _projectRepository.GetFiltered(projectName, defaultProcterNo, defaultProjectId, recordNo, creationDate, lastUpdatedDate, lastOneHour);

            foreach (var project in data)
            {
                List<Attachment> attachments = new List<Attachment>();

                var files = await _projectRepository.GetFilesByProjectId(project.ProjectId.Value);

                if (files.Count() > 0)
                {
                    foreach (var file in files)
                    {
                        attachments.Add(new Attachment()
                        {
                            FileName = file.FileName,
                            MimeType = file.MimeType,
                            Extension = file.Extension,
                            Base64 = file.Base64File,
                            Url = file.FileUrl
                        });
                    }
                }

                project.Attachments = attachments;
            }

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProjectResponseDto projectRecordDTO)
        {
            ProjectRecord projectRecord = new ProjectRecord()
            {
                ProctorNo = projectRecordDTO.ProctorNo,
                RecordNo = projectRecordDTO.RecordNo,
                CreationDate = projectRecordDTO.CreationDate,
                CreatedBy = projectRecordDTO.CreatedBy,
                ProjectId = projectRecordDTO.ProjectId,
                ProjectName = projectRecordDTO.ProjectName,
                ContractNo = projectRecordDTO.ContractNo,
                ProjectOHName = projectRecordDTO.ProjectOHName,
                ProjectStartDate = projectRecordDTO.ProjectStartDate,
                ProjectEndDate = projectRecordDTO.ProjectEndDate,
                Category = projectRecordDTO.Category,
                Status = projectRecordDTO.Status,
                Description = projectRecordDTO.Description,
                Currency = projectRecordDTO.Currency,
                CostCode = projectRecordDTO.CostCode,
                AnticipatedCost = projectRecordDTO.AnticipatedCost,
                ActualCostAmount = projectRecordDTO.ActualCostAmount,
                AttachUrl = projectRecordDTO.AttachUrl,
            };

            var data = await _projectRepository.AddProject(projectRecord);

            if (projectRecordDTO.Attachments != null && projectRecordDTO.Attachments.Count > 0)
            {
                foreach (var file in projectRecordDTO.Attachments)
                {
                    ProjectRecordFile projectRecordFile = new ProjectRecordFile()
                    {
                        ProjectRecordId = data.ProjectId.Value,
                        FileName = file.FileName,
                        MimeType = file.MimeType,
                        Extension = file.Extension,
                        Base64File = file.Base64,
                    };
                    await _projectRepository.AddFile(projectRecordFile);
                }
            }
            var result = await _projectRepository.GetById(data.ProctorNo);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProjectResponseDto projectRecordDto)
        {
            // Retrieve the existing record from the database
            var projectRecordFromDB = await _projectRepository.GetById(id);

            if (projectRecordFromDB == null)
            {
                return NotFound($"Project record with ProctorNo {projectRecordDto.ProctorNo} not found.");
            }

            projectRecordFromDB.ProctorNo = projectRecordDto.ProctorNo;
            projectRecordFromDB.RecordNo = projectRecordDto.RecordNo;
            projectRecordFromDB.CreationDate = projectRecordDto.CreationDate;
            projectRecordFromDB.CreatedBy = projectRecordDto.CreatedBy;
            projectRecordFromDB.ProjectId = projectRecordDto.ProjectId;
            projectRecordFromDB.ProjectName = projectRecordDto.ProjectName;
            projectRecordFromDB.ContractNo = projectRecordDto.ContractNo;
            projectRecordFromDB.ProjectOHName = projectRecordDto.ProjectOHName;
            projectRecordFromDB.ProjectStartDate = projectRecordDto.ProjectStartDate;
            projectRecordFromDB.ProjectEndDate = projectRecordDto.ProjectEndDate;
            projectRecordFromDB.Category = projectRecordDto.Category;
            projectRecordFromDB.Status = projectRecordDto.Status;
            projectRecordFromDB.Description = projectRecordDto.Description;
            projectRecordFromDB.Currency = projectRecordDto.Currency;
            projectRecordFromDB.CostCode = projectRecordDto.CostCode;
            projectRecordFromDB.AnticipatedCost = projectRecordDto.AnticipatedCost;
            projectRecordFromDB.ActualCostAmount = projectRecordDto.ActualCostAmount;
            projectRecordFromDB.AttachUrl = projectRecordDto.AttachUrl;

            // Map the incoming DTO to the existing entity
            _mapper.Map(projectRecordDto, projectRecordFromDB);

            // Update the record in the database
            var updatedRecord = await _projectRepository.Update(projectRecordFromDB);

            return Ok(updatedRecord);
        }
    }
}