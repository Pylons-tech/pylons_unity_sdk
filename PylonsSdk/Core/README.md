# Pylons SDK for Unity (Core) [![openupm](https://img.shields.io/npm/v/com.pylons.unity.sdk.core?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.pylons.unity.sdk.core/)
> Tools for building games that use the Pylons blockchain in Unity

[![NPM Version][npm-image]][npm-url]
[![Build Status][travis-image]][travis-url]
[![Downloads Stats][npm-downloads]][npm-url]

The Pylons SDK for Unity supplies developers with extensible, easy-to-use tools for communicating with a Pylons wallet from within
Unity, retrieving and managing data from the blockchain, and creating and submitting transactions.

This module provides the core functionality of the SDK. Additional high-level helpers and editor functionality for interacting with
profiles and native Pylons items are available below:

- [ProfileTools](https://openupm.com/packages/com.pylons.unity.sdk.profiletools/)
- [ItemSchema](https://openupm.com/packages/com.pylons.unity.sdk.itemschema/)

![](/.github/images/header.png)

## Installation

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

## Usage example

Example code is available in the [Pylons SDK examples package](https://openupm.com/packages/com.pylons.sdk.examples/).

## Release History

* 0.1.0
    * Initial release

## Meta

[Pylons LLC](https://pylons.tech) – contact@pylons.tech

Distributed under the MIT license. See ``LICENSE`` for more information.

[Pylons on Github](https://github.com/Pylons-tech/)