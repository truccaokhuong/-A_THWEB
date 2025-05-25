using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using CsvHelper;
using System.Globalization;
using TH_WEB.Models;

namespace TH_WEB.Data
{
    public static class HotelDataImporter
    {
        // Phương thức chính để thêm dữ liệu khách sạn vào DbInitializer
        public static async Task ImportHotelsToDatabase(ApplicationDbContext context, int count = 50)
        {
            // Kiểm tra nếu đã có dữ liệu
            if (context.Hotels.Any())
            {
                Console.WriteLine("Database đã có dữ liệu khách sạn. Bỏ qua import.");
                return;
            }

            Console.WriteLine($"Bắt đầu import {count} khách sạn...");

            // Lựa chọn 1: Import từ file JSON có sẵn
            var hotels = ImportFromJson("Data/SeedData/hotels.json");
            
            // Lựa chọn 2: Import từ file CSV có sẵn
            // var hotels = ImportFromCsv("Data/SeedData/hotels.csv");
            
            // Lựa chọn 3: Import từ API (cần thêm API key)
            // var hotels = await ImportFromApiAsync(count);

            // Thêm vào database
            if (hotels != null && hotels.Any())
            {
                foreach (var hotel in hotels)
                {
                    // Thêm thông tin chi tiết cho khách sạn
                    AddHotelRooms(hotel);
                    AddHotelAmenities(hotel);
                    AddHotelReviews(hotel);
                    
                    context.Hotels.Add(hotel);
                }
                
                await context.SaveChangesAsync();
                Console.WriteLine($"Đã import thành công {hotels.Count} khách sạn vào database.");
            }
            else
            {
                Console.WriteLine("Không có dữ liệu khách sạn để import.");
            }
        }

        // Import từ file JSON có sẵn
        private static List<Hotel> ImportFromJson(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    var json = File.ReadAllText(filePath);
                    return JsonConvert.DeserializeObject<List<Hotel>>(json);
                }
                else
                {
                    Console.WriteLine($"Không tìm thấy file {filePath}");
                    // Tạo mẫu dữ liệu khách sạn
                    return GenerateSampleHotels(30);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi import từ JSON: {ex.Message}");
                return new List<Hotel>();
            }
        }

