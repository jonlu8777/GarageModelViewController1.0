namespace GarageModelViewController.Models
{
    public class ReceiptViewModel
    {

        private int price;
        public DateTime Now { get; set; } = DateTime.Now;

        public TimeSpan ParkedTime { get; set; }
        public int Price
        {
            get { return 35; }
            private set { price = Price; }
        }

        public int TotalPrice { get { return Price + Price * (int)ParkedTime.TotalHours; } private set { price = 10; } } //Eventuellt räkna ut priset här här! 

        public ParkedVehicle? ParkedVehicle { get; set; }


    }
}
