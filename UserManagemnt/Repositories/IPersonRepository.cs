using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagemnt.Models.MasterData;

namespace UserManagemnt.Repositories
{
    /// <summary>
    /// Create interface for the PersonRepository
    /// After that implement the repository.
    /// Then inject them to the startup class.
    /// </summary>
    public interface IPersonRepository
    {
        //Declaration of the Get all persons method
        Task<IEnumerable<Person>> GetAllAsync();

        //Declaration of the Get person by id method
        Task<Person> GetAsync(Guid id);

        //Declaration of the Save person method
        Task<Person> AddAsync(Person person);

        //Declaration of the Update person method
        Task<Person> UpdateAsync(Person person);

        //Declaration of the Delete person method
        Task<bool> DeleteAsync(Guid id);
    }
}
