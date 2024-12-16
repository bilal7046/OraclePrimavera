namespace OraclePrimavera.Helper
{
    public static class Utility
    {
        public static string ConvertFileToBase64(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();
                return Convert.ToBase64String(fileBytes);
            }
        }
    }
}