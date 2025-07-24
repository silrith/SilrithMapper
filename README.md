# SilrithMapper 🔁

**SilrithMapper** is a lightweight, profile-free, rule-based object mapping library for .NET.  
It provides clean and customizable mapping logic between types without relying on heavy configuration.

---

## 🚀 Features

- ✅ Zero profile or attribute requirement
- 🧠 Rule-based and context-aware mapping
- 💉 Built-in Dependency Injection (DI) support
- ♻️ Reusable mapping rules per type pair
- 🔍 Context parameter for dynamic behavior (e.g., admin/user)
- 🧪 Test-friendly and minimalistic

---

## 📦 Installation

Install via NuGet:

bash
dotnet add package SilrithMapper

Or via Package Manager: 
Install-Package SilrithMapper

---

🧩 Getting Started
1. Register in Dependency Injection
In Program.cs or Startup.cs:

using SilrithMapper;

builder.Services.AddSilrithMapper();

---

2. Inject and Use the Mapper

public class MyService
{
    private readonly ISilrithMapper _mapper;

    public MyService(ISilrithMapper mapper)
    {
        _mapper = mapper;
    }

    public Destination MapData(Source source)
    {
        return _mapper.Map<Source, Destination>(source);
    }
}

---

🧠 Defining Mapping Rules
SilrithMapper lets you register mapping rules globally per type combination:

MapRuleRegistry.Register<Source, Destination>((src, dest, context) =>
{
    dest.FullName = context == "admin"
        ? src.FirstName.ToUpper()
        : src.FirstName;
});

You can register this rule anywhere at application startup (ideally during bootstrapping or composition root).

---

🧪 Example

public class UserEntity
{
    public string FirstName { get; set; } = "";
}

public class UserDto
{
    public string FullName { get; set; } = "";
}

// Register a rule
MapRuleRegistry.Register<UserEntity, UserDto>((src, dest, ctx) =>
{
    dest.FullName = ctx == "admin" ? src.FirstName.ToUpper() : src.FirstName;
});

// Mapping with context
var user = new UserEntity { FirstName = "Alice" };
var userDto = mapper.Map<UserEntity, UserDto>(user, "admin");

// userDto.FullName => "ALICE"

---

🔧 API Reference

ISilrithMapper

TTarget Map<TSource, TTarget>(TSource source, string? context = null)
    where TTarget : new();

---

MapRuleRegistry

static void Register<TSource, TTarget>(
    Action<TSource, TTarget, string?> rule
)

Allows you to register global rules for a pair of types with optional context.

---

🧱 Architecture Overview

File/Class	                    Responsibility

ISilrithMapper.cs	            Interface for the mapper
SilrithMapperService	        Implementation of the mapping engine
MapRuleRegistry.cs	            Static registry for mapping rules
MapRule.cs	                    Internal representation of a mapping rule
MappingConfig.cs	            Configuration settings (if extended in future)
AddSilrithMapper()	            DI extension for easy integration

---

🧪 Testing

SilrithMapper is stateless and test-friendly.

You can mock ISilrithMapper for unit tests.

Registered rules are pure functions and easily testable in isolation.

Example test:

var source = new UserEntity { FirstName = "Test" };
var dto = mapper.Map<UserEntity, UserDto>(source, "test");
Assert.Equal("Test", dto.FullName);

📝 License

MIT License
© 2025 Berk Özerdoğan

---

🌍 Project Links

GitHub: github.com/silrith/SilrithMapper

NuGet: nuget.org/packages/SilrithMapper
