﻿using System.ComponentModel;
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
		//private DateTime ankomsttid = DateTime.Now;
		//private DateTime now= DateTime.Now;
		

		public int Id { get; set; }
        [ReadOnly(true)]
        //[Display(Name = "Ankomsttid")] // Value =DateTime.Now)] prova sen... 

        public DateTime Ankomsttid
		{
			get { return ankomsttid; }
			set { ankomsttid = value; }
		}
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
		} //ska jag skapa ny model, eller ska jag skapa validering i Controllern, eller både och? 
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

		//public DateTime? Now { get {  return DateTime.Now; } }

		//public TimeSpan? ParkedTime { get { return this.now - this.ankomsttid; } }	

    }
}
