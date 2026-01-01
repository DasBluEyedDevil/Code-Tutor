// Complete the implementation for all platforms

// commonMain
expect class DatabaseDriverFactory {
    fun createDriver(): SqlDriver
}

// TODO: Implement for androidMain
// - Use AndroidSqliteDriver
// - Enable foreign keys
// - Database name: "tasks.db"

// TODO: Implement for iosMain
// - Use NativeSqliteDriver
// - Enable connection pooling (4 readers)
// - Database name: "tasks.db"

// TODO: Implement for desktopMain
// - Use JdbcSqliteDriver
// - Store in user's app data directory
// - Handle schema creation for new databases