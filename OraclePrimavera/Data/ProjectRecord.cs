using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OraclePrimavera.Data
{
    public class ProjectRecord
    {
        [Key]
        [Required]
        public int ProctorNo { get; set; }

        [Required]
        [StringLength(50)]
        public string RecordNo { get; set; }

        public DateTime? CreationDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }

        [StringLength(100)]
        public string CreatedBy { get; set; }

        public int? ProjectId { get; set; }

        [StringLength(100)]
        public string ProjectName { get; set; }

        [StringLength(100)]
        public string ContractNo { get; set; }

        [StringLength(100)]
        public string ProjectOHName { get; set; }

        public DateTime? ProjectStartDate { get; set; }

        public DateTime? ProjectEndDate { get; set; }

        [StringLength(100)]
        public string Category { get; set; }

        [StringLength(100)]
        public string Status { get; set; }

        [StringLength(4000)]
        public string Description { get; set; }

        [StringLength(15)]
        public string Currency { get; set; }

        [StringLength(50)]
        public string CostCode { get; set; }

        public decimal? AnticipatedCost { get; set; }

        public decimal? ActualCostAmount { get; set; }

        [StringLength(100)]
        public string AttachUrl { get; set; }

        public string Attachment { get; set; }
    }
}