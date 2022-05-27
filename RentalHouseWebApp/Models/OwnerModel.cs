namespace RentalHouseWebApp.Models
{
    public class OwnerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string ExpectedStatus { get; set; }
        public string Rules { get; set; }
        public string Password { get; set; }




        public OwnerModel()
        {
            Id = 0;
            Name = "";
            MobileNumber = "";
            ExpectedStatus = "";
            Rules = "";
            Password = "";
        }
        public bool IsValid()
        {

            if (Name == null || Name.Trim() == "" || Name.Trim().Length > 20)
            {
                return false;
            }


            return true;
        }

    }
}
