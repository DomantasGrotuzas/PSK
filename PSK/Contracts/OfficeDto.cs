namespace Contracts
{
    public class OfficeDto : DefaultDto
    {
        public string Name { get; set; }

        public AddressDto Address { get; set; }
    }
}