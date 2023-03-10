using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GarageModelViewController.Data;
using GarageModelViewController.Models;
//using GarageModelViewController.ViewModels;

namespace GarageModelViewController.Controllers
{
    public class ParkedVehiclesController : Controller
    {
        private readonly GarageModelViewControllerContext _context;

        public ParkedVehiclesController(GarageModelViewControllerContext context)
        {
            _context = context;
        }
        //Dropdown enum 
        public IActionResult VehicleType()
        {
            return View();
        }

        public async Task<IActionResult> OverviewModelView()
        {
            return _context.ParkedVehicle != null ?
                         View(await _context.ParkedVehicle.ToListAsync()) :
                         Problem("Entity set 'GarageModelViewControllerContext.ParkedVehicle'  is null.");
        }



        public async Task<IActionResult> Statistics()
        {

            var stats = _context.ParkedVehicle.ToListAsync();
            var wheels = stats.Result.Where(x => x.NumberOfWheels >= 0);
            var wheels2 = wheels.Sum(x => x.NumberOfWheels);
            var nOfNone = stats.Result.Where(e => e.VehicleType == Models.VehicleType.None).Count();
            var nOfCar = stats.Result.Where(e => e.VehicleType == Models.VehicleType.Car).Count();
            var nOfAirplane = stats.Result.Where(e => e.VehicleType == Models.VehicleType.Airplane).Count();
            var nOfMotorcycle = stats.Result.Where(e => e.VehicleType == Models.VehicleType.Motorcycle).Count();
            var nOfBus = stats.Result.Where(e => e.VehicleType == Models.VehicleType.Bus).Count();


            var totalIncome = stats.Result.Where(e=>e.ParkedTime>=TimeSpan.Zero).ToList();
            var totalHours = totalIncome.Sum(t => (int)t.ParkedTime.Value.Hours+1);
            var totalDays = totalIncome.Sum(t => (int)t.ParkedTime.Value.Days);
            var totalinc = (totalHours * 35)+(totalDays*840);

            var model = new Statistics()
            {

                TotalNumberOfWheels= wheels2,
                NumberOfAircrafts= nOfAirplane,
                NumberOfCars= nOfCar,
                NumberOfMotorcycles= nOfMotorcycle,
                NumberOfNone= nOfNone,
                NumberOfBuses=nOfBus,
                TotalIncome = totalinc

            };
            
            return _context.ParkedVehicle != null ?
                         View(model) :
                         Problem("Entity set 'GarageModelViewControllerContext.ParkedVehicle'  is null.");
        }


        public async Task<IActionResult> Receipt(int? id)
        {
            // Undvik dessa
            // ViewData["Name"] = name;
            // ViewBag.Name = name;
            // TempData["name"] = name;                                RegNr,in/ut-checkningstid, total parkerings period och pris automatiskt efter att en bil checkatsut. 
            
            
            if (id == null || _context.ParkedVehicle == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }

            //Nu kod här nedan, skapa ett kvitto view model, från en parkedVehicle model                    AKOMMENTERAR NEDAN, 14:34!

            //var newModel = new Receipt();
            //newModel.ParkedTime = (TimeSpan)parkedVehicle.ParkedTime;
            //newModel.Ankomsttid = (DateTime)parkedVehicle.Ankomsttid;
            //newModel.Now = DateTime.Now;
            //newModel.RegistrationNummber = parkedVehicle.RegistrationNumber;

            var newModel = new ReceiptViewModel()
            {
                ParkedVehicle = parkedVehicle,
                Now = DateTime.Now,
                ParkedTime = DateTime.Now - parkedVehicle.Ankomsttid.Value
             };
            
            //kan beböha mer data faktiskt!

            return View(newModel);
        }

        // GET: ParkedVehicles
        //public async Task<IActionResult> Index()
        //{
        //    return _context.ParkedVehicle != null ?
        //                View(await _context.ParkedVehicle.ToListAsync()) :
        //                Problem("Entity set 'GarageModelViewControllerContext.ParkedVehicle'  is null.");
        //}

