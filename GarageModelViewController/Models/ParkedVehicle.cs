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
		private DateTime ankomsttid;

		public int Id { get; set; }	

        [DataType(DataType.Date)]
        [Display(Name = "Ankomsttid")]
        public DateTime Ankomsttid
		{
			get { return ankomsttid; }
			set { ankomsttid = value; }
		}
		public int NumberOfWheels
		{
			get { return numberOfWheels; }
			set { numberOfWheels = value; }
		}
		public VehicleType VehicleType { get;set; }
		
		public string RegistrationNumber
		{
			get { return registrationNumber; }
			set { registrationNumber = value; }
		}
		public string Color
		{
			get { return color; }
			set { color = value; }
		}
		public string Brand
		{
			get { return brand; }
			set { brand = value; }
		}
		public string ModelName
		{
			get { return modelName; }
			set { modelName = value; }
		}

		public ParkedVehicle(VehicleType vehicleType, string registrationNumber, string color, string brand, string modelName, int numberOfWheels)
		{

			this.vehicleType = vehicleType;
			this.registrationNumber = registrationNumber;
            this.color = color;
            this.brand = brand;
            this.modelName = modelName;
            this.numberOfWheels = numberOfWheels;

        }

		


	}
}
