---
type: "THEORY"
title: "The OAuth Flow"
---

## The Five Steps of OAuth Authorization Code Flow

When a user clicks 'Sign in with Google' in your ShopFlow application, a carefully choreographed dance begins between your application, the user's browser, and Google's servers. Understanding these five steps is essential for debugging authentication issues and implementing secure OAuth.

**Step 1: Redirect to Google**
Your application redirects the user's browser to Google's authorization endpoint. This URL includes several critical parameters: your CLIENT_ID (identifying your application), the REDIRECT_URI (where Google should send the user back), the SCOPES you are requesting (email, profile), a STATE parameter (random value to prevent CSRF attacks), and RESPONSE_TYPE=code (requesting an authorization code). The user leaves your site entirely and is now on Google's domain.

**Step 2: User Consent**
Google displays a login page if the user is not already signed in, followed by a consent screen. The consent screen shows your application's name, logo, and exactly what permissions you are requesting. The user can see 'ShopFlow wants to view your email address and basic profile information.' They can choose to approve or deny. This happens entirely on Google's secure servers - your application never sees their Google password.

**Step 3: Callback with Authorization Code**
If the user approves, Google redirects their browser back to your REDIRECT_URI with two query parameters: CODE (a short-lived authorization code) and STATE (the same value you sent, which you MUST verify matches). The authorization code is NOT an access token - it is a one-time-use ticket that proves the user approved your request. This code expires in about 10 minutes and can only be used once.

**Step 4: Exchange Code for Tokens**
Your SERVER (not the browser) sends the authorization code to Google's token endpoint, along with your CLIENT_SECRET. This server-to-server communication is crucial - the CLIENT_SECRET must never be exposed to browsers or client-side code. Google validates the code and returns an ACCESS_TOKEN (for calling Google APIs), optionally a REFRESH_TOKEN (for long-term access), and an ID_TOKEN (JWT containing user identity claims).

**Step 5: Extract Claims and Create Session**
Your application decodes the ID_TOKEN (after verifying its signature) to extract claims about the user: their unique Google ID, email, name, and profile picture. You use this information to either find an existing user in your database or create a new one. Finally, you create a local session cookie so the user remains logged in to your application without repeating this flow on every request.

## Why Authorization Codes?

You might wonder why we do not just receive an access token directly from Google in step 3. The answer is security through indirection. If tokens were passed through the browser, they could be intercepted by malicious browser extensions, captured in server logs, or stolen via browser history. The authorization code is useless without your CLIENT_SECRET, which only your server knows. Even if someone intercepts the code, they cannot exchange it for tokens.

## The State Parameter Prevents CSRF

The STATE parameter is your protection against Cross-Site Request Forgery attacks. An attacker could trick a user into clicking a link that looks like a legitimate OAuth callback, potentially linking the attacker's account to the victim's session. By generating a random STATE value, storing it in the user's session, and verifying it matches when the callback arrives, you ensure the callback is genuinely a response to a request YOUR application initiated.