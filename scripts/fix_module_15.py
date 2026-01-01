import json
import os

filepath = 'content/courses/flutter/course.json'

with open(filepath, 'r') as f:
    data = json.load(f)

# Module 15 Updates
module_15_updates = {
    "15.3": {
        "Creating a DAO": """A DAO (Data Access Object) encapsulates all queries for a specific table or feature.
Instead of littering your UI code with raw queries, you call methods on the DAO.
This makes your code cleaner, easier to test, and allows you to change the underlying database implementation without breaking your UI.""",
    },
    "15.4": {
        "MigrationStrategy": """When your app evolves, your database schema must evolve with it.
The `MigrationStrategy` in Drift handles this lifecycle:
- `onCreate`: Runs only when the database is first created.
- `onUpgrade`: Runs when the current schema version is higher than the database version.
- `beforeOpen`: Runs every time the database opens (useful for enabling foreign keys).""",
        "Common Migration Patterns": """Drift provides helpers for common migration tasks:
- `addColumn`: Adds a new column to an existing table.
- `createTable`: Creates a new table.
- `deleteTable`: Removes a table.
- `customStatement`: Executes raw SQL for complex migrations (like renaming columns or transforming data)."""
    },
    "15.5": {
        "Installation and Setup": """To use Isar, add the `isar` and `isar_flutter_libs` dependencies.
You'll also need `isar_generator` and `build_runner` for code generation.
Isar uses a binary database format, so `isar_flutter_libs` contains the native binaries for each platform.""",
        "Opening the Database": """Opening an Isar database is asynchronous.
You typically create a singleton service (like `IsarService`) to manage the database connection.
Isar supports multiple schemas (collections) which must be registered when opening the database instance.""",
        "CRUD Operations": """Isar provides a synchronous and asynchronous API for CRUD operations.
- `put`: Creates or updates an object.
- `get`: Retrieves an object by ID.
- `delete`: Removes an object by ID.
- `writeTxn`: All write operations must be performed inside a transaction to ensure data integrity."""
    },
    "15.6": {
        "Query Performance": """Isar is optimized for speed, but how you write queries matters.
- **Fast**: Queries using indexes (e.g., `.where().priorityEqualTo(...)`).
- **Fast**: Composite indexes for multiple fields.
- **Slow**: Filtering on unindexed fields (requires scanning every record).
- **Recommendation**: Always add indexes to fields you plan to filter or sort by.""",
        "Filtering and Sorting": """Isar's query builder allows you to chain conditions.
- Start with `.where()` to use indexes.
- Use `.filter()` for non-indexed properties.
- Use `.sortBy...()` to order results.
- Use `.offset()` and `.limit()` for pagination.
- Use `.count()` to get the number of records without loading them."""
    },
    "15.7": {
        "Conflict Resolution Strategies": """When syncing data, conflicts happen. Two users might edit the same note offline.
Common strategies include:
- **Server Wins**: The server's version always overwrites the local one.
- **Client Wins**: The local version overwrites the server.
- **Last Write Wins**: Compare timestamps; the newest update wins.
- **Manual Merge**: Prompt the user to choose which version to keep.""",
        "Network Connectivity Detection": """A sync service needs to know when to sync.
We use the `connectivity_plus` package to listen for network changes.
- If the device goes offline, we pause sync and queue changes.
- When the device comes online, we automatically trigger the sync process.""",
        "Complete Sync Service": """The `SyncService` orchestrates the entire process:
1. **Push**: Uploads pending local changes (from the sync queue) to the server.
2. **Pull**: Downloads updates from the server since the last sync.
3. **Merge**: Resolves conflicts and updates the local database.
It handles retries, errors, and ensures the UI stays responsive."""
    },
    "15.8": {
        "Database Design": """Our Notes app needs two main collections:
1. **Note**: The actual data. It needs a `serverId` to map to the backend and `isDeleted` for soft deletes.
2. **SyncOperation**: A queue to track local changes (`create`, `update`, `delete`) that haven't been synced yet.""",
        "UI Implementation": """The UI should be reactive and offline-aware.
- Use `StreamBuilder` with Isar's `.watch()` method to update the list whenever data changes.
- Show a sync indicator (cloud icon) that changes color based on network status.
- Allow users to manually trigger a sync via `RefreshIndicator`.""",
        "Sync Implementation": """The `NoteSyncService` ties it all together.
It listens for connectivity changes to auto-sync.
When syncing:
- It pushes queued operations to the API.
- It fetches remote changes and merges them using Last-Write-Wins logic.
- It updates the local database within a transaction to ensure consistency."""
    }
}

count = 0
for module in data['modules']:
    if module['id'] == 'module-15':
        for lesson in module['lessons']:
            lesson_id = lesson['id']
            if lesson_id in module_15_updates:
                updates = module_15_updates[lesson_id]
                if 'contentSections' in lesson:
                    for section in lesson['contentSections']:
                        title = section['title']
                        if title in updates:
                            # Only update if content is empty or very short
                            current_content = section.get('content', '').strip()
                            if len(current_content) < 10:
                                section['content'] = updates[title]
                                print(f"Updated Lesson {lesson_id} - Section '{title}'")
                                count += 1

print(f"Total updates applied: {count}")

with open(filepath, 'w') as f:
    json.dump(data, f, indent=2)
