namespace MultiShop.Utilities
{
    public static class Extensions
    {
        public static bool CheckFileSize(this IFormFile file, int kbyte)
        {
            return file.Length / 1024 < kbyte;
        }
        public static bool CheckFileType(this IFormFile file, string type)
        {
            return file.ContentType.Contains(type);
        }
        public static async Task<string> SaveFileAsync(this IFormFile file, string wwwroot, params string[] route)
        {
            string resultPath = string.Empty;
            foreach (var routeItem in route)
            {
                resultPath = Path.Combine(resultPath, routeItem);
            }
            string fileName = Guid.NewGuid().ToString() + file.FileName;
            string resultFileName = Path.Combine(resultPath, fileName);
            using (FileStream stream = new FileStream(resultFileName, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return fileName;
        }
        public static async Task<string> Patcher(this IFormFile file, string wwwroot, params string[] route)
        {
            string resultPath = string.Empty;
            foreach (var routeItem in route)
            {
                resultPath = Path.Combine(resultPath, routeItem);
            }
            return resultPath;
        }

    }
}
