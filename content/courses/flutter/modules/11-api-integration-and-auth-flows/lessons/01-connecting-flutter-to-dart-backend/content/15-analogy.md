---
type: "ANALOGY"
title: "The Drive-Through Window"
---

Connecting your Flutter app to a Dart backend is like setting up a drive-through window at a restaurant. The customer (your Flutter app) pulls up and places an order through the speaker (an HTTP request). The kitchen (your backend) receives the order, prepares the food, and hands it back through the window (an HTTP response).

The customer never walks into the kitchen. They do not need to know how the food is prepared, which oven is used, or where the ingredients are stored. They just need to know the menu (the API contract) and how to place an order (the correct endpoint and request format). This separation is what makes the system work -- the kitchen can completely change its equipment without the customer noticing, as long as the food still comes out the same way through the window.

**The API is your drive-through menu.** It defines what you can order, what format to use, and what you will get back. When both your Flutter app and your Dart backend agree on this menu, they can work together seamlessly even though they run as completely separate programs.
