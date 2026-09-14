# Scrcpy Controller - ADB Manager

Ứng dụng quản lý kết nối ADB và Scrcpy cho Android, giúp điều khiển thiết bị Android từ xa qua WiFi.

## Tính năng

✅ **ADB TCP/IP 5555** - Cài đặt kết nối ADB qua WiFi  
✅ **ADB Wireless Pairing** - Ghép đôi Android 11+ bằng IP:cổng và mã ghép đôi  
✅ **Tắt màn hình** - Tùy chọn tắt màn hình thiết bị khi kết nối  
✅ **Độ phân giải 720p** - Tùy chọn giới hạn độ phân giải để tăng hiệu suất  
✅ **Lưu IP** - Tự động lưu lịch sử các IP đã kết nối  
✅ **Giao diện trực quan** - Dễ sử dụng với các tùy chọn rõ ràng  

## Yêu cầu hệ thống

- Windows 10/11
- .NET 6.0 hoặc cao hơn
- ADB (Android Debug Bridge)
- Scrcpy

## Cách cài đặt

### Bước 1: Cài đặt .NET 6.0 Runtime
Tải và cài đặt từ: https://dotnet.microsoft.com/download/dotnet/6.0

### Bước 2: Build ứng dụng

```powershell
cd ScrcpyController
dotnet restore
dotnet build --configuration Release
```

### Bước 3: Chạy ứng dụng

```powershell
cd bin\Release\net6.0-windows
.\ScrcpyController.exe
```

## Hướng dẫn sử dụng

### Lần đầu tiên kết nối:

1. **Kết nối thiết bị qua USB** - Cắm dây USB vào máy tính
2. **Bật USB Debugging** trên thiết bị Android (Settings > Developer Options > USB Debugging)
3. **Nhấn "Cài đặt TCP/IP"** - Thiết lập kết nối qua cổng 5555
4. **Rút dây USB** sau khi cài đặt thành công
5. **Nhập IP thiết bị** (Ví dụ: 192.168.1.100)
   - Xem IP trong Settings > About Phone > Status > IP Address
6. **Chọn tùy chọn** (nếu cần):
   - ☑ Tắt màn hình khi kết nối
   - ☑ Độ phân giải 720p
7. **Nhấn "Kết nối Scrcpy"** để bắt đầu

### Những lần sau:

1. Đảm bảo thiết bị và máy tính cùng mạng WiFi
2. Chọn IP từ lịch sử (double-click) hoặc nhập IP mới
3. Chọn tùy chọn và nhấn "Kết nối Scrcpy"

### Ghép đôi Android 11+:

1. Trên điện thoại mở **Developer options > Wireless debugging**.
2. Chọn **Pair device with pairing code** và giữ màn hình này mở.
3. Nhập địa chỉ `IP:cổng` đang hiển thị vào ô **Pair IP:cổng**.
4. Nhập mã 6 số vào ô **Mã**, sau đó nhấn **Ghép đôi ADB**.
5. Sau khi ghép đôi thành công, nhập **IP:cổng kết nối** đang hiển thị trong Wireless debugging vào ô **IP hoặc IP:cổng**, rồi nhấn **Kết nối Scrcpy**. Nếu chỉ nhập IP, ứng dụng dùng cổng cũ `5555`. Tính năng ghép đôi không tự kết nối thiết bị.

## Cấu trúc thư mục

```
scrcpy-win64-v3.3.4/
├── adb.exe              # Android Debug Bridge
├── scrcpy.exe           # Scrcpy chính
├── scrcpy-server        # Server cho scrcpy
└── ScrcpyController/    # Ứng dụng controller
    ├── Program.cs
    ├── MainForm.cs
    ├── MainForm.Designer.cs
    └── ScrcpyController.csproj
```

## Xử lý lỗi

### "Không tìm thấy adb.exe hoặc scrcpy.exe"
- Đảm bảo `adb.exe` và `scrcpy.exe` nằm cùng thư mục với ứng dụng hoặc trong PATH

### "Không thể kết nối"
- Kiểm tra thiết bị và máy tính cùng mạng WiFi
- Thử chạy lại "Cài đặt TCP/IP" với USB
- Kiểm tra tường lửa (firewall) không chặn cổng 5555

### "Scrcpy không khởi động"
- Đảm bảo file `scrcpy-server` nằm cùng thư mục với `scrcpy.exe`
- Kiểm tra thiết bị đã được ủy quyền USB Debugging

## Ghi chú

- IP được lưu tự động vào file `ip_history.json`
- Tối đa 10 IP gần nhất được lưu lại
- Ứng dụng tự động tìm `adb.exe` và `scrcpy.exe` trong thư mục hiện tại

## Phím tắt Scrcpy

Khi Scrcpy đang chạy:
- `Ctrl+S` - Chụp màn hình
- `Ctrl+O` - Tắt/bật màn hình thiết bị
- `Ctrl+Shift+O` - Bật màn hình thiết bị
- `Ctrl+R` - Xoay màn hình
- `Ctrl+F` - Toàn màn hình
- `Alt+←` - Quay lại (Back)
- `Home` - Về trang chủ
- `End` - Bật/tắt màn hình

## License

Free to use - Sử dụng tự do

## Tác giả

Created with ❤️ for Android developers
