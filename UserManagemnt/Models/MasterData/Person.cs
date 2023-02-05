using System;

namespace UserManagemnt.Models.MasterData
{
    public class Person
    {
        //A Globally Unique Identifier or GUID
        //a number so large that it is mathematically guaranteed to be unique
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SSN { get; set; }
        public DateTime DOB { get; set; }
        public string ProfileImageUrl { get; set; }

        /// <summary>
        ///Navigation property is use to define relationship
        ///between two entities
        /// </summary>
        // Navigation property of Email entity
        public Email EmailAddress { get; set; }

        // Navigation property of Phone entity
        public Phone PhoneNumber { get; set; }

        // Navigation property of Address entity
        public Address Address { get; set; }

    }
}
