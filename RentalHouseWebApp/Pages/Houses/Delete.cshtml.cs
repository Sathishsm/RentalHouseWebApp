using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RentalHouseWebApp.DataAccess;
using RentalHouseWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace RentalHouseWebApp.Pages.Houses
{
    [Authorize(Roles = "Owner,admin")]
    public class DeleteModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public bool ShowButton { get; set; }

        public string Location { get; set; }

        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }

        public DeleteModel()
        {
            Location = "";
            SuccessMessage = "";
            ErrorMessage = "";
            ShowButton = true;
        }

        public void OnGet(int id)
        {
            Id = id;

            if (Id <= 0)
            {
                ErrorMessage = "Invalid Id";
                return;
            }

            var housedata = new HouseData();
            var cus = housedata.GetHouseById(id);

            if (cus != null)
            {
                Location = cus.Location;
            }
            else
            {
                ErrorMessage = "No Record found with that Id";
            }
        }

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Invalid Data";
                return;
            }

            var housedata = new HouseData();
            var numOfRows = housedata.Delete(Id);
            if (numOfRows > 0)
            {
                SuccessMessage = $"House {Id} deleted successfully!";
                ShowButton = false;
            }
            else
            {
                ErrorMessage = $"Error! Unable to delete House {Id}";
            }
        }
    }
}