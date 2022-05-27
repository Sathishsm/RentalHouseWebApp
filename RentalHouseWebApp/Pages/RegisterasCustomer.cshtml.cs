using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using RentalHouseWebApp.DataAccess;
using RentalHouseWebApp.Models;
using Microsoft.AspNetCore.Authorization;


namespace RentalHouseWebApp.Pages
{
    public class RegisterasCustomerModel : PageModel
    {



       
       



            [BindProperty]
            [Display(Name = "Name")]
            [Required]
            public string Name { get; set; }
            [BindProperty]
            [Display(Name = "Password")]
            [Required]
            public string Password { get; set; }

            [BindProperty]
            [Display(Name = "Gender")]
            [Required]
            public string Gender { get; set; }

            public string[] Genders = new[] { "Male", "Female", "Unspecified" };

            [BindProperty]
            [Display(Name = "DOB")]
            [Required]
            [DataType(DataType.Date)]
            public DateTime DOB { get; set; }
            [BindProperty]
            [Display(Name = "MobileNumber")]
            [Required]
            public string MobileNumber { get; set; }

            [BindProperty]
            [Display(Name = "PermanentAddress")]
            [Required]
            public string PermanentAddress { get; set; }

            [BindProperty]
            [Display(Name = "PreferedLocation")]
            [Required]
            public string PreferedLocation { get; set; }

            [BindProperty]
            public List<SelectListItem> Locations { get; set; }

            [BindProperty]
            public List<SelectListItem> Status { get; set; }
            [BindProperty]
            [Display(Name = "Status")]
            public string SelectedStatus { get; set; }
            public string SuccessMessage { get; set; }
            public string ErrorMessage { get; set; }
            public RegisterasCustomerModel()
            {
                Name = "";
                Gender = "";
                DOB = DateTime.Now.AddYears(-20);
                MobileNumber = "";
                Password = "";
                PermanentAddress = "";
                PreferedLocation = "";
                SuccessMessage = "";
                ErrorMessage = "";
                Status = GetStatus();
                Locations = GetPreferedLocations();


            }

            public void OnGet()
            {
                SuccessMessage = "";
                ErrorMessage = "";
                Status = GetStatus();
                Locations = GetPreferedLocations();


            }
            private List<SelectListItem> GetStatus()
            {
                var selectItems = new List<SelectListItem>();
                selectItems.Add(new SelectListItem { Text = "Bachular", Value = "Bachular" });
                selectItems.Add(new SelectListItem { Text = "Family", Value = "Family" });


                return selectItems;
            }


            private List<SelectListItem> GetPreferedLocations()
            {
                var selectItems = new List<SelectListItem>();
            selectItems.Add(new SelectListItem { Text = "", Value = "" });
            selectItems.Add(new SelectListItem { Text = "Saravanambatti", Value = "Saravanambatti" });
                selectItems.Add(new SelectListItem { Text = "Gandhipuram", Value = "Gandhipuram" });
                selectItems.Add(new SelectListItem { Text = "Ganapathi", Value = "Ganapathi" });

                return selectItems;
            }



            public void OnPost()
            {
                if (!ModelState.IsValid)
                {
                    ErrorMessage = "Invalid Data.Please try again";
                    return;
                }
                Status = GetStatus();
                Locations = GetPreferedLocations();

                var rentalcustomerdata = new RentalCustomerData();
                var newRentalCustomerModel = new RentalCustomerModel
                {
                    Name = Name,
                    Gender = Gender,
                    DOB = DOB,
                    MobileNumber = MobileNumber,
                    Password = Password,

                    PermanentAddress = PermanentAddress,
                    PreferedLocation = PreferedLocation,
                    Status = SelectedStatus
                };
                var insertedCustomer = rentalcustomerdata.Insert(newRentalCustomerModel);

                if (insertedCustomer != null && insertedCustomer.Id > 0)
                {
                    SuccessMessage = $"Successfully Registered {insertedCustomer.Name}";
                    ModelState.Clear();
                }
                else
                {
                    ErrorMessage = "Error! Add Failed.Please try Again";
                }
            }
        }
    }
