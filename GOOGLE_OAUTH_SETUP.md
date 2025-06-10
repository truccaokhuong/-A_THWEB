# 🔧 GOOGLE OAUTH SETUP GUIDE

## **BƯỚC 1: Tạo Google Cloud Project**

1. **Truy cập Google Cloud Console:**
   - Đi đến: https://console.cloud.google.com/
   - Đăng nhập bằng Google account

2. **Tạo Project mới:**
   - Click "Select a project" → "New Project"
   - Project name: `TH-WEB-OAuth`
   - Organization: (để trống)
   - Click "Create"

## **BƯỚC 2: Enable Google+ API**

1. **Enable APIs:**
   - Vào "APIs & Services" → "Library"
   - Search "Google+ API" và enable
   - Search "Google Identity" và enable

## **BƯỚC 3: Configure OAuth Consent Screen**

1. **OAuth Consent Screen:**
   - Vào "APIs & Services" → "OAuth consent screen"
   - User Type: **External** (for testing)
   - Click "Create"

2. **App Information:**
   - App name: `TH WEB Application`
   - User support email: `your-email@gmail.com`
   - App logo: (optional)
   - App domain: `localhost:5001`
   - Developer contact: `your-email@gmail.com`
   - Click "Save and Continue"

3. **Scopes:**
   - Click "Add or Remove Scopes"
   - Select: 
     - `.../auth/userinfo.email`
     - `.../auth/userinfo.profile`
   - Click "Save and Continue"

4. **Test Users:**
   - Add your email addresses for testing
   - Click "Save and Continue"

## **BƯỚC 4: Create OAuth 2.0 Credentials**

1. **Create Credentials:**
   - Vào "APIs & Services" → "Credentials"
   - Click "+ Create Credentials" → "OAuth 2.0 Client IDs"

2. **Application Type:**
   - Application type: **Web application**
   - Name: `TH-WEB-OAuth-Client`

3. **Authorized URLs:**
   - **Authorized JavaScript origins:**
     ```
     https://localhost:5001
     http://localhost:5000
     ```
   
   - **Authorized redirect URIs:**
     ```
     https://localhost:5001/signin-google
     http://localhost:5000/signin-google
     ```

4. **Create:**
   - Click "Create"
   - **QUAN TRỌNG:** Copy `Client ID` và `Client Secret`

## **BƯỚC 5: Cập nhật Configuration**

1. **Update appsettings.Development.json:**
   ```json
   {
     "Authentication": {
       "Google": {
         "ClientId": "YOUR_GOOGLE_CLIENT_ID_HERE",
         "ClientSecret": "YOUR_GOOGLE_CLIENT_SECRET_HERE"
       }
     }
   }
   ```

2. **Replace YOUR_GOOGLE_CLIENT_ID_HERE và YOUR_GOOGLE_CLIENT_SECRET_HERE:**
   - Paste Client ID và Client Secret từ Google Console

## **BƯỚC 6: Test Google Login**

1. **Start Application:**
   ```bash
   cd D:\-A_THWEB
   dotnet run
   ```

2. **Test Google OAuth:**
   - Truy cập: https://localhost:5001/Identity/Account/LoginRegister
   - Click "Continue with Google"
   - Login bằng Google account
   - Kiểm tra được redirect về trang chủ

## **🔍 TROUBLESHOOTING**

### **Lỗi "redirect_uri_mismatch"**
- Kiểm tra Authorized redirect URIs trong Google Console
- Đảm bảo có: `https://localhost:5001/signin-google`

### **Lỗi "invalid_client"**
- Kiểm tra Client ID và Client Secret trong appsettings
- Đảm bảo không có spaces hoặc ký tự thừa

### **Lỗi "access_denied"**
- Thêm email vào Test Users trong OAuth consent screen
- Đảm bảo app đang ở Test mode

### **Lỗi "unauthorized_client"**
- Kiểm tra JavaScript origins có đúng URL không
- Đảm bảo HTTPS được enable

## **🎯 EXPECTED RESULT**

Khi setup thành công:
1. ✅ Nút "Continue with Google" không bị disabled
2. ✅ Click vào sẽ redirect đến Google OAuth
3. ✅ Sau khi login Google, tự động tạo user và đăng nhập
4. ✅ User mới có role "Customer" mặc định
5. ✅ Có thể xem user trong Admin → Users

## **📝 PRODUCTION NOTES**

### **Cho Production:**
1. Thay đổi OAuth consent screen thành "Internal" hoặc "External" verified
2. Update redirect URIs với domain thật
3. Store credentials trong Azure Key Vault hoặc secure environment
4. Never commit credentials vào Git

### **Security Best Practices:**
- Sử dụng HTTPS trong production
- Validate email domains nếu cần
- Implement proper error handling
- Log OAuth events for monitoring

## **🚀 QUICK START**

**Nếu chỉ muốn test nhanh:**
1. Tạo Google Cloud project
2. Enable APIs và OAuth consent
3. Tạo Web OAuth credentials
4. Copy Client ID/Secret vào appsettings.Development.json
5. Restart app và test

**Success indicator:** Nút Google không bị disabled và có thể login thành công!
