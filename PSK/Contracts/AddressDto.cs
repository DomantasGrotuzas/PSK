namespace Contracts
{
    public class AddressDto : DefaultDto
    {
        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string Country { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string HouseNumber { get; set; }
    }
}