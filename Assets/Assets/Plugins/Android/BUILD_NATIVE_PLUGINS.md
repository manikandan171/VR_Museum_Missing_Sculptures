# Building Native Plugins for Android

## Overview

The `libvoiceai.so` files need to be built from source. The meta files are already configured, but you need to build the actual `.so` files.

## Quick Setup

### Option 1: Use Existing Build Scripts

1. Navigate to `Assets/Plugins/Native/`
2. Run the build script for Android:
   ```bash
   # On Windows
   build_android.bat
   
   # On Linux/Mac
   ./build_android.sh
   ```

### Option 2: Manual Build

1. **Prerequisites:**
   - Android NDK (r21e or later)
   - CMake (3.10 or later)
   - C++ compiler

2. **Build Steps:**
   ```bash
   cd Assets/Plugins/Native
   mkdir build_android
   cd build_android
   
   # For ARM64
   cmake .. -DCMAKE_TOOLCHAIN_FILE=$ANDROID_NDK/build/cmake/android.toolchain.cmake \
            -DANDROID_ABI=arm64-v8a \
            -DANDROID_PLATFORM=android-23
   cmake --build . --config Release
   cp libvoiceai.so ../../Android/libs/arm64-v8a/
   
   # For ARMv7
   cmake .. -DCMAKE_TOOLCHAIN_FILE=$ANDROID_NDK/build/cmake/android.toolchain.cmake \
            -DANDROID_ABI=armeabi-v7a \
            -DANDROID_PLATFORM=android-23
   cmake --build . --config Release
   cp libvoiceai.so ../../Android/libs/armeabi-v7a/
   ```

## File Structure After Build

```
Assets/Plugins/Android/libs/
├── arm64-v8a/
│   ├── libvoiceai.so          ← Build this file
│   └── libvoiceai.so.meta     ✅ Already configured
└── armeabi-v7a/
    ├── libvoiceai.so          ← Build this file
    └── libvoiceai.so.meta     ✅ Already configured
```

## Plugin Configuration (Already Done)

The `.meta` files are already configured with:
- ✅ Platform: Android
- ✅ CPU: ARM64 (for arm64-v8a)
- ✅ CPU: ARMv7 (for armeabi-v7a)
- ✅ Enabled: Yes

## Verification

After building, verify:
1. Files exist: `libvoiceai.so` in both folders
2. File size: Should be several MB (not 0 bytes)
3. Unity recognizes them: Check Inspector shows plugin settings

## Troubleshooting

### "Plugin not found" error:
- Verify `.so` files exist (not just `.meta` files)
- Check file permissions
- Rebuild if files are corrupted

### "Function not found" error:
- Verify plugin exports `TranscribeAudio` and `FreeString`
- Check build was successful
- Ensure correct architecture match

## Next Steps

Once plugins are built:
1. ✅ Plugin meta files configured
2. ✅ Model file in StreamingAssets/Models/
3. ✅ VoiceAIManager in scene
4. Ready to test on Android device!

