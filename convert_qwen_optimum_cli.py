# Install optimum for ONNX export
!pip install -q optimum[exporters]

# Use optimum-cli to export
!optimum-cli export onnx \
    --model Qwen/Qwen2.5-Coder-1.5B-Instruct \
    --task text-generation \
    --trust-remote-code \
    /content/qwen2.5-coder-onnx

print("✓ Export complete!")
