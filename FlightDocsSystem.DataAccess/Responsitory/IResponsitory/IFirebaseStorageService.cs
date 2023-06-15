using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDocsSystem.DataAccess.Responsitory.IResponsitory
{
    public interface IFirebaseStorageService
    {
        public Task<string> UploadImage(IFormFile file);
    }
}
