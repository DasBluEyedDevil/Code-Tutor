# AI Tutor Model Setup

The Code Tutor AI assistant uses the Phi-4 model for intelligent tutoring.

## Automatic Download (Recommended)

When you first open the AI Tutor panel, Code Tutor will automatically offer to download the required model (~2.5GB). Simply click "Download Model" and wait for the download to complete.

## Requirements

- DirectX 12 compatible GPU (NVIDIA, AMD, or Intel)
- ~3GB disk space
- ~4GB GPU memory recommended
- Internet connection for initial download

## Manual Setup (Alternative)

If automatic download fails, you can download manually:

1. Open PowerShell
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

## Troubleshooting

If the AI Tutor shows "Model not found":
- Try the automatic download again
- Verify the model files exist in the `models/phi4/gpu/gpu-int4-rtn-block-32` folder
- Ensure you have a DirectX 12 compatible GPU
- Check your internet connection
- Try running Code Tutor as Administrator
