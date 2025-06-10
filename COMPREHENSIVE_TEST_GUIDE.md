# 🧪 HƯỚNG DẪN TEST TÍNH NĂNG PHÂN QUYỀN - TH_WEB

## 🚀 BƯỚC 1: KHỞI ĐỘNG VÀ SEEDING

### **1.1 Khởi động ứng dụng**
```bash
cd D:\-A_THWEB
dotnet run
```

Truy cập: **https://localhost:5001**

### **1.2 Tạo tài khoản test**
1. Đăng nhập với SuperAdmin:
   - Email: `admin@thweb.com`
   - Password: `Admin@123`

2. Truy cập: `https://localhost:5001/Seed`

3. Click **"Seed Test Accounts"** để tạo tất cả 17 tài khoản test

4. Xác nhận tại: `https://localhost:5001/Seed/TestAccountsList`

---

## 👥 DANH SÁCH TÀI KHOẢN TEST

### **🔴 SYSTEM ROLES**
| Email | Password | Role | Quyền |
|-------|----------|------|-------|
| admin@thweb.com | Admin@123 | SuperAdmin | Tất cả quyền (19 permissions) |
| admin2@test.com | Test@123 | Admin | Quản trị (15 permissions) |

### **🔵 MANAGEMENT ROLES** 
| Email | Password | Role | Quyền chính |
|-------|----------|------|-------------|
| hotelmanager@test.com | Test@123 | HotelManager | Quản lý khách sạn và phòng |
| attractionmanager@test.com | Test@123 | AttractionManager | Quản lý điểm tham quan |
| carmanager@test.com | Test@123 | CarRentalManager | Quản lý thuê xe |
| travelmanager@test.com | Test@123 | TravelPackageManager | Quản lý gói du lịch |

### **🟢 STAFF ROLES**
| Email | Password | Role | Quyền chính |
|-------|----------|------|-------------|
| hotelstaff@test.com | Test@123 | HotelStaff | Vận hành khách sạn |
| attractionstaff@test.com | Test@123 | AttractionStaff | Vận hành điểm tham quan |
| carstaff@test.com | Test@123 | CarRentalStaff | Vận hành thuê xe |
| customerservice@test.com | Test@123 | CustomerService | Hỗ trợ khách hàng |

### **🟡 OWNER ROLES**
| Email | Password | Role | Quyền chính |
|-------|----------|------|-------------|
| hotelowner@test.com | Test@123 | HotelOwner | Sở hữu khách sạn |
| attractionowner@test.com | Test@123 | AttractionOwner | Sở hữu điểm tham quan |
| carowner@test.com | Test@123 | CarRentalOwner | Sở hữu dịch vụ thuê xe |

### **🟠 CUSTOMER ROLES**
| Email | Password | Role | Quyền chính |
|-------|----------|------|-------------|
| customer@test.com | Test@123 | Customer | Khách hàng thường (9 permissions) |
| premium@test.com | Test@123 | PremiumCustomer | Khách VIP (11 permissions) |

### **⚪ SPECIAL ROLES**
| Email | Password | Role | Quyền chính |
|-------|----------|------|-------------|
| moderator@test.com | Test@123 | Moderator | Kiểm duyệt nội dung |
| creator@test.com | Test@123 | ContentCreator | Tạo nội dung |

---

## 🧪 KỊCH BẢN TEST CHI TIẾT

### **TEST 1: SYSTEM ADMINISTRATION**

#### **Test 1.1: SuperAdmin Access**
1. **Đăng nhập**: `admin@thweb.com` / `Admin@123`
2. **Kiểm tra truy cập**:
   - ✅ Admin Dashboard: `/Admin`
   - ✅ User Management: `/Admin/Users`
   - ✅ Role Management: `/Admin/Roles`
   - ✅ System Config: `/Seed`
   - ✅ All modules: Hotels, Attractions, Car Rental
3. **Thao tác**:
   - Tạo/sửa/xóa users
   - Assign/remove roles
   - Grant/revoke permissions
   - View all data

