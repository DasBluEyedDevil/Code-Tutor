---
type: "ANALOGY"
title: "Claims as ID Badge Details"
---

Picture the ID badge hanging around your neck at work. It is not just a piece of plastic that says 'Employee' - it contains specific facts about you. Your badge displays your full name, your photograph, your employee ID number, and your department: 'Engineering'. It shows your hire date: 'Since 2019'. It has a color-coded stripe indicating your security clearance level: 'Level 3'. There might be small icons showing your certifications: 'First Aid Trained', 'Forklift Licensed', 'Hazmat Certified'.

Each of these pieces of information is a claim - a statement of fact about you that the organization has verified and encoded on your badge. The badge does not say 'John can enter the server room'. Instead, it states facts: 'John is in IT department, has Level 3 clearance, and is Network Certified'. The security systems at each door read these claims and make their own access decisions based on their rules.

The power of claims becomes clear when you consider how access rules work. The server room door does not have a list of authorized names. Instead, it has a policy: 'Allow entry if department is IT AND clearance is Level 3 or higher AND has Network Certification'. Your badge's claims are evaluated against this policy. When you get promoted and your clearance increases to Level 4, you suddenly have access to more restricted areas - not because someone updated a list, but because your claims now satisfy more policies.

Compare this to role-based access, where your badge might simply say 'Senior Engineer'. To know what that means, every door would need a lookup table mapping 'Senior Engineer' to a list of permissions. With claims, the permissions are derived from the facts themselves. The door policy says 'require Level 3 clearance' - it does not need to know all the job titles that have Level 3 clearance.

Claims also enable external verification. When you attend a conference, the organizers do not call your company to verify your job title. Instead, your conference badge includes claims issued by your company: 'Employed at TechCorp', 'Role: Senior Engineer', 'Email: john@techcorp.com'. The conference trusts these claims because they trust your company (the issuer). This is exactly how federated identity works - claims issued by Google, Microsoft, or your corporate identity provider are trusted by applications that accept those identity providers.

In software, claims are key-value pairs attached to a user's identity. Rather than asking 'Is this user an Admin?', you ask 'What claims does this user have?' and evaluate policies against those claims. This shift in thinking enables more flexible, maintainable, and externally-compatible authorization systems.