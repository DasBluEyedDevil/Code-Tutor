---
type: "ANALOGY"
title: "The Live Radio Broadcast"
---

A Serverpod stream is like tuning into a live radio station. Once you turn the dial and connect (open the stream), the station continuously broadcasts music and news to your radio without you needing to do anything. You do not have to keep pressing a "refresh" button to hear the next song -- it just arrives.

When the DJ (server) has something new to share, every tuned-in radio (connected client) receives it simultaneously. If you turn off your radio (close the stream), you stop receiving updates but the broadcast continues for everyone else. And if the signal drops (network interruption), a good radio automatically tries to re-tune to the station.

**Serverpod streams bring this radio model to your Dart code.** Your Flutter app opens a stream connection, and the server pushes data through it as events occur -- new chat messages, status changes, or live updates. The `Stream` and `StreamController` in Dart handle all the plumbing, letting you focus on what to broadcast and how to display it.
