using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using RentalHouseWebApp.DataAccess;
using RentalHouseWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace RentalHouseWebApp.Pages.Houses
{
    [Authorize(Roles = "Owner,admin")]
    public class AddModel : PageModel
        {
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
        public List<SelectListItem> CustomerList { get; set; }
        [BindProperty]
        [Display(Name = "CustomerId")]
        public int SelectedCustomerId { get; set; }
        [BindProperty]
        [Display(Name = "Rent")]
        
        public string Rent { get; set; }

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
             
             public string Status { get; set; }
        [BindProperty]
        public List<SelectListItem> Selectedstatus { get; set; }
        [BindProperty]
        public List<SelectListItem> Locations { get; set; }

        public string SuccessMessage { get; set; }
            public string ErrorMessage { get; set; }
            public AddModel()
            {
            Picture = "";
                Rent= "";
            DateofJoining = DateTime.Now;
                        DateofEnd = DateTime.Now;
                        
            Location = "";
                SuccessMessage = "";
                ErrorMessage = "";
            CustomerList = GetCustomer();
                ownerList = GetOwners();
                Facility = Getfacility();
            Selectedstatus = GetStatus();
            Locations = GetLocations();


        }

            public void OnGet()
            {
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
                    Text = $"{customer.Id} - {customer.Name}",
                    Value = customer.Id.ToString(),
                });
            }
            return customerSelectList;
        }

        private List<SelectListItem> GetLocations()
        {
            var selectItems = new List<SelectListItem>();

            selectItems.Add(new SelectListItem { Text = "", Value = "" });
            selectItems.Add(new SelectListItem { Text = "Saravanambatti", Value = "Saravanambatti" });
            selectItems.Add(new SelectListItem { Text = "Gandhipuram", Value = "Gandhipuram" });
            selectItems.Add(new SelectListItem { Text = "Ganapathi", Value = "Ganapathi" });

            return selectItems;
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




        public void OnPost()
            {
            Locations = GetLocations();

            CustomerList = GetCustomer();
            ownerList = GetOwners();
            Facility = Getfacility();
            Selectedstatus = GetStatus();

            if (!ModelState.IsValid)
                {
                    ErrorMessage = "Invalid Data.Please try again";
                    return;
                }
                

                var housedata = new HouseData();
                var newHouseModel = new HouseModel
                {
                    Location=Location,

                    OwnerId = SelectedOwnerId,
                    Picture=Picture,
                    RentalCustomerId=SelectedCustomerId,
                    Facilities=Facilities,
                    Rent=Rent,
                    DateofJoining=DateofJoining,
                    DateofEnd=DateofEnd,
                    Status=Status
                };
                var insertedHouse = housedata.Insert(newHouseModel);

                if (insertedHouse != null && insertedHouse.Id > 0)
                {
                    SuccessMessage = $"Successfully Inserted House {insertedHouse.Id}";
                    ModelState.Clear();
                }
                else
                {
                    ErrorMessage = "Error! Add Failed.Please try Again";
                }
            }
        }
    }
