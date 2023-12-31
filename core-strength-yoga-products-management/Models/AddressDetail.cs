﻿namespace core_strength_yoga_products_management.Models
{
    public class AddressDetail
    {
        public int Id { get; set; }
        public int CustomerDetailId { get; set; }
        public string StreetAddr { get; set; }
        public string AddrLine2 { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Country { get; set; }
        public string PostCode { get; set; }

        public AddressDetail() { }

        public AddressDetail(int id, CustomerDetail? customer, string streetAddr,
            string addrLine2, string city, string county, string country, string postCode,
            int customerDetailId)
        {
            Id = id;
            StreetAddr = streetAddr;
            AddrLine2 = addrLine2;
            City = city;
            County = county;
            Country = country;
            PostCode = postCode;
            CustomerDetailId = customerDetailId;
        }

    }
}
