# 🚀 QUICK GOOGLE OAUTH SETUP

## **HIỆN TẠI: Trang Login đã hoạt động!**
✅ URL: https://localhost:5001/Identity/Account/LoginRegister

## **BƯỚC TIẾP THEO: Tạo Google OAuth Client**

### **🎯 BẠN ĐANG Ở ĐÂY - Click "Create an OAuth client"**

**Sau khi click "Create an OAuth client", làm theo:**

1. **Chọn Application Type:**
   - Chọn **"Web application"**

2. **Điền thông tin:**
   - **Name**: `THWEB Travel App` (hoặc tên bạn muốn)
   - **Authorized JavaScript origins**: 
     ```
     https://localhost:5001
     http://localhost:5000
     ```
   - **Authorized redirect URIs**:
     ```
     https://localhost:5001/signin-google
     ```

3. **Click "Create"**

4. **Copy credentials:**
   - Sau khi tạo xong, bạn sẽ thấy popup với:
     - **Client ID**: `xxxxx.apps.googleusercontent.com`
     - **Client Secret**: `GOCSPX-xxxxx`
   - **COPY CẢ HAI GIÁ TRỊ NÀY!**

### **Cập nhật appsettings.Development.json:**

Sau khi có credentials, update file này:

```json
{
  "Authentication": {
    "Google": {
      "ClientId": "PASTE_YOUR_CLIENT_ID_HERE.apps.googleusercontent.com",
      "ClientSecret": "PASTE_YOUR_CLIENT_SECRET_HERE"
    }
  }
}
```

### **Restart ứng dụng:**
```bash
# Stop current app (Ctrl+C in terminal)
# Then restart:
dotnet run
```

---

## **SAU KHI SETUP XONG - TEST NGAY**

1. **Mở:** https://localhost:5001/Identity/Account/LoginRegister
2. **Click Google button** trong tab Register hoặc Sign In
3. **Chọn Google account** của bạn
4. **Authorize app**
5. **Sẽ tự động tạo user mới** với role Customer

---

## **🎯 TROUBLESHOOTING**

**Nếu gặp lỗi redirect_uri_mismatch:**
- Kiểm tra lại **Authorized redirect URIs** phải là: `https://localhost:5001/signin-google`

**Nếu app không start:**
- Kiểm tra syntax trong appsettings.Development.json
- Đảm bảo có dấu phẩy và ngoặc đúng

**Nếu Google button không hoạt động:**
- Kiểm tra Client ID/Secret đã paste đúng chưa
- Restart app sau khi update settings

---

## **✅ SAU KHI HOÀN THÀNH**

Bạn sẽ có:
- ✅ Regular login/register (admin@thweb.com / Admin@123)
- ✅ Google OAuth login/register
- ✅ Auto role assignment (Customer cho user mới)
- ✅ Complete authentication system

**🎉 GOOGLE OAUTH SẼ HOẠT ĐỘNG HOÀN TOÀN!**
