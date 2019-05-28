﻿using System;
using System.Collections.Generic;

namespace Contracts
{
    public class TripDto : DefaultDto
    {
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Comment { get; set; }

        public Guid OrganizerId { get; set; }

        public Guid StartLocationId { get; set; }

        public Guid EndLocationId { get; set; }

        public IList<Guid> Employees { get; set; }
    }
}