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

Java 25 finalizes features (JEP 512: Compact Source Files) that eliminate this ceremony, making Java as concise as Python or JavaScript for simple programs.