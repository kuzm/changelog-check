#tool nuget:?package=NUnit.ConsoleRunner&version=3.11.1

#addin "Cake.Incubator&version=5.1.0"
#addin "Cake.FileHelpers&version=3.3.0"

using System.Text.RegularExpressions;

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////

var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("CheckChangeLog")
    .Does(() =>
{
    var parsedProject = ParseProject(new FilePath("./src/Example/Example.csproj"), configuration: "Release");
    var projectVersion = parsedProject.GetProjectProperty("Version");
    Information("Version: " + projectVersion);

    var regex = $"##\\s\\[{projectVersion}\\].*";
    var versionMatch = FindRegexMatchInFile(new FilePath("src/Example/CHANGELOG.md"), rxFindPattern: regex, RegexOptions.None);
    Information("Changelog entry: " + versionMatch);

    if (string.IsNullOrEmpty(versionMatch)) {
        throw new Exception("Add entry to CHANGELOG.md");
    }

});

//////////////////////////////////////////////////////////////////////
// TASK TARGETS
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("CheckChangeLog");

//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

RunTarget(target);