using Ecomm.Application.Commons.bases;
using Ecomm.Application.Commons.Bases.Request;
using Ecomm.Application.Dtos.Category.Request;
using Ecomm.Application.Dtos.Category.Response;
using Microsoft.AspNetCore.Http;

namespace Ecomm.Application.FileStorage
{
    public interface IFileStorageLocal
    {
        Task<string> SaveFile(string container, IFormFile file, string webRootPath, string scheme, string host);
        Task<string> UpdateFile(string container, IFormFile file, string route, string webRootPath, string scheme, string host);
        Task DeleteFile(string route, string container, string webRootPath);

    }
}
