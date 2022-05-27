using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using RentalHouseWebApp.DataAccess;
using RentalHouseWebApp.Models;




namespace RentalHouseWebApp.Pages
{
    public class LoginModel : PageModel
    {


        [BindProperty]
        public string Name { get; set; }
        [BindProperty]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public LoginModel()
        {
            Name = "";
            Password = "";
            ErrorMessage = "";
        }

        public void OnGet()
        {

        }
        public async void OnPost()
        {
            if (!ModelState.IsValid)
            {
                ErrorMessage = "Invalid Login or Password";
                return;
            }
            var rentalcustomerdata = new RentalCustomerData();
            var rentalcustomermodel = rentalcustomerdata.GetCustomerByName(Name, Password);

            if (rentalcustomermodel != null)
            {
                var userClaims = new List<Claim>()
                {
                     new Claim("UserId",rentalcustomermodel.Id.ToString()),
                     new Claim(ClaimTypes.NameIdentifier,rentalcustomermodel.Id.ToString()),
                    new Claim(ClaimTypes.Name,rentalcustomermodel.Name),

                    new Claim(ClaimTypes.Role,"User")
                };
                var userIdentity = new ClaimsIdentity(userClaims, "user Identity");
                var userPrincipal = new ClaimsPrincipal(new[] { userIdentity });
                await HttpContext.SignInAsync(userPrincipal);
                HttpContext.Session.SetInt32("UserId", rentalcustomermodel.Id);

                Response.Redirect("/Index");
                return;
            }

            var ownerdata = new OwnerData();
            var ownermodel = ownerdata.GetOwnerByName(Name, Password);

            if (ownermodel != null)
            {
                var userClaims = new List<Claim>()
                {
                     new Claim("UserId",ownermodel.Id.ToString()),
                     new Claim(ClaimTypes.NameIdentifier,ownermodel.Id.ToString()),
                    new Claim(ClaimTypes.Name,ownermodel.Name),

                    new Claim(ClaimTypes.Role,"Owner")
                };
                var userIdentity = new ClaimsIdentity(userClaims, "user Identity");
                var userPrincipal = new ClaimsPrincipal(new[] { userIdentity });
                var t = HttpContext.SignInAsync(userPrincipal);
                t.Wait();
                HttpContext.Session.SetInt32("UserId", ownermodel.Id);

                Response.Redirect("/Index");
                return;
            }
            else if (Name == "admin" && Password == "123456")
            {
                var userClaims = new List<Claim>()
                {
                    new Claim("UserId","3"),
                    new Claim(ClaimTypes.Name,"Administrator"),
                    new Claim(ClaimTypes.Role,"admin")
                };
                var userIdentity = new ClaimsIdentity(userClaims, "user Identity");
                var userPrincipal = new ClaimsPrincipal(new[] { userIdentity });

                await HttpContext.SignInAsync(userPrincipal);
                Response.Redirect("/Index");
                return;
            }
            ErrorMessage = "Invalid Login or Password";
        }
    }
}
