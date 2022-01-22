# Codelux.NetCore
A collection of tools that simplify and abstract processes such as dependency injection, password encryption etc. Includes various ServiceStack helper libraries. 

## What is Codelux.NetCore?

Codelux.NetCore is a set of tools and libraries that simply processes such as encrypting passwords, injecting dependencies and implementing caches.
It contains a variety of helpful tools that can assist a developer to speed up the coding process and avoid spending time to re-invent the wheel.

## What does Codelux.NetCore provide?

__Codelux.NetCore provides the following tools:__

* Thread-safe in-memory cache with expirable objects
* Dependency registration modules for the Unity DI framework
* A dependency container (inherits UnityContainer) that searches the current AppDomain for dependency modules to auto-register dependencies
* An HTTP client to consume RESTful APIs (uses Newtonsoft.Json for serialization) - uses POCO request/responses
* A generic abstract Mapper class that transforms one POCO to another based on user defined criteria
* A thread-safe Runnable abstract class that enables task isolation (has condition status, i.e running/stopping/stopped)
* A thread-safe repeating Runnable abstract class that enables task isolation and runs in a pre-defined period
* A task executor generic class that resembles a workflow transformer. Accepts a POCO input and returnes a new one user-defined one
* A paging system which includes its own collection implementation that splits enumerables into pages of pre-defined size
* Configuration source toolkit that helps implement configuration readers which can read configuration from various sources
* Various generic utilities such as: Captcha provider, Base64 encoder, MD5 encoder, a clock service and a password strength validator


__Codelux.NetCore.Common provides the following tools:__

* Dictionary extensions that provide a way to set items (create new or update if it exists) and add a range of key value pairs
* Exception extensions that provide a way to get all inner exceptions or all exception messages (inspired by ServiceStack's GetInnermostException extension)
* Object extenion that provides a fast nullity check (used for constructor params nullity checks) - throws ArgumentNullException if null
* Struct extensions to box and unbox values quickly. Includes type checking when unboxing
* Enum extension that reads a flag's DescriptionAttribute and returns its text
* Various base generic request objects that could be used in a web service (Request, AuthenticatedRequest that provides username and other identity data) etc.
* Various base generic response objects that could be used in a web service (ServiceResponse, ServiceErrorResponse) 


__Codelux.NetCore.ServiceStack provides the following tools:__

* A core service component that inherits ServiceStack's Service and provides basic versioning
* A ServiceStack exception handler that captures various common exceptions (TaskCancelledException, validation exceptions) and returns the appropriate, formatted response
* Depenedency registration modules similar to the ones in *Codelux.NetCore* but modified for compatibility with ServiceStack's IoC
* OrmLite mapping helper that enables simple model property mapping to table columns easily.
* A ServiceStack plugin that searches the AppDomain for OrmLite mappings and instantiates them (OrmLite requires instantiation of mappings to honor them)

__Codelux.NetCore.Plugins provides the following tools:__

* An agnostic plugin framework that can be implemented rapidly and fit most scenarios
* Easily extensible to include additional functionality
* A general plugin instance manager which loads plugin configurations and runs/stops plugins
* JSON plugin configuration source which reads plugin configurations from JSON files and adds them to the instance manager

## Why should I use this library?

My recommendation is not to use it as whole. I would advise taking the parts you need and implement them in your own project or compile a separate library
one. The code is clean, reusable and easy to understand and therefore modification should be easy.

In addition to that, most critical parts have been tested as well. 
If you're a developer that uses ServiceStack for your web projects, the Codelux.NetCore.ServiceStack library might be handy as it makes
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

# Conclusion #

I wrote this library in my free time to simplify some tasks that were repeated. Feel free to use it as you please.  
Any additions, fixes or enhancements are welcome, please submit a pull request if you want to make a change.  
If you have any questions, please feel free to create an issue on GitHub or Bitbucket and I will try to respond as soon as I can.