# Hướng dẫn Build và Chạy

## Cách 1: Sử dụng file .bat (Đơn giản nhất)

### Build ứng dụng:
1. Double-click vào file `build.bat`
2. Chờ quá trình build hoàn tất
3. Ứng dụng sẽ tự động mở sau khi build xong

### Chạy ứng dụng (sau khi đã build):
- Double-click vào file `run.bat`

---

## Cách 2: Sử dụng Command Line

### Yêu cầu:
- Cài đặt .NET 6.0 SDK: https://dotnet.microsoft.com/download/dotnet/6.0

### Build:
```cmd
cd ScrcpyController
dotnet restore
dotnet build --configuration Release
```

### Chạy:
```cmd
cd bin\Release\net6.0-windows
ScrcpyController.exe
```

---

## Cách 3: Sử dụng Visual Studio

1. Mở file `ScrcpyController.csproj` bằng Visual Studio 2022
2. Nhấn F5 để chạy hoặc Ctrl+Shift+B để build
3. File exe sẽ nằm trong `bin\Release\net6.0-windows\`

---

## Lưu ý quan trọng:

⚠️ **Nếu chưa cài .NET 6.0 SDK:**
- Tải tại: https://dotnet.microsoft.com/download/dotnet/6.0
- Chọn ".NET 6.0 SDK" (không phải Runtime)
- Cài đặt và khởi động lại Command Prompt

⚠️ **Đảm bảo các file sau nằm cùng thư mục với ScrcpyController.exe:**
- adb.exe (copy từ thư mục gốc)
- scrcpy.exe (copy từ thư mục gốc)
- scrcpy-server (copy từ thư mục gốc)
- Các .dll cần thiết của scrcpy

📁 **Cấu trúc thư mục đề xuất:**
```
scrcpy-win64-v3.3.4/
├── adb.exe
├── scrcpy.exe
├── scrcpy-server
├── ScrcpyController/
│   ├── build.bat          ← Chạy file này để build
│   ├── run.bat            ← Chạy file này để chạy app
│   ├── Program.cs
│   ├── MainForm.cs
│   └── ...
```

Hoặc copy ScrcpyController.exe vào thư mục gốc sau khi build.
