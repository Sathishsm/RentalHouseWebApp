using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using RentalHouseWebApp.DataAccess;
using RentalHouseWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace RentalHouseWebApp.Pages.Owners
{
    [Authorize(Roles = "Owner,admin")]
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
            [Display(Name = "Mobile Number")]
            [Required]
            public string MobileNumber { get; set; }

        [BindProperty]
        [Display(Name = "ExpectedStatus")]
        [Required]
        public List<SelectListItem> Status { get; set; }

        [BindProperty]
        public string ExpectedStatus { get; set; }

        [BindProperty]
            [Display(Name = "Rules")]
            [Required]
            public string Rules { get; set; }
       
       
      

            public string SuccessMessage { get; set; }
            public string ErrorMessage { get; set; }

            [BindProperty(SupportsGet = true)]
            public int Id { get; set; }


            public EditModel()
            {
                Name = "";
               
                MobileNumber = "0000000000";
                Status = GetExpecetedStatus();
                Rules = "";
            Password = "";
                
               

            }


        private List<SelectListItem> GetExpecetedStatus()
        {
            var selectItems = new List<SelectListItem>();
            selectItems.Add(new SelectListItem { Text = "Bachular", Value = "Bachular" });
            selectItems.Add(new SelectListItem { Text = "Family", Value = "Family" });
            selectItems.Add(new SelectListItem { Text = "Only Boys", Value = "Only Boys" });
            selectItems.Add(new SelectListItem { Text = "Only Girls ", Value = "Only Girls" });



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
                var ownermodel = new OwnerData();
                var emp = ownermodel.GetOwnerById(id);
                if (emp != null)
                {
                    Name = emp.Name;
                    

                    MobileNumber = emp.MobileNumber;
                    ExpectedStatus = emp.ExpectedStatus;

                    Rules= emp.Rules;
                Password = emp.Password;
                  
                }
                else
                {
                    ErrorMessage = "No record found with this id";
                }
            }

            public void OnPost()
            {
                 Status = GetExpecetedStatus();

            if (!ModelState.IsValid)
                {
                    ErrorMessage = "Invalid Data... Please enter again";
                    return;
                }
                

                var ownerdata = new OwnerData();
                var empToUpdate = new OwnerModel { Id = Id, Name = Name,  MobileNumber = MobileNumber, ExpectedStatus = ExpectedStatus, Rules = Rules,Password=Password };
                var updatedowner = ownerdata.Update(empToUpdate);

                if (updatedowner != null)
                {
                    SuccessMessage = $"Owner {updatedowner.Id} updated successfully";
                }
                else
                {
                    ErrorMessage = $"Error! Updating";
                }
            }
        }
    }


