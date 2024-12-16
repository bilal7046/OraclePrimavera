using OraclePrimavera.Data;

namespace OraclePrimavera.IRepository
{
    public interface IProjectRepository
    {
        Task<ProjectRecord> AddProject(ProjectRecord project);

        Task<ProjectRecord> Update(ProjectRecord project);

        Task<IEnumerable<ProjectRecord>> GetAll();

        Task<IEnumerable<ProjectRecordManual>> GetAllManual();

        Task<ProjectRecord> GetById(int id);

        Task<bool> IsExist(int id);
    }
}