dotnet pack
dotnet tool uninstall --global alpasec.cli
dotnet tool install --global --add-source ./nupkg alpasec.cli
