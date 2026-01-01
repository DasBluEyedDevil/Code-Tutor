default_platform(:android)

platform :android do
  desc "Run unit tests"
  lane :test do
    gradle(task: "testDebugUnitTest")
  end

  desc "Build release bundle"
  lane :build do
    gradle(
      task: "bundleRelease",
      properties: {
        "android.injected.signing.store.file" => ENV["KEYSTORE_PATH"],
        "android.injected.signing.store.password" => ENV["KEYSTORE_PASSWORD"],
        "android.injected.signing.key.alias" => ENV["KEY_ALIAS"],
        "android.injected.signing.key.password" => ENV["KEY_PASSWORD"]
      }
    )
  end

  desc "Deploy to internal testing"
  lane :deploy_internal do
    test
    build
    upload_to_play_store(
      track: "internal",
      aab: "build/outputs/bundle/release/composeApp-release.aab",
      skip_upload_metadata: true,
      skip_upload_images: true
    )
  end

  desc "Deploy to production"
  lane :deploy_production do |options|
    changelog = options[:changelog] || "Bug fixes and improvements"
    
    test
    build
    upload_to_play_store(
      track: "production",
      aab: "build/outputs/bundle/release/composeApp-release.aab",
      release_status: "completed",
      rollout: "1.0"
    )
    
    # Update changelog
    File.write("metadata/android/en-US/changelogs/default.txt", changelog)
  end
end