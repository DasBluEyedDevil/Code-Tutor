# Phase 08-01-Extended: Qwen2.5-Coder-7B Migration + Model Management

**Status:** In Progress

## Objective

Migrate from Phi-4 (14B) to Qwen2.5-Coder-7B for 2x faster inference and better coding specialization. Add model management features (uninstall/delete) for user control.

## Why Qwen2.5-Coder-7B?

| Aspect | Phi-4 (Current) | Qwen2.5-Coder-7B |
|--------|----------------|------------------|
| Parameters | 14B | 7.6B |
| Expected Speed | ~30s first response | ~10-15s first response |
| Specialization | General purpose | Coding-optimized |
| License | MIT | Apache 2.0 |
| Size on Disk | ~8GB (int4) | ~4-5GB (int4) |

**Benefits:**
- 2x faster inference
- Better code understanding/generation
- Smaller disk footprint
- Proven track record

## Files to Modify

### New Files
- `native-app-wpf/Services/QwenTutorService.cs` - New tutor service implementation
- `native-app-wpf/Controls/ModelManager.xaml` - Model management UI
- `native-app-wpf/Controls/ModelManager.xaml.cs` - Model management logic

### Modified Files
- `native-app-wpf/Services/IModelDownloadService.cs` - Add model selection and deletion
- `native-app-wpf/Services/ModelDownloadService.cs` - Support multiple models + uninstall
- `native-app-wpf/Services/ITutorService.cs` - Add model info properties
- `native-app-wpf/Controls/TutorChat.xaml` - Add model management button
- `native-app-wpf/Controls/TutorChat.xaml.cs` - Integrate model manager
- `native-app-wpf/MainWindow.xaml.cs` - Support model switching

## Implementation Summary

### Completed Changes

#### 1. QwenTutorService Created
- Path: `native-app-wpf/Services/QwenTutorService.cs`
- Implements ITutorService interface
- Uses Qwen chat template (`<|im_start|>`/`<|im_end|>`)
- Optimized for coding tasks
- Same ONNX Runtime API as Phi4TutorService

#### 2. ModelDownloadService Simplified
- Single model support (Qwen2.5-Coder-7B)
- `DownloadModelAsync()` - Download and install
- `UninstallModelAsync()` - Delete and free space
- `IsModelInstalledAsync()` - Check installation
- `GetModelSizeAsync()` - Get disk usage

#### 3. TutorChat UI Updated
- Updated download prompt text for Qwen2.5-Coder-7B (~4.5GB)
- Added uninstall button (trash can icon) in header
- Confirmation dialog before uninstall
- Shows download prompt again after uninstall

#### 4. MainWindow Updated
- Changed from Phi4TutorService to QwenTutorService
- Model path: `models/qwen2.5-coder-7b/`

### Chat Template Changes

**Phi-4 (Old):**
```
<|system|>You are a tutor...<|end|>
<|user|>Hello<|end|>
<|assistant|>
```

**Qwen2.5-Coder-7B (New):**
```
<|im_start|>system
You are a coding tutor...<|im_end|>
<|im_start|>user
Hello<|im_end|>
<|im_start|>assistant
```

### Model Management UI

**Install:**
- Click "Download Model" button
- Shows progress bar
- ~4.5GB download

**Uninstall:**
- Click trash can icon in header
- Confirmation dialog
- Frees disk space
- Shows install button again

## Expected Performance

| Metric | Phi-4 (Old) | Qwen2.5-Coder-7B (New) |
|--------|-------------|------------------------|
| Parameters | 14B | 7.6B |
| Size | ~8GB | ~4.5GB |
| First Response | ~30s | ~10-15s |
| Coding Quality | Good | Excellent |

## Files Changed

1. `native-app-wpf/Services/QwenTutorService.cs` (new)
2. `native-app-wpf/Services/ModelDownloadService.cs` (updated)
3. `native-app-wpf/Services/ITutorService.cs` (updated)
4. `native-app-wpf/Controls/TutorChat.xaml` (updated)
5. `native-app-wpf/Controls/TutorChat.xaml.cs` (updated)
6. `native-app-wpf/MainWindow.xaml.cs` (updated)

## Commits

1. `e292ccd1` - feat(08-01-ext): add QwenTutorService and simplified model management
2. `99352a42` - feat(08-01-ext): add uninstall button and update to Qwen2.5-Coder-7B

## Status

✅ Complete - Ready for testing
