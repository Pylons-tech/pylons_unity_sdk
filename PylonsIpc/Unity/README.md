# Pylons IPC) [![openupm](https://img.shields.io/npm/v/com.pylons.unity.ipc?label=openupm&registry_uri=https://package.openupm.com)](https://openupm.com/packages/com.pylons.unity.ipc/)
> High-level IPC abstractions for Unity

Pylons IPC provides a high-level API for writing game code that interacts with other processes in Unity in a platform-agnostic way.

Platform-specific implementation details are handled in the background by the Pylons IPC layer. Host applications must use a compatible IPCLayer implementation
in order to handle messages from the Unity side of the bridge. (TODO: split these out, link them)

At present, only a debug-use local HTTP backend is available for Pylons IPC. An Android backend exists, but is currently mothballed.

**Pylons IPC is not suitable for general-purpose use at the present time.** It's available as a dependency for the current preview release of the Pylons SDK for Unity.
If you want to use it for another purpose, you're on your own for now.

![](/.github/images/ipc-header.png)

## Installation

openupm-cli:

If [openupm-cli](https://github.com/openupm/openupm-cli) is already installed:

```
openupm add com.pylons.unity.ipc
```

Manually adding the scope within the Unity Package Manager:

- Edit > Project Settings > Package Manager
- Under the Scoped Registries heading, click the + button to add a new entry to the list.
- Configure the new registry as below, then hit apply:

![Registry config](/.github/images/registry.png)

- In the top left of the Package Manager window, click the 'Packages: Unity Registry' dropdown and select 'Packages: My Registries' instead. Then, select the package from the list on the left and click 'Install.'

## Usage example

(TODO)

## Release History

* 0.1.0
    * Initial release

## Meta

[Pylons LLC](https://pylons.tech) – contact@pylons.tech

Distributed under the MIT license. See ``LICENSE`` for more information.

[Pylons on Github](https://github.com/Pylons-tech/)