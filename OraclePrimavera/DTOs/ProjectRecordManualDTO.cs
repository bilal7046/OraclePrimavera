﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OraclePrimavera.DTOs
{
    public class ProjectRecordManualDTO
    {
        public int ProctorNo { get; set; }

        [Required]
        [StringLength(50)]
        public string RecordNo { get; set; }

        public DateTime? CreationDate { get; set; } = DateTime.Now;
        public DateTime? LastUpdateDate { get; set; } = DateTime.Now;

        [StringLength(100)]
        public string CreatedBy { get; set; }

        [Required]
        public int? ProjectId { get; set; }

        [StringLength(100)]
        [Required]
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
        public IFormFile AttachmentFile { get; set; }
    }
}