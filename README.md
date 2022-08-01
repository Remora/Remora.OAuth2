Remora.OAuth2
==============

Remora.OAuth2 is a C# library for interfacing with OAuth2 services. It is built
to fulfill a need for robust, feature-complete, highly available and concurrent 
applications.

Want to chat with users and developers? Come join us!

[![OAuth2 Server][5]][4] 

# Table of Contents
1. [Features](#1-features)
   1. [Support Table](#11-support-table)
2. [Goals](#2-goals)
    1. [Correctness](#21-correctness)
    2. [Robustness](#22-robustness)
    3. [True Asynchronicity and Concurrency](#23-true-asynchronicity-and-concurrency)
3. [Installation](#4-installation)
4. [Usage](#5-usage)
5. [Contributing](.github/CONTRIBUTING.md)

## 1. Features
  * Extensive API coverage - does anything and everything you need
  * Modern and active - uses contemporary technologies and usage patterns
  * Fully asynchronous - do many things at once at scale
  * Modular - swap parts of the library with your own implementations at will

### 1.1 Support Table
This table lists the various RFCs related to OAuth2 that the library supports.

| RFC  | Name                              | Supported      |
|------|-----------------------------------|----------------|
| 6749 | OAuth 2.0 Authorization Framework | API model only |
| 7009 | Token Revocation                  | API model only |
| 8628 | Device Authorization Grant        | API model only |


## 2. Goals
Remora.OAuth2 defines the following three goals that guides its development. 
These are shorter summaries - to read the full goal definitions and see 
examples, please refer to the [Contributing Guidelines][2].

### 2.1 Correctness
Correctness, in the context of Remora.OAuth2, means that the API available to 
the end user should as faithfully and accurately represent the actual reality of
data presented to or from an API; that is, no data or structure of data should 
meaningfully change between the library receiving it and the user accessing it.

### 2.2 Robustness
Robustness refers to a focus on never allowing problems originating from user 
data or real-life runtime conditions to bring down or otherwise corrupt the end 
user's application. The end user should be confident that, should an error 
arise, they will be aware of the fault potential before even compiling the 
application.

### 2.3 True Asynchronicity and Concurrency
Remora.OAuth2 aims to be truly asynchronous from the ground up, respecting and
utilizing established best practices for C# and the TPL. Furthermore, it aims to
be concurrent, allowing end users to react to and perform actions upon many 
tasks at once.

## 4. Installation
Remora.OAuth2's primary distribution format is via [nuget][3] - get it there!

If you wish to use or develop the library further, you will need to compile it 
from source.

```bash
git clone git@github.com:Nihlus/Remora.OAuth2.git
cd Remora.OAuth2
dotnet build
dotnet pack -c Release
```

## 5. Usage
Up-to-date documentation for the API, as well as a quickstart guide, is 
available online at [the repository pages][1].

Each package has its own README with more detailed information regarding its 
purpose and use. If you want to know more about each one of these, please refer
to the list below. It's roughly organized in order of importance to end users, 
but feel free to explore.

  * [Remora.OAuth2](Remora.OAuth2/README.md)
    * [Remora.OAuth2.API.Abstractions](Remora.OAuth2.Abstractions/README.md)

### 5.1 Versioning
A note on versioning - Remora.OAuth2 uses SEMVER 2.0.0, which, in short, means

Given a version number MAJOR.MINOR.PATCH, increment the:

  1. MAJOR version when you make incompatible API changes,
  2. MINOR version when you add functionality in a backwards compatible manner,
     and
  3. PATCH version when you make backwards compatible bug fixes.

### 5.2 Releases
Remora.OAuth2 does not follow a set release cycle, and releases new versions 
on a rolling basis as new features of the OAuth2 API are implemented or 
documented.

As a developer, you should check in every now and then to see what's changed - 
changelogs are released along with tags here on Github, as well as in the 
individual package descriptions.

Whenever a new set of packages are released, the commit the releases were built 
from is tagged with the year and an incremental release number - for example,
`2021.1`.

#### 5.2.1 Bleeding Edge Builds
Whenever a new push to `main` is made, a new set of packages based on the 
latest commit will be published to GitHub Packages.

The URL of the NuGet source is `https://nuget.pkg.github.com/Nihlus/index.json`.  
As the NuGet source requires authentication, follow GitHub's instructions: [here][9]

## 6. Contributing
See [Contributing][2].

## Thanks
Icon by [Twemoji][6], licensed under CC-BY 4.0.

[1]: https://nihlus.github.io/Remora.OAuth2/
[2]: .github/CONTRIBUTING.md
[3]: https://www.nuget.org/packages/Remora.OAuth2/
[4]: https://discord.gg/tRJbg8HNdt
[5]: https://img.shields.io/static/v1?label=Chat&message=on%20Discord&color=7289da&logo=Discord
[6]: https://twemoji.twitter.com/
[8]: https://github.com/Nihlus?tab=packages&repo_name=Remora.OAuth2
[9]: https://docs.github.com/en/packages/working-with-a-github-packages-registry/working-with-the-nuget-registry#authenticating-to-github-packages
