﻿using System;

namespace UserManagemnt.Models.ViewModels
{
    public class AddPerson
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string SSN { get; set; }
        public DateTime DOB { get; set; }
        public string Address { get; set; }
        public string ProfileImageUrl { get; set; }
        
    }
}
