using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RentalHouseWebApp.DataAccess;
using RentalHouseWebApp.Models;
using RentalHouseWebApp.Helpers;
using Microsoft.AspNetCore.Authorization;
namespace RentalHouseWebApp.Pages.Owners
{
    [Authorize(Roles = "Owner,admin")]
    public class ListModel : PageModel
        {



            [BindProperty]
            public string SearchText { get; set; }

            public string ErrorMessage { get; set; }
            public string SuccessMessage { get; set; }
            public List<OwnerModel>Owners { get; set; }



            public ListModel()
            {
                SearchText = "";
                SuccessMessage = "";
                ErrorMessage = "";
            Owners = new List<OwnerModel>();
            }


            public void OnGet()
            {
                var ownerdata = new OwnerData();
            Owners = ownerdata.GetAll();
            var userId = HttpContext.Session.GetInt32("UserId").Value;


            if (User.IsInRole("Owner"))
            {
                Owners = Owners.Where(X => X.Id == userId).ToList();
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

                var ownerdata = new OwnerData();
            Owners = ownerdata.GetOwnerByName(SearchText);




                if (Owners.Count > 0 && Owners != null)
                {
                    SuccessMessage = $"{Owners.Count} Owner found ";

                }
                else
                {
                    ErrorMessage = " Owner not found";

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

