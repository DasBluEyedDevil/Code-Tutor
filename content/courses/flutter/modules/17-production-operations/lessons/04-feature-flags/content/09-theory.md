---
type: "THEORY"
title: "A/B Testing with Firebase"
---


**What is A/B Testing?**

A/B testing (also called split testing) shows different variants to different users and measures which performs better. Firebase integrates Remote Config with Analytics for built-in A/B testing.

**The A/B Testing Process**

1. **Hypothesis** - "Variant B will increase conversions by 10%"
2. **Create Experiment** - Set up in Firebase Console
3. **Define Variants** - Control vs. one or more test variants
4. **Set Goals** - What metric defines success
5. **Run Experiment** - Let it collect data
6. **Analyze Results** - Check statistical significance
7. **Roll Out Winner** - Or iterate with new hypothesis

**Setting Up A/B Tests in Firebase Console**

1. Go to Remote Config > A/B Testing
2. Click "Create experiment"
3. Configure:
   - Name and description
   - Target users (% to include)
   - Variants and their values
   - Goal metrics (from Analytics)
4. Start experiment

**Choosing Goal Metrics**

Common metrics to measure:

- **Conversion events** - purchase, signup, subscription
- **Engagement** - session duration, screens per session
- **Revenue** - average revenue per user (ARPU)
- **Retention** - users returning after N days
- **Custom events** - any Analytics event you track

**Statistical Significance**

Firebase tells you when results are significant:

- **Leader** - Variant performing best
- **Probability to beat baseline** - Confidence in results
- **Required sample size** - Users needed for significance

**Best Practices**

1. **Test one thing at a time** - Don't change too many variables
2. **Have enough users** - Small samples = unreliable results
3. **Run long enough** - Account for day-of-week effects
4. **Define success upfront** - Don't move goalposts
5. **Document learnings** - Build institutional knowledge

**Logging Experiment Exposure**

Track which users saw which variant:

```dart
await analytics.logEvent(
  name: 'experiment_exposure',
  parameters: {
    'experiment_name': 'checkout_redesign',
    'variant': variant,
  },
);
```

