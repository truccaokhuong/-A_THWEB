using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TH_WEB.Areas.Attractions.Models;
using TH_WEB.Areas.Attractions.Repositories;
using TH_WEB.Areas.Attractions.ViewModels;

namespace TH_WEB.Areas.Attractions.Controllers
{
    [Area("Attractions")]
    public class AttractionsAdminController : Controller
    {
        private readonly IAttractionsRepository _attractionsRepository;

        public AttractionsAdminController(IAttractionsRepository attractionsRepository)
        {
            _attractionsRepository = attractionsRepository;
        }

        // GET: Attractions/Admin
        public async Task<IActionResult> Index(AttractionsSearchViewModel searchModel)
        {
            try
            {
                var attractions = await _attractionsRepository.SearchAsync(searchModel);
                var totalCount = await _attractionsRepository.GetTotalCountAsync();

                var viewModel = new AttractionsIndexViewModel
                {
                    Attractions = attractions,
                    SearchModel = searchModel,
                    CurrentPage = searchModel.Page,
                    TotalPages = (int)Math.Ceiling(totalCount / (double)searchModel.PageSize),
                    TotalCount = totalCount
                };

                ViewBag.Categories = new SelectList(Enum.GetValues(typeof(AttractionCategory))
                    .Cast<AttractionCategory>()
                    .Select(c => new { Value = c.ToString(), Text = c.ToString() }), "Value", "Text");

                ViewBag.Statuses = new SelectList(Enum.GetValues(typeof(AttractionStatus))
                    .Cast<AttractionStatus>()
                    .Select(s => new { Value = s.ToString(), Text = s.ToString() }), "Value", "Text");

                return View(viewModel);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View(new AttractionsIndexViewModel());
            }
        }

        // GET: Attractions/Admin/Create
        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(Enum.GetValues(typeof(AttractionCategory))
                .Cast<AttractionCategory>()
                .Select(c => new { Value = c.ToString(), Text = c.ToString() }), "Value", "Text");

            ViewBag.Statuses = new SelectList(Enum.GetValues(typeof(AttractionStatus))
                .Cast<AttractionStatus>()
                .Select(s => new { Value = s.ToString(), Text = s.ToString() }), "Value", "Text");

            return View(new CreateAttractionViewModel());
        }

