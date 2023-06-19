using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightDocsSystem.DataAccess.Repository.IRepository
{
    public interface ISendMailRepository
    {
        public Task SendEmailAsync(EmailDTO request, string filepath = null!);
    }
}