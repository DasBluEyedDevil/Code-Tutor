---
type: "EXAMPLE"
title: "Building a Responsive Breakpoint System"
---


Create reusable responsive utilities:



```dart
import 'package:flutter/material.dart';

// Breakpoint definitions
enum DeviceType { mobile, tablet, desktop }

class Breakpoints {
  static const double mobile = 600;
  static const double tablet = 900;
  static const double desktop = 1200;
  
  static DeviceType getDeviceType(BuildContext context) {
    final width = MediaQuery.sizeOf(context).width;
    if (width < mobile) return DeviceType.mobile;
    if (width < tablet) return DeviceType.tablet;
    return DeviceType.desktop;
  }
  
  static bool isMobile(BuildContext context) =>
      getDeviceType(context) == DeviceType.mobile;
  
  static bool isTablet(BuildContext context) =>
      getDeviceType(context) == DeviceType.tablet;
  
  static bool isDesktop(BuildContext context) =>
      getDeviceType(context) == DeviceType.desktop;
  
  // Get value based on screen size
  static T value<T>({
    required BuildContext context,
    required T mobile,
    T? tablet,
    T? desktop,
  }) {
    final deviceType = getDeviceType(context);
    switch (deviceType) {
      case DeviceType.desktop:
        return desktop ?? tablet ?? mobile;
      case DeviceType.tablet:
        return tablet ?? mobile;
      case DeviceType.mobile:
        return mobile;
    }
  }
}

// Responsive builder widget
class ResponsiveBuilder extends StatelessWidget {
  final Widget Function(BuildContext, DeviceType) builder;
  
  const ResponsiveBuilder({super.key, required this.builder});

  @override
  Widget build(BuildContext context) {
    return builder(context, Breakpoints.getDeviceType(context));
  }
}

// Alternative: Separate widgets for each size
class ResponsiveLayout extends StatelessWidget {
  final Widget mobile;
  final Widget? tablet;
  final Widget? desktop;
  
  const ResponsiveLayout({
    super.key,
    required this.mobile,
    this.tablet,
    this.desktop,
  });

  @override
  Widget build(BuildContext context) {
    return LayoutBuilder(
      builder: (context, constraints) {
        if (constraints.maxWidth >= Breakpoints.desktop) {
          return desktop ?? tablet ?? mobile;
        } else if (constraints.maxWidth >= Breakpoints.mobile) {
          return tablet ?? mobile;
        }
        return mobile;
      },
    );
  }
}

// Usage examples
class BreakpointDemo extends StatelessWidget {
  const BreakpointDemo({super.key});

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Column(
        children: [
          // Using value helper
          Padding(
            padding: EdgeInsets.all(
              Breakpoints.value(
                context: context,
                mobile: 8.0,
                tablet: 16.0,
                desktop: 24.0,
              ),
            ),
            child: Text(
              'Padding adapts to screen size',
              style: TextStyle(
                fontSize: Breakpoints.value(
                  context: context,
                  mobile: 14.0,
                  tablet: 16.0,
                  desktop: 18.0,
                ),
              ),
            ),
          ),
          
          // Using ResponsiveLayout
          Expanded(
            child: ResponsiveLayout(
              mobile: const MobileLayout(),
              tablet: const TabletLayout(),
              desktop: const DesktopLayout(),
            ),
          ),
        ],
      ),
    );
  }
}

class MobileLayout extends StatelessWidget {
  const MobileLayout({super.key});
  @override
  Widget build(BuildContext context) => const Center(child: Text('Mobile'));
}

class TabletLayout extends StatelessWidget {
  const TabletLayout({super.key});
  @override
  Widget build(BuildContext context) => const Center(child: Text('Tablet'));
}

class DesktopLayout extends StatelessWidget {
  const DesktopLayout({super.key});
  @override
  Widget build(BuildContext context) => const Center(child: Text('Desktop'));
}
```
