using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TH_WEB.Areas.Attractions.Models;
using TH_WEB.Areas.Attractions.Repositories;
using TH_WEB.Areas.Attractions.ViewModels;

namespace TH_WEB.Areas.Attractions.Controllers
{
    [ApiController]
    [Route("api/attractions")]
    public class AttractionsApiController : ControllerBase
    {
        private readonly IAttractionsRepository _attractionsRepository;

        public AttractionsApiController(
            IAttractionsRepository attractionsRepository)
        {
            _attractionsRepository = attractionsRepository;
        }

        // GET: api/attractions
        [HttpGet]
        public async Task<IActionResult> GetAttractions([FromQuery] AttractionsSearchViewModel searchModel = null)
        {
            try
            {
                IEnumerable<Attraction> attractions;

                if (searchModel != null && !string.IsNullOrEmpty(searchModel.Keyword))
                {
                    attractions = await _attractionsRepository.SearchAsync(searchModel);
                }
                else
                {
                    attractions = await _attractionsRepository.GetAllAsync();
                }

                var result = attractions.Select(a => new
                {
                    a.Id,
                    a.Name,
                    a.Description,
                    a.Location,
                    a.Price,
                    a.Rating,
                    a.Category,
                    a.Status,
                    a.ImageUrl,
                    a.OperatingHours,
                    a.IsFeatured,
                    a.CreatedAt
                });

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        // GET: api/attractions/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetAttraction(int id)
        {
            try
            {
                var attraction = await _attractionsRepository.GetByIdAsync(id);
                if (attraction == null)
                {
                    return NotFound(new { success = false, message = "Attraction not found" });
                }

                var result = new
                {
                    attraction.Id,
                    attraction.Name,
                    attraction.Description,
                    attraction.Location,
                    attraction.Price,
                    attraction.Rating,
                    attraction.Category,
                    attraction.Status,
                    attraction.ImageUrl,
                    attraction.OperatingHours,
                    attraction.IsFeatured,
                    attraction.Latitude,
                    attraction.Longitude,
                    attraction.ReviewCount,
                    attraction.ViewCount,
                    attraction.CreatedAt,
                    attraction.UpdatedAt
                };

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        // GET: api/attractions/popular
        [HttpGet("popular")]
        public async Task<IActionResult> GetPopularAttractions(int count = 10)
        {
            try
            {
                var attractions = await _attractionsRepository.GetPopularAsync(count);
                var result = attractions.Select(a => new
                {
                    a.Id,
                    a.Name,
                    a.Description,
                    a.Location,
                    a.Price,
                    a.Rating,
                    a.ImageUrl,
                    a.ViewCount
                });

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        // GET: api/attractions/featured
        [HttpGet("featured")]
        public async Task<IActionResult> GetFeaturedAttractions(int count = 5)
        {
            try
            {
                var attractions = await _attractionsRepository.GetFeaturedAsync(count);
                var result = attractions.Select(a => new
                {
                    a.Id,
                    a.Name,
                    a.Description,
                    a.Location,
                    a.Price,
                    a.Rating,
                    a.ImageUrl
                });

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        // GET: api/attractions/search
        [HttpGet("search")]
        public async Task<IActionResult> SearchAttractions([FromQuery] AttractionsSearchViewModel searchModel)
        {
            try
            {
                if (searchModel == null)
                {
                    return BadRequest(new { success = false, message = "Search parameters are required" });
                }

                var attractions = await _attractionsRepository.SearchAsync(searchModel);
                var result = attractions.Select(a => new
                {
                    a.Id,
                    a.Name,
                    a.Description,
                    a.Location,
                    a.Price,
                    a.Rating,
                    a.Category,
                    a.Status,
                    a.ImageUrl,
                    a.OperatingHours
                });

                return Ok(new { success = true, data = result, total = result.Count() });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        // GET: api/attractions/nearby
        [HttpGet("nearby")]
        public async Task<IActionResult> GetNearbyAttractions(double lat, double lng, double radius = 10)
        {
            try
            {
                var attractions = await _attractionsRepository.GetNearbyAsync(lat, lng, radius);
                var result = attractions.Select(a => new
                {
                    a.Id,
                    a.Name,
                    a.Description,
                    a.Location,
                    a.Price,
                    a.Rating,
                    a.ImageUrl,
                    a.Latitude,
                    a.Longitude,
                    Distance = CalculateDistance(lat, lng, a.Latitude ?? 0, a.Longitude ?? 0)
                });

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        // GET: api/attractions/categories
        [HttpGet("categories")]
        public IActionResult GetCategories()
        {
            var categories = Enum.GetValues(typeof(AttractionCategory))
                .Cast<AttractionCategory>()
                .Select(c => new { Value = c.ToString(), Text = c.ToString() });

            return Ok(new { success = true, data = categories });
        }

        // GET: api/attractions/statuses
        [HttpGet("statuses")]
        public IActionResult GetStatuses()
        {
            var statuses = Enum.GetValues(typeof(AttractionStatus))
                .Cast<AttractionStatus>()
                .Select(s => new { Value = s.ToString(), Text = s.ToString() });

            return Ok(new { success = true, data = statuses });
        }

        // GET: api/attractions/locations
        [HttpGet("locations")]
        public async Task<IActionResult> GetLocations()
        {
            try
            {
                var locations = await _attractionsRepository.GetLocationsAsync();
                return Ok(new { success = true, data = locations });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        // GET: api/attractions/statistics
        [HttpGet("statistics")]
        public async Task<IActionResult> GetStatistics()
        {
            try
            {
                var totalCount = await _attractionsRepository.GetTotalCountAsync();
                var averagePrice = await _attractionsRepository.GetAveragePriceAsync();
                
                var categoryStats = new List<object>();
                foreach (AttractionCategory category in Enum.GetValues(typeof(AttractionCategory)))
                {
                    var count = await _attractionsRepository.GetCountByCategoryAsync(category);
                    categoryStats.Add(new
                    {
                        Category = GetCategoryName(category),
                        Count = count
                    });
                }

                var statusStats = new List<object>();
                foreach (AttractionStatus status in Enum.GetValues(typeof(AttractionStatus)))
                {
                    var count = await _attractionsRepository.GetCountByStatusAsync(status);
                    statusStats.Add(new
                    {
                        Status = GetStatusName(status),
                        Count = count
                    });
                }

                var result = new
                {
                    TotalCount = totalCount,
                    AveragePrice = averagePrice,
                    CategoryStats = categoryStats,
                    StatusStats = statusStats
                };

                return Ok(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        // POST: api/attractions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AttractionCreateViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
                }

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

                var result = await _attractionsRepository.CreateAsync(attraction);
                return Ok(new { success = true, data = result, message = "Attraction created successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        // PUT: api/attractions/5
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateAttraction(int id, [FromBody] UpdateAttractionViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
                }

                var attraction = await _attractionsRepository.GetByIdAsync(id);
                if (attraction == null)
                {
                    return NotFound(new { success = false, message = "Attraction not found" });
                }

                attraction.Name = model.Name;
                attraction.Description = model.Description;
                attraction.Location = model.Location;
                attraction.Price = model.Price;
                attraction.Category = model.Category;
                attraction.Status = model.Status;
                attraction.OperatingHours = model.OperatingHours ?? attraction.OperatingHours;
                attraction.Latitude = model.Latitude;
                attraction.Longitude = model.Longitude;
                attraction.IsFeatured = model.IsFeatured;
                attraction.ImageUrl = model.ImageUrl;

                var result = await _attractionsRepository.UpdateAsync(attraction);
                return Ok(new { success = true, data = result, message = "Attraction updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        // DELETE: api/attractions/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAttraction(int id)
        {
            try
            {
                var exists = await _attractionsRepository.ExistsAsync(id);
                if (!exists)
                {
                    return NotFound(new { success = false, message = "Attraction not found" });
                }

                var result = await _attractionsRepository.DeleteAsync(id);
                return Ok(new { success = true, message = "Attraction deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        // POST: api/attractions/batch-update-status
        [HttpPost("batch-update-status")]
        public async Task<IActionResult> BatchUpdateStatus([FromBody] BatchUpdateStatusViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
                }

                var result = await _attractionsRepository.UpdateStatusBatchAsync(model.Ids, model.Status);
                return Ok(new { success = true, message = "Status updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        // DELETE: api/attractions/batch-delete
        [HttpDelete("batch-delete")]
        public async Task<IActionResult> BatchDelete([FromBody] BatchDeleteViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new { success = false, errors = ModelState.Values.SelectMany(v => v.Errors) });
                }

                var result = await _attractionsRepository.DeleteBatchAsync(model.Ids);
                return Ok(new { success = true, message = "Attractions deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        // GET: api/attractions/location
        [HttpGet("location")]
        public async Task<ActionResult<IEnumerable<Attraction>>> GetAttractionsByLocationAsync(
            [FromQuery] double latitude,
            [FromQuery] double longitude,
            [FromQuery] double radiusInKm = 10)
        {
            try
            {
                var attractions = await _attractionsRepository.GetAttractionsByLocationAsync(latitude, longitude, radiusInKm);
                return Ok(new { success = true, data = attractions });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        // GET: api/attractions/price-range
        [HttpGet("price-range")]
        public async Task<ActionResult<IEnumerable<Attraction>>> GetAttractionsByPriceRangeAsync(
            [FromQuery] decimal minPrice,
            [FromQuery] decimal maxPrice)
        {
            try
            {
                var attractions = await _attractionsRepository.GetAttractionsByPriceRangeAsync(minPrice, maxPrice);
                return Ok(new { success = true, data = attractions });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        #region Helper Methods

        private static double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371; // Earth's radius in kilometers
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) *
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            return R * c;
        }

        private static double ToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }

        private static string GetCategoryName(AttractionCategory category)
        {
            return category switch
            {
                AttractionCategory.Nature => "Thiên nhiên",
                AttractionCategory.Culture => "Văn hóa",
                AttractionCategory.Adventure => "Phiêu lưu",
                AttractionCategory.Entertainment => "Giải trí",
                AttractionCategory.History => "Lịch sử",
                AttractionCategory.Sports => "Thể thao",
                AttractionCategory.Food => "Ẩm thực",
                AttractionCategory.Shopping => "Mua sắm",
                AttractionCategory.Nightlife => "Cuộc sống về đêm",
                _ => "Khác"
            };
        }

        private static string GetStatusName(AttractionStatus status)
        {
            return status switch
            {
                AttractionStatus.Active => "Hoạt động",
                AttractionStatus.Inactive => "Tạm dừng",
                AttractionStatus.Maintenance => "Bảo trì",
                AttractionStatus.Closed => "Đóng cửa",
                _ => "Không xác định"
            };
        }

        #endregion
    }
} 