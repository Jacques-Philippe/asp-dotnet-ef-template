![Build & Test workflow](https://github.com/Jacques-Philippe/asp-dotnet-ef-template/actions/workflows/unit-tests.yml/badge.svg).  
Don't forget to change the link above when you clone this repo to make other projects!

# Get started (dev)

1. Install dotnet tools (`HTTP REPL`, `csharpier`, `dotnet-ef`)
   ```
   ./scripts/install-dotnet-tools.sh
   ```
1. Install yarn
1. Clone the repo
1. Run `yarn prepare` (You may need to run `yarn install` before this).  
   This will install all husky hooks.

# Database convenience scripts

Some scripts exist in the `scripts` directory with some basic db management operations for convenience.

## new-migration

Create a new migration if there was a change to the model

```
Usage:
./scripts/new-migration "MigrationName"
```

## apply-migrations

Apply all migrations existing in the `Migrations` directory to the db

```
Usage:
./scripts/apply-migrations.sh
```

# Devving with Sqlite on VS code

Install the `vscode-sqlite` extension via VS Code's extensions interface. Check out [the extension's repo](https://github.com/AlexCovizzi/vscode-sqlite) for more instructions on its use.

# Issues I ran into

## "Unable to find an OpenAPI description" on HTTPREPL connect

See [this](https://stackoverflow.com/questions/69278068/why-is-httprepl-unable-to-find-an-openapi-description-the-command-ls-does-not)  
What fixed the issue for me:

```
dotnet dev-certs https --trust
```

## Tables aren't created on Database.EnsureCreated()

For whatever reason, `Database.EnsureCreated()` wasn't creating the seeded database, and [this](https://stackoverflow.com/a/68796048) turned out to be the fix.


See [this](https://stackoverflow.com/questions/69278068/why-is-httprepl-unable-to-find-an-openapi-description-the-command-ls-does-not)  
What fixed the issue for me:

```
dotnet dev-certs https --trust
```

## Tables aren't created on Database.EnsureCreated()

For whatever reason, `Database.EnsureCreated()` wasn't creating the seeded database, and [this](https://stackoverflow.com/a/68796048) turned out to be the fix.
