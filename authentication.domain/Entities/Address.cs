using authentication.domain.entities;

namespace authentication.domain.Entities
{
    public class Address : Entity
    {
        public bool? Primary { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? StateOrProvince { get; set; }
        public string? AddressLine1 { get; set; }
        public string? AddressLine2 { get; set; }
        public string? AddressLine3 { get; set; }
        public string? AddressLine4 { get; set; }
        public string? AddressDistrict { get; set; } // bairro
        public string? AddressZip { get; set; }
    }
}