        // POST: Attractions/Admin/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AttractionCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(Enum.GetValues(typeof(AttractionCategory))
                    .Cast<AttractionCategory>()
                    .Select(c => new { Value = c.ToString(), Text = c.ToString() }), "Value", "Text");
                ViewBag.Statuses = new SelectList(Enum.GetValues(typeof(AttractionStatus))
                    .Cast<AttractionStatus>()
                    .Select(s => new { Value = s.ToString(), Text = s.ToString() }), "Value", "Text");
                return View(model);
            }

            try
            {
                var attraction = new Attraction
                {
                    Name = model.Name,
                    Description = model.Description,
                    Location = model.Location,
                    Price = model.Price,
                    Category = model.Category,
                    Status = model.Status,
                    OperatingHours = model.OperatingHours ?? new OperatingHours(),
                    Latitude = model.Latitude,
                    Longitude = model.Longitude,
                    IsFeatured = model.IsFeatured,
                    ImageUrl = model.ImageUrl
                };

                await _attractionsRepository.CreateAsync(attraction);
                TempData["SuccessMessage"] = "Điểm tham quan đã được tạo thành công.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Lỗi khi tạo điểm tham quan: {ex.Message}");
                ViewBag.Categories = new SelectList(Enum.GetValues(typeof(AttractionCategory))
                    .Cast<AttractionCategory>()
                    .Select(c => new { Value = c.ToString(), Text = c.ToString() }), "Value", "Text");
                ViewBag.Statuses = new SelectList(Enum.GetValues(typeof(AttractionStatus))
                    .Cast<AttractionStatus>()
                    .Select(s => new { Value = s.ToString(), Text = s.ToString() }), "Value", "Text");
                return View(model);
            }
        }

        // GET: Attractions/Admin/Edit/5
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var attraction = await _attractionsRepository.GetByIdAsync(id);
            if (attraction == null)
            {
                return NotFound();
            }

            var model = new EditAttractionViewModel
            {
                Id = attraction.Id,
                Name = attraction.Name,
                Description = attraction.Description,
                Location = attraction.Location,
                Price = attraction.Price,
                Category = attraction.Category,
                Status = attraction.Status,
                OperatingHours = attraction.OperatingHours?.ToString(),
                Latitude = attraction.Latitude,
                Longitude = attraction.Longitude,
                IsFeatured = attraction.IsFeatured,
                ImageUrl = attraction.ImageUrl,
                CurrentImageUrl = attraction.ImageUrl
            };

            ViewBag.Categories = new SelectList(Enum.GetValues(typeof(AttractionCategory))
                .Cast<AttractionCategory>()
                .Select(c => new { Value = c.ToString(), Text = c.ToString() }), "Value", "Text");
            ViewBag.Statuses = new SelectList(Enum.GetValues(typeof(AttractionStatus))
                .Cast<AttractionStatus>()
                .Select(s => new { Value = s.ToString(), Text = s.ToString() }), "Value", "Text");

            return View(model);
        }

        // POST: Attractions/Admin/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditAttractionViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = new SelectList(Enum.GetValues(typeof(AttractionCategory))
                    .Cast<AttractionCategory>()
                    .Select(c => new { Value = c.ToString(), Text = c.ToString() }), "Value", "Text");
                ViewBag.Statuses = new SelectList(Enum.GetValues(typeof(AttractionStatus))
                    .Cast<AttractionStatus>()
                    .Select(s => new { Value = s.ToString(), Text = s.ToString() }), "Value", "Text");
                return View(model);
            }

            try
            {
                var attraction = await _attractionsRepository.GetByIdAsync(model.Id);
                if (attraction == null)
                {
                    return NotFound();
                }

                attraction.Name = model.Name;
                attraction.Description = model.Description;
                attraction.Location = model.Location;
                attraction.Price = model.Price;
                attraction.Category = model.Category;
                attraction.Status = model.Status;
                attraction.OperatingHours = model.OperatingHours != null ? 
                    OperatingHours.Parse(model.OperatingHours) : attraction.OperatingHours;
                attraction.Latitude = model.Latitude;
                attraction.Longitude = model.Longitude;
                attraction.IsFeatured = model.IsFeatured;
                attraction.ImageUrl = model.ImageUrl;

                await _attractionsRepository.UpdateAsync(attraction);
                TempData["SuccessMessage"] = "Điểm tham quan đã được cập nhật thành công.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Lỗi khi cập nhật điểm tham quan: {ex.Message}");
                ViewBag.Categories = new SelectList(Enum.GetValues(typeof(AttractionCategory))
                    .Cast<AttractionCategory>()
                    .Select(c => new { Value = c.ToString(), Text = c.ToString() }), "Value", "Text");
                ViewBag.Statuses = new SelectList(Enum.GetValues(typeof(AttractionStatus))
                    .Cast<AttractionStatus>()
                    .Select(s => new { Value = s.ToString(), Text = s.ToString() }), "Value", "Text");
                return View(model);
            }
        }

        // POST: Attractions/Admin/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _attractionsRepository.DeleteAsync(id);
                if (result)
                {
                    TempData["Success"] = "Attraction deleted successfully";
                }
                else
                {
                    TempData["Error"] = "Attraction not found";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Attractions/Admin/BatchUpdateStatus
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BatchUpdateStatus(BatchUpdateStatusViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Error"] = "Invalid model state";
                    return RedirectToAction(nameof(Index));
                }

                var result = await _attractionsRepository.UpdateStatusBatchAsync(model.Ids, model.Status);
                if (result)
                {
                    TempData["Success"] = "Status updated successfully";
                }
                else
                {
                    TempData["Error"] = "Failed to update status";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: Attractions/Admin/BatchDelete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BatchDelete(BatchDeleteViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["Error"] = "Invalid model state";
                    return RedirectToAction(nameof(Index));
                }

                var result = await _attractionsRepository.DeleteBatchAsync(model.Ids);
                if (result)
                {
                    TempData["Success"] = "Attractions deleted successfully";
                }
                else
                {
                    TempData["Error"] = "Failed to delete attractions";
                }
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Attractions/Admin/Statistics
        public async Task<IActionResult> Statistics()
        {
            try
            {
                var totalCount = await _attractionsRepository.GetTotalCountAsync();
                var averagePrice = await _attractionsRepository.GetAveragePriceAsync();

                var categoryStats = new List<CategoryStat>();
                foreach (AttractionCategory category in Enum.GetValues(typeof(AttractionCategory)))
                {
                    var count = await _attractionsRepository.GetCountByCategoryAsync(category);
                    categoryStats.Add(new CategoryStat { Category = category.ToString(), Count = count });
                }

                var statusStats = new List<StatusStat>();
                foreach (AttractionStatus status in Enum.GetValues(typeof(AttractionStatus)))
                {
                    var count = await _attractionsRepository.GetCountByStatusAsync(status);
                    statusStats.Add(new StatusStat { Status = status.ToString(), Count = count });
                }

                var viewModel = new AttractionsStatisticsViewModel
                {
                    TotalCount = totalCount,
                    AveragePrice = averagePrice,
                    CategoryStats = categoryStats,
                    StatusStats = statusStats
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
} 