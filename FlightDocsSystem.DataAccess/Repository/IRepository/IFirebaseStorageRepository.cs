using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDocsSystem.DataAccess.Repository.IRepository
{
    public interface IFirebaseStorageRepository
    {
        public Task<string> UploadFile(IFormFile file, string? DocumentVersion);
        public Task<bool> DeleteFile(string fileName);
    }
}
