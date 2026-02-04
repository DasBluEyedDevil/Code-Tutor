---
type: WARNING
---

**Drift requires code generation -- forgetting to run it causes confusing errors.** After changing table definitions or query methods, you must run `dart run build_runner build` to regenerate the `*.g.dart` files. Without this step, your code will reference classes and methods that do not exist yet, producing errors like "The name 'XxxCompanion' is not defined."

Common mistakes:
- Adding a new column to a table class but forgetting to regenerate (companion class is outdated)
- Renaming a table field without regenerating (old generated code references the old name)
- Running `build_runner watch` but not noticing it stopped after a syntax error

Run `dart run build_runner build --delete-conflicting-outputs` to force a clean regeneration. Add a script alias in `pubspec.yaml` or your Makefile so the team always uses the same command. Never commit without verifying that the generated files match your table definitions.
