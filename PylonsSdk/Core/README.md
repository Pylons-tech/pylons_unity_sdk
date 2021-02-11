# Pylons SDK for Unity (Core) [![openupm](https://img.shields.io/npm/v/com.pylons.unity.sdk.core?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.pylons.unity.sdk.core/)
> Tools for building games that use the Pylons blockchain in Unity

The Pylons SDK for Unity supplies developers with extensible, easy-to-use tools for communicating with a Pylons wallet from within
Unity, retrieving and managing data from the blockchain, and creating and submitting transactions.

This module provides the core functionality of the SDK. Additional high-level helpers and editor functionality for interacting with
profiles and native Pylons items are available below:

**This is a preview release.** Efforts have been made to find and fix bugs and polish the available features, but new and exciting bugs
will definitely emerge when this package starts to see external use. Usability issues, too, are to be expected.

- [ProfileTools](https://openupm.com/packages/com.pylons.unity.sdk.profiletools/)
- [ItemSchema](https://openupm.com/packages/com.pylons.unity.sdk.itemschema/)

![](/.github/images/header.png)

## Installation

**Under Project Settings > Player, the scripting backend set for PC, Mac, and Linux Standalone builds must be IL2CPP, and the API compatibility level must be .NET 4.x.** This is required for editor integration with the Pylons dev wallet.

openupm-cli:

If [openupm-cli](https://github.com/openupm/openupm-cli) is already installed:

```
openupm add com.pylons.unity.sdk.core
```

Manually adding the scope within the Unity Package Manager:

- Edit > Project Settings > Package Manager
- Under the Scoped Registries heading, click the + button to add a new entry to the list.
- Configure the new registry as below, then hit apply:

![Registry config](/.github/images/registry.png)

- In the top left of the Package Manager window, click the 'Packages: Unity Registry' dropdown and select 'Packages: My Registries' instead. Then, select the package from the list on the left and click 'Install.'

## Usage example

Example code is available in the [Pylons SDK examples package](https://openupm.com/packages/com.pylons.sdk.examples/).

## Release History

* 0.1.0
    * Initial release

## Meta

[Pylons LLC](https://pylons.tech) – contact@pylons.tech

Distributed under the MIT license. See ``LICENSE`` for more information.

[Pylons on Github](https://github.com/Pylons-tech/)