        // Import từ file CSV có sẵn
        private static List<Hotel> ImportFromCsv(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    using var reader = new StreamReader(filePath);
                    using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                    return csv.GetRecords<Hotel>().ToList();
                }
                else
                {
                    Console.WriteLine($"Không tìm thấy file {filePath}");
                    return new List<Hotel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi import từ CSV: {ex.Message}");
                return new List<Hotel>();
            }
        }

        // Import từ API bên ngoài
        private static async Task<List<Hotel>> ImportFromApiAsync(int count)
        {
            try
            {
                // Đây là ví dụ sử dụng RapidAPI (cần đăng ký và lấy API key)
                // Thay URL và API key của bạn vào đây
                using var client = new HttpClient();
                client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "2825c12f27msh471a096b7960553p1632aejsn735325d87c65");
                client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "booking-com.p.rapidapi.com");

                var response = await client.GetAsync("https://booking-com.p.rapidapi.com/v1/hotels/search?country=vn&locale=vi&order_by=popularity&adults_number=2&units=metric&room_number=1&checkout_date=2024-06-20&checkin_date=2024-06-15&filter_by_currency=VND&dest_type=country&dest_id=187&children_number=0&page_number=0&include_adjacency=true");
                
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    // Chuyển đổi từ dữ liệu API thành đối tượng Hotel
                    // Mã chuyển đổi sẽ phụ thuộc vào cấu trúc dữ liệu của API
                    var hotelList = new List<Hotel>();
                    // Xử lý dữ liệu từ API ở đây...
                    
                    return hotelList;
                }
                else
                {
                    Console.WriteLine($"Lỗi API: {response.StatusCode}");
                    return new List<Hotel>();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi import từ API: {ex.Message}");
                return new List<Hotel>();
            }
        }

        // Tạo dữ liệu mẫu trong trường hợp không có nguồn dữ liệu
        private static List<Hotel> GenerateSampleHotels(int count)
        {
            var hotels = new List<Hotel>();
            var cities = new[] { "Hà Nội", "Hồ Chí Minh", "Đà Nẵng", "Nha Trang", "Phú Quốc", "Đà Lạt", "Hội An", "Huế", "Hạ Long", "Sapa" };
            var streetNames = new[] { "Nguyễn Huệ", "Lê Lợi", "Trần Phú", "Lý Thái Tổ", "Phan Đình Phùng", "Bà Triệu", "Đinh Tiên Hoàng", "Trần Hưng Đạo", "Lê Duẩn", "Nguyễn Du" };
            var hotelPrefixes = new[] { "Grand", "Royal", "Luxury", "Premium", "Elite", "Golden", "Imperial", "Majestic", "Splendid", "Palace" };
            var hotelSuffixes = new[] { "Hotel", "Resort", "Suites", "Inn", "Plaza", "Residence", "Boutique", "Lodge", "Retreat", "View" };
            
            Random random = new Random();
            
            for (int i = 0; i < count; i++)
            {
                var city = cities[random.Next(cities.Length)];
                var street = streetNames[random.Next(streetNames.Length)];
                var hotelName = $"{hotelPrefixes[random.Next(hotelPrefixes.Length)]} {hotelSuffixes[random.Next(hotelSuffixes.Length)]}";
                var rating = Math.Round(3.0 + random.NextDouble() * 2.0, 1); // Rating từ 3.0 đến 5.0
                
                hotels.Add(new Hotel
                {
                    Name = hotelName,
                    Description = $"Khách sạn {rating} sao tọa lạc tại trung tâm thành phố {city}, mang đến trải nghiệm lưu trú tuyệt vời với đầy đủ tiện nghi hiện đại.",
                    Address = $"{random.Next(1, 100)} {street}",
                    City = city,
                    Country = "Việt Nam",
                    Rating = (decimal)rating,
                    ImageUrl = $"/images/hotels/{hotelName.ToLower().Replace(" ", "-")}.jpg",
                    HasFreeWifi = true,
                    HasParking = random.Next(2) == 1,
                    HasSwimmingPool = random.Next(2) == 1,
                    HasBreakfast = random.Next(2) == 1,
                    HasRoomService = random.Next(2) == 1,
                    HasFitnessCenter = rating > 4.0 ? random.Next(2) == 1 : false,
                    HasSpa = rating > 4.0 ? random.Next(2) == 1 : false,
                    PricePerNight = (decimal)(random.NextDouble() * 1000),
                });
            }
            
            return hotels;
        }

        // Thêm các phòng cho khách sạn
        private static void AddHotelRooms(Hotel hotel)
        {
            Random random = new Random();
            int roomCount = random.Next(5, 16); // Từ 5-15 phòng
            
            var roomTypes = new[]
            {
                new { Type = "Standard", PriceFactor = 1.0m },
                new { Type = "Superior", PriceFactor = 1.3m },
                new { Type = "Deluxe", PriceFactor = 1.6m },
                new { Type = "Suite", PriceFactor = 2.2m },
                new { Type = "Family", PriceFactor = 1.8m }
            };
            
            hotel.Rooms = new List<Room>();
            
            foreach (var roomType in roomTypes)
            {
                int typeCount = random.Next(1, 4); // 1-3 phòng cho mỗi loại
                
                for (int i = 0; i < typeCount; i++)
                {
                    hotel.Rooms.Add(new Room
                    {
                        RoomNumber = $"{roomType.Type[0]}{100 + i}",
                        RoomType = roomType.Type,
                        Price = hotel.PricePerNight * roomType.PriceFactor,
                        MaxOccupancy = roomType.Type == "Family" ? 4 : (roomType.Type == "Suite" ? 3 : 2),
                        IsAvailable = true,
                        Description = $"Phòng {roomType.Type} tiện nghi với đầy đủ tiện nghi hiện đại",
                        BedType = roomType.Type == "Family" ? "2 giường đôi" : (roomType.Type == "Suite" ? "1 giường King" : "1 giường Queen"),
                        HasMinibar = roomType.Type != "Standard",
                        HasBalcony = random.Next(2) == 1,
                        SquareMeters = (int)(roomType.Type == "Standard" ? 25 : (roomType.Type == "Superior" ? 30 : (roomType.Type == "Deluxe" ? 35 : (roomType.Type == "Suite" ? 45 : 40))))
                    });
                }
            }
        }

        // Thêm các tiện ích cho khách sạn
        private static void AddHotelAmenities(Hotel hotel)
        {
            // Giả định rằng bạn có bảng Amenities trong models
            // Tiện ích có thể được thêm ở đây
        }

        // Thêm các đánh giá cho khách sạn
        private static void AddHotelReviews(Hotel hotel)
        {
            Random random = new Random();
            int reviewCount = random.Next(5, 16); // 5-15 đánh giá
            
            var reviewerNames = new[] { "Minh Anh", "Ngọc Linh", "Văn Hùng", "Thị Mai", "Đức Thắng", "Thu Hà", "Quốc Tuấn", "Thanh Thảo", "Nam Phong", "Phương Anh" };
            var commentTemplates = new[]
            {
                "Khách sạn {0} sao, dịch vụ tuyệt vời!",
                "Vị trí thuận tiện, phòng {1} rất sạch sẽ và thoáng mát.",
                "Nhân viên phục vụ chuyên nghiệp. Sẽ quay lại lần sau.",
                "Giá cả hợp lý cho một khách sạn {0} sao tại {2}.",
                "Tiện nghi đầy đủ, đặc biệt là {3} rất tốt.",
                "Đồ ăn ngon, vị trí đẹp, nhưng hơi ồn vào buổi tối.",
                "Phòng hơi nhỏ nhưng đầy đủ tiện nghi, giá tốt.",
                "Khách sạn đẹp, sạch sẽ, nhân viên thân thiện."
            };
            
            hotel.Reviews = new List<Review>();
            
            for (int i = 0; i < reviewCount; i++)
            {
                var rating = (decimal)(3.5 + random.NextDouble() * 1.5);
                var amenity = hotel.HasSwimmingPool ? "bể bơi" : (hotel.HasBreakfast ? "nhà hàng" : "wifi miễn phí");
                
                hotel.Reviews.Add(new Review
                {
                    ReviewerName = reviewerNames[random.Next(reviewerNames.Length)],
                    Rating = Math.Round(rating, 1),
                    Comment = string.Format(
                        commentTemplates[random.Next(commentTemplates.Length)],
                        hotel.Rating,
                        hotel.Rooms.First().RoomType.ToLower(),
                        hotel.City,
                        amenity
                    ),
                    ReviewDate = DateTime.Now.AddDays(-random.Next(1, 180))
                });
            }
        }
    }
} 