using Newtonsoft.Json;
using System.Net.Mail;

namespace OraclePrimavera.DTOs
{
    public class ProjectResponseDto
    {
        public int ProctorNo { get; set; }
        public string RecordNo { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? LastUpdateDate { get; set; }
        public string CreatedBy { get; set; }
        public int? ProjectId { get; set; }
        public string ProjectName { get; set; }
        public string ContractNo { get; set; }
        public string ProjectOHName { get; set; }
        public DateTime? ProjectStartDate { get; set; }
        public DateTime? ProjectEndDate { get; set; }
        public string Category { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public object Currency { get; set; }
        public object CostCode { get; set; }
        public object AnticipatedCost { get; set; }
        public object ActualCostAmount { get; set; }
        public List<Attachment> Attachments { get; set; }
    }

    public class Attachment
    {
        public string FileName { get; set; }
        public string MimeType { get; set; }
        public string Extensrion { get; set; }
        public string Base64 { get; set; }
        public string Url { get; set; }
    }
}