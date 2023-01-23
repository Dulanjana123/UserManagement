using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagemnt.Data;
using UserManagemnt.Models.MasterData;

namespace UserManagemnt.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly PMSDbContext _pMSDbContext;

        public PersonRepository(PMSDbContext pMSDbContext)
        {
            _pMSDbContext = pMSDbContext;
        }
        public async Task<Person> AddAsync(Person person)
        {
            await _pMSDbContext.AddAsync(person);
            await _pMSDbContext.SaveChangesAsync();
            return person;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var existingPerson = await _pMSDbContext.Person.FindAsync(id);

            if (existingPerson != null)
            {
                _pMSDbContext.Person.Remove(existingPerson);
                await _pMSDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _pMSDbContext.Person.Include(nameof(Person.EmailAddress)).Include(nameof(Person.PhoneNumber)).Include(nameof(Person.Address))
                .ToListAsync();
        }

        public async Task<Person> GetAsync(Guid id)
        {
            return await _pMSDbContext.Person.Include(nameof(Person.EmailAddress)).Include(nameof(Person.PhoneNumber)).Include(nameof(Person.Address))
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Person> UpdateAsync(Person person)
        {
            var existingPerson = await _pMSDbContext.Person
                .Include(nameof(Person.EmailAddress))
                .Include(nameof(Person.PhoneNumber))
                .Include(nameof(Person.Address))
                .FirstOrDefaultAsync(x => x.Id == person.Id);

            if (existingPerson != null)
            {
                existingPerson.FirstName = person.FirstName;
                existingPerson.LastName = person.LastName;
                existingPerson.SSN = person.SSN;
                existingPerson.DOB = person.DOB;
                existingPerson.Address = person.Address;
                existingPerson.ProfileImageUrl = person.ProfileImageUrl;
                existingPerson.Address = person.Address;
                existingPerson.EmailAddress = person.EmailAddress;
                existingPerson.PhoneNumber = person.PhoneNumber;
            }
            await _pMSDbContext.SaveChangesAsync();
            return existingPerson;
        }
    }
}
