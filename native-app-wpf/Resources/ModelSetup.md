# AI Tutor Model Setup

The Code Tutor AI assistant requires the Phi-4 model to be downloaded.

## Quick Setup (Windows)

1. Open PowerShell as Administrator
2. Run the following commands:

```powershell
# Install Python package manager for Hugging Face
pip install huggingface-hub

# Navigate to Code Tutor installation folder
cd "C:\Program Files\CodeTutor"

# Download the model (~2.5GB)
huggingface-cli download microsoft/Phi-4-mini-instruct-onnx --include gpu/gpu-int4-rtn-block-32/* --local-dir models/phi4
```

3. Restart Code Tutor

## Requirements

- DirectX 12 compatible GPU (NVIDIA, AMD, or Intel)
- ~3GB disk space
- ~4GB GPU memory recommended

## Troubleshooting

If the AI Tutor shows "Model not found":
- Verify the model files exist in the `models/phi4/gpu/gpu-int4-rtn-block-32` folder
- Ensure you have a DirectX 12 compatible GPU
- Try running Code Tutor as Administrator
