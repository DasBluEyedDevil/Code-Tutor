---
type: "EXAMPLE"
title: "Azure Container Apps Deployment"
---

Deploy ShopFlow to Azure Container Apps using infrastructure as code. This Bicep template creates the complete production environment.

```bicep
// infra/main.bicep - Complete Azure Container Apps deployment

@description('The environment name (dev, staging, prod)')
param environment string = 'prod'

@description('The Azure region for resources')
param location string = resourceGroup().location

@description('The container image to deploy')
param apiImage string

@description('The database connection string')
@secure()
param databaseConnectionString string

@description('The Stripe secret key')
@secure()
param stripeSecretKey string

@description('The Application Insights connection string')
@secure()
param appInsightsConnectionString string

var resourcePrefix = 'shopflow-${environment}'

// Log Analytics Workspace for Container Apps
resource logAnalytics 'Microsoft.OperationalInsights/workspaces@2022-10-01' = {
  name: '${resourcePrefix}-logs'
  location: location
  properties: {
    sku: {
      name: 'PerGB2018'
    }
    retentionInDays: 30
  }
}

// Container Apps Environment
resource containerAppsEnv 'Microsoft.App/managedEnvironments@2023-05-01' = {
  name: '${resourcePrefix}-env'
  location: location
  properties: {
    appLogsConfiguration: {
      destination: 'log-analytics'
      logAnalyticsConfiguration: {
        customerId: logAnalytics.properties.customerId
        sharedKey: logAnalytics.listKeys().primarySharedKey
      }
    }
  }
}

// Redis Cache for session and cart data
resource redis 'Microsoft.Cache/redis@2023-04-01' = {
  name: '${resourcePrefix}-cache'
  location: location
  properties: {
    sku: {
      name: 'Standard'
      family: 'C'
      capacity: 1
    }
    enableNonSslPort: false
    minimumTlsVersion: '1.2'
  }
}

// ShopFlow API Container App
resource apiApp 'Microsoft.App/containerApps@2023-05-01' = {
  name: '${resourcePrefix}-api'
  location: location
  properties: {
    managedEnvironmentId: containerAppsEnv.id
    configuration: {
      ingress: {
        external: true
        targetPort: 8080
        transport: 'http'
        corsPolicy: {
          allowedOrigins: [
            'https://shopflow.example.com'
          ]
          allowedMethods: ['GET', 'POST', 'PUT', 'DELETE', 'OPTIONS']
          allowedHeaders: ['*']
          allowCredentials: true
        }
      }
      secrets: [
        {
          name: 'db-connection'
          value: databaseConnectionString
        }
        {
          name: 'stripe-key'
          value: stripeSecretKey
        }
        {
          name: 'redis-connection'
          value: '${redis.properties.hostName}:6380,password=${redis.listKeys().primaryKey},ssl=True'
        }
        {
          name: 'appinsights-connection'
          value: appInsightsConnectionString
        }
      ]
    }
    template: {
      containers: [
        {
          name: 'api'
          image: apiImage
          resources: {
            cpu: json('0.5')
            memory: '1Gi'
          }
          env: [
            {
              name: 'ASPNETCORE_ENVIRONMENT'
              value: 'Production'
            }
            {
              name: 'Database__ConnectionString'
              secretRef: 'db-connection'
            }
            {
              name: 'Stripe__SecretKey'
              secretRef: 'stripe-key'
            }
            {
              name: 'Redis__ConnectionString'
              secretRef: 'redis-connection'
            }
            {
              name: 'ApplicationInsights__ConnectionString'
              secretRef: 'appinsights-connection'
            }
          ]
          probes: [
            {
              type: 'Liveness'
              httpGet: {
                path: '/healthz/live'
                port: 8080
              }
              initialDelaySeconds: 10
              periodSeconds: 30
            }
            {
              type: 'Readiness'
              httpGet: {
                path: '/healthz/ready'
                port: 8080
              }
              initialDelaySeconds: 5
              periodSeconds: 10
            }
          ]
        }
      ]
      scale: {
        minReplicas: 2
        maxReplicas: 10
        rules: [
          {
            name: 'http-scaling'
            http: {
              metadata: {
                concurrentRequests: '50'
              }
            }
          }
        ]
      }
    }
  }
}

output apiUrl string = 'https://${apiApp.properties.configuration.ingress.fqdn}'
output redisHostName string = redis.properties.hostName

// deploy.sh - Deployment script
/*
#!/bin/bash
set -e

RESOURCE_GROUP="shopflow-prod-rg"
LOCATION="eastus"
ACR_NAME="shopflowacr"
IMAGE_TAG="v1.0.0"

# Build and push Docker image
az acr build --registry $ACR_NAME \
  --image shopflow-api:$IMAGE_TAG \
  --file src/WebApi/Dockerfile .

# Deploy infrastructure
az deployment group create \
  --resource-group $RESOURCE_GROUP \
  --template-file infra/main.bicep \
  --parameters \
    environment=prod \
    apiImage="$ACR_NAME.azurecr.io/shopflow-api:$IMAGE_TAG" \
    databaseConnectionString="$DB_CONNECTION" \
    stripeSecretKey="$STRIPE_KEY" \
    appInsightsConnectionString="$APPINSIGHTS_CONNECTION"

echo "Deployment complete!"
*/
```
