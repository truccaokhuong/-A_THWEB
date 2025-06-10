namespace TH_WEB.Models
{
    public static class UserRoles
    {
        // Vai trò hệ thống
        public const string SuperAdmin = "SuperAdmin";
        public const string Admin = "Admin";
        
        // Vai trò quản lý
        public const string HotelManager = "HotelManager";
        public const string AttractionManager = "AttractionManager";
        public const string CarRentalManager = "CarRentalManager";
        public const string TravelPackageManager = "TravelPackageManager";
        
        // Vai trò nhân viên
        public const string HotelStaff = "HotelStaff";
        public const string AttractionStaff = "AttractionStaff";
        public const string CarRentalStaff = "CarRentalStaff";
        public const string CustomerService = "CustomerService";
        
        // Vai trò đối tác
        public const string HotelOwner = "HotelOwner";
        public const string AttractionOwner = "AttractionOwner";
        public const string CarRentalOwner = "CarRentalOwner";
        
        // Vai trò khách hàng
        public const string Customer = "Customer";
        public const string PremiumCustomer = "PremiumCustomer";
        
        // Vai trò đặc biệt
        public const string Moderator = "Moderator";
        public const string ContentCreator = "ContentCreator";
    }
    
    public static class Permissions
    {
        // Quyền hệ thống
        public const string SystemManagement = "system.management";
        public const string UserManagement = "user.management";
        public const string RoleManagement = "role.management";
        public const string SystemConfig = "system.config";
        
        // Quyền khách sạn
        public const string HotelView = "hotel.view";
        public const string HotelCreate = "hotel.create";
        public const string HotelEdit = "hotel.edit";
        public const string HotelDelete = "hotel.delete";
        public const string HotelManage = "hotel.manage";
        
        // Quyền phòng
        public const string RoomView = "room.view";
        public const string RoomCreate = "room.create";
        public const string RoomEdit = "room.edit";
        public const string RoomDelete = "room.delete";
        public const string RoomManage = "room.manage";
        
        // Quyền điểm tham quan
        public const string AttractionView = "attraction.view";
        public const string AttractionCreate = "attraction.create";
        public const string AttractionEdit = "attraction.edit";
        public const string AttractionDelete = "attraction.delete";
        public const string AttractionManage = "attraction.manage";
        
        // Quyền thuê xe
        public const string CarRentalView = "carrental.view";
        public const string CarRentalCreate = "carrental.create";
        public const string CarRentalEdit = "carrental.edit";
        public const string CarRentalDelete = "carrental.delete";
        public const string CarRentalManage = "carrental.manage";
        
        // Quyền gói du lịch
        public const string TravelPackageView = "travelpackage.view";
        public const string TravelPackageCreate = "travelpackage.create";
        public const string TravelPackageEdit = "travelpackage.edit";
        public const string TravelPackageDelete = "travelpackage.delete";
        public const string TravelPackageManage = "travelpackage.manage";
        
        // Quyền đặt chỗ
        public const string BookingView = "booking.view";
        public const string BookingCreate = "booking.create";
        public const string BookingEdit = "booking.edit";
        public const string BookingCancel = "booking.cancel";
        public const string BookingManage = "booking.manage";
        
        // Quyền đánh giá
        public const string ReviewView = "review.view";
        public const string ReviewCreate = "review.create";
        public const string ReviewEdit = "review.edit";
        public const string ReviewDelete = "review.delete";
        public const string ReviewModerate = "review.moderate";
        
        // Quyền báo cáo
        public const string ReportView = "report.view";
        public const string ReportGenerate = "report.generate";
        public const string ReportExport = "report.export";
        
        // Quyền tài chính
        public const string FinanceView = "finance.view";
        public const string FinanceManage = "finance.manage";
        public const string PaymentProcess = "payment.process";
        
        // Quyền nội dung
        public const string ContentView = "content.view";
        public const string ContentCreate = "content.create";
        public const string ContentEdit = "content.edit";
        public const string ContentDelete = "content.delete";
        public const string ContentPublish = "content.publish";
    }
}
