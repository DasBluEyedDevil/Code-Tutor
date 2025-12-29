# start-piston.ps1 - Start Piston code execution engine
# Requires Docker Desktop to be installed and running
#
# Piston provides sandboxed code execution for:
# - Python, JavaScript, C#, Java, Kotlin, Rust, Dart
#
# Usage:
#   .\start-piston.ps1          # Start Piston
#   .\start-piston.ps1 -Stop    # Stop Piston
#   .\start-piston.ps1 -Status  # Check status

param(
    [switch]$Stop,
    [switch]$Status
)

$ContainerName = "codetutor-piston"
$PistonImage = "ghcr.io/engineer-man/piston"
$PistonPort = 2000

function Write-ColorOutput($ForegroundColor, $Message) {
    $fc = $host.UI.RawUI.ForegroundColor
    $host.UI.RawUI.ForegroundColor = $ForegroundColor
    Write-Output $Message
    $host.UI.RawUI.ForegroundColor = $fc
}

# Check if Docker is available
$dockerAvailable = Get-Command docker -ErrorAction SilentlyContinue
if (-not $dockerAvailable) {
    Write-ColorOutput Red "Docker is not installed or not in PATH."
    Write-ColorOutput Yellow "Install Docker Desktop from https://docker.com"
    exit 1
}

# Check if Docker daemon is running
$dockerRunning = docker info 2>&1
if ($LASTEXITCODE -ne 0) {
    Write-ColorOutput Red "Docker daemon is not running."
    Write-ColorOutput Yellow "Please start Docker Desktop and try again."
    exit 1
}

if ($Status) {
    $container = docker ps --filter "name=$ContainerName" --format "{{.Status}}" 2>&1
    if ($container) {
        Write-ColorOutput Green "Piston is running: $container"
        Write-ColorOutput Cyan "API endpoint: http://localhost:$PistonPort"
    } else {
        Write-ColorOutput Yellow "Piston is not running."
    }
    exit 0
}

if ($Stop) {
    Write-ColorOutput Cyan "Stopping Piston..."
    docker stop $ContainerName 2>&1 | Out-Null
    docker rm $ContainerName 2>&1 | Out-Null
    Write-ColorOutput Green "Piston stopped."
    exit 0
}

# Check if container already exists
$existingContainer = docker ps -a --filter "name=$ContainerName" --format "{{.Names}}" 2>&1
if ($existingContainer -eq $ContainerName) {
    $runningContainer = docker ps --filter "name=$ContainerName" --format "{{.Names}}" 2>&1
    if ($runningContainer -eq $ContainerName) {
        Write-ColorOutput Green "Piston is already running on http://localhost:$PistonPort"
        exit 0
    } else {
        Write-ColorOutput Cyan "Starting existing Piston container..."
        docker start $ContainerName | Out-Null
        Write-ColorOutput Green "Piston started on http://localhost:$PistonPort"
        exit 0
    }
}

Write-ColorOutput Cyan "Starting Piston code execution engine..."
Write-ColorOutput Yellow "This may take a few minutes on first run (downloading image)..."

# Run Piston container
docker run -d `
    --name $ContainerName `
    -p "${PistonPort}:2000" `
    --restart unless-stopped `
    $PistonImage

if ($LASTEXITCODE -eq 0) {
    Write-ColorOutput Green ""
    Write-ColorOutput Green "Piston is now running on http://localhost:$PistonPort"
    Write-ColorOutput Green "Code-Tutor will automatically use Piston for sandboxed execution."
    Write-ColorOutput Cyan ""
    Write-ColorOutput Cyan "Supported languages: Python, JavaScript, C#, Java, Kotlin, Rust, Dart"
    Write-ColorOutput Cyan ""
    Write-ColorOutput Yellow "To stop Piston: .\start-piston.ps1 -Stop"
} else {
    Write-ColorOutput Red "Failed to start Piston. Check Docker logs for details."
    exit 1
}
