using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using RentalHouseWebApp.DataAccess;
using RentalHouseWebApp.Models;
 using Microsoft.AspNetCore.Authorization;

namespace RentalHouseWebApp.Pages.Owners
{
    [Authorize(Roles = "admin")]
    public class AddModel : PageModel
        {
         



            [BindProperty]
            [Display(Name = "Name")]
            [Required]
            public string Name { get; set; }

          
            [BindProperty]
            [Display(Name = "MobileNumber")]
            [Required]
            public string MobileNumber { get; set; }

            [BindProperty]
            [Display(Name = "ExpectedStatus")]
            [Required]
            public string SelectedExpectedStatus { get; set; }

            [BindProperty]
            [Display(Name = "Rules")]
            [Required]
            public string Rules { get; set; }
        [BindProperty]
        [Display(Name = "Password")]
        [Required]
        public string Password { get; set; }


        [BindProperty]
        public List<SelectListItem> ExpectedStatus { get; set; }
      
            public string SuccessMessage { get; set; }
            public string ErrorMessage { get; set; }
            public AddModel()
            {
                Name = "";
              
                MobileNumber = "";
                ExpectedStatus = GetExpecetedStatus();
            Password = "";
                Rules = "";
                SuccessMessage = "";
                ErrorMessage = "";
               


            }

            public void OnGet()
            {
                SuccessMessage = "";
                ErrorMessage = "";
                ExpectedStatus= GetExpecetedStatus();
                


            }
            private List<SelectListItem> GetExpecetedStatus()
            {
                var selectItems = new List<SelectListItem>();
                selectItems.Add(new SelectListItem { Text = "Bechular", Value = "Bechular" });
                selectItems.Add(new SelectListItem { Text = "Family", Value = "Family" });
            selectItems.Add(new SelectListItem { Text = "Only Boys", Value = "Only Boys" });
            selectItems.Add(new SelectListItem { Text = "Only Girls ", Value = "Only Girls" });



            return selectItems;
            }


          



            public void OnPost()
            {
            ExpectedStatus = GetExpecetedStatus();

            if (!ModelState.IsValid)
                {
                    ErrorMessage = "Invalid Data.Please try again";
                    return;
                }
               

                var ownerdata = new OwnerData();
                var newOwnerModel = new OwnerModel
                {
                    Name = Name,
                   
                    MobileNumber = MobileNumber,
                    ExpectedStatus=SelectedExpectedStatus,
                    Rules=Rules,
                    Password=Password

                    
                };
                var insertedOwner = ownerdata.Insert(newOwnerModel);

                if (insertedOwner != null && insertedOwner.Id > 0)
                {
                    SuccessMessage = $"Successfully Inserted Owner {insertedOwner.Id}";
                    ModelState.Clear();
                }
                else
                {
                    ErrorMessage = "Error! Add Failed.Please try Again";
                }
            }
        }
    }

