---
type: "EXAMPLE"
title: "Automating with Fastlane"
---

Use Fastlane for automated deployments:

```ruby
# Install Fastlane
gem install fastlane

# Initialize in your Android directory
cd composeApp
fastlane init

# Create Fastfile
# fastlane/Fastfile
default_platform(:android)

platform :android do
  desc "Deploy to internal testing track"
  lane :internal do
    gradle(
      task: "bundle",
      build_type: "Release"
    )
    upload_to_play_store(
      track: "internal",
      aab: "build/outputs/bundle/release/composeApp-release.aab",
      json_key: "play-store-key.json"
    )
  end

  desc "Promote internal to production"
  lane :promote_to_production do
    upload_to_play_store(
      track: "internal",
      track_promote_to: "production",
      json_key: "play-store-key.json",
      skip_upload_aab: true,
      skip_upload_metadata: true,
      skip_upload_images: true,
      skip_upload_screenshots: true
    )
  end

  desc "Deploy directly to production"
  lane :production do
    gradle(
      task: "bundle",
      build_type: "Release"
    )
    upload_to_play_store(
      track: "production",
      aab: "build/outputs/bundle/release/composeApp-release.aab",
      json_key: "play-store-key.json"
    )
  end
end

# Run deployment
fastlane internal   # Deploy to internal testing
fastlane production # Deploy to production
```