#### **Test 1.2: Admin Access**
1. **Đăng nhập**: `admin2@test.com` / `Test@123`
2. **Kiểm tra quyền**:
   - ✅ User Management
   - ✅ Business modules
   - ❌ System Management (should be blocked)
   - ❌ Finance Management
3. **Expected**: Không truy cập được `/Seed` (System Management)

---

### **TEST 2: ATTRACTION MANAGEMENT**

#### **Test 2.1: AttractionManager**
1. **Đăng nhập**: `attractionmanager@test.com` / `Test@123`
2. **Test quyền Attraction**:
   - ✅ View attractions: `/Attractions`
   - ✅ Edit attractions: `/Attractions/Edit/{id}`
   - ❌ Create attractions (should be limited)
   - ❌ Delete attractions (should be limited)
3. **Test quyền khác**:
   - ✅ View bookings
   - ✅ Edit bookings
   - ✅ View reviews
   - ❌ User management (should be blocked)

#### **Test 2.2: AttractionStaff**
1. **Đăng nhập**: `attractionstaff@test.com` / `Test@123`
2. **Test quyền hạn chế**:
   - ✅ View attractions
   - ❌ Edit attractions (should be blocked)
   - ❌ Management functions
3. **Expected**: Chỉ có quyền view và support

#### **Test 2.3: AttractionOwner**
1. **Đăng nhập**: `attractionowner@test.com` / `Test@123`
2. **Test resource ownership**:
   - ✅ Edit own attractions
   - ❌ Edit others' attractions
   - ✅ Manage owned resources

---

### **TEST 3: HOTEL MANAGEMENT**

#### **Test 3.1: HotelManager**
1. **Đăng nhập**: `hotelmanager@test.com` / `Test@123`
2. **Test Hotel permissions**:
   - ✅ View hotels: `/Hotel`
   - ✅ Edit hotels: `/Hotel/Edit/{id}`
   - ✅ Manage rooms: `/Room`
   - ❌ Create new hotels
3. **Test booking management**:
   - ✅ View bookings
   - ✅ Edit bookings
   - ✅ Process reservations

#### **Test 3.2: HotelStaff**
1. **Đăng nhập**: `hotelstaff@test.com` / `Test@123`
2. **Test limited access**:
   - ✅ View hotel info
   - ✅ Edit room details
   - ✅ Handle daily bookings
   - ❌ Hotel management functions

---

### **TEST 4: CAR RENTAL MANAGEMENT**

#### **Test 4.1: CarRentalManager**
1. **Đăng nhập**: `carmanager@test.com` / `Test@123`
2. **Test Car Rental permissions**:
   - ✅ View car rentals: `/CarRental`
   - ✅ Edit car details
   - ✅ Manage bookings
   - ❌ Finance operations

#### **Test 4.2: CarRentalStaff**
1. **Đăng nhập**: `carstaff@test.com` / `Test@123`
2. **Test operational access**:
   - ✅ View car rentals
   - ✅ Process daily rentals
   - ❌ Management functions

---

### **TEST 5: CUSTOMER EXPERIENCE**

#### **Test 5.1: Regular Customer**
1. **Đăng nhập**: `customer@test.com` / `Test@123`
2. **Test customer permissions**:
   - ✅ View all services
   - ✅ Create bookings
   - ✅ Write reviews
   - ❌ Management access
   - ❌ Edit others' content

#### **Test 5.2: Premium Customer**
1. **Đăng nhập**: `premium@test.com` / `Test@123`
2. **Test premium features**:
   - ✅ All customer rights
   - ✅ Edit own bookings
   - ✅ Create content
   - ✅ Advanced features

---

### **TEST 6: CUSTOMER SERVICE**

#### **Test 6.1: Customer Service**
1. **Đăng nhập**: `customerservice@test.com` / `Test@123`
2. **Test support permissions**:
   - ✅ View all services
   - ✅ View/edit bookings
   - ✅ Cancel bookings
   - ✅ View reviews
   - ❌ Create/delete operations

---

### **TEST 7: CONTENT MANAGEMENT**

#### **Test 7.1: Moderator**
1. **Đăng nhập**: `moderator@test.com` / `Test@123`
2. **Test moderation permissions**:
   - ✅ View all reviews
   - ✅ Moderate reviews
   - ✅ Edit content
   - ✅ Delete inappropriate content
   - ❌ Business operations

