using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RentalHouseWebApp.DataAccess;
using RentalHouseWebApp.Models;
using RentalHouseWebApp.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace RentalHouseWebApp.Pages.Houses
{
    [Authorize(Roles = "Owner,admin")]
    public class ListModel : PageModel
    {



        [BindProperty]
        public string SearchText { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        public List<HouseModel> Houses { get; set; }



        public ListModel()
        {
            SearchText = "";
            SuccessMessage = "";
            ErrorMessage = "";
            Houses = new List<HouseModel>();
        }


        public void OnGet()
        {
            var housedata = new HouseData();
            Houses = housedata.GetAll();
            var userId = HttpContext.Session.GetInt32("UserId").Value;

            if (User.IsInRole("Owner"))
            {
                Houses = Houses.Where(X => X.OwnerId == userId).ToList();
            }
        }


        public void OnPost()
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

            var housedata = new HouseData();
            Houses = housedata.GetHouseByLocation(SearchText);




            if (Houses.Count > 0 && Houses != null)
            {
                SuccessMessage = $"{Houses.Count} Owner found ";

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


