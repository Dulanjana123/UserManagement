using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using UserManagemnt.Data;
using UserManagemnt.Models.MasterData;
using UserManagemnt.Models.ViewModels;
using UserManagemnt.Repositories;


namespace UserManagemnt.Pages.Admin.Persons
{
    [Authorize]
    public class ListModel : PageModel
    {
        private readonly IPersonRepository _personRepository;

        public List<Person> PersonList { get; set; }

        public ListModel(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public async Task OnGet()
        {
            var notificationJson = (string)TempData["Notification"];
            if (notificationJson != null)
            {
                ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notificationJson);
            }
            PersonList = (await _personRepository.GetAllAsync())?.ToList();
        }


        public JsonResult OnGetPersonDetail(Guid id)
        {
            var person = _personRepository.GetAsync(id).GetAwaiter().GetResult();
            return new JsonResult(person);
        }

    }
}
