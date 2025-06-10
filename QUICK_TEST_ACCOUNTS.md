# 🚀 QUICK START - TEST ACCOUNTS

## **KHỞI ĐỘNG NHANH**

1. **Chạy ứng dụng**:
   ```bash
   cd D:\-A_THWEB
   dotnet run
   ```

2. **Truy cập**: https://localhost:5001

3. **Đăng nhập SuperAdmin**: 
   - Email: `admin@thweb.com`
   - Password: `Admin@123`

4. **Tạo test accounts**: Click menu user → "Database Seeding" → "Seed Test Accounts"

---

## **17 TÀI KHOẢN TEST**

| **Role Category** | **Email** | **Password** | **Role** |
|-------------------|-----------|--------------|----------|
| **System** | admin@thweb.com | Admin@123 | SuperAdmin |
| **System** | admin2@test.com | Test@123 | Admin |
| **Manager** | hotelmanager@test.com | Test@123 | HotelManager |
| **Manager** | attractionmanager@test.com | Test@123 | AttractionManager |
| **Manager** | carmanager@test.com | Test@123 | CarRentalManager |
| **Manager** | travelmanager@test.com | Test@123 | TravelPackageManager |
| **Staff** | hotelstaff@test.com | Test@123 | HotelStaff |
| **Staff** | attractionstaff@test.com | Test@123 | AttractionStaff |
| **Staff** | carstaff@test.com | Test@123 | CarRentalStaff |
| **Staff** | customerservice@test.com | Test@123 | CustomerService |
| **Owner** | hotelowner@test.com | Test@123 | HotelOwner |
| **Owner** | attractionowner@test.com | Test@123 | AttractionOwner |
| **Owner** | carowner@test.com | Test@123 | CarRentalOwner |
| **Customer** | customer@test.com | Test@123 | Customer |
| **Customer** | premium@test.com | Test@123 | PremiumCustomer |
| **Special** | moderator@test.com | Test@123 | Moderator |
| **Special** | creator@test.com | Test@123 | ContentCreator |

---

## **TEST NHANH**

### **🔴 Test SuperAdmin (Full Access)**
- Login: `admin@thweb.com` / `Admin@123`
- Có thể truy cập: Admin Dashboard, User Management, Seeding, tất cả modules

### **🔵 Test Manager (Limited to Domain)**
- Login: `attractionmanager@test.com` / `Test@123` 
- Có thể: Xem/Edit attractions, quản lý bookings
- Không thể: User management, system config

### **🟢 Test Staff (Operational Only)**
- Login: `hotelstaff@test.com` / `Test@123`
- Có thể: Xem hotels, edit rooms, handle bookings
- Không thể: Management functions, admin access

### **🟠 Test Customer (Public Access)**
- Login: `customer@test.com` / `Test@123`
- Có thể: Xem services, tạo bookings, viết reviews
- Không thể: Management, admin functions

---

## **KIỂM TRA NHANH ACCESS CONTROL**

1. **System Management** (chỉ SuperAdmin):
   - URL: `/Seed` - Chỉ SuperAdmin được truy cập

2. **User Management** (SuperAdmin + Admin):
   - URL: `/Admin/Users` - Staff/Customer bị block

3. **Business Management** (Manager+):
   - URL: `/Attractions/Create` - Staff/Customer bị block

4. **Own Resource Access**:
   - Managers chỉ edit được resources trong domain
   - Customers chỉ edit được own bookings

---

**✅ SUCCESS**: Tất cả roles hoạt động đúng với permissions được phân quyền
**❌ FAILURE**: Nếu có role truy cập được functions không được phép = Security bug
