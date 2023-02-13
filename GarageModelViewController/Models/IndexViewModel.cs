using System.ComponentModel;

namespace GarageModelViewController.Models
{
    public class IndexViewModel
    {
        [DisplayName("Total Parking Spots")]
        public int TotalParkingSpots { get; set; } = 30;

        
        public IEnumerable<ParkedVehicle> Vehicles { get; set; } = new List<ParkedVehicle>();


    }
}
