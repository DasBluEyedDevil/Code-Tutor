---
type: "EXAMPLE"
title: "GitHub Actions Docker Workflow"
---

This workflow builds and publishes Docker images to GitHub Container Registry. It uses Docker Buildx for advanced features like multi-platform builds and layer caching.

```yaml
# ===== .github/workflows/docker.yml =====
# Build and publish Docker images for ShopFlow

name: Docker Build and Push

on:
  push:
    branches: [main]
    tags: ['v*.*.*']      # Trigger on version tags
  pull_request:
    branches: [main]

# Required permissions for GHCR
permissions:
  contents: read
  packages: write

env:
  REGISTRY: ghcr.io
  IMAGE_NAME: ${{ github.repository }}

jobs:
  build-and-push:
    name: Build and Push Docker Image
    runs-on: ubuntu-latest
    
    steps:
      - name: Checkout repository
        uses: actions/checkout@v4
      
      # Set up QEMU for multi-platform builds (ARM64, etc.)
      - name: Set up QEMU
        uses: docker/setup-qemu-action@v3
      
      # Set up Docker Buildx for advanced build features
      - name: Set up Docker Buildx
        uses: docker/setup-buildx-action@v3
        with:
          driver-opts: |
            image=moby/buildkit:v0.12.0
      
      # Log in to GitHub Container Registry
      - name: Log in to Container Registry
        uses: docker/login-action@v3
        with:
          registry: ${{ env.REGISTRY }}
          username: ${{ github.actor }}
          password: ${{ secrets.GITHUB_TOKEN }}
      
      # Extract metadata (tags, labels) for Docker
      - name: Extract Docker metadata
        id: meta
        uses: docker/metadata-action@v5
        with:
          images: ${{ env.REGISTRY }}/${{ env.IMAGE_NAME }}
          tags: |
            # Tag with 'latest' for main branch
            type=raw,value=latest,enable=${{ github.ref == 'refs/heads/main' }}
            # Tag with branch name
            type=ref,event=branch
            # Tag with PR number
            type=ref,event=pr
            # Tag with semantic version (v1.2.3 -> 1.2.3, 1.2, 1)
            type=semver,pattern={{version}}
            type=semver,pattern={{major}}.{{minor}}
            type=semver,pattern={{major}}
            # Tag with commit SHA
            type=sha,prefix=sha-
      
      # Build and push Docker image
      - name: Build and push Docker image
        uses: docker/build-push-action@v5
        with:
          context: .
          file: ./Dockerfile
          # Push only for main branch and tags, not PRs
          push: ${{ github.event_name != 'pull_request' }}
          tags: ${{ steps.meta.outputs.tags }}
          labels: ${{ steps.meta.outputs.labels }}
          # Multi-platform build
          platforms: linux/amd64,linux/arm64
          # Cache layers in GitHub Actions cache
          cache-from: type=gha
          cache-to: type=gha,mode=max
          # Build arguments
          build-args: |
            BUILD_VERSION=${{ github.sha }}
            BUILD_DATE=${{ github.event.head_commit.timestamp }}
      
      # Generate SBOM (Software Bill of Materials) for security
      - name: Generate SBOM
        uses: anchore/sbom-action@v0
        if: github.event_name != 'pull_request'
        with:
          image: ${{ env.REGISTRY }}/${{ env.IMAGE_NAME }}:sha-${{ github.sha }}
          artifact-name: sbom.spdx.json
      
      # Scan image for vulnerabilities
      - name: Run Trivy vulnerability scanner
        uses: aquasecurity/trivy-action@master
        with:
          image-ref: ${{ env.REGISTRY }}/${{ env.IMAGE_NAME }}:sha-${{ github.sha }}
          format: 'sarif'
          output: 'trivy-results.sarif'
          severity: 'CRITICAL,HIGH'
      
      # Upload scan results to GitHub Security
      - name: Upload Trivy scan results
        uses: github/codeql-action/upload-sarif@v3
        if: always()
        with:
          sarif_file: 'trivy-results.sarif'
      
      # Output the image digest for downstream jobs
      - name: Output image digest
        run: |
          echo "Image digest: ${{ steps.build-push.outputs.digest }}"
          echo "Image pushed to: ${{ env.REGISTRY }}/${{ env.IMAGE_NAME }}"
```
