from optimum.exporters.onnx import main_export
from pathlib import Path
import sys

print("=" * 50)
print("Qwen2.5-Coder-7B ONNX Converter")
print("=" * 50)
print()

output_dir = Path("./qwen2.5-coder-7b-onnx")
output_dir.mkdir(exist_ok=True)

print("Downloading and converting Qwen2.5-Coder-7B...")
print("This will download ~15GB and convert to ONNX (~4-5GB)")
print("Estimated time: 30-60 minutes")
print()

try:
    main_export(
        model_name_or_path="Qwen/Qwen2.5-Coder-7B-Instruct",
        output=output_dir,
        task="text-generation",
        trust_remote_code=True,
        fp16=True,
        optimize="O4",
    )

    print()
    print("=" * 50)
    print("Conversion complete!")
    print("=" * 50)
    print()
    print(f"Files saved to: {output_dir.absolute()}")
    print()
    print("Files created:")
    for f in output_dir.iterdir():
        size_mb = f.stat().st_size / (1024 * 1024)
        print(f"  - {f.name}: {size_mb:.1f} MB")
    print()
    print("Next steps:")
    print("1. Upload to HuggingFace:")
    print("   python -m pip install huggingface-hub")
    print("   huggingface-cli login")
    print(
        "   huggingface-cli upload your-username/Qwen2.5-Coder-7B-Instruct-ONNX ./qwen2.5-coder-7b-onnx ."
    )

except Exception as e:
    print(f"Error: {e}")
    sys.exit(1)
