using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TH_WEB.Models;
using TH_WEB.Services;
using TH_WEB.Attributes;

namespace TH_WEB.Controllers
{
    public class CarRentalController : Controller
    {
        private readonly ICarRentalService _carRentalService;

        public CarRentalController(ICarRentalService carRentalService)
        {
            _carRentalService = carRentalService;
        }

        // GET: CarRental
        public async Task<IActionResult> Index()
        {
            var carRentals = await _carRentalService.GetAllAsync();
            return View(carRentals);
        }

        // GET: CarRental/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carRental = await _carRentalService.GetByIdAsync(id.Value);
            if (carRental == null)
            {
                return NotFound();
            }

            return View(carRental);
        }        // GET: CarRental/Create
        [Authorize]
        [RequirePermission("carrental.create", "carrental.manage")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: CarRental/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        [RequirePermission("carrental.create", "carrental.manage")]
        public async Task<IActionResult> Create([Bind("CompanyName,CompanyLogo,PickupLocationId,DropoffLocationId,CarModel,CarBrand,CarYear,CarImage,Category,TransmissionType,FuelType,Seats,Doors,LargeBags,SmallBags,PricePerDay,PricePerWeek,PricePerMonth,SecurityDeposit,InsuranceFee,HasAirConditioning,HasGPS,HasBluetooth,HasWifi,HasChildSeat,HasUSBCharger,MileageLimit,MinimumAge,MinimumDrivingExperience,Description,TermsAndConditions,IsAvailable,IsActive")] CarRental carRental)
        {
            if (ModelState.IsValid)
            {
                await _carRentalService.CreateAsync(carRental);
                return RedirectToAction(nameof(Index));
            }
            return View(carRental);
        }        // GET: CarRental/Edit/5
        [Authorize]
        [RequirePermission("carrental.edit", "carrental.manage")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carRental = await _carRentalService.GetByIdAsync(id.Value);
            if (carRental == null)
            {
                return NotFound();
            }
            return View(carRental);
        }

        // POST: CarRental/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        [RequirePermission("carrental.edit", "carrental.manage")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,CompanyName,CompanyLogo,PickupLocationId,DropoffLocationId,CarModel,CarBrand,CarYear,CarImage,Category,TransmissionType,FuelType,Seats,Doors,LargeBags,SmallBags,PricePerDay,PricePerWeek,PricePerMonth,SecurityDeposit,InsuranceFee,HasAirConditioning,HasGPS,HasBluetooth,HasWifi,HasChildSeat,HasUSBCharger,MileageLimit,MinimumAge,MinimumDrivingExperience,Description,TermsAndConditions,IsAvailable,IsActive")] CarRental carRental)
        {
            if (id != carRental.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _carRentalService.UpdateAsync(carRental);
                }
                catch (Exception)
                {
                    if (!await CarRentalExists(carRental.Id))
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
            return View(carRental);
        }        // GET: CarRental/Delete/5
        [Authorize]
        [RequirePermission("carrental.delete", "carrental.manage")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var carRental = await _carRentalService.GetByIdAsync(id.Value);
            if (carRental == null)
            {
                return NotFound();
            }

            return View(carRental);
        }

        // POST: CarRental/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        [RequirePermission("carrental.delete", "carrental.manage")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _carRentalService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CarRentalExists(int id)
        {
            var carRental = await _carRentalService.GetByIdAsync(id);
            return carRental != null;
        }
    }
} 