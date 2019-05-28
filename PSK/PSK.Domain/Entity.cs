using System;

namespace PSK.Domain
{
    public class Entity
    {
        public Guid Id { get; set; }

        public byte[] Version { get; set; }
    }
}