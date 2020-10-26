# Pylons SDK for Unity (ProfileTools) [![openupm](https://img.shields.io/npm/v/com.pylons.unity.sdk.profiletools?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.pylons.unity.sdk.profiletools/)
> High-level tools for tracking user profile state

The Pylons SDK for Unity supplies developers with extensible, easy-to-use tools for communicating with a Pylons wallet from within
Unity, retrieving and managing data from the blockchain, and creating and submitting transactions.

This module provides higher-level functionality for tracking the state of user profiles while maintaining a simpler programming paradigm more suitable for game logic.
It also provides the Profile Manager, an editor utility for maintaining multiple sets of credentials and swapping between them on the fly for development or testing purposes.

**This is a preview release.** Efforts have been made to find and fix bugs and polish the available features, but new and exciting bugs
will definitely emerge when this package starts to see external use. Usability issues, too, are to be expected.

![](/.github/images/header.png)

## Installation

openupm-cli:

If [openupm-cli](https://github.com/openupm/openupm-cli) is already installed:

```
openupm add com.pylons.unity.sdk.profiletools
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