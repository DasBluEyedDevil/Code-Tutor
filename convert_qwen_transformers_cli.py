# Alternative: Use transformers-cli to export
# This uses the older export method

!pip install -q transformers[onnx]

# Export using transformers CLI
!transformers-cli convert --model_type qwen2 \
    --model_name_or_path Qwen/Qwen2.5-Coder-1.5B-Instruct \
    --output /content/qwen2.5-coder-onnx \
    --framework pt \
    --opset 14

print("✓ Export complete!")
