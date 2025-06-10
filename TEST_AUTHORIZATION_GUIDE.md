# HƯỚNG DẪN TEST HỆ THỐNG AUTHORIZATION - TH_WEB

## 🚀 KHỞI ĐỘNG ỨNG DỤNG

```bash
cd D:\-A_THWEB
dotnet run
```

Ứng dụng sẽ chạy tại:
- **HTTPS**: https://localhost:5001
- **HTTP**: http://localhost:5000

---

## 👤 TÀI KHOẢN TEST MẶC ĐỊNH

### **Admin Account (SuperAdmin)**
- **Email**: `admin@thweb.com`
- **Password**: `Admin@123`
- **Quyền**: Tất cả permissions (SuperAdmin role)

### **Tạo User Test Thêm**
Sau khi đăng nhập admin, có thể tạo thêm user với các role khác nhau:

#### **Test Accounts to Create:**
1. **Hotel Manager**: `hotelmanager@test.com` / `Test@123` - Role: `HotelManager`
2. **Attraction Manager**: `attractionmanager@test.com` / `Test@123` - Role: `AttractionManager`  
3. **Car Rental Manager**: `carmanager@test.com` / `Test@123` - Role: `CarRentalManager`
4. **Hotel Staff**: `hotelstaff@test.com` / `Test@123` - Role: `HotelStaff`
5. **Customer**: `customer@test.com` / `Test@123` - Role: `Customer`
6. **Premium Customer**: `premium@test.com` / `Test@123` - Role: `PremiumCustomer`

### **Quick Role Assignment Guide:**
After creating users via registration, use Admin → Users → Assign Role:
- **Management Roles**: HotelManager, AttractionManager, CarRentalManager, TravelPackageManager
- **Staff Roles**: HotelStaff, AttractionStaff, CarRentalStaff, CustomerService  
- **Owner Roles**: HotelOwner, AttractionOwner, CarRentalOwner
- **Customer Roles**: Customer, PremiumCustomer
- **Special Roles**: Moderator, ContentCreator

---

## 🔧 CÁC BƯỚC TEST AUTHORIZATION

### **BƯỚC 1: Đăng nhập Admin**
1. Truy cập: `https://localhost:5001`
2. Click **Login** hoặc **Register**
3. Đăng nhập với:
   - Email: `admin@thweb.com`  
   - Password: `Admin@123`

### **BƯỚC 2: Truy cập Admin Dashboard**
1. Sau khi đăng nhập, click menu **Admin** (góc phải navigation)
2. Hoặc truy cập trực tiếp: `https://localhost:5001/Admin`
3. Kiểm tra Dashboard hiển thị:
   - ✅ Thống kê users, roles, permissions
   - ✅ Biểu đồ hoạt động
   - ✅ Danh sách hoạt động gần đây

### **BƯỚC 3: Test User Management**
1. Click **Users** từ Admin Dashboard
2. Kiểm tra:
   - ✅ Danh sách users với pagination
   - ✅ Search users by email/name
   - ✅ Filter by role
   - ✅ View user details

3. **Tạo User Mới:**
   - Click **Add New User**
   - Nhập thông tin user
   - Assign role (User, Staff, Manager, Admin)
   - Save và kiểm tra user được tạo

4. **Edit User:**
   - Click vào user bất kỳ
   - Modify roles và permissions
   - Save changes

### **BƯỚC 4: Test Role Management**
1. Click **Roles** từ Admin Dashboard
2. Kiểm tra:
   - ✅ Danh sách roles (SuperAdmin, Admin, HotelManager, AttractionManager, CarRentalManager, Customer, etc.)
   - ✅ Role statistics
   - ✅ Users count per role

3. **View Role Details:**
   - Click vào role bất kỳ
   - Kiểm tra permissions assigned
   - Kiểm tra users in role
   - Add/Remove permissions

### **BƯỚC 5: Test Resource Authorization**

#### **Test Hotel Management:**
1. Truy cập `https://localhost:5001/Hotel`
2. **With Admin Account:**
   - ✅ Có thể **Create** hotel mới
   - ✅ Có thể **Edit** bất kỳ hotel nào
   - ✅ Có thể **Delete** hotel

3. **Create Regular Customer** (không có quyền):
   - Đăng xuất admin
   - Register user mới với role "Customer"
   - Truy cập Hotel section
   - ❌ Không thấy nút **Create New Hotel**
   - ❌ Không thấy nút **Edit/Delete** 

4. **Test with HotelManager:**
   - Create user với role "HotelManager"
   - ✅ Có thể manage hotels
   - ❌ Không thể access admin panel

#### **Test Car Rental Management:**
1. Truy cập `https://localhost:5001/CarRental`  
2. Test tương tự như Hotel
3. Use "CarRentalManager" role for specific access

#### **Test Attractions Management:**
1. Truy cập `https://localhost:5001/Attractions`
2. Test tương tự như Hotel
3. Use "AttractionManager" role for specific access

### **BƯỚC 6: Test Resource Ownership**

1. **Tạo Hotel với User thường:**
   - Login với user không phải admin
   - Tạo hotel (nếu có quyền Create)
   - User đó sẽ trở thành owner của hotel

2. **Test Ownership Access:**
   - Owner có thể Edit/Delete hotel của mình
   - User khác không thể Edit/Delete hotel không phải của họ
   - Admin vẫn có thể Edit/Delete tất cả

