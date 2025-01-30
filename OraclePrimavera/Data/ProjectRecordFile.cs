namespace OraclePrimavera.Data
{
    public class ProjectRecordFile
    {
        public long Id { get; set; }
        public long ProjectRecordId { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string MimeType { get; set; }
        public long Size { get; set; }
        public string FileUrl { get; set; }
        public string Base64File { get; set; }
    }
}