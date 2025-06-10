# PHÂN TÍCH CHI TIẾT QUYỀN HẠNG THEO ROLE - TH_WEB

## 🔴 **SUPERADMIN** - Quyền cao nhất
**Permissions**: ALL (19 permissions)
```
✅ System Management, User Management, Role Management, System Config
✅ Hotel Manage, Room Manage, Attraction Manage, Car Rental Manage
✅ Travel Package Manage, Booking Manage, Review Moderate
✅ Report View/Generate/Export, Finance View/Manage, Payment Process
✅ Content View/Create/Edit/Delete/Publish
```

**Có thể làm**: 
- Quản lý toàn bộ hệ thống
- Tạo/sửa/xóa users và roles
- Truy cập tất cả modules
- Xem tất cả báo cáo và tài chính

---

## 🟠 **ADMIN** - Quản trị viên
**Permissions**: 15 permissions (trừ System Management)
```
✅ User Management, Hotel Manage, Room Manage, Attraction Manage
✅ Car Rental Manage, Travel Package Manage, Booking Manage
✅ Review Moderate, Report View/Generate, Finance View
✅ Content View/Create/Edit/Delete/Publish
❌ System Management, System Config, Finance Manage, Payment Process
```

**Có thể làm**:
- Quản lý users nhưng không thể thay đổi cấu hình hệ thống
- Quản lý tất cả business modules
- Xem báo cáo nhưng không quản lý tài chính
- Quản lý nội dung hoàn toàn

---

## 🔵 **HOTELMANAGER** - Quản lý khách sạn
**Permissions**: 8 permissions
```
✅ Hotel View/Edit, Room Manage, Booking View/Edit
✅ Review View, Report View, Content View/Edit
❌ Hotel Create/Delete, System functions, Finance
```

**Có thể làm**:
- Chỉnh sửa thông tin khách sạn (không tạo mới)
- Quản lý phòng hoàn thoàn
- Xem và chỉnh sửa bookings
- Xem reviews và reports
- Chỉnh sửa nội dung

---

## 🟣 **ATTRACTIONMANAGER** - Quản lý điểm tham quan  
**Permissions**: 7 permissions
```
✅ Attraction View/Edit, Booking View/Edit
✅ Review View, Report View, Content View/Edit
❌ Attraction Create/Delete, System functions
```

**Có thể làm**:
- Chỉnh sửa thông tin điểm tham quan
- Quản lý bookings của attractions
- Xem reviews và tạo reports
- Quản lý nội dung liên quan

---

## 🟢 **CARRENTALMANAGER** - Quản lý thuê xe
**Permissions**: 7 permissions  
```
✅ Car Rental View/Edit, Booking View/Edit
✅ Review View, Report View, Content View/Edit
❌ Car Rental Create/Delete, System functions
```

**Có thể làm**:
- Chỉnh sửa thông tin xe cho thuê
- Quản lý bookings thuê xe
- Xem reviews và reports
- Quản lý nội dung

---

## 🟡 **HOTELSTAFF** - Nhân viên khách sạn
**Permissions**: 6 permissions
```
✅ Hotel View, Room View/Edit, Booking View/Edit
✅ Review View, Content View
❌ Hotel Edit/Delete, Management functions
```

**Có thể làm**:
- Xem thông tin khách sạn
- Chỉnh sửa thông tin phòng
- Xử lý bookings hàng ngày
- Xem reviews từ khách

---

## 🔵 **CUSTOMERSERVICE** - Dịch vụ khách hàng
**Permissions**: 7 permissions
```
✅ Booking View/Edit/Cancel
✅ Hotel View, Room View, Attraction View, Car Rental View
✅ Review View, Content View
❌ Create/Delete functions, Management
```

**Có thể làm**:
- Hỗ trợ khách hàng với bookings
- Xem tất cả dịch vụ để tư vấn
- Hủy booking khi cần
- Xem reviews để hỗ trợ

---

## 🟢 **CUSTOMER** - Khách hàng thường
**Permissions**: 9 permissions
```
✅ Hotel/Room/Attraction/Car Rental/Travel Package View
✅ Booking Create/View, Review Create/Edit, Content View
❌ Management functions, Delete operations
```

**Có thể làm**:
- Xem tất cả dịch vụ
- Tạo bookings
- Viết và chỉnh sửa reviews của mình
- Xem nội dung

---

## 🌟 **PREMIUMCUSTOMER** - Khách hàng VIP
**Permissions**: 11 permissions (Customer + extras)
```
✅ Tất cả quyền của Customer
✅ Booking Edit, Content Create
❌ Management functions, Delete operations  
```

**Có thể làm**:
- Tất cả như Customer
- Chỉnh sửa bookings của mình
- Tạo nội dung (blog, review chi tiết)

---

## 🔍 **MODERATOR** - Kiểm duyệt viên
**Permissions**: 5 permissions
```
✅ Review View/Moderate
✅ Content View/Edit/Delete
❌ Business operations, System management
```

**Có thể làm**:
- Kiểm duyệt và xóa reviews không phù hợp
- Quản lý nội dung trên website
- Xóa nội dung vi phạm

---

## ✍️ **CONTENTCREATOR** - Tạo nội dung
**Permissions**: 3 permissions
```
✅ Content View/Create/Edit
❌ Content Delete, Business operations
```

**Có thể làm**:
- Tạo và chỉnh sửa nội dung
- Viết blog, articles
- Không thể xóa nội dung

---

## 📊 **SO SÁNH MỨC ĐỘ TRUY CẬP**

### **Cấp độ 1: SYSTEM CONTROL**
- SuperAdmin (19 permissions)
- Admin (15 permissions)

### **Cấp độ 2: BUSINESS MANAGEMENT** 
- HotelManager (8 permissions)
- AttractionManager (7 permissions)  
- CarRentalManager (7 permissions)

### **Cấp độ 3: OPERATIONAL STAFF**
- CustomerService (7 permissions)
- HotelStaff (6 permissions)
- Moderator (5 permissions)

### **Cấp độ 4: CUSTOMERS**
- PremiumCustomer (11 permissions)
- Customer (9 permissions)

### **Cấp độ 5: SPECIALIZED**
- ContentCreator (3 permissions)

---

## 🚨 **SECURITY NOTES**

### **Protected Actions**
- **System Management**: Chỉ SuperAdmin
- **User/Role Management**: SuperAdmin + Admin
- **Financial Operations**: Chỉ SuperAdmin
- **Create Business Resources**: Managers + Above
- **Delete Operations**: Restricted severely

### **Resource Ownership**
- Users có thể edit/delete resources mà họ sở hữu
- Ownership được track qua `OwnerId` field
- Managers có thể override ownership trong domain của họ

### **Permission Inheritance**
- Role permissions được inherit tự động
- Direct user permissions có thể được grant thêm
- Permissions có thể có expiration date

---

## 🔧 **CURRENT STATUS**

### **Attractions Module**
- ✅ Create/Edit/Delete functionality working
- ⚠️ Permission checks temporarily disabled for testing
- ✅ Role-based access implemented but commented out

### **Next Steps**
1. Re-enable permission checks in AttractionsController
2. Test role-based access end-to-end
3. Implement resource ownership for attractions
4. Add audit logging for all operations
