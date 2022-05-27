using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RentalHouseWebApp.DataAccess;
using RentalHouseWebApp.Models;
using RentalHouseWebApp.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace RentalHouseWebApp.Pages.RentalCustomers
{
    [Authorize(Roles = "User,admin")]
    public class ListModel : PageModel
    {
        

        
            [BindProperty]
            public string SearchText { get; set; }

            public string ErrorMessage { get; set; }
            public string SuccessMessage { get; set; }
            public List<RentalCustomerModel> RentalCustomers { get; set; }
        


            public ListModel()
            {
                SearchText = "";
                SuccessMessage = "";
                ErrorMessage = "";
              RentalCustomers = new List<RentalCustomerModel>();
            }


            public void OnGet()
            {
                var rentalcustomerdata = new RentalCustomerData();
                RentalCustomers = rentalcustomerdata.GetAll();

            var userId = HttpContext.Session.GetInt32("UserId").Value;
            if (User.IsInRole("Owner"))
            {
                RentalCustomers = RentalCustomers.Where(X => X.Id == userId).ToList();
            }
            else if (User.IsInRole("User"))
            {
                RentalCustomers = RentalCustomers.Where(X => X.Id == userId).ToList();
            }

        }


        public void Onpost()
            {
                if (!ModelState.IsValid)
                {
                    ErrorMessage = "Invalid Search";
                    return;
                }

                if (string.IsNullOrEmpty(SearchText))
                {
                    ErrorMessage = "Invalid Data";
                    return;

                }

                var rentalcustomerdata = new RentalCustomerData();
            RentalCustomers = rentalcustomerdata.GetRentalCustomerByName(SearchText);
            



            if (RentalCustomers.Count > 0 && RentalCustomers != null)
                {
                    SuccessMessage = $"{RentalCustomers.Count} Customer found ";

                }
                else
                {
                    ErrorMessage = " Customer not found";

                }

           
        }

            public void OnPostClear()
            {
                SearchText = "";
                ModelState.Clear();
                OnGet();
            }


        }

    }
