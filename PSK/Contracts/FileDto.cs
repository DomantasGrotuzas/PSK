using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Contracts
{
    public class FileDto : DefaultDto
    {
        public string Name { get; set; }
        public long Size { get; set; }
        public bool IsSelected { get; set; }
    }
}