# Docker Setup Guide for Code-Tutor

## Why Docker is Required

Code-Tutor uses Docker to safely execute student code in isolated containers. This provides:

- **Security**: Student code runs in sandboxed containers
- **Multi-language support**: Each language has its own executor
- **Consistency**: Same behavior on all systems
- **Resource limits**: Prevent infinite loops and memory issues

## Installing Docker Desktop

### Windows

1. **Download Docker Desktop:**
   - Visit: https://www.docker.com/products/docker-desktop
   - Click "Download for Windows"
   - Choose the version for your system (WSL 2 recommended)

2. **Install:**
   - Run the installer
   - Follow the installation wizard
   - **Restart your computer** when prompted

3. **Start Docker Desktop:**
   - Open Docker Desktop from Start menu
   - Wait for it to fully start
   - You should see "Docker Desktop is running" in the whale icon

4. **Verify Installation:**
   ```powershell
   docker --version
   docker-compose --version
   ```

### macOS

1. **Download Docker Desktop:**
   - Visit: https://www.docker.com/products/docker-desktop
   - Download for Mac (Intel or Apple Silicon)

2. **Install:**
   - Open the .dmg file
   - Drag Docker to Applications
   - Run Docker from Applications

3. **Verify:**
   ```bash
   docker --version
   docker-compose --version
   ```

### Linux

1. **Install Docker Engine:**
   ```bash
   curl -fsSL https://get.docker.com -o get-docker.sh
   sudo sh get-docker.sh
   ```

2. **Add user to docker group:**
   ```bash
   sudo usermod -aG docker $USER
   ```

3. **Install Docker Compose:**
   ```bash
   sudo apt-get install docker-compose-plugin
   ```

## Building the Executors

After installing Docker, build the code execution containers:

### Build All Executors

```bash
cd C:\Users\dasbl\WebstormProjects\Code-Tutor
docker-compose build
```

This builds containers for:
- Python
- Java
- JavaScript
- Kotlin
- Rust
- C#
- Dart/Flutter

### Build Individual Executors

```bash
# Python
docker-compose build python-executor

# Java
docker-compose build java-executor

# Add others as needed
```

## Starting the Executors

### Start All

```bash
docker-compose up -d
```

### Start Specific Language

```bash
docker-compose up -d python-executor java-executor
```

### Check Status

```bash
docker-compose ps
```

You should see containers running on ports:
- python-executor: 4000
- java-executor: 4001
- javascript-executor: 4002
- kotlin-executor: 4003
- rust-executor: 4004
- csharp-executor: 4005
- dart-executor: 4006

## Troubleshooting

### "Docker daemon is not running"

**Solution:**
1. Open Docker Desktop
2. Wait for it to fully start
3. Look for the whale icon in system tray
4. Try your command again

### "Cannot connect to Docker daemon"

**Windows:**
- Make sure Docker Desktop is running
- Check if WSL 2 is enabled (required for Docker Desktop)
- Restart Docker Desktop

**Linux:**
- Start Docker service: `sudo systemctl start docker`
- Enable on boot: `sudo systemctl enable docker`

### "Port already in use"

**Check what's using the port:**
```powershell
# Windows
netstat -ano | findstr :4000

# Linux/Mac
lsof -i :4000
```

**Stop the container and restart:**
```bash
docker-compose down
docker-compose up -d
```

### "Image not found" or "build failed"

**Clean build:**
```bash
docker-compose down
docker-compose build --no-cache
docker-compose up -d
```

### WSL 2 Installation (Windows)

If Docker Desktop asks for WSL 2:

1. **Open PowerShell as Administrator:**
   ```powershell
   wsl --install
   ```

2. **Restart your computer**

3. **Set WSL 2 as default:**
   ```powershell
   wsl --set-default-version 2
   ```

4. **Restart Docker Desktop**

## Verifying Everything Works

### 1. Check Docker is Running

```bash
docker ps
```

Should show running containers.

### 2. Test Python Executor

```bash
curl http://localhost:4000/health
```

Should return: `{"status":"healthy"}`

### 3. Test Code Execution

```bash
curl -X POST http://localhost:4000/execute \
  -H "Content-Type: application/json" \
  -d '{"code":"print(\"Hello, World!\")"}'
```

Should return the execution result.

### 4. Check All Executors

```bash
# Python
curl http://localhost:4000/health

# Java
curl http://localhost:4001/health

# JavaScript
curl http://localhost:4002/health

# Kotlin
curl http://localhost:4003/health

# Rust
curl http://localhost:4004/health

# C#
curl http://localhost:4005/health

# Dart
curl http://localhost:4006/health
```

## Managing Docker Containers

### View Logs

```bash
# All executors
docker-compose logs -f

# Specific executor
docker-compose logs -f python-executor
```

### Restart Executors

```bash
docker-compose restart
```

### Stop Executors

```bash
docker-compose down
```

### Remove Everything (clean slate)

```bash
docker-compose down -v
docker system prune -a
```

## Resource Configuration

### Limit Container Resources

Edit `docker-compose.yml` to set limits:

```yaml
services:
  python-executor:
    # ...
    deploy:
      resources:
        limits:
          cpus: '0.5'
          memory: 512M
```

### Check Resource Usage

```bash
docker stats
```

## Common Commands Quick Reference

| Action | Command |
|--------|---------|
| Start all executors | `docker-compose up -d` |
| Stop all executors | `docker-compose down` |
| View status | `docker-compose ps` |
| View logs | `docker-compose logs -f` |
| Rebuild containers | `docker-compose build` |
| Restart containers | `docker-compose restart` |
| Remove all | `docker-compose down -v` |

## Next Steps

Once Docker is set up:

1. ✅ Docker Desktop running
2. ✅ Containers built: `docker-compose build`
3. ✅ Executors started: `docker-compose up -d`
4. ✅ Run Code-Tutor: `.\start.ps1` or `START.bat`
5. ✅ Open browser: http://localhost:3000
6. ✅ Try running code in the platform!

## Need Help?

- **Docker Documentation**: https://docs.docker.com/
- **Docker Desktop Issues**: https://docs.docker.com/desktop/troubleshoot/overview/
- **WSL 2 Setup**: https://docs.microsoft.com/en-us/windows/wsl/install

---

**Status:** Docker is now required for Code-Tutor to function properly!

