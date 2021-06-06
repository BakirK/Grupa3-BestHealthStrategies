using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using BestHealtStrategies.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

using static BestHealtStrategies.Models.ValueObjects.ValueObjects;

namespace BestHealtStrategies.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            // TODOO
            [Required]
            public string Name { get; set; }

            [Required]
            public string Surname { get; set; }

            [Required]
            public int Age { get; set; }

            [Required]
            public int Height { get; set; }

            [Required]
            public int Weight { get; set; }

            [Required, EnumDataType(typeof(Gender))]
            public Gender Gender { get; set; }

            [Required, EnumDataType(typeof(ActivityLevel))]
            public ActivityLevel Activity { get; set; }

            [Required, EnumDataType(typeof(Benefit))]
            public Benefit Benefit { get; set; }

            [Required, EnumDataType(typeof(Diet))]
            public Diet Diet { get; set; }

            [ScaffoldColumn(false)]
            public double Bmi { get; set; }

            public List<Intolerance> Intolerances { get; set; }

            //zipa
            public List<DailyMealPlan> WeeklyMealPlan { get; set; }


            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = Input.Email,
                    NormalizedUserName = Input.Email.ToUpper(),
                    Email = Input.Email,
                    NormalizedEmail = Input.Email.ToUpper(),
                    Name = Input.Name,
                    Surname = Input.Surname,
                    Role = Role.USER,
                    Age = Input.Age,
                    Height = Input.Height,
                    Weight = Input.Weight,
                    Gender = Input.Gender,
                    Activity = Input.Activity,
                    Benefit = Input.Benefit,
                    Diet = Input.Diet,
                    Intolerances = Input.Intolerances
                };
                //automatski se kreira ID za usera kad god se pozove new User(); bez inserta u bazu
                user.UpdateTargetCalories();
                user.WeeklyMealPlan = SpoonacularApi.Instance.getWeeklyMealPlan(user);

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);
                    // TODO: ZA DOVENI USER (ID) POZVATI WEEKLYMEALPLAN KOJI CE KREIRATI DAILYMEALPLAN itd
                    // MORA BITI callback js msm
                    //SpoonacularApi api = new SpoonacularApi();
                    //List<DailyMealPlan> mealPlan = api.getWeeklyMealPlan(InitialUser);
                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
