using OraclePrimavera.Data;
using OraclePrimavera.DTOs;

namespace OraclePrimavera.IRepository
{
    public interface IProjectRepository
    {
        Task<ProjectRecord> AddProject(ProjectRecord project);

        Task AddFile(ProjectRecordFile file);

        Task<ProjectRecord> Update(ProjectRecord project);

        Task<IEnumerable<ProjectRecord>> GetAll();

        Task<IEnumerable<ProjectResponseDto>> GetProjectsWithFiles();

        Task<IEnumerable<ProjectResponseDto>> GetProjectWithFiles(int id);

        Task<IEnumerable<ProjectRecordManual>> GetAllManual();

        Task<ProjectRecord> GetById(int id);

        Task<bool> IsExist(int id);

        Task<IEnumerable<ProjectRecord>> GetFiltered(string projectName, int procterNo, int projectId, string recordNo, DateTime? creationDate, DateTime? latUpdatedDateTime, bool lastOneHour);
    }
}