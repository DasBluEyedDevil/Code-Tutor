# ONNX Conversion Setup for Qwen2.5-Coder-7B

## Prerequisites

Install the required Python packages:

```bash
pip install onnxruntime-genai transformers torch onnx optimum[exporters]
```

## Conversion Script

Create a file named `convert_qwen.py`:

```python
import argparse
import os
from pathlib import Path
from transformers import AutoTokenizer, AutoModelForCausalLM
import torch

def convert_qwen_to_onnx(model_id: str, output_dir: str):
    """
    Convert Qwen2.5-Coder-7B to ONNX format using Optimum.
    """
    print(f"Loading model: {model_id}")
    
    # Load tokenizer and model
    tokenizer = AutoTokenizer.from_pretrained(model_id, trust_remote_code=True)
    model = AutoModelForCausalLM.from_pretrained(
        model_id,
        trust_remote_code=True,
        torch_dtype=torch.float16,
        device_map="auto"
    )
    
    output_path = Path(output_dir)
    output_path.mkdir(parents=True, exist_ok=True)
    
    print(f"Converting to ONNX...")
    
    # Export to ONNX using Optimum
    from optimum.exporters.onnx import main_export
    
    main_export(
        model_name_or_path=model_id,
        output=output_path,
        task="text-generation",
        trust_remote_code=True,
        fp16=True,
        optimize="O4",  # Aggressive optimization
    )
    
    print(f"Conversion complete! Files saved to: {output_dir}")
    print(f"\nFiles created:")
    for f in output_path.iterdir():
        size_mb = f.stat().st_size / (1024 * 1024)
        print(f"  - {f.name}: {size_mb:.1f} MB")

if __name__ == "__main__":
    parser = argparse.ArgumentParser()
    parser.add_argument(
        "--model-id",
        default="Qwen/Qwen2.5-Coder-7B-Instruct",
        help="HuggingFace model ID"
    )
    parser.add_argument(
        "--output",
        default="./qwen2.5-coder-7b-onnx",
        help="Output directory"
    )
    args = parser.parse_args()
    
    convert_qwen_to_onnx(args.model_id, args.output)
```

## Run Conversion

```bash
python convert_qwen.py --model-id Qwen/Qwen2.5-Coder-7B-Instruct --output ./qwen2.5-coder-7b-onnx
```

This will:
1. Download the Qwen2.5-Coder-7B model (~15GB)
2. Convert to ONNX format with INT4 quantization
3. Save to `./qwen2.5-coder-7b-onnx/` (~4-5GB)

## Upload to HuggingFace

After conversion, upload to your HuggingFace account:

```bash
pip install huggingface-hub

huggingface-cli login
# Enter your token

huggingface-cli upload your-username/Qwen2.5-Coder-7B-Instruct-ONNX ./qwen2.5-coder-7b-onnx .
```

## Update App

Once uploaded, update the ModelDownloadService with your repo:

```csharp
HuggingFaceRepo = "your-username/Qwen2.5-Coder-7B-Instruct-ONNX",
ModelPath = "",  // Files at root
```

## Notes

- Conversion requires ~20GB RAM
- Takes 30-60 minutes depending on hardware
- GPU acceleration recommended but not required
- Final model will be ~4-5GB with INT4 quantization
