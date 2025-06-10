# Quick Debug Steps - Add Permission Button

## Hiện tại ứng dụng đang chạy tại:
- HTTPS: https://localhost:5001
- HTTP: http://localhost:5000

## Các bước debug vấn đề "Add Permission" button:

### 1. Đăng nhập vào hệ thống
- Mở https://localhost:5001
- Đăng nhập với tài khoản admin:
  - Email: admin@thweb.com
  - Password: Admin@123

### 2. Vào trang Role Management
- Từ menu Admin > Roles
- Hoặc đi trực tiếp: https://localhost:5001/Admin/Roles

### 3. Click vào một role để xem details
- Click "View Details" trên một role bất kỳ (trừ SuperAdmin)
- Hoặc đi trực tiếp: https://localhost:5001/Admin/RoleDetails/{roleId}

### 4. Test Add Permission button
- Click nút "Add Permission"
- Quan sát xem có modal hiện ra không

### 5. Kiểm tra Developer Tools (F12)

#### Console Tab:
- Tìm lỗi JavaScript (màu đỏ)
- Chú ý các lỗi liên quan đến:
  - Uncaught TypeError
  - 404 Not Found
  - 500 Internal Server Error

#### Network Tab:
- Click "Add Permission" button
- Xem có request nào được gửi không:
  - `/Admin/GrantPermissionToRole` (POST)
  - Status code: 200 OK, 400 Bad Request, 500 Error?

#### Response của API:
```json
{
  "success": true/false,
  "message": "..."
}
```

### 6. Các lỗi thường gặp:

#### Lỗi 1: Modal không hiện
- Kiểm tra Bootstrap JS có load không
- Kiểm tra jQuery có load không

#### Lỗi 2: API call thất bại
- Kiểm tra CSRF token
- Kiểm tra user có đủ quyền không
- Kiểm tra database connection

#### Lỗi 3: JavaScript error
- Function `addSelectedPermissions` không tồn tại
- Selector không đúng
- Ajax call bị lỗi

### 7. Server logs cần kiểm tra:
- Authorization errors
- Database errors
- Permission validation errors

## Common Solutions:

### Fix 1: Thêm CSRF token
```javascript
headers: {
    'Content-Type': 'application/json',
    'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
}
```

### Fix 2: Check user permissions
- User phải có permission "role.management"
- User phải đăng nhập

### Fix 3: Ensure modal is properly initialized
```javascript
$('#addPermissionModal').modal('show');
```
