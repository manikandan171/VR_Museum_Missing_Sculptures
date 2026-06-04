@echo off
REM Quick build script - runs from Assets root
cd /d "%~dp0"
cd ..\..\UnityVoiceAI\Native

if exist "build_android_windows.bat" (
    call build_android_windows.bat
) else (
    echo Building Android plugins...
    echo.
    echo Prerequisites:
    echo   1. Android NDK installed
    echo   2. ANDROID_NDK_ROOT environment variable set
    echo   3. whisper.cpp-master in Assets folder
    echo   4. llama.cpp-master in Assets folder
    echo.
    echo Run: UnityVoiceAI\Native\build_android_windows.bat
    pause
)

