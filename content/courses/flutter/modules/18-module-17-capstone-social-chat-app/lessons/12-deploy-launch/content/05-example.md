---
type: "EXAMPLE"
title: "Post-Launch Operations"
---


**Monitoring, Feedback Collection, and Iteration**

After launch, continuous monitoring and rapid response capabilities are essential. This section covers Crashlytics integration, user feedback systems, hotfix deployment, and planning the next iteration.



```dart
# ============================================================
# Crashlytics Integration and Monitoring
# ============================================================
// lib/core/monitoring/crashlytics_service.dart
import 'dart:async';
import 'dart:isolate';
import 'package:firebase_crashlytics/firebase_crashlytics.dart';
import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';

class CrashlyticsService {
  static final FirebaseCrashlytics _crashlytics = FirebaseCrashlytics.instance;

  /// Initialize Crashlytics with proper error handling
  static Future<void> initialize() async {
    // Enable collection only in release mode
    await _crashlytics.setCrashlyticsCollectionEnabled(!kDebugMode);

    // Catch Flutter framework errors
    FlutterError.onError = (errorDetails) {
      _crashlytics.recordFlutterFatalError(errorDetails);
    };

    // Catch errors outside of Flutter
    PlatformDispatcher.instance.onError = (error, stack) {
      _crashlytics.recordError(error, stack, fatal: true);
      return true;
    };

    // Catch errors in isolates
    Isolate.current.addErrorListener(RawReceivePort((pair) async {
      final List<dynamic> errorAndStacktrace = pair;
      await _crashlytics.recordError(
        errorAndStacktrace.first,
        errorAndStacktrace.last,
        fatal: true,
      );
    }).sendPort);
  }

  /// Set user identifier for crash reports
  static Future<void> setUserId(String userId) async {
    await _crashlytics.setUserIdentifier(userId);
  }

  /// Add custom keys for debugging
  static Future<void> setCustomKey(String key, dynamic value) async {
    await _crashlytics.setCustomKey(key, value.toString());
  }

  /// Log breadcrumb for crash context
  static Future<void> log(String message) async {
    await _crashlytics.log(message);
  }

  /// Record non-fatal error
  static Future<void> recordError(
    dynamic exception,
    StackTrace? stack, {
    String? reason,
    bool fatal = false,
  }) async {
    await _crashlytics.recordError(
      exception,
      stack,
      reason: reason,
      fatal: fatal,
    );
  }
}

// Usage in main.dart
void main() async {
  await runZonedGuarded(
    () async {
      WidgetsFlutterBinding.ensureInitialized();
      await Firebase.initializeApp();
      await CrashlyticsService.initialize();

      runApp(const MyApp());
    },
    (error, stack) {
      CrashlyticsService.recordError(error, stack, fatal: true);
    },
  );
}

---

# ============================================================
# User Feedback Collection System
# ============================================================
// lib/features/feedback/presentation/feedback_sheet.dart
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import '../data/feedback_repository.dart';

class FeedbackSheet extends ConsumerStatefulWidget {
  final String? screenName;
  final String? featureName;

  const FeedbackSheet({super.key, this.screenName, this.featureName});

  static Future<void> show(BuildContext context, {
    String? screenName,
    String? featureName,
  }) {
    return showModalBottomSheet(
      context: context,
      isScrollControlled: true,
      useSafeArea: true,
      builder: (_) => FeedbackSheet(
        screenName: screenName,
        featureName: featureName,
      ),
    );
  }

  @override
  ConsumerState<FeedbackSheet> createState() => _FeedbackSheetState();
}

class _FeedbackSheetState extends ConsumerState<FeedbackSheet> {
  final _controller = TextEditingController();
  FeedbackType _type = FeedbackType.suggestion;
  int _rating = 0;
  bool _includeScreenshot = true;
  bool _isSubmitting = false;

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: EdgeInsets.only(
        bottom: MediaQuery.of(context).viewInsets.bottom,
        left: 16,
        right: 16,
        top: 16,
      ),
      child: Column(
        mainAxisSize: MainAxisSize.min,
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Row(
            children: [
              Text(
                'Send Feedback',
                style: Theme.of(context).textTheme.titleLarge,
              ),
              const Spacer(),
              IconButton(
                icon: const Icon(Icons.close),
                onPressed: () => Navigator.pop(context),
              ),
            ],
          ),
          const SizedBox(height: 16),

          // Feedback type selector
          SegmentedButton<FeedbackType>(
            segments: const [
              ButtonSegment(
                value: FeedbackType.bug,
                label: Text('Bug'),
                icon: Icon(Icons.bug_report),
              ),
              ButtonSegment(
                value: FeedbackType.suggestion,
                label: Text('Idea'),
                icon: Icon(Icons.lightbulb),
              ),
              ButtonSegment(
                value: FeedbackType.other,
                label: Text('Other'),
                icon: Icon(Icons.chat),
              ),
            ],
            selected: {_type},
            onSelectionChanged: (types) {
              setState(() => _type = types.first);
            },
          ),
          const SizedBox(height: 16),

          // Rating (for suggestions)
          if (_type == FeedbackType.suggestion) ...[
            Text(
              'How important is this to you?',
              style: Theme.of(context).textTheme.bodyMedium,
            ),
            const SizedBox(height: 8),
            Row(
              mainAxisAlignment: MainAxisAlignment.center,
              children: List.generate(5, (index) {
                return IconButton(
                  icon: Icon(
                    index < _rating ? Icons.star : Icons.star_border,
                    color: Colors.amber,
                  ),
                  onPressed: () => setState(() => _rating = index + 1),
                );
              }),
            ),
            const SizedBox(height: 16),
          ],

          // Feedback text
          TextField(
            controller: _controller,
            maxLines: 5,
            decoration: InputDecoration(
              hintText: _type == FeedbackType.bug
                  ? 'Please describe what happened and steps to reproduce...'
                  : 'Tell us your thoughts...',
              border: const OutlineInputBorder(),
            ),
          ),
          const SizedBox(height: 16),

          // Screenshot toggle
          SwitchListTile(
            title: const Text('Include screenshot'),
            subtitle: const Text('Helps us understand the issue'),
            value: _includeScreenshot,
            onChanged: (value) {
              setState(() => _includeScreenshot = value);
            },
          ),
          const SizedBox(height: 16),

          // Submit button
          SizedBox(
            width: double.infinity,
            child: FilledButton(
              onPressed: _isSubmitting ? null : _submitFeedback,
              child: _isSubmitting
                  ? const SizedBox(
                      height: 20,
                      width: 20,
                      child: CircularProgressIndicator(strokeWidth: 2),
                    )
                  : const Text('Submit Feedback'),
            ),
          ),
          const SizedBox(height: 16),
        ],
      ),
    );
  }

  Future<void> _submitFeedback() async {
    if (_controller.text.trim().isEmpty) {
      ScaffoldMessenger.of(context).showSnackBar(
        const SnackBar(content: Text('Please enter your feedback')),
      );
      return;
    }

    setState(() => _isSubmitting = true);

    try {
      await ref.read(feedbackRepositoryProvider).submitFeedback(
            type: _type,
            message: _controller.text.trim(),
            rating: _rating,
            screenName: widget.screenName,
            featureName: widget.featureName,
            includeScreenshot: _includeScreenshot,
          );

      if (mounted) {
        Navigator.pop(context);
        ScaffoldMessenger.of(context).showSnackBar(
          const SnackBar(
            content: Text('Thank you for your feedback!'),
            behavior: SnackBarBehavior.floating,
          ),
        );
      }
    } catch (e) {
      if (mounted) {
        ScaffoldMessenger.of(context).showSnackBar(
          SnackBar(
            content: Text('Failed to submit: $e'),
            backgroundColor: Theme.of(context).colorScheme.error,
          ),
        );
      }
    } finally {
      if (mounted) {
        setState(() => _isSubmitting = false);
      }
    }
  }

  @override
  void dispose() {
    _controller.dispose();
    super.dispose();
  }
}

enum FeedbackType { bug, suggestion, other }

---

# ============================================================
# Hotfix Deployment Process
# ============================================================
# .github/workflows/hotfix.yml
name: Hotfix Deployment

on:
  workflow_dispatch:
    inputs:
      version:
        description: 'Hotfix version (e.g., 1.0.1)'
        required: true
      description:
        description: 'Brief description of the fix'
        required: true
      platforms:
        description: 'Platforms to deploy'
        required: true
        default: 'both'
        type: choice
        options:
          - android
          - ios
          - both

jobs:
  create-hotfix-branch:
    runs-on: ubuntu-latest
    outputs:
      branch: ${{ steps.branch.outputs.name }}
    steps:
      - uses: actions/checkout@v4
        with:
          fetch-depth: 0

      - name: Create hotfix branch
        id: branch
        run: |
          BRANCH="hotfix/v${{ github.event.inputs.version }}"
          git checkout -b $BRANCH
          git push origin $BRANCH
          echo "name=$BRANCH" >> $GITHUB_OUTPUT

  build-android:
    needs: create-hotfix-branch
    if: contains(github.event.inputs.platforms, 'android') || github.event.inputs.platforms == 'both'
    runs-on: ubuntu-latest
    steps:
      - uses: actions/checkout@v4
        with:
          ref: ${{ needs.create-hotfix-branch.outputs.branch }}

      - uses: subosito/flutter-action@v2
        with:
          flutter-version: '3.24.0'
          channel: 'stable'

      - name: Build Android
        run: |
          flutter build appbundle --release \
            --build-number=${{ github.run_number }}

      - name: Deploy to Play Store (Production 100%)
        uses: r0adkll/upload-google-play@v1
        with:
          serviceAccountJsonPlainText: ${{ secrets.PLAY_STORE_SERVICE_ACCOUNT }}
          packageName: com.yourcompany.yourapp
          releaseFiles: build/app/outputs/bundle/release/app-release.aab
          track: production
          status: completed
          changelogs: "[HOTFIX] ${{ github.event.inputs.description }}"

  build-ios:
    needs: create-hotfix-branch
    if: contains(github.event.inputs.platforms, 'ios') || github.event.inputs.platforms == 'both'
    runs-on: macos-latest
    steps:
      - uses: actions/checkout@v4
        with:
          ref: ${{ needs.create-hotfix-branch.outputs.branch }}

      - uses: subosito/flutter-action@v2
        with:
          flutter-version: '3.24.0'
          channel: 'stable'

      - name: Install Apple certificate
        env:
          CERTIFICATE_BASE64: ${{ secrets.APPLE_CERTIFICATE }}
          CERTIFICATE_PASSWORD: ${{ secrets.APPLE_CERTIFICATE_PASSWORD }}
        run: |
          # Install certificates and provisioning profiles
          echo "$CERTIFICATE_BASE64" | base64 --decode > certificate.p12
          security import certificate.p12 -P "$CERTIFICATE_PASSWORD"

      - name: Build iOS
        run: |
          flutter build ipa --release \
            --build-number=${{ github.run_number }} \
            --export-options-plist=ios/ExportOptions.plist

      - name: Upload to App Store (Expedited Review)
        run: |
          xcrun altool --upload-app \
            --type ios \
            --file build/ios/ipa/*.ipa \
            --username "${{ secrets.APPLE_ID }}" \
            --password "${{ secrets.APPLE_APP_PASSWORD }}"

          # Request expedited review via App Store Connect API
          # Note: Requires App Store Connect API key
          echo "Remember to request expedited review in App Store Connect!"

  notify:
    needs: [build-android, build-ios]
    if: always()
    runs-on: ubuntu-latest
    steps:
      - name: Send Slack notification
        uses: slackapi/slack-github-action@v1
        with:
          payload: |
            {
              "channel": "#releases",
              "text": "Hotfix v${{ github.event.inputs.version }} deployed",
              "blocks": [
                {
                  "type": "section",
                  "text": {
                    "type": "mrkdwn",
                    "text": "*Hotfix Deployed* :ambulance:\n${{ github.event.inputs.description }}"
                  }
                },
                {
                  "type": "section",
                  "fields": [
                    { "type": "mrkdwn", "text": "*Version:* v${{ github.event.inputs.version }}" },
                    { "type": "mrkdwn", "text": "*Platforms:* ${{ github.event.inputs.platforms }}" },
                    { "type": "mrkdwn", "text": "*Android:* ${{ needs.build-android.result }}" },
                    { "type": "mrkdwn", "text": "*iOS:* ${{ needs.build-ios.result }}" }
                  ]
                }
              ]
            }
        env:
          SLACK_WEBHOOK_URL: ${{ secrets.SLACK_WEBHOOK }}

---

# ============================================================
# Analytics Review Dashboard
# ============================================================
// lib/features/analytics/presentation/analytics_dashboard.dart
import 'package:flutter/material.dart';
import 'package:flutter_riverpod/flutter_riverpod.dart';
import 'package:fl_chart/fl_chart.dart';
import '../providers/analytics_providers.dart';

class AnalyticsDashboard extends ConsumerWidget {
  const AnalyticsDashboard({super.key});

  @override
  Widget build(BuildContext context, WidgetRef ref) {
    final metrics = ref.watch(launchMetricsProvider);

    return Scaffold(
      appBar: AppBar(
        title: const Text('Launch Analytics'),
        actions: [
          IconButton(
            icon: const Icon(Icons.refresh),
            onPressed: () => ref.invalidate(launchMetricsProvider),
          ),
        ],
      ),
      body: metrics.when(
        loading: () => const Center(child: CircularProgressIndicator()),
        error: (e, _) => Center(child: Text('Error: $e')),
        data: (data) => _buildDashboard(context, data),
      ),
    );
  }

  Widget _buildDashboard(BuildContext context, LaunchMetrics metrics) {
    return SingleChildScrollView(
      padding: const EdgeInsets.all(16),
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          // Key metrics row
          Row(
            children: [
              Expanded(
                child: _MetricCard(
                  title: 'Daily Active Users',
                  value: metrics.dau.toString(),
                  change: metrics.dauChange,
                  icon: Icons.people,
                ),
              ),
              const SizedBox(width: 16),
              Expanded(
                child: _MetricCard(
                  title: 'Crash-Free Rate',
                  value: '${metrics.crashFreeRate.toStringAsFixed(1)}%',
                  change: metrics.crashFreeChange,
                  icon: Icons.check_circle,
                ),
              ),
            ],
          ),
          const SizedBox(height: 16),
          Row(
            children: [
              Expanded(
                child: _MetricCard(
                  title: 'Avg Session',
                  value: '${metrics.avgSessionMinutes.toStringAsFixed(1)}m',
                  change: metrics.sessionChange,
                  icon: Icons.timer,
                ),
              ),
              const SizedBox(width: 16),
              Expanded(
                child: _MetricCard(
                  title: 'Retention (D1)',
                  value: '${metrics.d1Retention.toStringAsFixed(1)}%',
                  change: metrics.retentionChange,
                  icon: Icons.repeat,
                ),
              ),
            ],
          ),
          const SizedBox(height: 24),

          // DAU Chart
          Text(
            'Daily Active Users (Last 7 Days)',
            style: Theme.of(context).textTheme.titleMedium,
          ),
          const SizedBox(height: 16),
          SizedBox(
            height: 200,
            child: LineChart(
              LineChartData(
                gridData: const FlGridData(show: false),
                titlesData: const FlTitlesData(show: false),
                borderData: FlBorderData(show: false),
                lineBarsData: [
                  LineChartBarData(
                    spots: metrics.dauHistory
                        .asMap()
                        .entries
                        .map((e) => FlSpot(e.key.toDouble(), e.value.toDouble()))
                        .toList(),
                    isCurved: true,
                    color: Theme.of(context).colorScheme.primary,
                    barWidth: 3,
                    dotData: const FlDotData(show: false),
                    belowBarData: BarAreaData(
                      show: true,
                      color: Theme.of(context).colorScheme.primary.withOpacity(0.1),
                    ),
                  ),
                ],
              ),
            ),
          ),
          const SizedBox(height: 24),

          // Top crashes
          Text(
            'Top Crashes',
            style: Theme.of(context).textTheme.titleMedium,
          ),
          const SizedBox(height: 16),
          ...metrics.topCrashes.map((crash) => _CrashItem(crash: crash)),
          const SizedBox(height: 24),

          // User feedback
          Text(
            'Recent Feedback',
            style: Theme.of(context).textTheme.titleMedium,
          ),
          const SizedBox(height: 16),
          ...metrics.recentFeedback.map((fb) => _FeedbackItem(feedback: fb)),
        ],
      ),
    );
  }
}

class _MetricCard extends StatelessWidget {
  final String title;
  final String value;
  final double change;
  final IconData icon;

  const _MetricCard({
    required this.title,
    required this.value,
    required this.change,
    required this.icon,
  });

  @override
  Widget build(BuildContext context) {
    final isPositive = change >= 0;

    return Card(
      child: Padding(
        padding: const EdgeInsets.all(16),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Row(
              children: [
                Icon(icon, size: 20, color: Theme.of(context).colorScheme.primary),
                const Spacer(),
                Container(
                  padding: const EdgeInsets.symmetric(horizontal: 8, vertical: 4),
                  decoration: BoxDecoration(
                    color: isPositive ? Colors.green.withOpacity(0.1) : Colors.red.withOpacity(0.1),
                    borderRadius: BorderRadius.circular(12),
                  ),
                  child: Text(
                    '${isPositive ? '+' : ''}${change.toStringAsFixed(1)}%',
                    style: TextStyle(
                      fontSize: 12,
                      color: isPositive ? Colors.green : Colors.red,
                      fontWeight: FontWeight.bold,
                    ),
                  ),
                ),
              ],
            ),
            const SizedBox(height: 12),
            Text(
              value,
              style: Theme.of(context).textTheme.headlineMedium?.copyWith(
                    fontWeight: FontWeight.bold,
                  ),
            ),
            const SizedBox(height: 4),
            Text(
              title,
              style: Theme.of(context).textTheme.bodySmall,
            ),
          ],
        ),
      ),
    );
  }
}

class _CrashItem extends StatelessWidget {
  final CrashInfo crash;

  const _CrashItem({required this.crash});

  @override
  Widget build(BuildContext context) {
    return Card(
      child: ListTile(
        leading: CircleAvatar(
          backgroundColor: Theme.of(context).colorScheme.errorContainer,
          child: Text(crash.count.toString()),
        ),
        title: Text(
          crash.title,
          maxLines: 1,
          overflow: TextOverflow.ellipsis,
        ),
        subtitle: Text(crash.location),
        trailing: Text(
          '${crash.affectedUsers} users',
          style: Theme.of(context).textTheme.bodySmall,
        ),
      ),
    );
  }
}

class _FeedbackItem extends StatelessWidget {
  final UserFeedback feedback;

  const _FeedbackItem({required this.feedback});

  @override
  Widget build(BuildContext context) {
    return Card(
      child: Padding(
        padding: const EdgeInsets.all(12),
        child: Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
            Row(
              children: [
                Chip(
                  label: Text(feedback.type.name),
                  visualDensity: VisualDensity.compact,
                ),
                const Spacer(),
                if (feedback.rating > 0)
                  Row(
                    children: List.generate(
                      5,
                      (i) => Icon(
                        i < feedback.rating ? Icons.star : Icons.star_border,
                        size: 16,
                        color: Colors.amber,
                      ),
                    ),
                  ),
              ],
            ),
            const SizedBox(height: 8),
            Text(feedback.message),
            const SizedBox(height: 8),
            Text(
              feedback.createdAt.toString(),
              style: Theme.of(context).textTheme.bodySmall,
            ),
          ],
        ),
      ),
    );
  }
}
```
