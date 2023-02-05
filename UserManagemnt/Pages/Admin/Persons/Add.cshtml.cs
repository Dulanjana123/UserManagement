using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Threading.Tasks;
using UserManagemnt.Data;
using UserManagemnt.Models.MasterData;
using UserManagemnt.Models.ViewModels;
using UserManagemnt.Repositories;

namespace UserManagemnt.Pages.Admin.Persons
{
    //Only authenticated users can access this page
    [Authorize]
    //[Authorize(Roles = "Admin")]
    public class AddModel : PageModel
    {
        //create and assign private field 
        //called _personRepository
        //We can use this field inside this page methods
        private readonly IPersonRepository _personRepository;

        //To dynamically bind values to object
        [BindProperty]
        public AddPerson AddPersonRequest { get; set; }

        [BindProperty]
        public IFormFile profileImage { get; set; }


        /// <summary>
        /// Inject person repository
        /// Call to the dbcontext via repository
        /// </summary>
        /// <param name="personRepository"></param>
        public AddModel(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public void OnGet()
        {
        }

        /// <summary>
        /// Method to create or save
        /// new person
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPost()
        {
            //Create new Person object 
            //And map that object to local person object instance

            //object of type Person
            //use that object and pass it to the AddAsync method
            var person = new Person()
            {
                //dont need to assign id 
                //PMSDbContext when try to add to DB will automatically assign new id
                FirstName = AddPersonRequest.FirstName,
                LastName = AddPersonRequest.LastName,

                //create new Phone object 
                //Inside this object has Phonenumber value 
                PhoneNumber = new Phone() { Number = AddPersonRequest.Phone },
                SSN = AddPersonRequest.SSN,
                DOB = AddPersonRequest.DOB,
                Address = new Address() { Name = AddPersonRequest.Address},
                ProfileImageUrl = AddPersonRequest.ProfileImageUrl,
                EmailAddress = new Email() { Name = AddPersonRequest.Email }

            };

            //provide person object in to the Add method of the person repository
            await _personRepository.AddAsync(person);

            var notification = new Notification
            {
                Type = Enums.NotificationType.Success,
                Message = "New person created!"
            };

            //serialize the notification object 
            //save serialize object inside the notification TempData  
            TempData["Notification"] = JsonSerializer.Serialize(notification);

            return RedirectToPage("/Admin/Persons/List");
        }
    }
}
