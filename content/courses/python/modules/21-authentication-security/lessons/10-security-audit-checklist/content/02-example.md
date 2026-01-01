---
type: "EXAMPLE"
title: "Automated Security Scanning"
---

**Integrate security scanning into your development workflow:**

**Tools Overview:**
- **Bandit**: Python static analysis for security issues
- **Safety/pip-audit**: Check dependencies for known vulnerabilities
- **Semgrep**: Pattern-based code scanning
- **OWASP ZAP**: Dynamic application security testing

```python
import subprocess
import json
import sys
from dataclasses import dataclass
from typing import List, Optional, Dict, Any
from enum import Enum

class Severity(Enum):
    CRITICAL = "critical"
    HIGH = "high"
    MEDIUM = "medium"
    LOW = "low"
    INFO = "info"

@dataclass
class SecurityFinding:
    tool: str
    severity: Severity
    title: str
    description: str
    file_path: Optional[str] = None
    line_number: Optional[int] = None
    recommendation: Optional[str] = None

class SecurityScanner:
    """
    Automated security scanner that runs multiple tools
    and aggregates findings.
    """
    
    def __init__(self, project_path: str = "."):
        self.project_path = project_path
        self.findings: List[SecurityFinding] = []
    
    def run_bandit(self) -> List[SecurityFinding]:
        """
        Run Bandit static analysis for Python security issues.
        
        In real usage:
        pip install bandit
        bandit -r src/ -f json
        """
        # Simulated Bandit output for demonstration
        simulated_results = [
            {
                "issue_severity": "HIGH",
                "issue_text": "Possible hardcoded password",
                "filename": "config.py",
                "line_number": 15,
                "test_id": "B105"
            },
            {
                "issue_severity": "MEDIUM",
                "issue_text": "Use of exec detected",
                "filename": "utils.py",
                "line_number": 42,
                "test_id": "B102"
            }
        ]
        
        findings = []
        for result in simulated_results:
            severity_map = {
                "CRITICAL": Severity.CRITICAL,
                "HIGH": Severity.HIGH,
                "MEDIUM": Severity.MEDIUM,
                "LOW": Severity.LOW
            }
            findings.append(SecurityFinding(
                tool="Bandit",
                severity=severity_map.get(result["issue_severity"], Severity.INFO),
                title=f"[{result['test_id']}] {result['issue_text']}",
                description=result["issue_text"],
                file_path=result["filename"],
                line_number=result["line_number"],
                recommendation="Review code and remediate security issue"
            ))
        
        return findings
    
    def run_dependency_audit(self) -> List[SecurityFinding]:
        """
        Check dependencies for known vulnerabilities.
        
        In real usage:
        pip install pip-audit
        pip-audit --format json
        """
        # Simulated vulnerability findings
        simulated_vulnerabilities = [
            {
                "package": "requests",
                "version": "2.25.0",
                "vulnerability_id": "CVE-2023-32681",
                "severity": "HIGH",
                "description": "Unintended leak of Proxy-Authorization header",
                "fixed_version": "2.31.0"
            },
            {
                "package": "pyyaml",
                "version": "5.3.1",
                "vulnerability_id": "CVE-2020-14343",
                "severity": "CRITICAL",
                "description": "Arbitrary code execution via yaml.load()",
                "fixed_version": "5.4"
            }
        ]
        
        findings = []
        for vuln in simulated_vulnerabilities:
            severity_map = {
                "CRITICAL": Severity.CRITICAL,
                "HIGH": Severity.HIGH,
                "MEDIUM": Severity.MEDIUM,
                "LOW": Severity.LOW
            }
            findings.append(SecurityFinding(
                tool="pip-audit",
                severity=severity_map.get(vuln["severity"], Severity.INFO),
                title=f"{vuln['vulnerability_id']}: {vuln['package']} {vuln['version']}",
                description=vuln["description"],
                recommendation=f"Upgrade to version {vuln['fixed_version']} or later"
            ))
        
        return findings
    
    def check_security_headers(self, response_headers: Dict[str, str]) -> List[SecurityFinding]:
        """
        Check for missing security headers.
        """
        findings = []
        required_headers = {
            "Strict-Transport-Security": (
                Severity.HIGH,
                "HSTS header missing - vulnerable to downgrade attacks",
                "Add header: Strict-Transport-Security: max-age=31536000; includeSubDomains"
            ),
            "X-Content-Type-Options": (
                Severity.MEDIUM,
                "Missing X-Content-Type-Options header",
                "Add header: X-Content-Type-Options: nosniff"
            ),
            "X-Frame-Options": (
                Severity.MEDIUM,
                "Missing X-Frame-Options header - vulnerable to clickjacking",
                "Add header: X-Frame-Options: DENY"
            ),
            "Content-Security-Policy": (
                Severity.MEDIUM,
                "Missing Content-Security-Policy header",
                "Define a restrictive CSP for your application"
            )
        }
        
        for header, (severity, description, recommendation) in required_headers.items():
            if header not in response_headers:
                findings.append(SecurityFinding(
                    tool="Header Check",
                    severity=severity,
                    title=f"Missing {header}",
                    description=description,
                    recommendation=recommendation
                ))
        
        return findings
    
    def run_all_scans(self) -> Dict[str, Any]:
        """
        Run all security scans and aggregate results.
        """
        print("Running security scans...")
        print("-" * 40)
        
        # Run each scan
        print("[1/3] Running static analysis (Bandit)...")
        bandit_findings = self.run_bandit()
        self.findings.extend(bandit_findings)
        
        print("[2/3] Running dependency audit...")
        dep_findings = self.run_dependency_audit()
        self.findings.extend(dep_findings)
        
        print("[3/3] Checking security headers...")
        # Simulated headers (missing some)
        headers = {"Content-Type": "application/json"}
        header_findings = self.check_security_headers(headers)
        self.findings.extend(header_findings)
        
        # Aggregate results
        summary = {
            "total_findings": len(self.findings),
            "by_severity": {},
            "by_tool": {},
            "critical_count": 0,
            "high_count": 0,
            "passed": True
        }
        
        for finding in self.findings:
            # Count by severity
            sev = finding.severity.value
            summary["by_severity"][sev] = summary["by_severity"].get(sev, 0) + 1
            
            # Count by tool
            tool = finding.tool
            summary["by_tool"][tool] = summary["by_tool"].get(tool, 0) + 1
            
            # Track critical/high for pass/fail
            if finding.severity == Severity.CRITICAL:
                summary["critical_count"] += 1
            elif finding.severity == Severity.HIGH:
                summary["high_count"] += 1
        
        # Fail if any critical or high findings
        summary["passed"] = summary["critical_count"] == 0 and summary["high_count"] == 0
        
        return summary
    
    def print_report(self):
        """Print formatted security report."""
        summary = self.run_all_scans()
        
        print("\n" + "=" * 50)
        print("SECURITY SCAN REPORT")
        print("=" * 50)
        
        print(f"\nTotal Findings: {summary['total_findings']}")
        print(f"  Critical: {summary.get('by_severity', {}).get('critical', 0)}")
        print(f"  High: {summary.get('by_severity', {}).get('high', 0)}")
        print(f"  Medium: {summary.get('by_severity', {}).get('medium', 0)}")
        print(f"  Low: {summary.get('by_severity', {}).get('low', 0)}")
        
        print("\nFindings by Tool:")
        for tool, count in summary.get("by_tool", {}).items():
            print(f"  {tool}: {count}")
        
        if self.findings:
            print("\n" + "-" * 50)
            print("DETAILED FINDINGS:")
            print("-" * 50)
            
            for i, finding in enumerate(self.findings, 1):
                print(f"\n[{i}] {finding.severity.value.upper()}: {finding.title}")
                print(f"    Tool: {finding.tool}")
                if finding.file_path:
                    loc = f"{finding.file_path}"
                    if finding.line_number:
                        loc += f":{finding.line_number}"
                    print(f"    Location: {loc}")
                print(f"    Description: {finding.description}")
                if finding.recommendation:
                    print(f"    Fix: {finding.recommendation}")
        
        print("\n" + "=" * 50)
        status = "PASSED" if summary["passed"] else "FAILED"
        print(f"RESULT: {status}")
        print("=" * 50)
        
        return summary["passed"]

# Run the security scanner
print("Finance Tracker Security Audit")
print("=" * 50 + "\n")

scanner = SecurityScanner()
passed = scanner.print_report()

if not passed:
    print("\nAction Required: Fix critical and high severity issues before deployment!")
```
