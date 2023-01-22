using System;

namespace UserManagemnt.Models.MasterData
{
    public class Email
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Guid PersonId { get; set; }
    }
}
