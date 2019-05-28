using System;
using System.ComponentModel.DataAnnotations;

namespace PSK.Domain
{
    public class Entity
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public byte[] Version { get; set; }
    }
}