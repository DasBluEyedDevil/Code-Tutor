---
type: "ANALOGY"
title: "Identity as Your User Management System"
---

Imagine you are opening a new exclusive members-only club. Before the first guest arrives, you need an entire membership management system in place. You need membership application forms, a secure vault to store member records, staff trained to verify credentials at the door, and processes for issuing and revoking membership cards.

ASP.NET Core Identity is exactly this complete membership management system for your application. Instead of building everything from scratch - designing database tables for users, writing password hashing algorithms, implementing email confirmation flows, handling account lockouts after failed attempts - Identity provides all of this out of the box.

The ClubMembershipManager in our analogy is the UserManager<TUser> service. It handles creating new members (CreateAsync), verifying membership credentials (CheckPasswordAsync), updating member information (UpdateAsync), and revoking memberships (DeleteAsync). The front door security guard is SignInManager<TUser>, responsible for checking credentials and either admitting members or turning them away.

The membership card system represents Identity's token and cookie generation. When a member successfully authenticates, they receive a secure credential (cookie or token) that proves their identity for subsequent visits. The card cannot be forged because it is cryptographically signed.

The member records vault is your Identity database - tables like AspNetUsers, AspNetRoles, and AspNetUserRoles that store all membership data. Entity Framework Core manages these tables, and Identity migrations keep them in sync with your application.

Just like a real club might start with basic membership and later add VIP tiers, loyalty points, or family memberships, Identity is extensible. You can add custom properties to users, define your own roles, and implement additional claims. The foundation is solid, and you build upon it for your specific needs.