# Android Plugin Build Instructions

## 🎯 Quick Start

### Automated Build (Recommended)
```cmd
cd Assets\Plugins\Android
CHECK_AND_BUILD.bat
```

This will:
1. ✅ Check all prerequisites
2. ✅ Build for ARM64 and ARMv7
3. ✅ Copy files to correct locations
4. ✅ Verify setup

## 📋 Prerequisites Checklist

Before building, ensure you have:

- [ ] **Android NDK** installed (r21+)
  - Set: `set ANDROID_NDK_ROOT=C:\path\to\ndk`
- [ ] **whisper.cpp-master** in `Assets\` folder
  - Download: https://github.com/ggerganov/whisper.cpp
- [ ] **llama.cpp-master** in `Assets\` folder
  - Download: https://github.com/ggerganov/llama.cpp
- [ ] **CMake** installed and in PATH
  - Download: https://cmake.org/download/

## 🔨 Build Process

### Step 1: Check Prerequisites
```cmd
cd Assets\Plugins\Android
CHECK_AND_BUILD.bat
```

### Step 2: Build (if prerequisites OK)
The script will automatically:
- Build `libvoiceai.so` for arm64-v8a
- Build `libvoiceai.so` for armeabi-v7a
- Copy to `Assets\Plugins\Android\libs\`

### Step 3: Verify
```cmd
cd Assets\Plugins\Android
VERIFY_PLUGINS.bat
```

## 📁 Expected Output

After successful build:

```
Assets/Plugins/Android/libs/
├── arm64-v8a/
│   ├── libvoiceai.so          ← Built file (several MB)
│   └── libvoiceai.so.meta     ✅ Already configured
└── armeabi-v7a/
    ├── libvoiceai.so          ← Built file (several MB)
    └── libvoiceai.so.meta     ✅ Already configured
```

## ✅ Unity Verification

1. Open Unity
2. Select `libvoiceai.so` in `arm64-v8a/` folder
3. Inspector should show:
   - Platform: Android ✓
   - CPU: ARM64 ✓
   - Enabled: ✓

4. Repeat for `armeabi-v7a/` (CPU: ARMv7)

## 🚀 Ready to Test!

Once built and verified:
- ✅ Plugins configured
- ✅ Model file ready
- ✅ Code integrated
- ✅ Ready for Android deployment!

