@echo off
echo ========================================
echo VoiceAI Android Plugin - Build Setup
echo ========================================
echo.

REM Check prerequisites
set MISSING=0

echo Checking prerequisites...
echo.

REM Check Android NDK
if "%ANDROID_NDK_ROOT%"=="" (
    if "%ANDROID_NDK%"=="" (
        echo ❌ ANDROID_NDK_ROOT not set
        set MISSING=1
    ) else (
        echo ✅ ANDROID_NDK found: %ANDROID_NDK%
        set ANDROID_NDK_ROOT=%ANDROID_NDK%
    )
) else (
    echo ✅ ANDROID_NDK_ROOT found: %ANDROID_NDK_ROOT%
)

REM Check whisper.cpp
if not exist "..\..\..\whisper.cpp-master" (
    echo ❌ whisper.cpp-master not found
    echo    Expected: Assets\whisper.cpp-master
    set MISSING=1
) else (
    echo ✅ whisper.cpp-master found
)

REM Check llama.cpp
if not exist "..\..\..\llama.cpp-master" (
    echo ❌ llama.cpp-master not found
    echo    Expected: Assets\llama.cpp-master
    set MISSING=1
) else (
    echo ✅ llama.cpp-master found
)

REM Check CMake
where cmake >nul 2>nul
if %errorlevel% neq 0 (
    echo ❌ CMake not found in PATH
    set MISSING=1
) else (
    echo ✅ CMake found
)

echo.

if %MISSING%==1 (
    echo ========================================
    echo ❌ Prerequisites Missing!
    echo ========================================
    echo.
    echo Please install/setup:
    echo.
    if "%ANDROID_NDK_ROOT%"=="" (
        echo   1. Android NDK
        echo      - Install via Android Studio SDK Manager
        echo      - Set: set ANDROID_NDK_ROOT=C:\path\to\ndk
    )
    if not exist "..\..\..\whisper.cpp-master" (
        echo   2. whisper.cpp
        echo      - Download: https://github.com/ggerganov/whisper.cpp
        echo      - Extract to: Assets\whisper.cpp-master
    )
    if not exist "..\..\..\llama.cpp-master" (
        echo   3. llama.cpp
        echo      - Download: https://github.com/ggerganov/llama.cpp
        echo      - Extract to: Assets\llama.cpp-master
    )
    where cmake >nul 2>nul
    if %errorlevel% neq 0 (
        echo   4. CMake
        echo      - Download: https://cmake.org/download/
        echo      - Add to PATH
    )
    echo.
    pause
    exit /b 1
)

echo ========================================
echo ✅ All Prerequisites Found!
echo ========================================
echo.
echo Ready to build Android plugins...
echo.
echo This will build:
echo   - libvoiceai.so for arm64-v8a (64-bit)
echo   - libvoiceai.so for armeabi-v7a (32-bit)
echo.
echo This may take 10-30 minutes depending on your system.
echo.
set /p CONTINUE="Continue with build? (Y/N): "
if /i not "%CONTINUE%"=="Y" (
    echo Build cancelled.
    pause
    exit /b 0
)

echo.
echo Starting build...
echo.

REM Navigate to build directory
cd ..\..\..\UnityVoiceAI\Native

if exist "build_android_windows.bat" (
    call build_android_windows.bat
) else (
    echo ❌ Build script not found!
    echo Expected: UnityVoiceAI\Native\build_android_windows.bat
    pause
    exit /b 1
)

echo.
echo ========================================
echo Build Process Complete!
echo ========================================
echo.
echo Next: Verify files in Unity Inspector
pause

