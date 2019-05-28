using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public class AccommodationDto : DefaultDto
    {
        public string Name { get; set; }

        public int? TotalSpaces { get; set; }

        public OfficeDto Office { get; set; }

        public AddressDto Address { get; set; }
    }
}
