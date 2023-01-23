using System;

namespace UserManagemnt.Models.MasterData
{
    public class Address
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid PersonId { get; set; }
    }
}
