using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RentalHouseWebApp.DataAccess;
using RentalHouseWebApp.Models;
using RentalHouseWebApp.Helpers;
using Microsoft.AspNetCore.Authorization;

namespace RentalHouseWebApp.Pages.Transections
{
    [Authorize(Roles = "admin")]
    public class ListModel : PageModel
    {



        [BindProperty]
        public int SearchText { get; set; }

        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        public List<TransectionModel> Transections { get; set; }



        public ListModel()
        {
            SearchText = 0;
            SuccessMessage = "";
            ErrorMessage = "";
            Transections = new List<TransectionModel>();
        }


        public void OnGet()
        {
            var transectiondata = new TransectionData();
            Transections = transectiondata.GetAll();
        }


        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Invalid Search";
                return;
            }

            if (SearchText <= 0)
            {
                ErrorMessage = "Invalid Data";
                return;

            }

            var transectiondata = new TransectionData();
            Transections = transectiondata.GetAll();




            if (Transections.Count > 0 && Transections != null)
            {
                SuccessMessage = $"{Transections.Count} Owner found ";

            }
            else
            {
                ErrorMessage = " Transection not found";

            }


        }

        public void OnPostClear()
        {
            SearchText = 0;
            ModelState.Clear();
            OnGet();
        }


    }

}


