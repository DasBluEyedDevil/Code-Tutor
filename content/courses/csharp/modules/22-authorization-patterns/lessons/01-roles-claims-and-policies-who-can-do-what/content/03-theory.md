---
type: "THEORY"
title: "Authorization Strategies"
---

## Role-Based Authorization (RBAC)

Role-Based Access Control is the simplest and most widely understood authorization model. Users are assigned to roles like 'Admin', 'Manager', or 'Customer', and permissions are granted to roles rather than individual users. This works well when your permission structure maps cleanly to organizational hierarchy.

**Strengths:** Easy to understand, simple to implement, maps to business structures.
**Weaknesses:** Role explosion (too many roles), coarse-grained (all-or-nothing per role), rigid.

Use RBAC when: Your permissions are relatively simple and align with job functions.

## Claim-Based Authorization

Claims are key-value pairs that describe facts about a user. Unlike roles which are binary (you have it or you don't), claims carry values: 'department: Engineering', 'clearance_level: 3', 'subscription: premium'. Claims come from identity providers and can encode rich information.

**Strengths:** Flexible data model, supports external identity providers, extensible.
**Weaknesses:** Claims alone don't define permissions - you need logic to interpret them.

Use Claims when: You need to make decisions based on user attributes rather than just group membership.

## Policy-Based Authorization

Policies are named authorization rules that combine requirements. A policy like 'CanDeleteOrders' might require: (Role is Admin) OR (Role is Manager AND order is from their department AND order is not shipped). Policies centralize authorization logic and make it reusable across your application.

**Strengths:** Reusable rules, testable logic, separation of policy from code, supports complex conditions.
**Weaknesses:** Learning curve, can become complex, policy definitions scattered from usage.

Use Policies when: You have complex authorization rules that you want to define once and enforce consistently.

## Resource-Based Authorization

Sometimes authorization depends not just on who the user is, but on what specific resource they're accessing. 'Can this user edit THIS document?' requires examining both the user's claims and the document's properties (owner, department, status). ASP.NET Core's IAuthorizationHandler pattern enables this.

**Strengths:** Fine-grained control, handles ownership scenarios, context-aware decisions.
**Weaknesses:** More complex implementation, requires loading resources before authorization, performance considerations.

Use Resource-Based when: Authorization depends on the specific resource being accessed, like 'users can only edit their own posts'.

## Choosing Your Strategy

Most real applications combine these approaches. ShopFlow might use:
- **Roles** for broad access categories (Admin, Manager, Customer)
- **Claims** for user attributes (department, subscription tier, verified status)
- **Policies** for reusable rules (CanManageInventory, CanProcessRefunds)
- **Resource-Based** for ownership checks (CanEditThisOrder, CanViewThisReport)

Start simple with roles, add claims as needed, extract repeated logic into policies, and implement resource-based checks for ownership scenarios.