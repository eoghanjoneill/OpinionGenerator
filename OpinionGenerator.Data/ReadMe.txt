Some PowerShell dotnet cli commands used in working with SQL DB:

cd C:\repos\OpinionGenerator\OpinionGenerator.Data

dotnet ef dbcontext info -s ..\OpinionGenerator\OpinionGenerator.csproj

dotnet ef migrations add initialcreate -s ..\OpinionGenerator\OpinionGenerator.csproj

dotnet ef database update -s ..\OpinionGenerator\OpinionGenerator.csproj