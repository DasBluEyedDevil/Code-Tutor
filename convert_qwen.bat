@echo off
REM Convert Qwen2.5-Coder-7B to ONNX using transformers

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
python -m pip install -q transformers torch onnx onnxruntime

echo.
echo Step 2: Starting conversion...
echo This will download ~15GB and convert to ONNX (~4-5GB)
echo Estimated time: 30-60 minutes
echo.

REM Use transformers.onnx CLI
python -m transformers.onnx export ^
    --model=Qwen/Qwen2.5-Coder-7B-Instruct ^
    --feature=text-generation ^
    --framework=pt ^
    --preprocessor=Qwen/Qwen2.5-Coder-7B-Instruct ^
    --trust-remote-code ^
    ./qwen2.5-coder-7b-onnx

echo.
if errorlevel 1 (
    echo Conversion failed. Trying alternative method...
    echo.
    python convert_qwen_torch.py
) else (
    echo ==========================================
    echo Conversion complete!
    echo ==========================================
)

echo.
pause
