namespace OraclePrimavera.Helper
{
    public static class Utility
    {
        public static string ConvertFileToBase64WithMimeType(IFormFile file)
        {
            using (var memoryStream = new MemoryStream())
            {
                // Copy file data to memory stream
                file.CopyTo(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();

                // Determine MIME type
                string mimeType = file.ContentType; // Get MIME type from IFormFile

                // Combine MIME type and base64 string
                string base64String = Convert.ToBase64String(fileBytes);
                return $"data:{mimeType};base64,{base64String}";
            }
        }

        public static string GetFileTypeFromBase64(string base64String)
        {
            if (string.IsNullOrEmpty(base64String))
                return "Unknown";

            // Check if the base64 string contains the MIME type
            if (base64String.Contains("data:"))
            {
                var mimeType = base64String.Split(';')[0].Replace("data:", "");

                if (mimeType.StartsWith("image/"))
                {
                    return "Image";
                }
                else if (mimeType == "application/pdf")
                {
                    return "PDF";
                }
            }

            return "Unknown";
        }
    }
}