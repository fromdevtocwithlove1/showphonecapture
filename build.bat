@echo off
echo ========================================
echo  Scrcpy Controller - Build Script
echo ========================================
echo.

REM Kiểm tra .NET có được cài đặt chưa
dotnet --version >nul 2>&1
if errorlevel 1 (
    echo [LỖI] .NET 6.0 SDK chưa được cài đặt!
    echo.
    echo Vui lòng tải và cài đặt .NET 6.0 SDK từ:
    echo https://dotnet.microsoft.com/download/dotnet/6.0
    echo.
    pause
    exit /b 1
)

echo [OK] Đã tìm thấy .NET SDK
dotnet --version
echo.

echo Đang restore packages...
dotnet restore
if errorlevel 1 (
    echo [LỖI] Không thể restore packages
    pause
    exit /b 1
)
echo.

echo Đang build ứng dụng...
dotnet build --configuration Release
if errorlevel 1 (
    echo [LỖI] Build thất bại
    pause
    exit /b 1
)
echo.

echo ========================================
echo  Build thành công!
echo ========================================
echo.
echo File thực thi tại:
echo bin\Release\net6.0-windows\ScrcpyController.exe
echo.
echo Nhấn phím bất kỳ để chạy ứng dụng...
pause >nul

cd bin\Release\net6.0-windows
start ScrcpyController.exe
