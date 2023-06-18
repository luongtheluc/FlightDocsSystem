﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightDocsSystem.DataAccess.Repository.IRepository
{
    public interface IFirebaseStorageService
    {
        public Task<string> UploadFile(IFormFile file, string? DocumentVersion);
    }
}