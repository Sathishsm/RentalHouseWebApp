using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using RentalHouseWebApp.DataAccess;
using RentalHouseWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace RentalHouseWebApp.Pages.RentalCustomers
{
    [Authorize(Roles = "User,admin")]
    public class EditModel : PageModel
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


            [BindProperty]
            [Display(Name = "DOB")]
            [DataType(DataType.Date)]
            public DateTime DOB { get; set; }

            [BindProperty]
            [Display(Name = "Mobile Number")]
            [Required]
            public string MobileNumber { get; set; }

            [BindProperty]
            [Display(Name = "Permanent Address")]
            [Required]
            public string  PermanentAddress{ get; set; }

            [BindProperty]
            [Display(Name = "PreferedLocation")]
            [Required]
            public List<SelectListItem> Locations { get; set; }

            [BindProperty]
            public string PreferedLocation{ get; set; }
            [BindProperty]
            [Display(Name = "Status")]
            [Required]
            public string Status { get; set; }

            public string[] Genders = new[] { "Male", "Female", "Unspecified" };


            public string SuccessMessage { get; set; }
            public string ErrorMessage { get; set; }

            [BindProperty(SupportsGet = true)]
            public int Id { get; set; }


            public EditModel()
            {
                Name = "";
                Gender = "";
                DOB = DateTime.Now;
                
                MobileNumber = "0000000000";
            Password = "";
                PermanentAddress = "";
                Locations= GetPreferedLocation();
                Status = Status;

            }


        private static List<SelectListItem> GetStatus()
        {
            var selectItems = new List<SelectListItem>();
            selectItems.Add(new SelectListItem { Text = "Bechular", Value = "Bechular" });
            selectItems.Add(new SelectListItem { Text = "Family", Value = "Family" });


            return selectItems;
        }


        private List<SelectListItem> GetPreferedLocation()
        {
            var selectItems = new List<SelectListItem>();
            selectItems.Add(new SelectListItem { Text = "Saravanambatti", Value = "Saravanambatti" });
            selectItems.Add(new SelectListItem { Text = "Gandhipuram", Value = "Gandhipuram" });
            selectItems.Add(new SelectListItem { Text = "Ganapathi", Value = "Ganapathi" });

            return selectItems;
        }







        public void OnGet(int id)
            {
                Id = id;
                if (Id < 0)
                {
                    ErrorMessage = "Invalid Id";
                    return;
                }
                Locations = GetPreferedLocation();
                var rentalcustomermodel = new RentalCustomerData();
            var emp = rentalcustomermodel.GetRentalCustomerById(id);
                if (emp != null)
                {
                    Name = emp.Name;
                    Gender = emp.Gender;
                    DOB = emp.DOB;
                Password = emp.Password;
                    MobileNumber = emp.MobileNumber;
                PermanentAddress = emp.PermanentAddress;
                    
                    PreferedLocation = emp.PreferedLocation;
                    Status = emp.Status;
                }
                else
                {
                    ErrorMessage = "No record found with this id";
                }
            }

            public void OnPost()
            {
                if (!ModelState.IsValid)
                {
                    ErrorMessage = "Invalid Data... Please enter again";
                    return;
                }
                Locations = GetPreferedLocation();

                var rentalcustomerdata = new RentalCustomerData();
                var empToUpdate = new RentalCustomerModel { Id = Id, Name = Name, Gender = Gender, DOB = DOB, MobileNumber = MobileNumber,Password=Password,PermanentAddress=PermanentAddress, PreferedLocation = PreferedLocation, Status = Status };
                var updatedcustomer = rentalcustomerdata.Update(empToUpdate);

                if (updatedcustomer != null)
                {
                    SuccessMessage = $"Customer{updatedcustomer.Id} updated successfully";
                }
                else
                {
                    ErrorMessage = $"Error! Updating";
                }
            }
        }
    }

