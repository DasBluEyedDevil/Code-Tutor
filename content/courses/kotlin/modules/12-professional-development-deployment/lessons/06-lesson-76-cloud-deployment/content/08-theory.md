---
type: "THEORY"
title: "SSL/TLS Certificates"
---

### Heroku (Automatic)

Heroku provides HTTPS automatically on all apps! No configuration needed.

### AWS Certificate Manager

**1. Request certificate**:

```bash
aws acm request-certificate \
  --domain-name myapp.com \
  --validation-method DNS \
  --subject-alternative-names "*.myapp.com"
```

**2. Validate domain** - Add the DNS records provided by AWS to your domain registrar.

**3. Attach to Load Balancer**:

```bash
aws elbv2 create-listener \
  --load-balancer-arn arn:aws:elasticloadbalancing:...:loadbalancer/app/my-lb/xxx \
  --protocol HTTPS \
  --port 443 \
  --certificates CertificateArn=arn:aws:acm:...:certificate/xxx \
  --default-actions Type=forward,TargetGroupArn=arn:aws:elasticloadbalancing:...
```

### Let's Encrypt with Certbot

**For self-hosted servers**:

```bash
# Install certbot
sudo apt-get install certbot

# Get certificate
sudo certbot certonly --standalone -d myapp.com

# Convert to JKS for Ktor
openssl pkcs12 -export \
  -in /etc/letsencrypt/live/myapp.com/fullchain.pem \
  -inkey /etc/letsencrypt/live/myapp.com/privkey.pem \
  -out keystore.p12 -name myapp

keytool -importkeystore \
  -srckeystore keystore.p12 -srcstoretype PKCS12 \
  -destkeystore keystore.jks -deststoretype JKS
```

**Configure Ktor for HTTPS**:

```kotlin
fun main() {
    val keyStoreFile = File("/etc/letsencrypt/live/myapp.com/keystore.jks")
    val keyStore = KeyStore.getInstance(keyStoreFile, "password".toCharArray())

    embeddedServer(Netty, environment = applicationEngineEnvironment {
        connector {
            port = 80
        }
        sslConnector(
            keyStore = keyStore,
            keyAlias = "myapp",
            keyStorePassword = { "password".toCharArray() },
            privateKeyPassword = { "password".toCharArray() }
        ) {
            port = 443
            keyStorePath = keyStoreFile
        }
        module {
            module()
        }
    }).start(wait = true)
}
```
