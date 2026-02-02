---
type: "EXAMPLE"
title: "Real-World Example - Adaptive Dashboard"
---


A complete dashboard that adapts to all screen sizes:



```dart
import 'package:flutter/material.dart';

// Device type enum and breakpoints
enum DeviceType { mobile, tablet, desktop }

class ResponsiveDashboard extends StatefulWidget {
  const ResponsiveDashboard({super.key});

  @override
  State<ResponsiveDashboard> createState() => _ResponsiveDashboardState();
}

class _ResponsiveDashboardState extends State<ResponsiveDashboard> {
  int _selectedIndex = 0;
  
  DeviceType _getDeviceType(double width) {
    if (width < 600) return DeviceType.mobile;
    if (width < 1100) return DeviceType.tablet;
    return DeviceType.desktop;
  }
  
  @override
  Widget build(BuildContext context) {
    return LayoutBuilder(
      builder: (context, constraints) {
        final deviceType = _getDeviceType(constraints.maxWidth);
        
        switch (deviceType) {
          case DeviceType.mobile:
            return _MobileLayout(
              selectedIndex: _selectedIndex,
              onDestinationSelected: (i) => setState(() => _selectedIndex = i),
            );
          case DeviceType.tablet:
            return _TabletLayout(
              selectedIndex: _selectedIndex,
              onDestinationSelected: (i) => setState(() => _selectedIndex = i),
            );
          case DeviceType.desktop:
            return _DesktopLayout(
              selectedIndex: _selectedIndex,
              onDestinationSelected: (i) => setState(() => _selectedIndex = i),
            );
        }
      },
    );
  }
}

// Mobile: Bottom navigation, single column
class _MobileLayout extends StatelessWidget {
  final int selectedIndex;
  final ValueChanged<int> onDestinationSelected;
  
  const _MobileLayout({
    required this.selectedIndex,
    required this.onDestinationSelected,
  });

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(title: const Text('Dashboard')),
      body: _DashboardContent(columns: 1),
      bottomNavigationBar: NavigationBar(
        selectedIndex: selectedIndex,
        onDestinationSelected: onDestinationSelected,
        destinations: const [
          NavigationDestination(icon: Icon(Icons.home), label: 'Home'),
          NavigationDestination(icon: Icon(Icons.analytics), label: 'Analytics'),
          NavigationDestination(icon: Icon(Icons.settings), label: 'Settings'),
        ],
      ),
    );
  }
}

// Tablet: Navigation rail, two columns
class _TabletLayout extends StatelessWidget {
  final int selectedIndex;
  final ValueChanged<int> onDestinationSelected;
  
  const _TabletLayout({
    required this.selectedIndex,
    required this.onDestinationSelected,
  });

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Row(
        children: [
          NavigationRail(
            selectedIndex: selectedIndex,
            onDestinationSelected: onDestinationSelected,
            labelType: NavigationRailLabelType.all,
            destinations: const [
              NavigationRailDestination(
                icon: Icon(Icons.home_outlined),
                selectedIcon: Icon(Icons.home),
                label: Text('Home'),
              ),
              NavigationRailDestination(
                icon: Icon(Icons.analytics_outlined),
                selectedIcon: Icon(Icons.analytics),
                label: Text('Analytics'),
              ),
              NavigationRailDestination(
                icon: Icon(Icons.settings_outlined),
                selectedIcon: Icon(Icons.settings),
                label: Text('Settings'),
              ),
            ],
          ),
          const VerticalDivider(thickness: 1, width: 1),
          Expanded(
            child: Column(
              children: [
                AppBar(title: const Text('Dashboard')),
                Expanded(child: _DashboardContent(columns: 2)),
              ],
            ),
          ),
        ],
      ),
    );
  }
}

// Desktop: Full sidebar, three columns with detail panel
class _DesktopLayout extends StatelessWidget {
  final int selectedIndex;
  final ValueChanged<int> onDestinationSelected;
  
  const _DesktopLayout({
    required this.selectedIndex,
    required this.onDestinationSelected,
  });

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Row(
        children: [
          // Sidebar navigation
          NavigationDrawer(
            selectedIndex: selectedIndex,
            onDestinationSelected: onDestinationSelected,
            children: [
              const DrawerHeader(
                child: Text('Dashboard', style: TextStyle(fontSize: 24)),
              ),
              const NavigationDrawerDestination(
                icon: Icon(Icons.home_outlined),
                selectedIcon: Icon(Icons.home),
                label: Text('Home'),
              ),
              const NavigationDrawerDestination(
                icon: Icon(Icons.analytics_outlined),
                selectedIcon: Icon(Icons.analytics),
                label: Text('Analytics'),
              ),
              const NavigationDrawerDestination(
                icon: Icon(Icons.settings_outlined),
                selectedIcon: Icon(Icons.settings),
                label: Text('Settings'),
              ),
            ],
          ),
          // Main content
          Expanded(
            flex: 2,
            child: Column(
              children: [
                AppBar(
                  title: const Text('Dashboard'),
                  automaticallyImplyLeading: false,
                ),
                Expanded(child: _DashboardContent(columns: 3)),
              ],
            ),
          ),
          // Detail panel
          const VerticalDivider(thickness: 1, width: 1),
          Expanded(
            flex: 1,
            child: Column(
              children: [
                AppBar(
                  title: const Text('Details'),
                  automaticallyImplyLeading: false,
                ),
                const Expanded(
                  child: Center(child: Text('Select an item to see details')),
                ),
              ],
            ),
          ),
        ],
      ),
    );
  }
}

// Dashboard content grid
class _DashboardContent extends StatelessWidget {
  final int columns;
  
  const _DashboardContent({required this.columns});

  @override
  Widget build(BuildContext context) {
    return GridView.count(
      crossAxisCount: columns,
      padding: const EdgeInsets.all(16),
      mainAxisSpacing: 16,
      crossAxisSpacing: 16,
      children: [
        _StatCard(title: 'Users', value: '1,234', icon: Icons.people, color: Colors.blue),
        _StatCard(title: 'Revenue', value: '\$12.5k', icon: Icons.attach_money, color: Colors.green),
        _StatCard(title: 'Orders', value: '456', icon: Icons.shopping_cart, color: Colors.orange),
        _StatCard(title: 'Growth', value: '+12%', icon: Icons.trending_up, color: Colors.purple),
        _StatCard(title: 'Active', value: '89', icon: Icons.timer, color: Colors.red),
        _StatCard(title: 'Rating', value: '4.8', icon: Icons.star, color: Colors.amber),
      ],
    );
  }
}

class _StatCard extends StatelessWidget {
  final String title;
  final String value;
  final IconData icon;
  final Color color;
  
  const _StatCard({
    required this.title,
    required this.value,
    required this.icon,
    required this.color,
  });

  @override
  Widget build(BuildContext context) {
    return Card(
      child: Padding(
        padding: const EdgeInsets.all(16),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Icon(icon, size: 40, color: color),
            const SizedBox(height: 8),
            Text(
              value,
              style: Theme.of(context).textTheme.headlineMedium?.copyWith(
                fontWeight: FontWeight.bold,
              ),
            ),
            Text(title, style: Theme.of(context).textTheme.bodyMedium),
          ],
        ),
      ),
    );
  }
}
```
