using Microsoft.EntityFrameworkCore;
using OraclePrimavera.Data;
using OraclePrimavera.IRepository;

namespace OraclePrimavera.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly ApplicationDbContext _context;

        public ProjectRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ProjectRecord> AddProject(ProjectRecord project)
        {
            _context.ProjectRecords.Add(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<IEnumerable<ProjectRecord>> GetAll()
        {
            return await _context.ProjectRecords.AsNoTracking().Select(z => new ProjectRecord()
            {
                ProctorNo = z.ProctorNo,
                ProjectName = z.ProjectName,
                RecordNo = z.RecordNo,
                ContractNo = z.ContractNo,
                ProjectStartDate = z.ProjectStartDate,
                ProjectEndDate = z.ProjectEndDate,
                Status = z.Status
            }).ToListAsync();
        }

        public async Task<IEnumerable<ProjectRecordManual>> GetAllManual()
        {
            return await _context.ProjectRecordManual.AsNoTracking().Select(z => new ProjectRecordManual()
            {
                ProctorNo = z.ProctorNo,
                ProjectName = z.ProjectName,
                RecordNo = z.RecordNo,
                ContractNo = z.ContractNo,
                ProjectStartDate = z.ProjectStartDate,
                ProjectEndDate = z.ProjectEndDate,
                Status = z.Status
            }).ToListAsync();
        }

        public async Task<ProjectRecord> GetById(int id)
        {
            return await _context.ProjectRecords
         .AsNoTracking()
         .FirstOrDefaultAsync(p => p.ProctorNo == id);
        }

        public async Task<bool> IsExist(int id)
        {
            return await _context.ProjectRecords.AnyAsync(z => z.ProctorNo == id);
        }

        public async Task<ProjectRecord> Update(ProjectRecord project)
        {
            _context.ProjectRecords.Update(project);
            await _context.SaveChangesAsync();
            return project;
        }
    }
}