[![Codelux](https://img.shields.io/nuget/v/Codelux.svg?style=flat-square&label=Codelux)](https://www.nuget.org/packages/Codelux/) [![Codelux](https://img.shields.io/nuget/v/Codelux.Common.svg?style=flat-square&label=Codelux.Common)](https://www.nuget.org/packages/Codelux.Common/) [![Codelux](https://img.shields.io/nuget/v/Codelux.ServiceStack.svg?style=flat-square&label=Codelux.ServiceStack)](https://www.nuget.org/packages/Codelux.ServiceStack/) [![Codelux](https://img.shields.io/nuget/v/Codelux.Plugins.svg?style=flat-square&label=Codelux.Plugins)](https://www.nuget.org/packages/Codelux.Plugins/)

[![Tests](https://github.com/Codeh4ck/Codelux/actions/workflows/ActionRunUnitTests.yml/badge.svg?branch=main)](https://github.com/Codeh4ck/Codelux/actions/workflows/ActionRunUnitTests.yml)

[Click here for the old BitBucket repository](https://bitbucket.org/nickandreou/codelux.netcore/src/develop/) 

# Codelux

Codelux is a collection of tools that simplify and abstract processes such as dependency injection, caching, configuration, mapping etc. Codelux.ServiceStack includes various ServiceStack helper libraries that make OrmLite model mapping, dependency injection and bootstrapping simple, elegant and clean. Additionally, Codelux.Plugins provides a base  plugin framework for a pluggable architecture design.

# Installation

**Install Codelux core library to your project:**
```powershell
PM> Install-Package Codelux
```

**Install Codelux.Common library to your project**

```powershell
PM> Install-Package Codelux.Common
```

**Install Codelux.ServiceStack library to your project**

```powershell
PM> Install-Package Codelux.ServiceStack
```

**Install Codelux.Plugins library to your project**

```powershell
PM> Install-Package Codelux.Plugins
```

## What does Codelux provide?

__Codelux provides the following tools:__

* Thread-safe in-memory cache with expirable objects
* Dependency registration modules for the Unity DI framework
* A dependency container (inherits UnityContainer) that searches the current AppDomain for dependency modules to auto-register dependencies
* An HTTP client to consume RESTful APIs (uses Newtonsoft.Json for serialization) - uses POCO request/responses
* A generic abstract Mapper class that transforms one POCO to another based on user defined criteria
* A thread-safe Runnable abstract class that enables task isolation (has condition status, i.e running/stopping/stopped)
* A thread-safe repeating Runnable abstract class that enables task isolation and runs in a pre-defined period
* A task executor generic class that resembles a workflow transformer. Accepts a POCO input and returnes a new one user-defined one
* A type activator system that instantiates types based on user-defined logic
* A simple and minimal logging framework that currently supports debug logging and LogDNA
* A paging system which includes its own collection implementation that splits enumerables into pages of pre-defined size
* Configuration source toolkit that helps implement configuration readers which can read configuration from various sources
* Various generic utilities such as: Captcha provider, Base64 encoder, MD5 encoder, an e-mail sender service, a clock service and a password strength validator


__Codelux.Common provides the following tools:__

* Dictionary extensions that provide a way to set items (create new or update if it exists) and add a range of key value pairs
* Exception extensions that provide a way to get all inner exceptions or all exception messages (inspired by ServiceStack's GetInnermostException extension)
* Object extenion that provides a fast nullity check (used for constructor params nullity checks) - throws ArgumentNullException if null
* Struct extensions to box and unbox values quickly. Includes type checking when unboxing
* Enum extension that reads a flag's DescriptionAttribute and returns its text
* Various base generic request objects that could be used in a web service (Request, AuthenticatedRequest that provides username and other identity data) etc.
* Various base generic response objects that could be used in a web service (ServiceResponse, ServiceErrorResponse) 


__Codelux.ServiceStack provides the following tools:__

* A core service component that inherits ServiceStack's Service and provides basic versioning
* A ServiceStack exception handler that captures various common exceptions (TaskCancelledException, validation exceptions) and returns the appropriate, formatted response
* Depenedency registration modules similar to the ones in *Codelux* but modified for compatibility with ServiceStack's IoC
* OrmLite mapping helper that enables simple model property mapping to table columns easily.
* A ServiceStack plugin that searches the AppDomain for OrmLite mappings and instantiates them (OrmLite requires instantiation of mappings to honor them)

__Codelux.Plugins provides the following tools:__

* An agnostic plugin framework that can be implemented rapidly and fit most scenarios
* Easily extensible to include additional functionality
* A general plugin instance manager which loads plugin configurations and runs/stops plugins
* JSON plugin configuration source which reads plugin configurations from JSON files and adds them to the instance manager

## Why should I use this library?

My recommendation is not to use it as whole. I would advise taking the parts you need and implement them in your own project or compile a separate library
one. The code is clean, reusable and easy to understand and therefore modification should be easy.

In addition to that, most critical parts have been tested as well. 
If you're a developer that uses ServiceStack for your web projects, the Codelux.ServiceStack library might be handy as it makes
certain tasks trivial.

## How to use the OrmLiteMapping feature:

1. Define your model like the example below:
```csharp 
public class ExampleModel
{
    public Guid Id { get; set; }
    public Guid AnotherId { get; set; }
    public string StringValue { get; set; }
    public int IntegerValue { get; set; }
}
```

2. Create a new class that inherits OrmLiteMapping around your model's type as shown below:
```csharp
public class ExampleModelMapping : OrmLiteMapping<ExampleModel>
{
    public ExampleModelMapping()
    {
        MapToSchema("dbo");
        MapToTable("example_model");

        MapToColumn(x => x.Id, "id");
        MapToColumn(x => x.AnotherId, "another_id");
        MapToColumn(x => x.StringValue, "string_value");
        MapToColumn(x => x.IntegerValue, "integer_Value");
    }
}
```

Your mappings should be placed inside the constructor. The OrmLiteMappingFeature plugin will search the assembly for classes
derived from OrmLiteMapping and will instantiate them. Therefore, all mapping are honored by OrmLite.

__Other mapping functions:__

*```OrmLiteMapping.Ignore()``` - Ignores the given property and column  
*```OrmLiteMapping.AutoIncrement()```- Maps a property to a column and marks it as auto increment

3. Register OrmLiteMappingFeature plugin on your ServiceStack AppConfigurator:

```csharp
appHost.Plugins.Add(new OrmLiteMappingFeature());
```

## Conclusion
I wrote this library in my free time to simplify some tasks that were repeated. Feel free to use it as you please.  
Any additions, fixes or enhancements are welcome, please submit a pull request if you want to make a change.  
If you have any questions, please feel free to create an issue on GitHub and I will try to respond as soon as I can.

# Contributing

## Found an issue?

Please report any issues you have found by [creating a new issue](https://github.com/Codeh4ck/Codelux/issues). We will review the case and if it is indeed a problem with the code, I will try to fix it as soon as possible. I want to maintain a healthy and bug-free standard for our code. Additionally, if you have a solution ready for the issue please submit a pull request. 

## Submitting pull requests

Before submitting a pull request to the repository please ensure the following:

* Your code follows the naming conventions [suggested by Microsoft](https://docs.microsoft.com/en-us/dotnet/standard/design-guidelines/naming-guidelines)
* Your code works flawlessly, is fault tolerant and it does not break the library or aspects of it
* Your code follows proper object oriented design principles. Use interfaces!

Your code will be reviewed and if it is found suitable it will be merged. Please understand that the final decision always rests with me. By submitting a pull request you automatically agree that I hold the right to accept or deny a pull request based on my own criteria.

## Contributor License Agreement

By contributing your code to Codelux you grant Nikolas Andreou a non-exclusive, irrevocable, worldwide, royalty-free, sublicenseable, transferable license under all of Your relevant intellectual property rights (including copyright, patent, and any other rights), to use, copy, prepare derivative works of, distribute and publicly perform and display the Contributions on any licensing terms, including without limitation: (a) open source licenses like the MIT license; and (b) binary, proprietary, or commercial licenses. Except for the licenses granted herein, You reserve all right, title, and interest in and to the Contribution.

You confirm that you are able to grant us these rights. You represent that you are legally entitled to grant the above license. If your employer has rights to intellectual property that you create, You represent that you have received permission to make the contributions on behalf of that employer, or that your employer has waived such rights for the contributions.

You represent that the contributions are your original works of authorship and to your knowledge, no other person claims, or has the right to claim, any right in any invention or patent related to the contributions. You also represent that you are not legally obligated, whether by entering into an agreement or otherwise, in any way that conflicts with the terms of this license.

Nikolas Andreou acknowledges that, except as explicitly described in this agreement, any contribution which you provide is on an "as is" basis, without warranties or conditions of any kind, either express or implied, including, without limitation, any warranties or conditions of title, non-infringement, merchantability, or fitness for a particular purpose.