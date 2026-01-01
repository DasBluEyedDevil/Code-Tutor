---
type: "ANALOGY"
title: "Orders as Legal Contracts"
---

When you sign a contract to buy a house, that document becomes a legal record of your agreement. You cannot simply erase parts you do not like later. If circumstances change, you create amendments or addendums, but the original agreement remains intact for legal and historical purposes. The same principle applies to e-commerce orders.

An order represents a binding agreement between customer and merchant. Once a customer clicks 'Place Order', that moment is frozen in time. The prices they agreed to, the items they selected, and the shipping address they provided become part of an immutable record. If a product's price changes tomorrow, it does not affect orders placed today. If the customer wants to change their address, we create a shipping update, not modify the original order.

Orders also follow a strict lifecycle, much like a legal proceeding. A case moves from filing to discovery to trial to judgment, and you cannot skip steps or go backward arbitrarily. Similarly, an order progresses through states: Pending, Confirmed, Processing, Shipped, Delivered. Each transition has rules. You cannot ship an order that has not been confirmed. You cannot deliver an order that has not been shipped. And critically, you cannot undo a delivery just because the customer changed their mind.

This immutability and state machine behavior is not just good software design, it is a legal and financial requirement. Auditors need to trace every order's history. Tax authorities need records of what was sold and when. Customer service needs to see exactly what happened and in what sequence. By modeling orders as immutable records with controlled state transitions, we satisfy all these requirements while building a system that is easier to debug and maintain.