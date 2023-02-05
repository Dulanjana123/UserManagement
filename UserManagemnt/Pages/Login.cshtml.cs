using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using UserManagemnt.Models.ViewModels;

namespace UserManagemnt.Pages
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;

        [BindProperty]
        public Login LoginViewModel { get; set; }

        /// <summary>
        /// Inject UserManager class
        /// from AspNetCore.Identity
        /// </summary>
        /// <param name="signInManager"></param>
        public LoginModel(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }

        public void OnGet()
        {
        }

        /// <summary>
        /// Login method
        /// </summary>
        /// <param name="ReturnUrl"></param>
        /// <returns></returns>
        public async Task<IActionResult> OnPost(string? ReturnUrl)
        {
            // if there is any issues with the data posted to the method
            // based on the data added to the properties of LoginViewModel.
            if (ModelState.IsValid)
            {
                var signInResult = await signInManager.PasswordSignInAsync(
                LoginViewModel.Username, LoginViewModel.Password, false, false);

                if (signInResult.Succeeded)
                {
                    if (!string.IsNullOrWhiteSpace(ReturnUrl))
                    {
                        return RedirectToPage(ReturnUrl);
                    }

                    return RedirectToPage("Index");
                }
                else
                {
                    ViewData["Notification"] = new Notification
                    {
                        Type = Enums.NotificationType.Error,
                        Message = "Unable to login"
                    };

                    return Page();
                }
            }

            return Page();
        }
    }
}
