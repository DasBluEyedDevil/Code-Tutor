---
type: "THEORY"
title: "The Problem: Many Related Values"
---

You want to store the scores of 5 students:

int score1 = 85;
int score2 = 92;
int score3 = 78;
int score4 = 95;
int score5 = 88;

To calculate the average:
int total = score1 + score2 + score3 + score4 + score5;
double avg = total / 5.0;

This doesn't scale! What if you have 100 students? 1000?

You need a way to store MANY values of the same type under ONE name.

This is what ARRAYS do!