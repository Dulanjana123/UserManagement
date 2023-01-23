using System;

namespace UserManagemnt.Models.MasterData
{
    public class Phone
    {
        public Guid Id { get; set; }
        public string Number { get; set; }

        public Guid PersonId { get; set; }
    }
}
