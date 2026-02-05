@echo off
REM Convert Qwen2.5-Coder-7B to ONNX

echo ==========================================
echo Qwen2.5-Coder-7B ONNX Converter
echo ==========================================
echo.

REM Check if Python is installed
python --version >nul 2>&1
if errorlevel 1 (
    echo ERROR: Python is not installed or not in PATH
    echo Please install Python 3.9+ from https://python.org
    pause
    exit /b 1
)

echo Step 1: Installing required packages...
pip install -q onnxruntime-genai transformers torch onnx optimum[exporters] huggingface-hub

echo.
echo Step 2: Starting conversion...
echo This will download ~15GB and convert to ONNX (~4-5GB)
echo Estimated time: 30-60 minutes
echo.

python -c "
from optimum.exporters.onnx import main_export
from pathlib import Path

output_dir = Path('./qwen2.5-coder-7b-onnx')
output_dir.mkdir(exist_ok=True)

print('Downloading and converting Qwen2.5-Coder-7B...')
print('This may take 30-60 minutes...')
print()

main_export(
    model_name_or_path='Qwen/Qwen2.5-Coder-7B-Instruct',
    output=output_dir,
    task='text-generation',
    trust_remote_code=True,
    fp16=True,
    optimize='O4',
)

print()
print('Conversion complete!')
print(f'Files saved to: {output_dir.absolute()}')
print()
print('Files created:')
for f in output_dir.iterdir():
    size_mb = f.stat().st_size / (1024 * 1024)
    print(f'  - {f.name}: {size_mb:.1f} MB')
"

echo.
echo ==========================================
echo Conversion Complete!
echo ==========================================
echo.
echo Next steps:
echo 1. Upload to HuggingFace:
echo    huggingface-cli login
echo    huggingface-cli upload your-username/Qwen2.5-Coder-7B-Instruct-ONNX ./qwen2.5-coder-7b-onnx .
echo.
echo 2. Update the app with your repo URL
echo.
pause
