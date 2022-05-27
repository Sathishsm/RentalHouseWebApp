using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RentalHouseWebApp.DataAccess;
using RentalHouseWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace RentalHouseWebApp.Pages.RentalCustomers
{
    [Authorize(Roles = "admin")]
    public class DeleteModel : PageModel
        {
            [BindProperty(SupportsGet = true)]
            public int Id { get; set; }

            public bool ShowButton { get; set; }

            public string Name { get; set; }

            public string SuccessMessage { get; set; }
            public string ErrorMessage { get; set; }

            public DeleteModel()
            {
                Name = "";
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

                var rentalcustomerdata = new RentalCustomerData();
                var cus = rentalcustomerdata.GetRentalCustomerById(id);

                if (cus != null)
                {
                    Name = cus.Name;
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

                var rentalcustomerdata = new RentalCustomerData();
                var numOfRows = rentalcustomerdata.Delete(Id);
                if (numOfRows > 0)
                {
                    SuccessMessage = $"customer {Id} deleted successfully!";
                    ShowButton = false;
                }
                else
                {
                    ErrorMessage = $"Error! Unable to delete customer {Id}";
                }
            }
        }
    }