        public async Task<IActionResult> Index()
        {
            if (_context.ParkedVehicle != null)
            {
                var existingNumbers = await _context.ParkedVehicle.Select(e => e.ParkingSpot).ToListAsync();
               
                var allNumbers = Enumerable.Range(1, 5);
                var result = allNumbers.Where(x => !existingNumbers.Contains(x)).ToList();             
                
                var parkingSpots = await _context.ParkedVehicle.CountAsync();
                var list = await _context.ParkedVehicle.ToListAsync();
                var veiwModel = new IndexViewModel()
                {
                    Vehicles = list,
                    TotalParkingSpots = 5-parkingSpots,
                    AvailableSpots=result

                };

                return View(veiwModel);
            }
            else
                return Problem("Entity set 'GarageModelViewControllerContext.ParkedVehicle'  is null.");
        }

        public async Task<IActionResult> Filter(string title, int? genre)
        {

            var model = string.IsNullOrWhiteSpace(title) ?
                                    _context.ParkedVehicle :
                                    _context.ParkedVehicle.Where(m => m.RegistrationNumber.StartsWith(title));

            model = genre is null ?
                                model :
                                model.Where(m => (int)m.VehicleType == genre);
            await model.ToListAsync();


            var existingNumbers = await _context.ParkedVehicle.Select(e => e.ParkingSpot).ToListAsync();
            var allNumbers = Enumerable.Range(1, 5);
            var result = allNumbers.Where(x => !existingNumbers.Contains(x)).ToList();

            var parkingSpots = await _context.ParkedVehicle.CountAsync();
            var modelView = new IndexViewModel()
            {
                Vehicles= model,
                TotalParkingSpots= 5-parkingSpots,
                AvailableSpots=result
                
            };

            return View(nameof(Index), modelView);
        }

        // GET: ParkedVehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ParkedVehicle == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }

            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Park
        public IActionResult Park()
        {
            return View();
        }

