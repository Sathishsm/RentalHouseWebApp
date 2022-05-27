using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using RentalHouseWebApp.DataAccess;
using RentalHouseWebApp.Models;
using Microsoft.AspNetCore.Authorization;

namespace RentalHouseWebApp.Pages
{
   
    public class IndexModel : PageModel
        {
            public int RentalCustomerCount { get; set; }
            public int OwnerCount { get; set; }
            public int HouseCount { get; set; }
          //  public int CompletedTrainingCount { get; set; }

            public string ErrorMessage { get; set; }

            [FromQuery(Name = "action")]
            public string Action { get; set; }
            private readonly ILogger<IndexModel> _logger;


            public IndexModel(ILogger<IndexModel> logger)
            {
                _logger = logger;
                RentalCustomerCount = 0;
                OwnerCount = 0;
                HouseCount = 0;
               // CompletedTrainingCount = 0;
                ErrorMessage = "";

            }
            public void OnGet()
            {
                if (!String.IsNullOrEmpty(Action) && Action.ToLower() == "logout")
                {
                    Logout();
                    return;
                }

                var dashBoardData = new DashBoardData();
                var dashboard = dashBoardData.GetAll();
                if (dashboard != null)
                {
                    RentalCustomerCount = dashboard.RentalCustomerCount;
                    OwnerCount = dashboard.OwnerCount;
                    HouseCount = dashboard.HouseCount;
                  //  CompletedTrainingCount = dashboard.CompletedTrainingCount;
                }
                else
                {
                    ErrorMessage = $"No Dashboard Data Available - {dashBoardData.ErrorMessage}";
                }
            }
            public void OnPost()
            {
                Logout();
            }
            private void Logout()
            {
                HttpContext.SignOutAsync();
                Response.Redirect("/Index");
            }
        }
    }
