using Microsoft.EntityFrameworkCore;
using OraclePrimavera.Data;
using OraclePrimavera.DTOs;
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

        public async Task AddFile(ProjectRecordFile file)
        {
            _context.ProjectRecordFile.Add(file);
            await _context.SaveChangesAsync();
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
                Status = z.Status,
                CreationDate = z.CreationDate,
                LastUpdateDate = z.LastUpdateDate,
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

        public async Task<IEnumerable<ProjectResponseDto>> GetFiltered(
string projectName,
int procterNo,
int projectId,
string recordNo,
DateTime? creationDate,
DateTime? lastUpdatedDate,
bool lastOneHour)
        {
            var query = _context.ProjectRecords.AsQueryable();

            if (!string.IsNullOrEmpty(projectName))
            {
                query = query.Where(z => z.ProjectName.ToLower() == projectName.ToLower());
            }
            if (procterNo > 0)
            {
                query = query.Where(z => z.ProctorNo == procterNo);
            }
            if (projectId > 0)
            {
                query = query.Where(z => z.ProjectId == projectId);
            }
            if (!string.IsNullOrEmpty(recordNo))
            {
                query = query.Where(z => z.RecordNo == recordNo);
            }
            if (creationDate.HasValue)
            {
                query = query.Where(z => z.CreationDate.Value.Date == creationDate.Value.Date);
            }
            if (lastUpdatedDate.HasValue)
            {
                query = query.Where(z => z.LastUpdateDate.Value.Date == lastUpdatedDate.Value.Date);
            }
            if (lastOneHour)
            {
                var oneHourAgo = DateTime.Now.AddHours(-1);
                query = query.Where(z => z.CreationDate.HasValue && z.CreationDate.Value >= oneHourAgo);
            }
            var results = await query.ToListAsync();

            var data = results.Select(z => new ProjectResponseDto()
            {
                ProctorNo = z.ProctorNo,
                ProjectName = z.ProjectName,
                CreatedBy = z.CreatedBy,
                ProjectId = z.ProjectId,
                RecordNo = z.RecordNo,
                ProjectOHName = z.ProjectOHName,
                Category = z.Category,
                Description = z.Description,
                Currency = z.Currency,
                CostCode = z.CostCode,
                AnticipatedCost = z.AnticipatedCost,
                ActualCostAmount = z.ActualCostAmount,
                ContractNo = z.ContractNo,
                ProjectStartDate = z.ProjectStartDate,
                ProjectEndDate = z.ProjectEndDate,
                Status = z.Status,
                CreationDate = z.CreationDate.Value,
                LastUpdateDate = z.LastUpdateDate.HasValue ? z.LastUpdateDate.Value : (DateTime?)null
            }).ToList();
            foreach (var project in data)
            {
                List<Attachment> attachments = new List<Attachment>();
                var files = await GetFilesByProjectId(project.ProjectId.Value);
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

            return data;
        }

        public async Task<IEnumerable<ProjectResponseDto>> GetProjectsWithFiles()
        {
            var projects = await _context.ProjectRecords.AsNoTracking().Select(z => new ProjectResponseDto()
            {
                ProctorNo = z.ProctorNo,
                ProjectName = z.ProjectName,
                CreatedBy = z.CreatedBy,
                ProjectId = z.ProjectId,
                RecordNo = z.RecordNo,
                ProjectOHName = z.ProjectOHName,
                Category = z.Category,
                Description = z.Description,
                Currency = z.Currency,
                CostCode = z.CostCode,
                AnticipatedCost = z.AnticipatedCost,
                ActualCostAmount = z.ActualCostAmount,
                ContractNo = z.ContractNo,
                ProjectStartDate = z.ProjectStartDate,
                ProjectEndDate = z.ProjectEndDate,
                Status = z.Status,
                CreationDate = z.CreationDate.Value,
                LastUpdateDate = z.LastUpdateDate.HasValue ? z.LastUpdateDate.Value : (DateTime?)null
            }).ToListAsync();

            foreach (var project in projects)
            {
                List<Attachment> attachments = new List<Attachment>();
                var files = await GetFilesByProjectId(project.ProjectId.Value);
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

            return projects;
        }

        public async Task<IEnumerable<ProjectRecordFile>> GetFilesByProjectId(int projectId)
        {
            return await _context.ProjectRecordFile.Where(z => z.ProjectRecordId == projectId).ToListAsync();
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

        public async Task<IEnumerable<ProjectResponseDto>> GetProjectWithFiles(int id)
        {
            var projects = await _context.ProjectRecords.Where(z => z.ProctorNo == id).AsNoTracking().Select(z => new ProjectResponseDto()
            {
                ProctorNo = z.ProctorNo,
                ProjectName = z.ProjectName,
                CreatedBy = z.CreatedBy,
                ProjectId = z.ProjectId,
                RecordNo = z.RecordNo,
                ProjectOHName = z.ProjectOHName,
                Category = z.Category,
                Description = z.Description,
                Currency = z.Currency,
                CostCode = z.CostCode,
                AnticipatedCost = z.AnticipatedCost,
                ActualCostAmount = z.ActualCostAmount,
                ContractNo = z.ContractNo,
                ProjectStartDate = z.ProjectStartDate,
                ProjectEndDate = z.ProjectEndDate,
                Status = z.Status,
                CreationDate = z.CreationDate.Value,
                LastUpdateDate = z.LastUpdateDate.HasValue ? z.LastUpdateDate.Value : (DateTime?)null
            }).ToListAsync();

            foreach (var project in projects)
            {
                List<Attachment> attachments = new List<Attachment>();
                var files = await GetFilesByProjectId(project.ProjectId.Value);
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

            return projects;
        }

        public async Task<IEnumerable<ProjectRecordFile>> GetFiles(int id)
        {
            return await _context.ProjectRecordFile.Where(z => z.Id == id).ToListAsync();
        }
    }
}