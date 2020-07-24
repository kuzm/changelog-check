This prototype showcases how to enforce release notes for library projects. 

# Project setup
- .NET Core project as a sample library project
- Cake script that builds the project and checks `CHANGELOG.md`
- `CHANGELOG.md` contains release notes

# How to run

```
dotnet tool install Cake.Tool --version 0.38.4
dotnet cake
```

# How it works
The `build.cake` script checks that the record for the current version is present in `CHANGELOG.md`. The script retrieves the current version from `<Version>` element in the `*.csproj` file of the sample library. 

