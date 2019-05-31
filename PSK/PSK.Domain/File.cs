using System;
using System.Collections.Generic;
using System.Text;

namespace PSK.Domain
{
    public class File : Entity
    {
        public string Name { get; set; }
        
        public long Size { get; set; }

        public TripEmployee TripEmployee { get; set; }

        public string FullName { get; set; }
    }
}
