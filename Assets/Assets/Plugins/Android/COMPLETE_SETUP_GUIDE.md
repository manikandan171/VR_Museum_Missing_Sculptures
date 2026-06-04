# Complete Android Plugin Setup Guide

## ✅ What's Already Done

1. ✅ **Plugin meta files created** with correct settings
2. ✅ **Model file** (`ggml-base.bin`) copied to StreamingAssets
3. ✅ **Directory structure** created
4. ✅ **Build scripts** created
5. ✅ **Code integration** complete

## 🔧 What You Need to Do

### Step 1: Install Prerequisites

#### 1.1 Android NDK
- **Download**: Via Android Studio → SDK Manager → SDK Tools → NDK
- **Or**: Download from https://developer.android.com/ndk/downloads
- **Set Environment Variable**:
  ```cmd
  set ANDROID_NDK_ROOT=C:\Users\YourName\AppData\Local\Android\Sdk\ndk\25.1.8937393
  ```
  (Replace with your actual NDK path)

#### 1.2 whisper.cpp
- **Download**: https://github.com/ggerganov/whisper.cpp
- **Extract to**: `Assets\whisper.cpp-master`
- **Or**: Clone: `git clone https://github.com/ggerganov/whisper.cpp.git Assets\whisper.cpp-master`

#### 1.3 llama.cpp
- **Download**: https://github.com/ggerganov/llama.cpp
- **Extract to**: `Assets\llama.cpp-master`
- **Or**: Clone: `git clone https://github.com/ggerganov/llama.cpp.git Assets\llama.cpp-master`

#### 1.4 CMake
- **Download**: https://cmake.org/download/
- **Install** and add to PATH
- **Verify**: Run `cmake --version` in command prompt

### Step 2: Build the Plugins

#### Option A: Automated Build (Recommended)
```cmd
cd Assets\Plugins\Android
CHECK_AND_BUILD.bat
```

This script will:
- ✅ Check all prerequisites
- ✅ Build for both ARM64 and ARMv7
- ✅ Copy files to correct locations
- ✅ Verify setup

#### Option B: Manual Build
```cmd
cd Assets\UnityVoiceAI\Native
build_android_windows.bat
```

### Step 3: Verify Setup

Run the verification script:
```cmd
cd Assets\Plugins\Android
VERIFY_PLUGINS.bat
```

This checks:
- ✅ Plugin files exist
- ✅ Meta files exist
- ✅ Model file exists
- ✅ File sizes are correct

### Step 4: Unity Inspector Verification

1. **Open Unity**
2. **Navigate to**: `Assets/Plugins/Android/libs/arm64-v8a/`
3. **Select**: `libvoiceai.so`
4. **In Inspector, verify**:
   - ✅ Platform: Android
   - ✅ CPU: ARM64
   - ✅ Enabled: ✓
5. **Repeat for**: `armeabi-v7a/libvoiceai.so` (CPU: ARMv7)

### Step 5: Add VoiceAIManager to Scene

1. **Create GameObject** in your scene
2. **Add Component**: `VoiceAIManager`
3. **Set Properties**:
   - `whisperModelPath`: `"ggml-base.bin"`
   - `initializeOnStart`: ✓ (checked)
4. **Save scene**

### Step 6: Test on Android Device

1. **Build for Android**
2. **Deploy to device**
3. **Check logs** for:
   - "VoiceAIManager found"
   - "Whisper initialized"
   - "Whisper transcription successful"

## 📋 Quick Checklist

- [ ] Android NDK installed and ANDROID_NDK_ROOT set
- [ ] whisper.cpp-master in Assets folder
- [ ] llama.cpp-master in Assets folder
- [ ] CMake installed and in PATH
- [ ] Built libvoiceai.so for arm64-v8a
- [ ] Built libvoiceai.so for armeabi-v7a
- [ ] Verified files in Unity Inspector
- [ ] VoiceAIManager added to scene
- [ ] Tested on Android device

## 🚀 Build Commands Summary

```cmd
REM Check prerequisites and build
cd Assets\Plugins\Android
CHECK_AND_BUILD.bat

REM Or build directly
cd Assets\UnityVoiceAI\Native
build_android_windows.bat

REM Verify setup
cd Assets\Plugins\Android
VERIFY_PLUGINS.bat
```

## 💡 Troubleshooting

### "NDK not found"
- Install Android NDK via Android Studio
- Set ANDROID_NDK_ROOT environment variable
- Restart command prompt after setting

### "whisper.cpp not found"
- Download from GitHub
- Extract to `Assets\whisper.cpp-master`
- Ensure folder name is exactly `whisper.cpp-master`

### "Build failed"
- Check CMake version (3.10+)
- Verify NDK version (r21+)
- Check build logs for specific errors

### "Plugin not recognized in Unity"
- Ensure `.so` files exist (not just `.meta`)
- Check file sizes (should be several MB)
- Verify Inspector settings match meta file

## ✅ Success Indicators

After successful build:
- ✅ `libvoiceai.so` files exist in both folders
- ✅ File sizes are several MB (not 0 bytes)
- ✅ Unity Inspector shows correct settings
- ✅ No errors in Unity console
- ✅ VoiceAIManager initializes successfully

## 🎯 You're Ready!

Once plugins are built and verified, your mobile Whisper speech recognition is ready to use!

