@echo off

adb push "%~1" /sdcard/DCIM/Camera/

adb shell am broadcast -a android.intent.action.MEDIA_SCANNER_SCAN_FILE -d file:///sdcard/DCIM/Camera/%~nx1

pause