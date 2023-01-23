using System;

namespace UserManagemnt.Models.MasterData
{
    public class Person
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SSN { get; set; }
        public DateTime DOB { get; set; }
        public string ProfileImageUrl { get; set; }
        public Email EmailAddress { get; set; }
        public Phone PhoneNumber { get; set; }
        public Address Address { get; set; }

    }
}
