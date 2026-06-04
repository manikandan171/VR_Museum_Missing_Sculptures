# Build script for Whisper.cpp Android static libraries using NDK r19
# This script builds static .a files compatible with Unity 2019.4.3

# Configuration
$NDK_PATH = "D:\NDK"
$WHISPER_DIR = Get-Location
$BUILD_DIR = Join-Path $WHISPER_DIR "build-android-ndk19"
$ABI = "arm64-v8a"
$API_LEVEL = 28

Write-Host "=== Building Whisper.cpp for Android with NDK r19 ===" -ForegroundColor Cyan
Write-Host "NDK Path: $NDK_PATH"
Write-Host "Whisper Dir: $WHISPER_DIR"
Write-Host "Build Dir: $BUILD_DIR"
Write-Host "ABI: $ABI"
Write-Host "API Level: $API_LEVEL"
Write-Host ""

# Check if NDK exists
if (-not (Test-Path $NDK_PATH)) {
    Write-Host "ERROR: NDK not found at $NDK_PATH" -ForegroundColor Red
    exit 1
}

# Find cmake
$cmakeExe = "cmake.exe"
$cmakeCmd = Get-Command cmake -ErrorAction SilentlyContinue
if (-not $cmakeCmd) {
    # Try Unity's CMake
    $unityCmake = "C:\Program Files\Unity\Hub\Editor\6000.2.0f1\Editor\Data\PlaybackEngines\AndroidPlayer\SDK\cmake\3.22.1\bin\cmake.exe"
    if (Test-Path $unityCmake) {
        $cmakeExe = $unityCmake
    } else {
        Write-Host "ERROR: cmake not found. Please install CMake or ensure Unity is installed." -ForegroundColor Red
        exit 1
    }
}

# Create build directory
New-Item -ItemType Directory -Force -Path $BUILD_DIR | Out-Null

# Configure with CMake
Write-Host "=== Configuring CMake ===" -ForegroundColor Cyan
$cmakeArgs = @(
    "-DCMAKE_TOOLCHAIN_FILE=$NDK_PATH\build\cmake\android.toolchain.cmake",
    "-DANDROID_ABI=$ABI",
    "-DANDROID_PLATFORM=android-$API_LEVEL",
    "-DCMAKE_BUILD_TYPE=Release",
    "-DBUILD_SHARED_LIBS=OFF",
    "-DGGML_OPENMP=OFF",
    "-DGGML_LLAMAFILE=OFF",
    "-DWHISPER_BUILD_TESTS=OFF",
    "-DWHISPER_BUILD_EXAMPLES=OFF",
    "-DWHISPER_BUILD_SERVER=OFF",
    "-S", $WHISPER_DIR,
    "-B", $BUILD_DIR
)

& $cmakeExe $cmakeArgs
if ($LASTEXITCODE -ne 0) {
    Write-Host "ERROR: CMake configuration failed!" -ForegroundColor Red
    exit $LASTEXITCODE
}

# Build
Write-Host ""
Write-Host "=== Building libraries ===" -ForegroundColor Cyan
& $cmakeExe --build $BUILD_DIR --config Release -j
if ($LASTEXITCODE -ne 0) {
    Write-Host "ERROR: Build failed!" -ForegroundColor Red
    exit $LASTEXITCODE
}

Write-Host ""
Write-Host "=== Build complete! ===" -ForegroundColor Green
Write-Host "Static libraries should be in: $BUILD_DIR"
Write-Host ""
Write-Host "Copy the following files to your Unity project:" -ForegroundColor Yellow
Write-Host "  - $BUILD_DIR\src\libwhisper.a" -ForegroundColor White
Write-Host "  - $BUILD_DIR\ggml\src\libggml.a" -ForegroundColor White
Write-Host "  - $BUILD_DIR\ggml\src\libggml-base.a" -ForegroundColor White
Write-Host "  - $BUILD_DIR\ggml\src\ggml-cpu\libggml-cpu.a" -ForegroundColor White