### **BƯỚC 7: Test Activity Logging**

1. Thực hiện các actions:
   - Login/Logout
   - Create/Edit/Delete resources
   - View pages

2. Kiểm tra Activity Log:
   - Vào Admin Dashboard
   - Check **Recent Activities** section
   - Vào User Details để xem activity của user cụ thể

---

## 🔐 CÁC PERMISSION ĐÃ ĐƯỢC ĐỊNH NGHĨA

### **System Management**
- `system.view` - View system info
- `system.manage` - Manage system settings

### **User Management**  
- `user.view` - View users
- `user.create` - Create users
- `user.edit` - Edit users
- `user.delete` - Delete users
- `user.manage` - Full user management

### **Role Management**
- `role.view` - View roles
- `role.create` - Create roles  
- `role.edit` - Edit roles
- `role.delete` - Delete roles
- `role.manage` - Full role management

### **Hotel Management**
- `hotel.view` - View hotels
- `hotel.create` - Create hotels
- `hotel.edit` - Edit hotels
- `hotel.delete` - Delete hotels
- `hotel.manage` - Full hotel management

### **Car Rental Management**
- `carrental.view` - View car rentals
- `carrental.create` - Create car rentals
- `carrental.edit` - Edit car rentals  
- `carrental.delete` - Delete car rentals
- `carrental.manage` - Full car rental management

### **Attractions Management**
- `attraction.view` - View attractions
- `attraction.create` - Create attractions
- `attraction.edit` - Edit attractions
- `attraction.delete` - Delete attractions
- `attraction.manage` - Full attractions management

---

## 🧪 KỊCH BẢN TEST CHI TIẾT

### **Scenario 1: Admin Full Access**
1. Login admin@thweb.com
2. Truy cập tất cả trang admin ✅
3. Create/Edit/Delete tất cả resources ✅
4. Manage users và roles ✅

### **Scenario 2: Regular User Restricted Access**  
1. Register user mới với role "Customer"
2. Truy cập admin pages ❌ (should redirect/403)
3. Truy cập hotel/car/attraction listings ✅
4. Create new resources ❌ (button không hiển thị)

### **Scenario 3: Staff User Partial Access**
1. Create user với role "HotelStaff" or "AttractionStaff" 
2. Assign specific permissions (vd: hotel.create, hotel.edit)
3. User có thể create/edit hotels ✅
4. Nhưng không thể delete ❌
5. Không thể access admin panel ❌

### **Scenario 4: Manager User Extended Access**
1. Create user với role "HotelManager" or "AttractionManager"
2. Có thể manage resources trong phạm vi của mình ✅
3. Có thể view some admin stats ✅
4. Không thể manage users ❌

### **Scenario 5: Resource Ownership**
1. User A tạo Hotel X → A là owner của Hotel X
2. User B không thể edit Hotel X ❌
3. User A có thể edit Hotel X ✅  
4. Admin có thể edit Hotel X ✅

---

## 🐛 TROUBLESHOOTING

### **Lỗi 403 Forbidden**
- Check user có đúng role/permission không
- Check authorization attributes trên controller

### **Admin Panel không hiển thị**
- Verify user có permission `user.manage` hoặc `system.manage`
- Check navigation trong `_Layout.cshtml`

### **Database Errors**  
- Run migration: `dotnet ef database update`
- Check connection string trong `appsettings.json`

### **Seeding Issues**
- Restart app để trigger seeding
- Check logs trong console

---

## 📝 GHI CHÚ QUAN TRỌNG

### **SYSTEM ROLES HIERARCHY:**
1. **SuperAdmin** có tất cả permissions (system-level access)
2. **Admin** có hầu hết permissions trừ system-level  
3. **Managers** (HotelManager, AttractionManager, CarRentalManager, etc.) có permissions trong domain cụ thể  
4. **Staff** (HotelStaff, AttractionStaff, etc.) có permissions hạn chế
5. **Owners** (HotelOwner, AttractionOwner, etc.) có quyền sở hữu resource
6. **Customer/PremiumCustomer** chỉ có basic permissions
7. **Specialized Roles** (Moderator, ContentCreator, CustomerService)

### **AVAILABLE ROLES IN SYSTEM:**
- SuperAdmin, Admin
- HotelManager, AttractionManager, CarRentalManager, TravelPackageManager  
- HotelStaff, AttractionStaff, CarRentalStaff, CustomerService
- HotelOwner, AttractionOwner, CarRentalOwner
- Customer, PremiumCustomer
- Moderator, ContentCreator

### **IMPORTANT NOTES:**
6. **Resource Ownership** được tự động assign khi user tạo resource
7. **Activity Logging** tự động ghi lại tất cả actions
8. **Permission Check** xảy ra ở cả Controller và View level

---

## ✅ CHECKLIST TEST COMPLETION

- [ ] Admin login thành công
- [ ] Admin Dashboard hiển thị đầy đủ
- [ ] User Management CRUD
- [ ] Role Management CRUD  
- [ ] Hotel authorization working
- [ ] Car Rental authorization working
- [ ] Attractions authorization working
- [ ] Resource Ownership working
- [ ] Activity Logging working
- [ ] Regular user restrictions working
- [ ] Permission-based UI hiding working

**🎉 Khi tất cả checklist ✅, hệ thống Authorization hoạt động hoàn chỉnh!**
