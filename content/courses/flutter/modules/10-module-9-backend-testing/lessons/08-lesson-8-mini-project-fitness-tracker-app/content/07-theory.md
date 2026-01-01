---
type: "THEORY"
title: "Step 5: Home Screen with Step Counter"
---


**lib/screens/home_screen.dart:**



```dart
import 'package:flutter/material.dart';
import 'package:sensors_plus/sensors_plus.dart';
import 'package:intl/intl.dart';
import 'dart:async';
import '../services/database_service.dart';

class HomeScreen extends StatefulWidget {
  @override
  State<HomeScreen> createState() => _HomeScreenState();
}

class _HomeScreenState extends State<HomeScreen> with TickerProviderStateMixin {
  int _todaySteps = 0;
  int _stepGoal = 10000;
  StreamSubscription? _accelerometerSubscription;
  List<double> _recentAcceleration = [];

  late AnimationController _progressController;
  late Animation<double> _progressAnimation;

  @override
  void initState() {
    super.initState();

    _progressController = AnimationController(
      vsync: this,
      duration: Duration(milliseconds: 1000),
    );

    _progressAnimation = Tween<double>(begin: 0, end: 0).animate(
      CurvedAnimation(parent: _progressController, curve: Curves.easeInOut),
    );

    _loadTodaySteps();
    _startStepCounter();
  }

  @override
  void dispose() {
    _accelerometerSubscription?.cancel();
    _progressController.dispose();
    super.dispose();
  }

  Future<void> _loadTodaySteps() async {
    final today = DateFormat('yyyy-MM-dd').format(DateTime.now());
    final steps = await DatabaseService().getStepsForDate(today);

    setState(() {
      _todaySteps = steps ?? 0;
      _updateProgress();
    });
  }

  void _updateProgress() {
    final progress = (_todaySteps / _stepGoal).clamp(0.0, 1.0);

    _progressAnimation = Tween<double>(
      begin: _progressAnimation.value,
      end: progress,
    ).animate(
      CurvedAnimation(parent: _progressController, curve: Curves.easeInOut),
    );

    _progressController.reset();
    _progressController.forward();
  }

  void _startStepCounter() {
    _accelerometerSubscription = accelerometerEventStream().listen((event) {
      final magnitude = (event.x * event.x + event.y * event.y + event.z * event.z);

      _recentAcceleration.add(magnitude);
      if (_recentAcceleration.length > 10) {
        _recentAcceleration.removeAt(0);
      }

      // Simple step detection: detect peaks in acceleration
      if (_recentAcceleration.length == 10) {
        final avg = _recentAcceleration.reduce((a, b) => a + b) / _recentAcceleration.length;

        if (magnitude > avg * 1.5 && magnitude > 150) {
          setState(() {
            _todaySteps++;
            _updateProgress();
          });

          _saveTodaySteps();
        }
      }
    });
  }

  Future<void> _saveTodaySteps() async {
    final today = DateFormat('yyyy-MM-dd').format(DateTime.now());
    await DatabaseService().saveDailySteps(today, _todaySteps);
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text('Fitness Tracker'),
        actions: [
          IconButton(
            icon: Icon(Icons.person),
            onPressed: () {
              Navigator.push(
                context,
                MaterialPageRoute(builder: (_) => ProfileScreen()),
              );
            },
          ),
        ],
      ),
      body: SingleChildScrollView(
        padding: EdgeInsets.all(16),
        child: Column(
          children: [
            // Animated Step Counter
            AnimatedBuilder(
              animation: _progressAnimation,
              builder: (context, child) {
                return CustomPaint(
                  size: Size(200, 200),
                  painter: CircularProgressPainter(
                    progress: _progressAnimation.value,
                    color: Theme.of(context).primaryColor,
                  ),
                  child: Container(
                    width: 200,
                    height: 200,
                    child: Center(
                      child: Column(
                        mainAxisAlignment: MainAxisAlignment.center,
                        children: [
                          Text(
                            '$_todaySteps',
                            style: TextStyle(
                              fontSize: 48,
                              fontWeight: FontWeight.bold,
                            ),
                          ),
                          Text(
                            'steps',
                            style: TextStyle(
                              fontSize: 18,
                              color: Colors.grey,
                            ),
                          ),
                          SizedBox(height: 8),
                          Text(
                            'Goal: $_stepGoal',
                            style: TextStyle(fontSize: 14, color: Colors.grey),
                          ),
                        ],
                      ),
                    ),
                  ),
                );
              },
            ),

            SizedBox(height: 40),

            // Quick Actions
            _buildQuickActionButton(
              icon: Icons.directions_run,
              label: 'Start Running',
              color: Colors.blue,
              onTap: () {
                // Navigate to workout tracker
                ScaffoldMessenger.of(context).showSnackBar(
                  SnackBar(content: Text('Starting workout tracker...')),
                );
              },
            ),

            SizedBox(height: 12),

            _buildQuickActionButton(
              icon: Icons.history,
              label: 'Workout History',
              color: Colors.green,
              onTap: () {
                // Navigate to history
              },
            ),

            SizedBox(height: 12),

            _buildQuickActionButton(
              icon: Icons.bar_chart,
              label: 'Statistics',
              color: Colors.orange,
              onTap: () {
                // Navigate to stats
              },
            ),
          ],
        ),
      ),
    );
  }

  Widget _buildQuickActionButton({
    required IconData icon,
    required String label,
    required Color color,
    required VoidCallback onTap,
  }) {
    return Card(
      child: InkWell(
        onTap: onTap,
        borderRadius: BorderRadius.circular(12),
        child: Padding(
          padding: EdgeInsets.all(16),
          child: Row(
            children: [
              Container(
                padding: EdgeInsets.all(12),
                decoration: BoxDecoration(
                  color: color.withOpacity(0.1),
                  borderRadius: BorderRadius.circular(12),
                ),
                child: Icon(icon, color: color, size: 32),
              ),
              SizedBox(width: 16),
              Text(
                label,
                style: TextStyle(fontSize: 18, fontWeight: FontWeight.bold),
              ),
              Spacer(),
              Icon(Icons.arrow_forward_ios, size: 16, color: Colors.grey),
            ],
          ),
        ),
      ),
    );
  }
}

// Custom painter for circular progress
class CircularProgressPainter extends CustomPainter {
  final double progress;
  final Color color;

  CircularProgressPainter({required this.progress, required this.color});

  @override
  void paint(Canvas canvas, Size size) {
    final center = Offset(size.width / 2, size.height / 2);
    final radius = size.width / 2;

    // Background circle
    final bgPaint = Paint()
      ..color = color.withOpacity(0.1)
      ..strokeWidth = 20
      ..style = PaintingStyle.stroke;

    canvas.drawCircle(center, radius - 10, bgPaint);

    // Progress arc
    final progressPaint = Paint()
      ..color = color
      ..strokeWidth = 20
      ..style = PaintingStyle.stroke
      ..strokeCap = StrokeCap.round;

    canvas.drawArc(
      Rect.fromCircle(center: center, radius: radius - 10),
      -90 * (3.14159 / 180),  // Start at top
      progress * 360 * (3.14159 / 180),  // Sweep angle
      false,
      progressPaint,
    );
  }

  @override
  bool shouldRepaint(CircularProgressPainter oldDelegate) {
    return oldDelegate.progress != progress;
  }
}
```
