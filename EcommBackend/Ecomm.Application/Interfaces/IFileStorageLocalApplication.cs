using Ecomm.Application.Commons.bases;
using Ecomm.Application.Commons.Bases.Request;
using Ecomm.Application.Dtos.Category.Request;
using Ecomm.Application.Dtos.Category.Response;
using Microsoft.AspNetCore.Http;

namespace Ecomm.Application.Interfaces
{
    public interface IFileStorageLocalApplication
    {
        Task<string> SaveFile(string container, IFormFile file);
        Task<string> EditFile(string container, IFormFile file, string route);
        Task DeleteFile(string route, string container);
    }
}
