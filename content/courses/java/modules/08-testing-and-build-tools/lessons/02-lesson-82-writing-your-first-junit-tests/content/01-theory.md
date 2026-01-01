---
type: "THEORY"
title: "The Anatomy of a JUnit Test"
---

A JUnit test has three parts: ARRANGE, ACT, ASSERT

@Test
public void testAdd() {
    // ARRANGE: Set up test data
    Calculator calc = new Calculator();
    
    // ACT: Call the method being tested
    int result = calc.add(2, 3);
    
    // ASSERT: Verify the result
    assertEquals(5, result);
}

KEY ANNOTATIONS:
@Test - Marks a method as a test
@BeforeEach - Runs before each test (setup)
@AfterEach - Runs after each test (cleanup)
@BeforeAll - Runs once before all tests
@AfterAll - Runs once after all tests