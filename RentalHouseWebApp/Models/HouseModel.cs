namespace RentalHouseWebApp.Models
{
    public class HouseModel
    {
        public int Id { get; set; }
        public string Location { get; set; }

        public int OwnerId { get; set; }
        public int RentalCustomerId { get; set; }
        public string Picture { get; set; }
        public string Facilities{ get; set; }
        public string? Rent { get; set; }
        public DateTime DateofJoining { get; set; }
        public DateTime? DateofEnd { get; set; }
        public string Status { get; set; }

        


        //constructor


        public HouseModel()
        {
            Id = 0;
            Location = "";
            OwnerId = 0;
            Picture = "";
            Facilities = "";
            Rent = "";
            DateofJoining = DateTime.Now;
            DateofEnd = DateTime.Now;
            Status = "";
        }

        public bool IsValid()
        {

            if (Location == null || Location.Trim() == "" || Location.Trim().Length > 20)
            {
                return false;
            }


            return true;
        }

    }
}
