using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        //public JsonResult OnPostDataSource([FromBody] DataManagerRequest dm)
        //{
        //    IEnumerable<OrdersDetails> DataSource = PersonList = (await _personRepository.GetAllAsync())?.ToList();
        //    DataOperations operation = new DataOperations();
        //    if (dm.Search != null && dm.Search.Count > 0)
        //    {
        //        DataSource = operation.PerformSearching(DataSource, dm.Search);  //Search
        //    }
        //    if (dm.Sorted != null && dm.Sorted.Count > 0) //Sorting
        //    {
        //        DataSource = operation.PerformSorting(DataSource, dm.Sorted);
        //    }
        //    if (dm.Where != null && dm.Where.Count > 0) //Filtering
        //    {
        //        DataSource = operation.PerformFiltering(DataSource, dm.Where, dm.Where[0].Operator);
        //    }
        //    int count = DataSource.Cast<OrdersDetails>().Count();
        //    if (dm.Skip != 0)
        //    {
        //        DataSource = operation.PerformSkip(DataSource, dm.Skip);   //Paging
        //    }
        //    if (dm.Take != 0)
        //    {
        //        DataSource = operation.PerformTake(DataSource, dm.Take);
        //    }
        //    return dm.RequiresCounts ? new JsonResult(new { result = DataSource, count = count }) : new JsonResult(DataSource);
        //}
    }
}
