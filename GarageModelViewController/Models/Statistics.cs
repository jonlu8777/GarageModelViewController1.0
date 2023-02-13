using System.ComponentModel;

namespace GarageModelViewController.Models
{
    public class Statistics
    {

        [DisplayName("Number Of None")]
        public int NumberOfNone { get; set; }
        [DisplayName("Number Of Cars")]
        public int NumberOfCars { get; set; }
        [DisplayName("Number Of Airplanes")]
        public int NumberOfAircrafts { get; set; }
        [DisplayName("Number Of Motorcycles")]
        public int NumberOfMotorcycles { get; set; }
        [DisplayName("Number Of Buses")]
        public int NumberOfBuses { get; set; }
        [DisplayName("Number Of Wheels")]
        public int TotalNumberOfWheels { get; set; }
        [DisplayName("Total Income")]
        public int TotalIncome { get; set; }


    }
}
