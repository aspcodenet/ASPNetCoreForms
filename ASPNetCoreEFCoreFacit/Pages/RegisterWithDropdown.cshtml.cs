using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using ASPNetCoreEFCoreFacit.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASPNetCoreEFCoreFacit.Pages
{
    public class RegisterWithDropdownModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        public RegisterWithDropdownModel(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var userreg = new UserRegistration();
                userreg.Country = _dbContext.Countries.First(e => e.Id == CountryId);
                userreg.First = First;
                userreg.Email = Email;
                userreg.Last = Last;
                userreg.OkUpdates = OkUpdates;
                userreg.Password = Password;
                userreg.UserType = Enum.Parse<Usertype>(Usertype);
                _dbContext.UserRegistrations.Add(userreg);
                _dbContext.SaveChanges();
                return RedirectToPage("/RegisterConfirmation", new
                {
                    firstname = First
                });
            }

            return Page();
        }

        public void OnGet()
        {
            AllCountries = _dbContext.Countries.Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Namn
            }).ToList();


            AllUserTypes = Enum.GetValues(typeof(Usertype)).Cast<Usertype>().Select(r=>new 
                SelectListItem(r.ToString(),r.ToString())).ToList();

        }

        [BindProperty]
        public bool OkUpdates { get; set; }

        [BindProperty]
        [MaxLength(100)]
        public string Password { get; set; }

        [BindProperty]
        [MaxLength(100)]
        public string Last { get; set; }

        [BindProperty]
        [MaxLength(100)]
        public string First { get; set; }

        [BindProperty]
        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [BindProperty] public int CountryId { get; set; }

        [BindProperty] public string Usertype { get; set; }

        public List<SelectListItem> AllCountries { get; set; }
        public List<SelectListItem> AllUserTypes { get; set; }
    }
}
