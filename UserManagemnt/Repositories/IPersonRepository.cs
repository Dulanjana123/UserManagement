using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagemnt.Models.MasterData;

namespace UserManagemnt.Repositories
{
    public interface IPersonRepository
    {
        Task<IEnumerable<Person>> GetAllAsync();

        //Task<IEnumerable<Person>> GetAllAsync(string tagName);
        Task<Person> GetAsync(Guid id);
        Task<Person> AddAsync(Person person);
        Task<Person> UpdateAsync(Person person);
        Task<bool> DeleteAsync(Guid id);

    }
}
