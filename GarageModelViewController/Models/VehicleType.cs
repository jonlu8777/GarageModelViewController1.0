using System.ComponentModel.DataAnnotations;

namespace GarageModelViewController.Models
{
    public enum VehicleType
    {
        [Display(Name = "None")]
        None,
        [Display(Name = "Airplane")]
        Airplane,
        [Display(Name = "Car")]
        Car,
        [Display(Name = "Bus")]
        Bus,
        [Display(Name = "Motorcycle")]
        Motorcycle
    }
}
