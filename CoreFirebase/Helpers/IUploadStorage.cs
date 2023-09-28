using CoreFirebase.Models;
using Microsoft.AspNetCore.Mvc;

namespace CoreFirebase.Helpers
{
    public interface IUploadStorage
    {
        Task<string> SubirStorage(Stream archivo, string nombre);
    }
}
