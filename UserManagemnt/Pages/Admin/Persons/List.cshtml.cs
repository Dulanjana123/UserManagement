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
        //Create and assign private field 
        //called _personRepository
        //we can use this field inside this page methods
        private readonly IPersonRepository _personRepository;

        //Create public property
        //property type is person
        public List<Person> PersonList { get; set; }

        /// <summary>
        /// Inject person repository
        /// Call to the dbcontext via repository
        /// </summary>
        /// <param name="personRepository"></param>
        public ListModel(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        /// <summary>
        /// Get all persons
        /// </summary>
        /// <returns>PersonList</returns>
        public async Task OnGet()
        {
            //Read notification TempData
            //Store it in JSON type variable
            var notificationJson = (string)TempData["Notification"];

            //if Tempdata not null
            if (notificationJson != null)
            {

                //Deserialize to Notification type
                //Assign to the viewData to show it in view
                ViewData["Notification"] = JsonSerializer.Deserialize<Notification>(notificationJson);
            }
            
            //Store person list into a public list object
            PersonList = (await _personRepository.GetAllAsync())?.ToList();
        }

        /// <summary>
        /// Get person.
        /// Person who has this id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>JSON type person</returns>
        public JsonResult OnGetPersonDetail(Guid id)
        {
            var person = _personRepository.GetAsync(id).GetAwaiter().GetResult();
            return new JsonResult(person);
        }

    }
}
