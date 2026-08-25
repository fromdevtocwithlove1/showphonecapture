@echo off
echo ========================================
echo  Scrcpy Controller - Chạy ứng dụng
echo ========================================
echo.

if not exist "bin\Release\net6.0-windows\ScrcpyController.exe" (
    echo [LỖI] Chưa build ứng dụng!
    echo Vui lòng chạy build.bat trước.
    echo.
    pause
    exit /b 1
)

echo Đang khởi động ứng dụng...
cd bin\Release\net6.0-windows
start ScrcpyController.exe

echo.
echo Ứng dụng đã được khởi động!
