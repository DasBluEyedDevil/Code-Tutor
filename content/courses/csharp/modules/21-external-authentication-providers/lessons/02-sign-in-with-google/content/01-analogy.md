---
type: "ANALOGY"
title: "OAuth as a Valet Key"
---

Picture yourself arriving at an upscale restaurant with your expensive car. Instead of parking it yourself in a distant lot, you hand the valet attendant a special key. But here is the crucial detail: this is not your regular key ring with your house keys, office access cards, and gym locker key. This is a VALET KEY - specifically designed to start the engine and open the doors, but it CANNOT open the trunk where your laptop and valuables are stored, and it CANNOT access the glove compartment containing your registration and insurance documents.

This is exactly how OAuth 2.0 works when you click 'Sign in with Google'. You are not giving the ShopFlow application your Google password - that would be like handing over your entire key ring, including your house keys, to a stranger. Instead, you are telling Google: 'Give ShopFlow a valet key. Let them verify who I am and see my basic profile information, but absolutely do not let them read my private emails, access my Google Drive documents, or delete my YouTube watch history.'

The permissions you grant are called SCOPES - just like how a valet key has physical limitations built into its design, OAuth scopes define exactly what the application can and cannot access. When ShopFlow requests the 'email' and 'profile' scopes, it is asking for a very limited valet key that only reveals your name, email address, and profile picture.

The beauty of this system is that you maintain complete control. Just as you can ask the restaurant to return your valet key at any time, you can visit your Google Account settings and revoke ShopFlow's access instantly. The application immediately loses its ability to verify your identity - no password changes required, no worry about whether they saved your credentials. The valet key simply stops working.

This delegation of LIMITED access without sharing secrets is the fundamental principle that makes OAuth secure and widely trusted across the internet.