using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightDocsSystem.Helper
{
    public class ApiResponse
    {
        public string? Message { get; set; }

        public object? Data { get; set; }

        public bool Success { get; set; }
    }
}