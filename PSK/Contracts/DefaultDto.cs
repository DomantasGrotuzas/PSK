using System;
using System.ComponentModel.DataAnnotations;

namespace Contracts
{
    public class DefaultDto
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Version { get; set; }
    }
}