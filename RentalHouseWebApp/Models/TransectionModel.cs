namespace RentalHouseWebApp.Models
{
    public class TransectionModel
    {
        
        public int OwnerId { get; set; }
        public int HouseId { get; set; }
        public int RentalCustomerId{ get; set; }
        public DateTime DateofJoining { get; set; }
        public DateTime DateofEnd { get; set; }
        public string Rent { get; set; }
        public string Status { get; set; }

      



        public TransectionModel()
        {
           
            OwnerId=0;
            HouseId=0;
            RentalCustomerId=0;
            DateofJoining=DateTime.Now;
            DateofEnd=DateTime.Now;
            Rent="";
            Status = "";
        }
    }
}