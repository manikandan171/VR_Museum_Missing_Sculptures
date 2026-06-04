@echo off
echo ========================================
echo VoiceAI Android Plugin Builder
echo ========================================
echo.
echo This script will build libvoiceai.so for Android
echo and copy them to the correct Unity plugin folders.
echo.

REM Check if we're in the right directory
if not exist "..\..\UnityVoiceAI\Native" (
    echo ❌ Error: UnityVoiceAI\Native folder not found!
    echo Please run this script from: Assets\Plugins\Android\
    pause
    exit /b 1
)

echo ✅ Found UnityVoiceAI\Native folder
echo.

REM Navigate to build directory
cd ..\..\UnityVoiceAI\Native

echo Building from: %CD%
echo.

REM Check if build script exists
if exist "build_android_windows.bat" (
    echo ✅ Found build script, starting build...
    echo.
    call build_android_windows.bat
) else if exist "build_android.sh" (
    echo ⚠️  Found Linux build script, but you're on Windows
    echo Please use WSL or create build_android_windows.bat
    echo.
    pause
    exit /b 1
) else (
    echo ❌ Build script not found!
    echo Expected: build_android_windows.bat or build_android.sh
    pause
    exit /b 1
)

echo.
echo ========================================
echo Build process completed!
echo ========================================
pause

