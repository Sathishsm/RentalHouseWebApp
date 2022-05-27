namespace RentalHouseWebApp.Models
{
    public class RentalCustomerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Gender { get; set; }
        public DateTime DOB { get; set; }
        public string MobileNumber { get; set; }
        public string PermanentAddress { get; set; }
        public string PreferedLocation { get; set; }
        public string Status { get; set; }
        public string Password { get; set; }


        //constructor


        public RentalCustomerModel()
        {
            Id = 0;
            Name = "";
            Gender = "";
            DOB = DateTime.Now;
            MobileNumber = "";
            PermanentAddress = "";
            PreferedLocation = "";
            Status = "";
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
