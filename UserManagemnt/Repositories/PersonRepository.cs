using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagemnt.Data;
using UserManagemnt.Models.MasterData;

namespace UserManagemnt.Repositories
{
    //Why repository?
    //We dont need to directly access DbContext class from the pages.
    //repository call to DbContext class
    /// <summary>
    /// Repository design pattern act as 
    /// middle layer between the rest of the app
    /// and the data access logic.
    /// Repository isolate the all data access code from the application.
    /// When making changes all the changes are in a one place
    /// </summary>
    public class PersonRepository : IPersonRepository
    {
        //assign PMSDbContext private field
        private readonly PMSDbContext _pMSDbContext;

        /// <summary>
        /// Inject PMSDbContext class 
        /// into the person repository
        /// </summary>
        /// <param name="pMSDbContext"></param>
        public PersonRepository(PMSDbContext pMSDbContext)
        {
            _pMSDbContext = pMSDbContext;
        }

        /// <summary>
        /// Taking person object
        /// Add it to the database
        /// Finally save changes.
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public async Task<Person> AddAsync(Person person)
        {
            //tell PMSDbContext to add person object
            await _pMSDbContext.Person.AddAsync(person);

            //call PMSDbContext to save the changes
            await _pMSDbContext.SaveChangesAsync();

            //return person object to the where this call came from
            return person;
        }

        /// <summary>
        /// Find existing person by passing id
        /// If existing person not null
        /// Delete existing person
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Guid id)
        {
            //access person table using PMSDbContext
            //use LINQ query to find person that has this id
            //store existing person in to the variable
            var existingPerson = await _pMSDbContext.Person.FindAsync(id);

            //if existing person not null
            if (existingPerson != null)
            {
                //using PMSDbContext access person table
                //and delete this existing person by calling remove method
                _pMSDbContext.Person.Remove(existingPerson);

                //save changes to the DB
                await _pMSDbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Get all persons 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            //access person table
            //using PMSDbContext 
            //getting list from there
            return await _pMSDbContext.Person
                .Include(nameof(Person.EmailAddress))
                .Include(nameof(Person.PhoneNumber))
                .Include(nameof(Person.Address))
                .ToListAsync();
        }

        /// <summary>
        /// Get person
        /// Where this id equals
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Person> GetAsync(Guid id)
        {
            //access person table
            //using PMSDbContext
            //use LINQ query to find person that has this id
            return await _pMSDbContext.Person
                .Include(nameof(Person.EmailAddress))
                .Include(nameof(Person.PhoneNumber))
                .Include(nameof(Person.Address))
                //return the first element according to the given id
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// Get existing person.
        /// Change values of the existing person
        /// to the values coming from method call.
        /// </summary>
        /// <param name="person"></param>
        /// <returns></returns>
        public async Task<Person> UpdateAsync(Person person)
        {
            //existing person come from submited form
            var existingPerson = await _pMSDbContext.Person

                //read Email entity from the PMSDbContext as part of the person query by adding navigation property name
                .Include(nameof(Person.EmailAddress))

                //read Phone entity from the PMSDbContext as part of the person query by adding navigation property name
                .Include(nameof(Person.PhoneNumber))

                //read Address entity from the PMSDbContext as part of the person query by adding navigation property name
                .Include(nameof(Person.Address))

                //search using id
                //return the first element according to the given id
                .FirstOrDefaultAsync(x => x.Id == person.Id);

            //change values of the existing person 
            //to the values coming from form
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

            //using PMS DB Context
            //save changes by calling saveChangesAsync method
            //to save changes to database
            await _pMSDbContext.SaveChangesAsync();

            //return ExistingPerson object to the where this call came from
            return existingPerson;
        }
    }
}