#### **Test 7.2: Content Creator**
1. **Đăng nhập**: `creator@test.com` / `Test@123`
2. **Test creation permissions**:
   - ✅ Create content
   - ✅ Edit own content
   - ❌ Delete content
   - ❌ Business operations

---

## 🔍 KIỂM TRA ACCESS CONTROL

### **URLs để test truy cập bị cấm**

#### **System Management (chỉ SuperAdmin)**
- `/Seed` - Should block all except SuperAdmin
- `/Admin/ActivityLog` - System activities

#### **User Management (SuperAdmin + Admin)**
- `/Admin/Users` - Should block all except SuperAdmin/Admin
- `/Admin/Roles` - Role management

#### **Business Management (Managers + Above)**
- `/Attractions/Create` - Should block Staff/Customer
- `/Hotel/Create` - Should block non-managers
- `/CarRental/Create` - Should block staff

#### **Finance Operations (SuperAdmin only)**
- Any finance-related operations
- Payment processing

---

## 📊 BẢNG KIỂM PERMISSION

| Action | SuperAdmin | Admin | Manager | Staff | Customer |
|--------|------------|-------|---------|-------|----------|
| System Config | ✅ | ❌ | ❌ | ❌ | ❌ |
| User Management | ✅ | ✅ | ❌ | ❌ | ❌ |
| Create Business Resources | ✅ | ✅ | ✅ | ❌ | ❌ |
| Edit Own Resources | ✅ | ✅ | ✅ | ✅ | ✅ |
| View All Resources | ✅ | ✅ | ✅ | ✅ | ✅ |
| Delete Resources | ✅ | ✅ | Limited | ❌ | ❌ |
| Financial Operations | ✅ | ❌ | ❌ | ❌ | ❌ |
| Create Bookings | ✅ | ✅ | ✅ | ✅ | ✅ |
| Cancel Bookings | ✅ | ✅ | ✅ | ✅ | Own only |

---

## 🚨 EXPECTED RESULTS

### **Access Denied Scenarios**
1. **Staff trying to access management functions** → 403 Forbidden
2. **Customer trying to access admin panel** → Redirect to login/home
3. **Manager trying to access system config** → 403 Forbidden
4. **Non-owner trying to edit others' resources** → 403 Forbidden

### **Success Scenarios**
1. **Manager editing resources in their domain** → Success
2. **Customer creating bookings** → Success
3. **Staff handling daily operations** → Success
4. **SuperAdmin accessing everything** → Success

---

## 🔧 DEBUGGING TIPS

### **Permission Check Debugging**
1. Check console logs for permission denials
2. Verify user roles in Admin panel
3. Check RouteData for area/controller/action
4. Verify permission attributes on controllers

### **Common Issues**
1. **Permission attribute typos** → Check spelling
2. **Missing role assignments** → Use Admin panel to assign
3. **Cached authorization** → Clear browser cache
4. **Route conflicts** → Check area routing

### **Tools for Testing**
1. **Browser Developer Tools** → Network tab for 403 errors
2. **Admin Dashboard** → Check user roles and permissions
3. **Database** → Verify RolePermissions table
4. **Application Logs** → Check authorization failures

---

## 📝 TEST CHECKLIST

- [ ] All 17 test accounts created successfully
- [ ] SuperAdmin has full access
- [ ] Admin blocked from System Management
- [ ] Managers can access their domains only
- [ ] Staff have limited operational access
- [ ] Customers can't access management functions
- [ ] Resource ownership working correctly
- [ ] Permission inheritance working
- [ ] Access denied pages display correctly
- [ ] Audit logging captures all actions

---

## 🎯 SUCCESS CRITERIA

✅ **Role-based access control working**
✅ **Resource ownership enforced**
✅ **Permission inheritance functional**
✅ **Security boundaries respected**
✅ **User experience consistent**
✅ **Admin tools functional**
✅ **Audit logging operational**

**TEST COMPLETION**: All scenarios should pass with expected results. Any failures indicate security vulnerabilities that need immediate attention.
