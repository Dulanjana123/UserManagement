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
    [Authorize]
    //[Authorize(Roles = "Admin")]
    public class AddModel : PageModel
    {
        private readonly IPersonRepository _personRepository;

        [BindProperty]
        public AddPerson AddPersonRequest { get; set; }

        [BindProperty]
        public IFormFile profileImage { get; set; }
        public AddModel(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            var person = new Person()
            {
                FirstName = AddPersonRequest.FirstName,
                LastName = AddPersonRequest.LastName,
                PhoneNumber = new Phone() { Number = AddPersonRequest.Phone},
                SSN = AddPersonRequest.SSN,
                DOB = AddPersonRequest.DOB,
                Address = new Address() { Name = AddPersonRequest.Address},
                ProfileImageUrl = AddPersonRequest.ProfileImageUrl,
                EmailAddress = new Email() { Name = AddPersonRequest.Email }

            };
            await _personRepository.AddAsync(person);

            var notification = new Notification
            {
                Type = Enums.NotificationType.Success,
                Message = "New person created!"
            };

            TempData["Notification"] = JsonSerializer.Serialize(notification);

            return RedirectToPage("/Admin/Persons/List");
        }
    }
}
