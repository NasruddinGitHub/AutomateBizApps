using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomateCe.DataModel
{
    public class AccountQuickCreateForm
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string AccountName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string AddressStreet1 { get; set; }

        public string AddressStreet2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public int StreetNumber { get; set; }
        public string StreetName { get; set; }
        public string StreetSuffix { get; set; }
        public string PreDirection { get; set; }
        public string PostDirection { get; set; }
        public string County { get; set; }

        public AccountQuickCreateForm(string firstName, string lastName, string accountName, string phoneNumber, string email, string addressStreet1, string addressStreet2, string city, string state, string country, string zipCode)
        {
            FirstName = firstName;
            LastName = lastName;
            AccountName = accountName;
            PhoneNumber = phoneNumber;
            Email = email;
            AddressStreet1 = addressStreet1;
            AddressStreet2 = addressStreet2;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;
        }

        public AccountQuickCreateForm(string firstName, string lastName, string accountName, string phoneNumber, string email, string addressStreet1, string addressStreet2, string city, string state, string country, string zipCode, int streetNumber, string streetName, string streetSuffix, string preDir, string postDir, string county)
        {
            FirstName = firstName;
            LastName = lastName;
            AccountName = accountName;
            PhoneNumber = phoneNumber;
            Email = email;
            AddressStreet1 = addressStreet1;
            AddressStreet2 = addressStreet2;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipCode;
            StreetNumber = streetNumber;
            StreetName = streetName;
            StreetSuffix = streetSuffix;
            PreDirection = preDir;
            PostDirection = postDir;
            County = county;

        }
    }
}
