---
type: "THEORY"
title: "The Problem: Java's Ceremony"
---

Traditional Java requires a lot of boilerplate just to print 'Hello':

public class Main {
    public static void main(String[] args) {
        System.out.println("Hello, World!");
    }
}

That's 5 lines and 4 concepts (public, class, static, void, String[] args) just to print one message!

Java 23 introduces features that eliminate this ceremony, making Java as concise as Python or JavaScript for simple programs.