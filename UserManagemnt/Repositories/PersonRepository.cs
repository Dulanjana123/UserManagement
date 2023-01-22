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
            return await _pMSDbContext.Person.Include(nameof(Person.EmailAddress))
                .ToListAsync();
        }

        public async Task<Person> GetAsync(Guid id)
        {
            return await _pMSDbContext.Person.Include(nameof(Person.EmailAddress))
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Person> UpdateAsync(Person person)
        {
            var existingPerson = await _pMSDbContext.Person.Include(nameof(Person.EmailAddress))
                .FirstOrDefaultAsync(x => x.Id == person.Id);

            if (existingPerson != null)
            {
                existingPerson.FirstName = person.FirstName;
                existingPerson.LastName = person.LastName;
                existingPerson.Mobile = person.Mobile;
                existingPerson.SSN = person.SSN;
                existingPerson.DOB = person.DOB;
                existingPerson.Address = person.Address;
                existingPerson.ProfileImageUrl = person.ProfileImageUrl;

                if (person.EmailAddress != null)
                {
                    //Delete the existing email
                    _pMSDbContext.Email.RemoveRange(existingPerson.EmailAddress);

                    person.EmailAddress.PersonId = existingPerson.Id;

                    await _pMSDbContext.Email.AddAsync(person.EmailAddress);
                }
            }
            await _pMSDbContext.SaveChangesAsync();
            return existingPerson;
        }
    }
}
