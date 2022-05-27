using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using RentalHouseWebApp.DataAccess;
using RentalHouseWebApp.Models;
using Microsoft.AspNetCore.Authorization;
namespace RentalHouseWebApp.Pages.Houses
{
    [Authorize(Roles = "Owner,admin")]
    public class EditModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty]
        [Display(Name = "Picture")]
        [Required]
        public string Picture { get; set; }

       
        [BindProperty]
        [Display(Name = "Location")]
        [Required]
        public string Location { get; set; }

        [BindProperty]
        [Display(Name = "Facility")]
        [Required]
        public string Facilities { get; set; }

        [BindProperty]
        public List<SelectListItem> Facility { get; set; }

        [BindProperty]
        public List<SelectListItem> ownerList { get; set; }
        [BindProperty]
        [Display(Name = "OwnerId")]
        public int SelectedOwnerId { get; set; }

        [BindProperty]
        [Display(Name = "Rent")]
    
        public string Rent { get; set; }
        [BindProperty]
        public List<SelectListItem> CustomerList { get; set; }
        [BindProperty]
        [Display(Name = "CustomerId")]
        public int SelectedCustomerId { get; set; }
        [BindProperty]
        [Display(Name = "DateofJoining")]
        [Required]
        public DateTime DateofJoining { get; set; }
        [BindProperty]
        [Display(Name = "DateofEnd")]
        [Required]
        public DateTime DateofEnd { get; set; }
        [BindProperty]
        [Display(Name = "Status")]
        [Required]
        public string Status { get; set; }
        [BindProperty]
        public List<SelectListItem> Selectedstatus { get; set; }
        public string SuccessMessage { get; set; }
        public string ErrorMessage { get; set; }
        public EditModel()
        {
            Picture = "";
            Rent = "";
            Location = "";
            SuccessMessage = "";
            ErrorMessage = "";
            CustomerList = GetCustomer();
            ownerList = GetOwners();
            Facility = Getfacility();
            Selectedstatus = GetStatus();


        }
        private List<SelectListItem> GetOwners()
        {
            //Get Data from Data Access
            var ownerdata = new OwnerData();
            var ownerList = ownerdata.GetAll();

            //Create SelectListItem
            var ownerSelectList = new List<SelectListItem>();
            foreach (var owner in ownerList)
            {
                ownerSelectList.Add(new SelectListItem
                {
                    Text = $"{owner.Id} - {owner.MobileNumber}",
                    Value = owner.Id.ToString(),
                });
            }
            return ownerSelectList;
        }
        private List<SelectListItem> GetCustomer()
        {
            //Get Data from Data Access
            var rentalcustomerdata = new RentalCustomerData();
            var customerList = rentalcustomerdata.GetAll();

            //Create SelectListItem
            var customerSelectList = new List<SelectListItem>();
            foreach (var customer in customerList)
            {
                customerSelectList.Add(new SelectListItem
                {
                    Text = $"{customer.Id} - {customer.MobileNumber}",
                    Value = customer.Id.ToString(),
                });
            }
            return customerSelectList;
        }
        private List<SelectListItem> Getfacility()
        {
            var selectItems = new List<SelectListItem>();
            selectItems.Add(new SelectListItem { Text = "1BHK", Value = "1BHK" });
            selectItems.Add(new SelectListItem { Text = "2BHK", Value = "2BHK" });
            selectItems.Add(new SelectListItem { Text = "3BHK", Value = "3BHK" });

            return selectItems;
        }
        private List<SelectListItem> GetStatus()
        {
            var selectItems = new List<SelectListItem>();
            selectItems.Add(new SelectListItem { Text = "Rented", Value = "Rented" });
            selectItems.Add(new SelectListItem { Text = "NotRented", Value = "NotRented" });


            return selectItems;
        }






        public void OnGet(int id)
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Invalid Data.Please try again";
                return;
            }
            ownerList = GetOwners();
            Facility = Getfacility();

            var housedata = new HouseData();
            var newHouseModel = new HouseModel
            {
                Location = Location,

                OwnerId = SelectedOwnerId,
                Picture = Picture,
                Facilities = Facilities,
                Rent = Rent,
                RentalCustomerId=SelectedCustomerId,
                DateofJoining = DateofJoining,
                DateofEnd = DateofEnd,
                Status = Status
            };
        }
    
           
        
            public void OnPost()
            {
            CustomerList = GetCustomer();
            ownerList = GetOwners();
            Facility = Getfacility();
            Selectedstatus = GetStatus();


            if (!ModelState.IsValid)
                {
                    ErrorMessage = "Invalid Data... Please enter again";
                    return;
                }
                Facility = Getfacility();

                var housedata = new HouseData();
                var houseToUpdate = new HouseModel { Id=Id,Location=Location,OwnerId=SelectedOwnerId,Picture=Picture,Facilities=Facilities, Rent = Rent, RentalCustomerId = SelectedCustomerId, DateofJoining = DateofJoining, DateofEnd = DateofEnd, Status = Status };
                var updatedhouse = housedata.Update(houseToUpdate);

                if (updatedhouse != null)
                {
                    SuccessMessage = $"House {updatedhouse.Id} updated successfully";
                }
                else
                {
                    ErrorMessage = $"Error! Updating";
                }
            }
    }
}


