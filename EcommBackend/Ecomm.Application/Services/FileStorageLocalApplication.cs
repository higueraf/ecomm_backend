using Ecomm.Application.FileStorage;
using Ecomm.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Ecomm.Application.Services
{
    internal class FileStorageLocalApplication : IFileStorageLocalApplication
        {
        private readonly IWebHostEnvironment _env;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IFileStorageLocal _fileStorageLocal;
        public FileStorageLocalApplication(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor, IFileStorageLocal fileStorageLocal)
        {
            _env = env;
            _httpContextAccessor = httpContextAccessor;
            _fileStorageLocal = fileStorageLocal;
        }


        public async Task<string> SaveFile(string container, IFormFile file)
        {
            var webRootPath = _env.WebRootPath;
            var scheme = _httpContextAccessor.HttpContext!.Request.Scheme;
            var host = _httpContextAccessor.HttpContext?.Request.Host;
            return await _fileStorageLocal.SaveFile(container, file, webRootPath, scheme, host.Value.ToString());

        }
    

        public async Task<string> EditFile(string container, IFormFile file, string route)
        {
            var webRootPath = _env.WebRootPath;
            var scheme = _httpContextAccessor.HttpContext!.Request.Scheme;
            var host = _httpContextAccessor.HttpContext?.Request.Host;
            return await _fileStorageLocal.UpdateFile(container, file, route, webRootPath, scheme, host.Value.ToString());
        }
    public async Task DeleteFile(string route, string container)
        {
            var webRootPath = _env.WebRootPath;
            await _fileStorageLocal.DeleteFile(route, container, webRootPath);
        }


}
}
