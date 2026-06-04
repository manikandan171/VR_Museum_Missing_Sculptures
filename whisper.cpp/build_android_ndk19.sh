#!/bin/bash
# Build script for Whisper.cpp Android static libraries using NDK r19
# This script builds static .a files compatible with Unity 2019.4.3

set -e

# Configuration
NDK_PATH="D:/NDK"
WHISPER_DIR="$PWD"
BUILD_DIR="$WHISPER_DIR/build-android-ndk19"
ABI="arm64-v8a"
API_LEVEL=28

echo "=== Building Whisper.cpp for Android with NDK r19 ==="
echo "NDK Path: $NDK_PATH"
echo "Whisper Dir: $WHISPER_DIR"
echo "Build Dir: $BUILD_DIR"
echo "ABI: $ABI"
echo "API Level: $API_LEVEL"

# Check if NDK exists
if [ ! -d "$NDK_PATH" ]; then
    echo "ERROR: NDK not found at $NDK_PATH"
    exit 1
fi

# Create build directory
mkdir -p "$BUILD_DIR"

# Configure with CMake
cmake \
    -DCMAKE_TOOLCHAIN_FILE="$NDK_PATH/build/cmake/android.toolchain.cmake" \
    -DANDROID_ABI="$ABI" \
    -DANDROID_PLATFORM=android-$API_LEVEL \
    -DCMAKE_BUILD_TYPE=Release \
    -DBUILD_SHARED_LIBS=OFF \
    -DGGML_OPENMP=OFF \
    -DGGML_LLAMAFILE=OFF \
    -DWHISPER_BUILD_TESTS=OFF \
    -DWHISPER_BUILD_EXAMPLES=OFF \
    -DWHISPER_BUILD_SERVER=OFF \
    -S "$WHISPER_DIR" \
    -B "$BUILD_DIR"

# Build
echo ""
echo "=== Building libraries ==="
cmake --build "$BUILD_DIR" --config Release -j

echo ""
echo "=== Build complete! ==="
echo "Static libraries should be in: $BUILD_DIR"
echo ""
echo "Copy the following files to your Unity project:"
echo "  - $BUILD_DIR/src/libwhisper.a"
echo "  - $BUILD_DIR/ggml/src/libggml.a"
echo "  - $BUILD_DIR/ggml/src/libggml-base.a"
echo "  - $BUILD_DIR/ggml/src/ggml-cpu/libggml-cpu.a"

