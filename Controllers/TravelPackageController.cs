using Microsoft.AspNetCore.Mvc;
using TH_WEB.Models;
using TH_WEB.Services;

namespace TH_WEB.Controllers
{
    public class TravelPackageController : Controller
    {
        private readonly ITravelPackageService _packageService;

        public TravelPackageController(ITravelPackageService packageService)
        {
            _packageService = packageService;
        }

        // GET: TravelPackage
        public async Task<IActionResult> Index()
        {
            var packages = await _packageService.GetAllAsync();
            return View(packages);
        }

        // GET: TravelPackage/Featured
        public async Task<IActionResult> Featured()
        {
            var packages = await _packageService.GetFeaturedPackagesAsync();
            return View(packages);
        }

        // GET: TravelPackage/Search
        public async Task<IActionResult> Search(string destination, DateTime? startDate, DateTime? endDate, decimal? maxPrice)
        {
            var packages = await _packageService.SearchPackagesAsync(destination, startDate, endDate, maxPrice);
            return View(packages);
        }

        // GET: TravelPackage/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var package = await _packageService.GetByIdAsync(id.Value);
            if (package == null)
            {
                return NotFound();
            }

            return View(package);
        }

        // GET: TravelPackage/Create
        public IActionResult Create()
        {
            return View();
        }        // POST: TravelPackage/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Description,DestinationCode,Country,Region,City,StartDate,EndDate,AdultPrice,ChildPrice,InfantPrice,TotalPrice,MaxAdults,MaxChildren,MaxInfants,IsActive,IsFeatured,Priority,ImageUrl,HotelId,CarRentalId,DiscountPercentage,IncludesHotel,IncludesBreakfast,IncludesMeals,IncludesTransfers,IncludesTours,IncludesInsurance")] TravelPackage package)
        {
            if (ModelState.IsValid)
            {
                await _packageService.CreateAsync(package);
                return RedirectToAction(nameof(Index));
            }
            return View(package);
        }

        // GET: TravelPackage/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var package = await _packageService.GetByIdAsync(id.Value);
            if (package == null)
            {
                return NotFound();
            }
            return View(package);
        }        // POST: TravelPackage/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,DestinationCode,Country,Region,City,StartDate,EndDate,AdultPrice,ChildPrice,InfantPrice,TotalPrice,MaxAdults,MaxChildren,MaxInfants,IsActive,IsFeatured,Priority,ImageUrl,HotelId,CarRentalId,DiscountPercentage,IncludesHotel,IncludesBreakfast,IncludesMeals,IncludesTransfers,IncludesTours,IncludesInsurance")] TravelPackage package)
        {
            if (id != package.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _packageService.UpdateAsync(package);
                }
                catch (Exception)
                {
                    if (!await PackageExists(package.Id))
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
            return View(package);
        }

        // GET: TravelPackage/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var package = await _packageService.GetByIdAsync(id.Value);
            if (package == null)
            {
                return NotFound();
            }

            return View(package);
        }

        // POST: TravelPackage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _packageService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // GET: TravelPackage/Reviews/5
        public async Task<IActionResult> Reviews(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var package = await _packageService.GetByIdAsync(id.Value);
            if (package == null)
            {
                return NotFound();
            }

            return View(package.Reviews);
        }

        // GET: TravelPackage/Itinerary/5
        public async Task<IActionResult> Itinerary(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var package = await _packageService.GetByIdAsync(id.Value);
            if (package == null)
            {
                return NotFound();
            }

            return View(package.Itinerary);
        }

        // GET: TravelPackage/Extras/5
        public async Task<IActionResult> Extras(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var package = await _packageService.GetByIdAsync(id.Value);
            if (package == null)
            {
                return NotFound();
            }

            return View(package.Extras);
        }

        // GET: TravelPackage/FAQs/5
        public async Task<IActionResult> FAQs(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var package = await _packageService.GetByIdAsync(id.Value);
            if (package == null)
            {
                return NotFound();
            }

            return View(package.FAQs);
        }

        private async Task<bool> PackageExists(int id)
        {
            var package = await _packageService.GetByIdAsync(id);
            return package != null;
        }
    }
} 