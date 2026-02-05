@echo off
REM Convert Qwen2.5-Coder-7B to ONNX using Optimum CLI

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
python -m pip install -q transformers torch onnx optimum

echo.
echo Step 2: Starting conversion...
echo This will download ~15GB and convert to ONNX (~4-5GB)
echo Estimated time: 30-60 minutes
echo.

REM Use optimum-cli instead of Python script
python -m optimum.exporters.onnx export ^
    --model Qwen/Qwen2.5-Coder-7B-Instruct ^
    --task text-generation ^
    --trust-remote-code ^
    --fp16 ^
    --optimize O4 ^
    ./qwen2.5-coder-7b-onnx

echo.
if errorlevel 1 (
    echo Conversion failed. See error above.
) else (
    echo ==========================================
    echo Conversion complete!
    echo ==========================================
    echo.
    echo Files saved to: ./qwen2.5-coder-7b-onnx/
    echo.
    echo Next steps:
    echo 1. Upload to HuggingFace:
    echo    python -m pip install huggingface-hub
    echo    huggingface-cli login
    echo    huggingface-cli upload your-username/Qwen2.5-Coder-7B-Instruct-ONNX ./qwen2.5-coder-7b-onnx .
)

echo.
pause
