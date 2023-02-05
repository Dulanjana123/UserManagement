using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace UserManagemnt.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;

        /// <summary>
        /// Inject UserManager class
        /// from AspNetCore.Identity
        /// </summary>
        /// <param name="signInManager"></param>
        public LogoutModel(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        /// <summary>
        /// Call to SignOutAsync method
        /// in AspNetCore.Identity
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult>  OnGet()
        {
            await signInManager.SignOutAsync();

            //return user to the Index page/Home page
            return RedirectToPage("Index");

        }
    }
}
