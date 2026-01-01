---
type: "ANALOGY"
title: "Checking the Name on the Document"
---

Imagine you are a librarian at a university that allows students to store their research documents in the library's archive. A student approaches the desk and asks to see 'Document #4521'. You cannot simply check if they are a student - that only tells you they can use the library. You need to check who owns Document #4521.

You pull up the document's record in your system. It shows: 'Owner: Sarah Chen, Department: Biology, Classification: Research Draft'. Now you can make a proper decision. If the student standing in front of you is Sarah Chen, she can view, edit, or even delete her own document. If they are Professor Williams from the Biology department, perhaps they can view it (departmental access) but not modify it. If they are just another student with no connection to this document, they cannot access it at all - unless Sarah marked it as 'Public' in which case anyone can read it.

This is fundamentally different from role-based authorization. A role check asks: 'Is this person a professor?' Resource-based authorization asks: 'Does this person have rights to THIS SPECIFIC document?' The answer depends on examining both the user's identity and the resource's properties.

Consider another scenario: a shared apartment building. The building manager has a master key (role-based access - Managers can enter all apartments). But residents only have keys to their own apartment (resource-based access - you can enter apartments where you are the tenant). A resident cannot enter apartment 4B just because they are 'a resident' - they need to be 'the resident of 4B'. The authorization decision requires looking at both the person and the specific apartment they want to access.

In ShopFlow, this pattern appears constantly. Can this user view Order #1234? Only if they placed that order, or they are a Seller who sold items in that order, or they are an Admin. Can this Seller edit Product #567? Only if they created that product. Can this user cancel this specific order? Only if they own it AND the order status allows cancellation. These decisions require examining the resource (the order, the product) alongside the user's identity and claims.