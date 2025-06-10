# SCRIPT TẠO TÀI KHOẢN TEST

## Cách 1: Qua Admin Dashboard (Khuyến nghị)

1. **Login Admin:**
   - Email: `admin@thweb.com`
   - Password: `Admin@123`

2. **Tạo User Test:**
   - Vào Admin → Users → Add New User
   - Tạo các user sau:

### **Test User 1 - Regular User**
- Email: `user@test.com`
- Password: `User@123`
- Role: `User`
- Permissions: Chỉ view basic resources

### **Test User 2 - Staff**  
- Email: `staff@test.com`
- Password: `Staff@123`
- Role: `Staff`
- Permissions: Create, Edit một số resources

### **Test User 3 - Manager**
- Email: `manager@test.com` 
- Password: `Manager@123`
- Role: `Manager`
- Permissions: Quản lý resources trong domain

### **Test User 4 - Admin**
- Email: `admin2@test.com`
- Password: `Admin2@123`  
- Role: `Admin`
- Permissions: Hầu hết admin functions

---

## Cách 2: Qua Registration Page

1. Truy cập: `https://localhost:5001/Identity/Account/Register`
2. Register với thông tin bất kỳ
3. User mới sẽ có role mặc định là `User`
4. Admin có thể thay đổi role sau

---

## Cách 3: Add vào AuthorizationSeeder (Nâng cao)

Nếu muốn tạo sẵn nhiều user test, có thể thêm vào file `AuthorizationSeeder.cs`:

```csharp
// Thêm vào method SeedDefaultAdminUserAsync hoặc tạo method mới
private static async Task SeedTestUsersAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
{
    var testUsers = new[]
    {
        new { Email = "user@test.com", Password = "User@123", Role = "User" },
        new { Email = "staff@test.com", Password = "Staff@123", Role = "Staff" },
        new { Email = "manager@test.com", Password = "Manager@123", Role = "Manager" },
        new { Email = "admin2@test.com", Password = "Admin2@123", Role = "Admin" }
    };

    foreach (var testUser in testUsers)
    {
        var user = await userManager.FindByEmailAsync(testUser.Email);
        if (user == null)
        {
            user = new ApplicationUser
            {
                UserName = testUser.Email,
                Email = testUser.Email,
                FirstName = "Test",
                LastName = testUser.Role,
                EmailConfirmed = true,
                IsActive = true,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var result = await userManager.CreateAsync(user, testUser.Password);
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, testUser.Role);
            }
        }
    }
}
```

Sau đó gọi method này trong `SeedAsync`.

---

## 🎯 QUICK START TEST

**1. Start Application:**
```bash
cd D:\-A_THWEB
dotnet run
```

**2. Login Admin:**
- Go to: https://localhost:5001
- Login: admin@thweb.com / Admin@123

**3. Access Admin Dashboard:**
- Click "Admin" menu
- Explore Users, Roles, Dashboard

**4. Test Permissions:**
- Try accessing Hotel/Create (should work)
- Logout and register new user
- Try accessing Hotel/Create (should be hidden/restricted)

**5. Create Test Users:**
- From Admin → Users → Add User
- Create users with different roles
- Test each user's access levels

---

## 🔍 DEBUGGING TIPS

**Check Current User Info:**
```csharp
// Add to any controller action for debugging
var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
var userEmail = User.FindFirst(ClaimTypes.Email)?.Value;  
var userRoles = User.FindAll(ClaimTypes.Role).Select(r => r.Value);
```

**Check User Permissions:**
```csharp
// In controller with IPermissionService injected
var hasPermission = await _permissionService.HasPermissionAsync(userId, "hotel.create");
```

**Verify Database Seeding:**
- Check tables: Users, Roles, UserRoles, Permissions, RolePermissions
- Verify admin user exists with SuperAdmin role
- Verify permissions are properly seeded

---

**🚀 BÂY GIỜ BẠN CÓ THỂ BẮT ĐẦU TEST HỒ THỐNG AUTHORIZATION!**
