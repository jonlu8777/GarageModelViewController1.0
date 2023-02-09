using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GarageModelViewController.Data;
using GarageModelViewController.Models;

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

            //Nu kod här nedan, skapa ett kvitto view model, från en parkedVehicle model

            var newModel = new Receipt();
            newModel.ParkedTime = (TimeSpan)parkedVehicle.ParkedTime;
            newModel.Ankomsttid = (DateTime)parkedVehicle.Ankomsttid;
            newModel.Now = DateTime.Now;
            newModel.RegistrationNummber = parkedVehicle.RegistrationNumber;
             
            


            return View(newModel);
        }

        // GET: ParkedVehicles
        public async Task<IActionResult> Index()
        {
              return _context.ParkedVehicle != null ? 
                          View(await _context.ParkedVehicle.ToListAsync()) :
                          Problem("Entity set 'GarageModelViewControllerContext.ParkedVehicle'  is null.");
        }

        public async Task<IActionResult> Filter(string title, int? genre)
        {

            var model = string.IsNullOrWhiteSpace(title) ?
                                    _context.ParkedVehicle :
                                    _context.ParkedVehicle.Where(m => m.RegistrationNumber.StartsWith(title));

            model = genre is null ?
                                model :
                                model.Where(m => (int)m.VehicleType == genre);

            return View(nameof(Index), await model.ToListAsync());
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
        public async Task<IActionResult> Park([Bind("Id,Ankomsttid,NumberOfWheels,VehicleType,RegistrationNumber,Color,Brand,ModelName")] ParkedVehicle parkedVehicle)
        {


            if (!_context.ParkedVehicle.Any(e => e.RegistrationNumber == parkedVehicle.RegistrationNumber))    //Inget registreringnummer ska matcha !! 
            if (ModelState.IsValid)
            {
                _context.Add(parkedVehicle);
                await _context.SaveChangesAsync();

                    TempData["success"] = "PARKING WAS SUCCESSFULL!";               //Feedback parkering lyckades!

                    return RedirectToAction(nameof(Index));
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ankomsttid,NumberOfWheels,VehicleType,RegistrationNumber,Color,Brand,ModelName")] ParkedVehicle parkedVehicle)
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

            var newModel = new Receipt();
            newModel.ParkedTime = (TimeSpan)parkedVehicle.ParkedTime;
            newModel.Ankomsttid = (DateTime)parkedVehicle.Ankomsttid;
            newModel.Now = DateTime.Now;
            newModel.RegistrationNummber = parkedVehicle.RegistrationNumber;

            return View(nameof(Receipt), newModel); //tvungen att skicka med rätt view modell!!!!  
            //return RedirectToAction(nameof(Index)); 
        }

        private bool ParkedVehicleExists(int id)
        {
          return (_context.ParkedVehicle?.Any(e => e.Id == id)).GetValueOrDefault();
        }



    }
}