        // POST: ParkedVehicles/Park
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Park([Bind("Id,Ankomsttid,NumberOfWheels,VehicleType,RegistrationNumber,Color,Brand,ModelName,ParkingSpot")] ParkedVehicle parkedVehicle)
        {


            if (!_context.ParkedVehicle.Any(e => e.RegistrationNumber == parkedVehicle.RegistrationNumber))    //Inget registreringnummer ska matcha !! 
            if (ModelState.IsValid)
            {

                    var parkingSpots =await _context.ParkedVehicle.CountAsync(); //Antal P-upptagna
                   
                    if(parkingSpots>=5) //Full parkering return! obs 5 platser!
                    {
                        ModelState.AddModelError(string.Empty, "Parking lot is full!");
                        if (_context.ParkedVehicle.Any(e => e.RegistrationNumber == parkedVehicle.RegistrationNumber))  //dubbelkollar regNr...
                            ModelState.AddModelError(string.Empty, "Your registration number is already parked!");

                        return View(parkedVehicle);

                    }
                   // var listOfparkingSpots = parkedVehicle.ParkingSpot.ToList();

                    
                    var resu = _context.ParkedVehicle.Where(e=>e.Id >0).Select(e=>e.ParkingSpot);
                    if (resu != null)
                    // if(_context.ParkedVehicle.Any(e=>e.ParkingSpot == 1)) //Om Det finns något parkerat forddon sedan tidigare.                  
                    {
                        var existingNumbers = await _context.ParkedVehicle.Select(e => e.ParkingSpot).ToListAsync();

                        var allNumbers = Enumerable.Range(1, 5);
                        var result = allNumbers.Where(x => !existingNumbers.Contains(x)).First();
                       // var listParkingSpots = new List<int>() { result};
                        parkedVehicle.ParkingSpot = result;

                    }
                    else //Annars tillsätt ett första P-plats
                    {
                        parkedVehicle.ParkingSpot = 1;

                        //if (parkedVehicle.VehicleType == Models.VehicleType.Car|| parkedVehicle.VehicleType == Models.VehicleType.None|| parkedVehicle.VehicleType == Models.VehicleType.Motorcycle)
                        //    parkedVehicle.ParkingSpot = new List<int>() { 1 };
                        //if(parkedVehicle.VehicleType == Models.VehicleType.Bus)
                        //    parkedVehicle.ParkingSpot= new List<int>() {1,2 };
                        //if(parkedVehicle.VehicleType == Models.VehicleType.Airplane)
                        //    parkedVehicle.ParkingSpot= new List<int> {1,2,3};
                    }

                 


                _context.Add(parkedVehicle);
                await _context.SaveChangesAsync();

                    TempData["success"] = "PARKING WAS SUCCESSFULL!";               //Feedback parkering lyckades!

                    return RedirectToAction(nameof(Index));
                    //TEST 18:44
                    //var list = await _context.ParkedVehicle.ToListAsync();
                    //var newView = new IndexViewModel()
                    //{
                    //    TotalParkingSpots = parkingSpots,
                    //    Vehicles = list

                    //};
                    //return View(newView);

                }

                ModelState.AddModelError(string.Empty, "Your vehicle was not parked, try again!");
            if (_context.ParkedVehicle.Any(e => e.RegistrationNumber == parkedVehicle.RegistrationNumber))  //dubbelkollar regNr...
                ModelState.AddModelError(string.Empty, "Your registration number is already parked!");

            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ParkedVehicle == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }
            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ankomsttid,NumberOfWheels,VehicleType,RegistrationNumber,Color,Brand,ModelName,ParkingSpot")] ParkedVehicle parkedVehicle)
        {
            if (id != parkedVehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(parkedVehicle);
                    await _context.SaveChangesAsync();

                    TempData["success"] = "PARKED VEHICLE UPDATED SUCCESSFULLY!";               //Feedback success! 
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ParkedVehicleExists(parkedVehicle.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(parkedVehicle);
        }

        // GET: ParkedVehicles/Delete/5
        public async Task<IActionResult> Unpark(int? id) 
        {
            if (id == null || _context.ParkedVehicle == null)
            {
                return NotFound();
            }

            var parkedVehicle = await _context.ParkedVehicle          //hämtar från db rätt id...
                .FirstOrDefaultAsync(m => m.Id == id);
            if (parkedVehicle == null)
            {
                return NotFound();
            }

            return View(parkedVehicle);
        }

        // POST: ParkedVehicles/Delete/5
        [HttpPost, ActionName("Unpark")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UnparkConfirmed(int id)
        {
            if (_context.ParkedVehicle == null)
            {
                return Problem("Entity set 'GarageModelViewControllerContext.ParkedVehicle'  is null.");
            }
            var parkedVehicle = await _context.ParkedVehicle.FindAsync(id);
            if (parkedVehicle != null)
            {
                _context.ParkedVehicle.Remove(parkedVehicle);

            }
            
            await _context.SaveChangesAsync();
            TempData["success"] = "UNPARKED VEHICLE SUCCESS!";               //Feedback success! 

            //skapar ett nytt kvitt view model, från den utvalda parkedVehicle 

            //var newModel = new Receipt();
            //newModel.ParkedTime = (TimeSpan)parkedVehicle.ParkedTime;
            //newModel.Ankomsttid = (DateTime)parkedVehicle.Ankomsttid;
            //newModel.Now = DateTime.Now;
            //newModel.RegistrationNummber = parkedVehicle.RegistrationNumber;

            var newModel = new ReceiptViewModel()
            {
                ParkedVehicle = parkedVehicle,
                Now = DateTime.Now,
                ParkedTime = DateTime.Now - parkedVehicle.Ankomsttid.Value
            };
            
            //kan beböha mer data faktiskt!

            return View(nameof(Receipt), newModel); //tvungen att skicka med rätt view modell!!!!  
            //return RedirectToAction(nameof(Index)); 
        }

        private bool ParkedVehicleExists(int id)
        {
          return (_context.ParkedVehicle?.Any(e => e.Id == id)).GetValueOrDefault();
        }



    }
}
