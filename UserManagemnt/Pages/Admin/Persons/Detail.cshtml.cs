using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Threading.Tasks;
using UserManagemnt.Models.MasterData;
using UserManagemnt.Repositories;

namespace UserManagemnt.Pages.Admin.Persons
{
    public class DetailModel : PageModel
    {
        private readonly IPersonRepository _personRepository;

        [BindProperty]
        public Person person { get; set; }

        public IFormFile profileImage { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        public string Address { get; set; }
        [BindProperty]
        public string Phone { get; set; }

        public DetailModel(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public async Task OnGet(Guid id)
        {
            person = await _personRepository.GetAsync(id);

            if (person != null && person.EmailAddress !=null)
            {
                Email = person.EmailAddress.Name.ToString();
            }
            if (person != null && person.PhoneNumber != null)
            {
                Phone = person.PhoneNumber.Number.ToString();
            }
            if (person != null && person.Address != null)
            {
                Address = person.Address.Name.ToString();
            }
        }

    }
}
