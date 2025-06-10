using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TH_WEB.Areas.Attractions.Models;
using TH_WEB.Areas.Attractions.Repositories;
using TH_WEB.Attributes;
using System.Security.Claims;

namespace TH_WEB.Areas.Attractions.Controllers
{
    [Area("Attractions")]
    public class AttractionsController : Controller
    {
        private readonly IAttractionsRepository _attractionsRepository;

        public AttractionsController(IAttractionsRepository attractionsRepository)
        {
            _attractionsRepository = attractionsRepository;
        }        public async Task<IActionResult> Index()
        {
            var attractions = await _attractionsRepository.GetAllAsync();
            return View(attractions);
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var attraction = await _attractionsRepository.GetByIdAsync(id);
                return View(attraction);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }        }        [Authorize]
        //[RequirePermission("attraction.create", "attraction.manage")]
        public IActionResult Create()
        {
            return View();
        }        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        //[RequirePermission("attraction.create", "attraction.manage")]
        public async Task<IActionResult> Create(Attraction attraction)
        {
            try
            {                
                // Set the current user as owner and creator
                var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                attraction.OwnerId = currentUserId ?? "system-user";
                attraction.CreatedById = currentUserId ?? "system-user";
                attraction.CreatedAt = DateTime.Now;
                attraction.LastUpdated = DateTime.Now;
                attraction.Status = AttractionStatus.Active; // Set default status
                
                // Initialize required complex objects
                attraction.Tags = new List<AttractionTag>();
                attraction.Images = new List<AttractionImage>();
                attraction.Features = new List<AttractionFeature>();
                attraction.Reviews = new List<Review>();
                
                // Create default OperatingHours
                attraction.OperatingHours = new OperatingHours
                {
                    Monday = "9:00 AM - 5:00 PM",
                    Tuesday = "9:00 AM - 5:00 PM",
                    Wednesday = "9:00 AM - 5:00 PM",
                    Thursday = "9:00 AM - 5:00 PM",
                    Friday = "9:00 AM - 5:00 PM",
                    Saturday = "9:00 AM - 5:00 PM",
                    Sunday = "9:00 AM - 5:00 PM"
                };
                
                // Create default Pricing
                attraction.Pricing = new Pricing
                {
                    AdultPrice = attraction.Price,
                    ChildPrice = attraction.Price * 0.5m,
                    SeniorPrice = attraction.Price * 0.8m,
                    StudentPrice = attraction.Price * 0.7m,
                    IsFree = attraction.Price == 0,
                    Currency = "USD"
                };
                
                // Set default values for required fields that might be missing
                if (string.IsNullOrEmpty(attraction.Phone)) attraction.Phone = "Not provided";
                if (string.IsNullOrEmpty(attraction.Email)) attraction.Email = "not-provided@example.com";
                if (string.IsNullOrEmpty(attraction.Website)) attraction.Website = "https://example.com";
                if (string.IsNullOrEmpty(attraction.ZipCode)) attraction.ZipCode = "00000";
                
                // Clear validation errors for fields we're setting programmatically
                ModelState.Remove(nameof(attraction.OwnerId));
                ModelState.Remove(nameof(attraction.CreatedById));
                ModelState.Remove(nameof(attraction.OperatingHours));
                ModelState.Remove(nameof(attraction.Pricing));
                
                // Validate essential fields that must come from user input
                if (string.IsNullOrEmpty(attraction.Name))
                {
                    ModelState.AddModelError(nameof(attraction.Name), "Attraction name is required.");
                }
                if (string.IsNullOrEmpty(attraction.Description))
                {
                    ModelState.AddModelError(nameof(attraction.Description), "Description is required.");
                }
                if (string.IsNullOrEmpty(attraction.Address))
                {
                    ModelState.AddModelError(nameof(attraction.Address), "Address is required.");
                }
                if (string.IsNullOrEmpty(attraction.City))
                {
                    ModelState.AddModelError(nameof(attraction.City), "City is required.");
                }
                if (string.IsNullOrEmpty(attraction.State))
                {
                    ModelState.AddModelError(nameof(attraction.State), "State is required.");
                }
                if (string.IsNullOrEmpty(attraction.Country))
                {
                    ModelState.AddModelError(nameof(attraction.Country), "Country is required.");
                }
                
                if (ModelState.IsValid)
                {
                    var result = await _attractionsRepository.CreateAsync(attraction);
                    
                    // Redirect with success message
                    TempData["SuccessMessage"] = $"Attraction '{attraction.Name}' has been created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Debug: Show validation errors
                    var errors = ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .Select(x => new { Field = x.Key, Errors = x.Value.Errors.Select(e => e.ErrorMessage) })
                        .ToList();
                    
                    ViewBag.ValidationErrors = errors;
                }
            }
            catch (Exception ex)
            {
                // Log the error and show user-friendly message
                TempData["ErrorMessage"] = $"Error creating attraction: {ex.Message}";
            }
            
            return View(attraction);
        }        [Authorize]
        //[RequirePermission("attraction.edit", "attraction.manage")]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var attraction = await _attractionsRepository.GetByIdAsync(id);
                return View(attraction);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        //[RequirePermission("attraction.edit", "attraction.manage")]
        public async Task<IActionResult> Edit(int id, Attraction attraction)
        {
            if (id != attraction.Id)
            {
                return NotFound();
            }

            try
            {
                // Update the LastUpdated timestamp
                attraction.LastUpdated = DateTime.Now;
                
                // Set default values for required fields that might be missing
                if (string.IsNullOrEmpty(attraction.Phone)) attraction.Phone = "Not provided";
                if (string.IsNullOrEmpty(attraction.Email)) attraction.Email = "not-provided@example.com";
                if (string.IsNullOrEmpty(attraction.Website)) attraction.Website = "https://example.com";
                if (string.IsNullOrEmpty(attraction.ZipCode)) attraction.ZipCode = "00000";
                
                // Clear validation errors for fields we're setting programmatically
                ModelState.Remove(nameof(attraction.LastUpdated));
                
                // Validate essential fields that must come from user input
                if (string.IsNullOrEmpty(attraction.Name))
                {
                    ModelState.AddModelError(nameof(attraction.Name), "Attraction name is required.");
                }
                if (string.IsNullOrEmpty(attraction.Description))
                {
                    ModelState.AddModelError(nameof(attraction.Description), "Description is required.");
                }
                if (string.IsNullOrEmpty(attraction.Address))
                {
                    ModelState.AddModelError(nameof(attraction.Address), "Address is required.");
                }
                if (string.IsNullOrEmpty(attraction.City))
                {
                    ModelState.AddModelError(nameof(attraction.City), "City is required.");
                }
                if (string.IsNullOrEmpty(attraction.State))
                {
                    ModelState.AddModelError(nameof(attraction.State), "State is required.");
                }
                if (string.IsNullOrEmpty(attraction.Country))
                {
                    ModelState.AddModelError(nameof(attraction.Country), "Country is required.");
                }

                if (ModelState.IsValid)
                {
                    await _attractionsRepository.UpdateAsync(attraction);
                    TempData["SuccessMessage"] = $"Attraction '{attraction.Name}' has been updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Debug: Show validation errors
                    var errors = ModelState
                        .Where(x => x.Value.Errors.Count > 0)
                        .Select(x => new { Field = x.Key, Errors = x.Value.Errors.Select(e => e.ErrorMessage) })
                        .ToList();
                    
                    ViewBag.ValidationErrors = errors;
                }
            }
            catch (Exception ex)
            {
                // Log the error and show user-friendly message
                TempData["ErrorMessage"] = $"Error updating attraction: {ex.Message}";
            }
            
            return View(attraction);
        }[Authorize]
        //[RequirePermission("attraction.delete", "attraction.manage")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var attraction = await _attractionsRepository.GetByIdAsync(id);
                return View(attraction);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        //[RequirePermission("attraction.delete", "attraction.manage")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // DEBUG: Log khi action được gọi
            Console.WriteLine($"🔴 DEBUG: DeleteConfirmed called with ID: {id}");
            
            try
            {
                Console.WriteLine($"🔴 DEBUG: Attempting to find attraction with ID: {id}");
                var attraction = await _attractionsRepository.GetByIdAsync(id);
                Console.WriteLine($"🔴 DEBUG: Found attraction: {attraction.Name}");
                
                Console.WriteLine($"🔴 DEBUG: Attempting to delete attraction with ID: {id}");
                await _attractionsRepository.DeleteAsync(id);
                Console.WriteLine($"🔴 DEBUG: Successfully deleted attraction");
                
                TempData["SuccessMessage"] = $"Attraction '{attraction.Name}' has been deleted successfully!";
                Console.WriteLine($"🔴 DEBUG: Set success message, redirecting to Index");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine($"🔴 DEBUG: Attraction not found: {ex.Message}");
                TempData["ErrorMessage"] = "Attraction not found.";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"🔴 DEBUG: Error deleting attraction: {ex.Message}");
                TempData["ErrorMessage"] = $"Error deleting attraction: {ex.Message}";
            }
            
            Console.WriteLine($"🔴 DEBUG: Returning redirect to Index");
            return RedirectToAction(nameof(Index));
        }
    }
}
