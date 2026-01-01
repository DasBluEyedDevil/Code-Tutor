---
type: "EXAMPLE"
title: "Complete Android Fastfile"
---

Full Fastlane configuration for Android:

```ruby
# composeApp/fastlane/Fastfile
default_platform(:android)

platform :android do
  desc "Run unit tests"
  lane :test do
    gradle(task: "testDebugUnitTest")
  end

  desc "Build debug APK"
  lane :build_debug do
    gradle(
      task: "assembleDebug",
      print_command: true
    )
  end

  desc "Build release bundle"
  lane :build_release do
    gradle(
      task: "bundleRelease",
      print_command: true,
      properties: {
        "android.injected.signing.store.file" => ENV["KEYSTORE_PATH"],
        "android.injected.signing.store.password" => ENV["KEYSTORE_PASSWORD"],
        "android.injected.signing.key.alias" => ENV["KEY_ALIAS"],
        "android.injected.signing.key.password" => ENV["KEY_PASSWORD"]
      }
    )
  end

  desc "Deploy to internal testing"
  lane :internal do
    build_release
    upload_to_play_store(
      track: "internal",
      aab: "build/outputs/bundle/release/composeApp-release.aab",
      skip_upload_metadata: true,
      skip_upload_images: true,
      skip_upload_screenshots: true
    )
  end

  desc "Promote internal to beta"
  lane :promote_to_beta do
    upload_to_play_store(
      track: "internal",
      track_promote_to: "beta",
      skip_upload_aab: true,
      skip_upload_metadata: true
    )
  end

  desc "Deploy to production"
  lane :production do
    build_release
    upload_to_play_store(
      track: "production",
      aab: "build/outputs/bundle/release/composeApp-release.aab",
      release_status: "completed",  # or "draft", "halted"
      rollout: "1.0"  # 100% rollout, use "0.1" for 10%
    )
  end

  desc "Increment version code"
  lane :bump_version do
    # Read current version
    version_code = google_play_track_version_codes(
      track: "internal"
    ).max || 0
    
    new_version_code = version_code + 1
    
    # Update build.gradle.kts
    # This requires a custom approach or gradle task
    UI.success("Next version code: #{new_version_code}")
  end
end
```
