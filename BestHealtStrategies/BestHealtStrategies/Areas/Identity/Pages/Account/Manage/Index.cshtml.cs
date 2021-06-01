using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BestHealtStrategies.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using static BestHealtStrategies.Models.ValueObjects.ValueObjects;

namespace BestHealtStrategies.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
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



            [Phone]
            [Display(Name = "Phone number")]
            public string PhoneNumber { get; set; }
        }

        private async Task LoadAsync(User user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Username = userName;

            Input = new InputModel
            {
                Age = user.Age,
                Height = user.Height,
                Weight = user.Weight,
                Gender = user.Gender,
                Activity = user.Activity,
                Benefit = user.Benefit,
                Diet = user.Diet,
                Bmi = user.Weight / user.Height * user.Height,
                Intolerances = user.Intolerances,
                PhoneNumber = phoneNumber
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            if (Input.Age != user.Age)
            {
                user.Age = Input.Age;
            }
            if (Input.Height != user.Height)
            {
                user.Height = Input.Height;
            }
            if (Input.Weight != user.Weight)
            {
                user.Weight = Input.Weight;
            }
            if (Input.Activity != user.Activity)
            {
                user.Activity = Input.Activity;
            }

            await _userManager.UpdateAsync(user);

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }
    }
}
