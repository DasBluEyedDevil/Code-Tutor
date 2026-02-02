---
type: "THEORY"
title: "Docker in CI/CD Explained"
---

Building and deploying Docker images in GitHub Actions:

1. **Container Registries**:
   ```
   GitHub Container Registry (ghcr.io)
   - Free for public repos
   - Integrated with GitHub
   - ghcr.io/username/repo:latest

   Docker Hub
   - Free tier available
   - Most widely used
   - username/repo:latest

   AWS ECR, Google GCR, Azure ACR
   - Cloud provider registries
   - Private by default
   ```

2. **Image Tagging Strategy**:
   ```yaml
   tags: |
     # Branch name (e.g., main, develop)
     type=ref,event=branch

     # Short commit SHA (e.g., abc1234)
     type=sha,prefix=

     # Semantic version from git tag
     type=semver,pattern={{version}}

     # 'latest' only on main branch
     type=raw,value=latest,enable={{is_default_branch}}

   # Result: ghcr.io/user/app:main
   #         ghcr.io/user/app:abc1234
   #         ghcr.io/user/app:latest
   ```

3. **Caching for Faster Builds**:
   ```yaml
   - uses: docker/build-push-action@v5
     with:
       # Use GitHub Actions cache
       cache-from: type=gha
       cache-to: type=gha,mode=max

       # Layers are cached between runs
       # Rebuilds only changed layers
   ```

4. **Multi-Platform Builds**:
   ```yaml
   - uses: docker/build-push-action@v5
     with:
       platforms: linux/amd64,linux/arm64
       # Builds for both Intel and ARM (M1/M2 Macs)
   ```

5. **Deploy After Push**:
   ```yaml
   deploy:
     needs: build-and-push  # Wait for image
     if: github.ref == 'refs/heads/main'  # Only main

     steps:
       # Platform-specific deploy commands
       - name: Deploy to Fly.io
         run: flyctl deploy --image ghcr.io/user/app:latest

       # Or pull new image on server
       - name: Deploy via SSH
         run: |
           ssh server 'docker pull ghcr.io/user/app:latest && docker-compose up -d'
   ```