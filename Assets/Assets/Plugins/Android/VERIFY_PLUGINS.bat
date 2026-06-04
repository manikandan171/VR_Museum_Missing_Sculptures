@echo off
echo ========================================
echo Verifying Android Plugin Setup
echo ========================================
echo.

set ALL_GOOD=1

REM Check plugin files
echo Checking plugin files...
echo.

if exist "libs\arm64-v8a\libvoiceai.so" (
    echo ✅ libvoiceai.so found (arm64-v8a)
    for %%A in ("libs\arm64-v8a\libvoiceai.so") do echo    Size: %%~zA bytes
) else (
    echo ❌ libvoiceai.so MISSING (arm64-v8a)
    echo    Expected: libs\arm64-v8a\libvoiceai.so
    set ALL_GOOD=0
)

if exist "libs\armeabi-v7a\libvoiceai.so" (
    echo ✅ libvoiceai.so found (armeabi-v7a)
    for %%A in ("libs\armeabi-v7a\libvoiceai.so") do echo    Size: %%~zA bytes
) else (
    echo ❌ libvoiceai.so MISSING (armeabi-v7a)
    echo    Expected: libs\armeabi-v7a\libvoiceai.so
    set ALL_GOOD=0
)

echo.

REM Check meta files
echo Checking meta files...
echo.

if exist "libs\arm64-v8a\libvoiceai.so.meta" (
    echo ✅ Meta file found (arm64-v8a)
) else (
    echo ❌ Meta file MISSING (arm64-v8a)
    set ALL_GOOD=0
)

if exist "libs\armeabi-v7a\libvoiceai.so.meta" (
    echo ✅ Meta file found (armeabi-v7a)
) else (
    echo ❌ Meta file MISSING (armeabi-v7a)
    set ALL_GOOD=0
)

echo.

REM Check model file
echo Checking model file...
echo.

if exist "..\..\StreamingAssets\Models\ggml-base.bin" (
    echo ✅ Model file found
    for %%A in ("..\..\StreamingAssets\Models\ggml-base.bin") do echo    Size: %%~zA bytes
) else (
    echo ❌ Model file MISSING
    echo    Expected: StreamingAssets\Models\ggml-base.bin
    set ALL_GOOD=0
)

echo.

if %ALL_GOOD%==1 (
    echo ========================================
    echo ✅ ALL CHECKS PASSED!
    echo ========================================
    echo.
    echo Setup is complete. You can now:
    echo   1. Open Unity
    echo   2. Verify plugins in Inspector
    echo   3. Add VoiceAIManager to scene
    echo   4. Test on Android device
    echo.
) else (
    echo ========================================
    echo ❌ SETUP INCOMPLETE
    echo ========================================
    echo.
    echo Please fix the missing items above.
    echo.
)

pause

