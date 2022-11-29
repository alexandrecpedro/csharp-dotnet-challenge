# NOVIDADES - C# 11 / .NET 7

(1) "public static void Main structure" at Program.cs
    - it is not mandatory

(2) Configuration file
    - appsettings.json

(3) Namespace
    - it does not require brackets

(4) Multiple lines
```shell
    var i = 0;
    string template = $$"""
        Hello,
        World!

        {
            "employees": [
                {"firstName": "John", "lastName": "Doe - {{ i }}"},
                {"firstName": "Anna", "lastName": "Smith"},
                {"firstName": "Peter", "lastName": "Jones"}
            ]
        }
    """;
```

(5) Attributes
    - Take a look at TableAttribute.cs

(6) Nullable
    - Take a look at console.csproj
    - Take a look at Client.cs

(7) Validations
    - Take a look at Program.cs

(8) Compare values
    - index of value in a array
        - int[] values = { 2, 3, 4, 5 };
        - if (values is [_, _, _, 4]) // return false
        - if (values is [_, _, 4, _]) // return true
        
        - Range comparison
            - if (values is [.., 4, _]) // return true
            - if (values is [.., 4]) // return false

        - Get values
            - if (values is [.., var penultimate, var ultimate]) {
                Console.WriteLine(penultimate); // 4
                Console.WriteLine(ultimate); // 5
            }

(9) Show project structure
    - Blazor project
        - dotnet new razor --dry-run

    - WebAPI project
        - dotnet new webapi --dry-run

    - Console project
        - dotnet new console --dry-run

(10) Container support (Docker)
    - Generate an image
        - docker add package Microsoft.NET.Build.Containers
    - Publish a container
        - docker publish --os linux --arch x64 -p:PublishProfile=DefaultContainer

(11) Velocity
    - Faster processor than Node.JS

(12) Publish
    - MacOS
        - dotnet publish -r osx-x64 -c ReleaseAOTOsx