---
type: "THEORY"
title: "Section 2: Project Setup"
---


### Step 1: Create Flutter Project


### Step 2: Add Dependencies


Run:

### Step 3: Create Project Structure




```dart
task_master_pro/
├── lib/
│   ├── main.dart
│   ├── models/
│   │   ├── task.dart
│   │   └── task.g.dart (generated)
│   ├── repositories/
│   │   └── task_repository.dart
│   ├── bloc/
│   │   ├── task_bloc.dart
│   │   ├── task_event.dart
│   │   └── task_state.dart
│   ├── screens/
│   │   ├── home_screen.dart
│   │   └── add_edit_task_screen.dart
│   ├── widgets/
│   │   ├── task_list.dart
│   │   ├── task_item.dart
│   │   ├── filter_buttons.dart
│   │   └── statistics_widget.dart
│   └── utils/
│       └── date_utils.dart
├── test/
│   ├── models/
│   │   └── task_test.dart
│   ├── repositories/
│   │   └── task_repository_test.dart
│   ├── bloc/
│   │   └── task_bloc_test.dart
│   ├── widgets/
│   │   ├── task_list_test.dart
│   │   ├── task_item_test.dart
│   │   └── filter_buttons_test.dart
│   └── utils/
│       └── date_utils_test.dart
├── integration_test/
│   ├── app_test.dart
│   └── task_flow_test.dart
├── scripts/
│   ├── coverage.sh
│   └── run_all_tests.sh
└── .github/
    └── workflows/
        ├── ci.yml
        └── integration.yml
```
