# Hướng dẫn Debug Web Application

## 1. Kiểm tra Browser Console

### Cách mở Developer Tools:
- **Chrome/Edge**: F12 hoặc Ctrl+Shift+I
- **Firefox**: F12 hoặc Ctrl+Shift+I

### Các tab cần kiểm tra:
1. **Console**: Xem lỗi JavaScript
2. **Network**: Xem các request API có lỗi không
3. **Application**: Kiểm tra Session/Local Storage

## 2. Kiểm tra Network Tab

### Tìm request bị lỗi:
- Status Code đỏ (400, 500, etc.)
- Request bị pending quá lâu
- Response time quá cao

### Thông tin cần xem:
- Request URL
- Status Code
- Response Headers
- Response Body (có error message không)

## 3. Kiểm tra Server Logs

### Visual Studio Output:
- Debug Output Window
- IIS Express logs

### Console Application logs:
- Exception messages
- SQL errors
- Authorization errors

## 4. Common Issues

### 4.1 Authentication/Authorization
- User chưa đăng nhập
- Không có quyền truy cập
- Session timeout

### 4.2 Database Issues
- Connection string sai
- Migration chưa chạy
- SQL Server không khởi động

### 4.3 JavaScript Errors
- Missing dependencies
- Network timeout
- CORS issues

## 5. Debugging Steps

1. Check browser console for errors
2. Check network tab for failed requests
3. Check server logs/output
4. Test with simplified request
5. Check database connectivity
