using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using UserManagemnt.Models.MasterData;
using UserManagemnt.Models.ViewModels;
using UserManagemnt.Repositories;

namespace UserManagemnt.Pages.Admin.Persons
{
    [Authorize]
    //[Authorize(Roles = "Admin")]
    public class EditModel : PageModel
    {
        private readonly IPersonRepository _personRepository;

        [BindProperty]
        public Person person { get; set; }

        public IFormFile profileImage { get; set; }

        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Phone { get; set; }

        [BindProperty]
        public string Address { get; set; }

        /// <summary>
        /// Inject person repository
        /// Call to the dbcontext via repository
        /// </summary>
        /// <param name="personRepository"></param>
        public EditModel(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        //id comes from List page
        public async Task OnGet(Guid id)
        {
            //Pass id to GetAsync method in the personrepository
            //store result in to the person object
            //result could be a Person or null
            person = await _personRepository.GetAsync(id);

            //check Person and EmailAddress is not null 
            if(person !=null && person.EmailAddress != null)
            {
                //if not null then pass emaill address to string type
                //and bind to public Email property
                Email = person.EmailAddress.Name.ToString();
            }
            if(person != null && person.PhoneNumber != null)
            {
                Phone = person.PhoneNumber.Number.ToString();
            }
            if (person != null && person.Address != null)
            {
                Address = person.Address.Name.ToString();
            }
        }

        /// <summary>
        /// Update person
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostEdit()
        {
            try
            {
                //create new Email object 
                //Inside this object has Email value 
                person.EmailAddress = new Email() { Name = Email };
                person.PhoneNumber = new Phone() { Number= Phone };
                person.Address = new Address() { Name = Address };

                await _personRepository.UpdateAsync(person);

                //Create model from notification object
                ViewData["Notification"] = new Notification
                {
                    //Assign values to the notification object properties
                    Message = "Record updated successfully!",
                    Type = Enums.NotificationType.Success
                };
                
            }
            catch(Exception ex)
            {
                ViewData["Notification"] = new Notification
                {
                    Message = "Something went wrong!",
                    Type = Enums.NotificationType.Error
                };
            }
            //after successfully updated
            //return to the same page
            return Page();
        }

        /// <summary>
        /// Delete person
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPostDelete()
        {

            //assign result in to the variable
            var deleted = await _personRepository.DeleteAsync(person.Id);

            //check delete is success
            if (deleted == true)
            {
                //create notification type object
                var notification = new Notification
                {
                    //assign values to notification object properties
                    Type = Enums.NotificationType.Success,
                    Message = "Person was deleted seccessfully!"
                };

                //serialize the message to send into another page
                TempData["Notification"] = JsonSerializer.Serialize(notification);

                //after successfully deleted return to the List page
                return RedirectToPage("/Admin/Persons/List");
            }

            //if this was not found return the page
            return Page();
        }
    }
}
