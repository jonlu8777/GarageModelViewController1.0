using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace GarageModelViewController.Models
{
    public class ParkedVehicle
    {
		//fordonstyp​, ​registreringsnummer​, ​färg​, ​märke​, ​modell​, ​antal hjul samtankomsttid​.
		private string registrationNumber;
		private string color;
		private string brand;
	    private string modelName;
		private int numberOfWheels;
		private VehicleType vehicleType = VehicleType.None;
		

		public int Id { get; set; }
        [ReadOnly(true)]
		[DisplayName("Time Of Arrival")]
        public DateTime? Ankomsttid {	get; private set;  } = DateTime.Now;
        [Range(0,8)]
        public int NumberOfWheels
		{
			get { return numberOfWheels; }
			set { numberOfWheels = value; }
		}
		public VehicleType VehicleType { get;set; }
        [StringLength(6)]
        
        public string RegistrationNumber
		{
			get { return registrationNumber; }
			set { registrationNumber = value; }   
		}                                         
		[StringLength(20)]
        public string Color
		{
			get { return color; }
			set { color = value; }
		}
        [StringLength(20)]
        public string Brand
		{
			get { return brand; }
			set { brand = value; }
		}
        [StringLength(20)]
        public string ModelName
		{
			get { return modelName; }
			set { modelName = value; }
		}
		public TimeSpan? ParkedTime { get { return DateTime.Now - this.Ankomsttid; } }	

		public int? ParkingSpot { get; set; }	

    }
}
