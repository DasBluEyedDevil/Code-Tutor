---
type: "WARNING"
title: "Git Common Mistakes"
---

## Watch Out For These Issues!

**Committing secrets**: NEVER commit passwords, API keys, or connection strings to Git! Even if you delete them later, they remain in Git history. Use .gitignore to exclude appsettings.Development.json and use User Secrets (`dotnet user-secrets`) for local development.

**Force push destroys history**: `git push --force` overwrites remote history. If teammates have pulled, their work diverges. Use `git push --force-with-lease` if you must force push -- it checks for upstream changes first.

**git reset --hard loses work**: `git reset --hard` permanently discards uncommitted changes. There is no undo! Use `git stash` to temporarily shelve changes, or `git reset --soft` to undo commits while keeping changes staged.

**Forgetting .gitignore**: Always create .gitignore BEFORE your first commit. For .NET projects, ignore: bin/, obj/, .vs/, *.user, appsettings.Development.json. GitHub provides a Visual Studio .gitignore template at `github.com/github/gitignore`.
