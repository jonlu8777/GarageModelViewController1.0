using System.ComponentModel;

namespace GarageModelViewController.Models
{
    public class IndexViewModel
    {
        [DisplayName("Total Parking Spots Available:")]
        public int TotalParkingSpots { get; set; } = 5;
        [DisplayName("List of Parking Spots Available:")]
        public IEnumerable<int> AvailableSpots { get; set; } 
        public IEnumerable<ParkedVehicle> Vehicles { get; set; } = new List<ParkedVehicle>();


    }
}
