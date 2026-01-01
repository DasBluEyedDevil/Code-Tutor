---
type: "ANALOGY"
title: "JWT as a Boarding Pass"
---

When you check in for a flight, the airline verifies your identity against your passport and booking. Once verified, they issue you a boarding pass. This boarding pass contains key information: your name, flight number, seat assignment, boarding time, and a barcode the airline can scan. You do not need to show your passport again at every checkpoint - the boarding pass proves you have already been verified.

A JWT (JSON Web Token) is exactly like this boarding pass. After you log in and prove your identity (like showing your passport), the server issues you a JWT. This token contains claims about you - your user ID, roles, email, and when the token expires. Just like the boarding pass, you present this token with each request instead of re-entering your password every time.

Let us break down the boarding pass analogy further to understand JWT structure:

**The Header (Top of boarding pass - airline logo and barcode type)**
The JWT header specifies the token type (JWT) and the signing algorithm (like HS256 or RS256). This tells the receiving system how to validate the token. On a boarding pass, this is like the barcode format that scanners need to know how to read.

**The Payload (Main boarding pass info - your details)**
The payload contains claims - statements about the user. Standard claims include 'sub' (subject - who the token is about), 'exp' (expiration time), 'iat' (issued at), and 'iss' (issuer - who created the token). You can add custom claims like roles, permissions, or department. On your boarding pass, this is your name, flight details, and seat number.

**The Signature (Security hologram and barcode)**
The signature ensures the token has not been tampered with. The server signs the header and payload using a secret key. Anyone can read the token (it is just Base64 encoded), but only the server can create a valid signature. This is like the hologram and encrypted barcode on your boarding pass - easy to verify, impossible to forge without access to the airline's systems.

When you present your boarding pass at the gate, they scan it to verify: Is this a valid boarding pass format? Has it been tampered with? Is this flight still boarding? Is this the right passenger? JWT validation works identically: verify the signature, check expiration, confirm the issuer, and extract user claims.