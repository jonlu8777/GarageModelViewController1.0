using GarageModelViewController.Models;

namespace GarageModelViewController.ViewModels
{
    public class ReceiptViewModel
    {

        private int price;
        public DateTime Now { get; set; } = DateTime.Now;

        public TimeSpan ParkedTime { get; set; }
        public int Price
        {
            get { return 35; }
            private set { price = this.Price; }
        }

        public int TotalPrice { get { return this.Price + (this.Price * (int)this.ParkedTime.TotalHours); } private set { price = 10; } } //Eventuellt räkna ut priset här här! 

        public ParkedVehicle? ParkedVehicle { get; set; }


    }
}
