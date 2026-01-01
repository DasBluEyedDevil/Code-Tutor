---
type: "THEORY"
title: "Type Conversion and Common Mistakes"
---

⚠️ Java is STRICT about types:

WRONG:
int x = 3.14;  // ERROR! Can't put decimal in int
String age = 25;  // ERROR! Numbers need quotes to be Strings

CORRECT:
int x = 3;  // Whole number for int
double y = 3.14;  // Decimal for double
String age = "25";  // Text needs quotes

Converting between types:
int age = 25;
String ageText = "" + age;  // Converts to "25"
// OR
String ageText = String.valueOf(age);

double price = 19.99;
int dollars = (int) price;  // Truncates to 19 (loses .99!)

Key rule: Java won't automatically convert if data could be lost.