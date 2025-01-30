using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using OraclePrimavera.Data;
using OraclePrimavera.DTOs;
using OraclePrimavera.Helper;
using OraclePrimavera.IRepository;

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
        public async Task<IActionResult> Get(int id)
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
            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProjectRecordDTO projectRecordDTO)
        {
            var projectRecord = _mapper.Map<ProjectRecord>(projectRecordDTO);
            var data = await _projectRepository.AddProject(projectRecord);
            return CreatedAtAction(nameof(Get), data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ProjectRecordDTO projectRecordDto)
        {
            // Retrieve the existing record from the database
            var projectRecordFromDB = await _projectRepository.GetById(id);
            projectRecordFromDB.CreationDate = projectRecordFromDB.CreationDate;

            if (projectRecordFromDB == null)
            {
                return NotFound($"Project record with ProctorNo {projectRecordDto.ProctorNo} not found.");
            }

            if (string.IsNullOrEmpty(projectRecordDto.Attachment))
            {
                projectRecordFromDB.Attachment = projectRecordFromDB.Attachment;
            }

            // Map the incoming DTO to the existing entity
            _mapper.Map(projectRecordDto, projectRecordFromDB);

            // Update the record in the database
            var updatedRecord = await _projectRepository.Update(projectRecordFromDB);

            return Ok(updatedRecord);
        }
    }
}