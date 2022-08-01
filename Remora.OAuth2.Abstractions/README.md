Remora.OAuth2.Abstractions
===============================

This package contains a complete set of type and API abstractions for the 
OAuth2 API. It provides no concrete implementations; rather, it acts as a 
general, library-agnostic standard definition of OAuth2's API.

These types serve as the foundation of Remora.OAuth2's entire API surface, but 
can just as easily be used to implement your own OAuth2 library, independently
of Remora.OAuth2.

The primary goal of this project is to model OAuth2's API as closely as 
possible, while at the same time applying appropriate C# practices and builtin
types (such as `DateTimeOffset`).

## Structure
The library is divided into type categories, organized to match OAuth2's API 
documentation as closely as is realistic. Each object defined by OAuth2 has a 
corresponding interface, with inline documentation that matches OAuth2's.

## Usage
No particular usage recommendations exist for this library. It's up to you to 
decide how to implement or utilize these definitions.
