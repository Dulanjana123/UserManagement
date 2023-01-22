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
        public string Emails { get; set; }

        public EditModel(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public async Task OnGet(Guid id)
        {
            person = await _personRepository.GetAsync(id);

            if(person !=null && person.EmailAddress != null)
            {
                Emails = person.EmailAddress.Name.ToString();
            }
        }

        public async Task<IActionResult> OnPostEdit()
        {
            try
            {
                person.EmailAddress = new Email() { Name = Emails };

                await _personRepository.UpdateAsync(person);
                ViewData["Notification"] = new Notification
                {
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
            return Page();

        }

        public async Task<IActionResult> OnPostDelete()
        {
            var deleted = await _personRepository.DeleteAsync(person.Id);
            if (deleted == true)
            {
                var notification = new Notification
                {
                    Type = Enums.NotificationType.Success,
                    Message = "Person was deleted seccessfully!"
                };

                TempData["Notification"] = JsonSerializer.Serialize(notification);

                return RedirectToPage("/Admin/Persons/List");
            }
            return Page();
        }
    }
}
