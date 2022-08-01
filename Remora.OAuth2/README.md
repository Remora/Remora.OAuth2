Remora.OAuth2
==================

This package contains the implementation of OAuth2's API interface models, 
provided by Remora.OAuth2.Abstractions. 

Primarily, this project takes the completely agnostic interfaces and applies 
additional, domain-specific knowledge to them, such as serialization formats and
data range validations. 

Like its sibling, these types serve as the foundation of Remora.OAuth2's 
internal implementation, but can just as easily be used to implement your own 
OAuth2 library.

The primary goal of this project is to provide a concrete implementation of 
OAuth2's API types, including data object de/serialization and externally 
imposed limitations.

## Structure
The library mostly mirrors Remora.OAuth2.Abstractions in structure, 
maintaining an implementation of each corresponding API object interface. The 
implementations are `record`s, enabling thread-safe usage across as many 
concurrent actors as needed.

The `Json` folder contains various converters to handle cases where the default
behaviour (from `Remora.Rest`) is not appropriate, or the type is too complex to
realistically deserialize without special treatment.

There are also some helper types for accessing CDN assets, checking image
formats, and storing OAuth2-provided constant values. 

## Usage
Beyond the concrete implementations of OAuth2's objects, the library provides a
single extension method for the `IServiceCollection` type.

```c#
services.ConfigureOAuth2JsonConverters();
```

This method will add a named set of `JsonSerializerOptions` with all 
appropriate JSON converters for the various API, objects along with configuring 
naming rules.
