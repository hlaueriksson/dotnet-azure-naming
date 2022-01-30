# ☁️✍️ Azure Naming

This is a `dotnet` tool helping you to name Azure Resources.

This tool is based on Klabbet naming convention:

- https://github.com/klabbet/azure-naming

It uses the *abbreviations* of Azure resources recommended by Microsoft:

- https://docs.microsoft.com/en-us/azure/cloud-adoption-framework/ready/azure-best-practices/resource-abbreviations

You can create your own naming convention by editing the settings.

See instructions at:

- https://github.com/hlaueriksson/dotnet-azure-naming

## Installation

Install:

```cmd
dotnet tool install -g dotnet-azure-naming
```

Update:

```cmd
dotnet tool update -g dotnet-azure-naming
```

Uninstall:

```cmd
dotnet tool uninstall -g dotnet-azure-naming
```

## Usage

Without arguments / Interactive:

```cmd
azure-naming
```

![Interactive / Without arguments](https://raw.githubusercontent.com/hlaueriksson/dotnet-azure-naming/main/azure-naming.gif)

- Start typing to filter Resource Types
- Use ⬅️ and ➡️ keys to turn pages
- Use ⬆️ and ⬇️ keys to select
- Use `Enter` key to confirm

With arguments:

```cmd
azure-naming --resource-type "Function app" --project-name Titanic --component-name Web --environment Development
```

Short:

```cmd
azure-naming -r func -p Titanic -c Web -e dev
```

![With arguments](https://raw.githubusercontent.com/hlaueriksson/dotnet-azure-naming/main/azure-naming-args-short.png)

Help:

```cmd
azure-naming --help
```

![Help](https://raw.githubusercontent.com/hlaueriksson/dotnet-azure-naming/main/azure-naming-args-help.png)
