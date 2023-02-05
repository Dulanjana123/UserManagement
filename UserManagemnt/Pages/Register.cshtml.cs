using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;
using UserManagemnt.Models.ViewModels;

namespace UserManagemnt.Pages
{
    public class RegisterModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        [BindProperty]
        public Register RegisterViewModel { get; set; }

        /// <summary>
        /// Inject UserManager class
        /// from AspNetCore.Identity
        /// </summary>
        /// <param name="userManager"></param>
        public RegisterModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public void OnGet()
        {
        }

        /// <summary>
        /// User registration
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> OnPost()
        {
            // if there is any issues with the data posted to the method
            // based on the data added to the properties of RegisterViewModel.
            if (ModelState.IsValid)
            {
                //Initialize new IdentityUser instance
                var user = new IdentityUser
                {
                    UserName = RegisterViewModel.Username,
                    Email = RegisterViewModel.Email
                };

                var identityResult = await _userManager.CreateAsync(user, RegisterViewModel.Password);

                //check user creation is success
                if (identityResult.Succeeded)
                {
                    //add role type as user type to newly created user
                    var addRolesResult = await _userManager.AddToRoleAsync(user, "User");

                    //check role assignment success
                    if (addRolesResult.Succeeded)
                    {
                        //if success show success message
                        ViewData["Notification"] = new Notification
                        {
                            Type = Enums.NotificationType.Success,
                            Message = "User registered successfully."
                        };

                        return Page();
                    }
                }

                //user creation not success show error message
                ViewData["Notification"] = new Notification
                {
                    Type = Enums.NotificationType.Error,
                    Message = "Something went wrong."
                };

                return Page();
            }
            else
            {
                return Page();
            }
        }
    }
}
