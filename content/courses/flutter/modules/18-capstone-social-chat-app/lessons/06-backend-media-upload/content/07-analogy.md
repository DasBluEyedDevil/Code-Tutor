---
type: "ANALOGY"
title: "The Package Delivery Service"
---

Media upload in a chat app works like a package delivery service. When a user sends a photo, they are not emailing the actual photo to every recipient. Instead, they drop the package (image file) at the delivery hub (your storage service like S3 or Serverpod's file storage). The hub gives them a tracking number (the file URL). Then the chat message contains just the tracking number, not the actual package.

When a recipient opens the message, their app uses the tracking number to fetch the package from the hub. If ten people are in the group chat, the package sits in one place at the hub while ten delivery trucks pick up copies. This is dramatically more efficient than the sender shipping ten separate packages.

**The upload pipeline matters as much as the storage.** You compress the image before shipping (to reduce bandwidth), generate a thumbnail for quick previews (so the chat loads fast), validate the file type and size (to prevent abuse), and track upload progress (so the user sees a progress bar instead of wondering if anything is happening). Each step in the pipeline protects both the user experience and your server resources.
