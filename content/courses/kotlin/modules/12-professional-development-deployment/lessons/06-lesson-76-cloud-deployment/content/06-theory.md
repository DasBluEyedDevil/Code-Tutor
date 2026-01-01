---
type: "THEORY"
title: "Google Cloud Deployment"
---


### Option 1: Cloud Run (Easiest GCP)

**1. Install gcloud CLI**:

**2. Build and deploy**:

**Your app is live at**: `https://my-app-HASH-uc.a.run.app`

### Option 2: Google Kubernetes Engine (GKE)

**1. Create Kubernetes cluster**:

**2. Build and push image**:

**3. Create deployment.yaml**:

**4. Deploy to Kubernetes**:

---



```bash
# Apply deployment
kubectl apply -f deployment.yaml

# Get external IP
kubectl get service my-app-service

# Scale deployment
kubectl scale deployment my-app --replicas=5

# Update image
kubectl set image deployment/my-app my-app=gcr.io/YOUR_PROJECT_ID/my-app:v2
```
