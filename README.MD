<p align="center">
  <img width="500" src="https://raw.githubusercontent.com/ShimanoRDP/ShimanoRDP/develop/Tools/img/logo.png">
</p>
  
<p align="center">
  An open source, multi-protocol, tabbed remote connections manager allowing you to view all of your connections in a simple yet powerful interface
</p>

<p align="center">
  <img alt="GitHub All Releases" src="https://img.shields.io/github/downloads/ShimanoRDP/ShimanoRDP/total?label=Overall%20Downloads&style=for-the-badge">
</p>

<p align="center">
  <a href="https://www.reddit.com/r/ShimanoRDP/">
    <img alt="Subreddit subscribers" src="https://img.shields.io/reddit/subreddit-subscribers/ShimanoRDP?label=Reddit&logo=Reddit&style=flat-square">
  </a>
  <a href="https://twitter.com/ShimanoRDP">
    <img alt="Twitter Follow" src="https://img.shields.io/twitter/follow/ShimanoRDP?color=%231DA1F2&label=Twitter&logo=Twitter&style=flat-square">
  </a>
  <a href="https://gitter.im/ShimanoRDP/PublicChat">
    <img alt="Gitter" src="https://img.shields.io/gitter/room/ShimanoRDP/PublicChat?label=Join%20the%20Chat&logo=Gitter&style=flat-square">
  </a>  
  <a href="https://www.paypal.me/DavidSparer">
    <img alt="PayPal" src="https://img.shields.io/badge/%24-PayPal-blue.svg?label=Donate&logo=PayPal&style=flat-square">
  </a>
  <a href="bitcoin:16fUnHUM3k7W9Fvpc6dug7TAdfeGEcLbSg">
    <img alt="Bitcoin" src="https://img.shields.io/badge/%24-Bitcoin.svg?label=Donate&logo=bitcoin&style=flat-square">
  </a>
</p>

<p align="center">
  <a href="https://github.com/ShimanoRDP/ShimanoRDP/blob/develop/COPYING.TXT">
    <img alt="License" src="https://img.shields.io/github/license/ShimanoRDP/ShimanoRDP?label=License&style=flat">
  </a>
  <a href="https://bestpractices.coreinfrastructure.org/projects/529">
    <img alt="CII Best Practices" src="https://bestpractices.coreinfrastructure.org/projects/529/badge?style=flat">
  </a>
  <a href='https://ShimanoRDP.readthedocs.io/en/latest/?badge=latest'>
    <img src='https://readthedocs.org/projects/ShimanoRDP/badge/?version=latest' alt='Documentation Status' />
  </a>
</p>

---

| Update Channel | Build Status | Downloads |
| ---------------|--------------|-----------|
| Stable | [![Build status](https://ci.appveyor.com/api/projects/status/k0sdbxmq90fgdmj6/branch/master?svg=true)](https://ci.appveyor.com/project/ShimanoRDP/ShimanoRDP/branch/master) | [![Github Releases (by Release)](https://img.shields.io/github/downloads/ShimanoRDP/ShimanoRDP/v1.76.20/total.svg)](https://github.com/ShimanoRDP/ShimanoRDP/releases/tag/v1.76.20) |
| Prerelease | [![Build status](https://ci.appveyor.com/api/projects/status/k0sdbxmq90fgdmj6/branch/develop?svg=true)](https://ci.appveyor.com/project/ShimanoRDP/ShimanoRDP/branch/develop) | [![Github Releases (by Release)](https://img.shields.io/github/downloads/ShimanoRDP/ShimanoRDP/v1.77.1/total.svg)](https://github.com/ShimanoRDP/ShimanoRDP/releases/tag/v1.77.1) |

## Features

The following protocols are supported:

* RDP (Remote Desktop Protocol)
* VNC (Virtual Network Computing)
* SSH (Secure Shell)
* Telnet (TELecommunication NETwork)
* HTTP/HTTPS (Hypertext Transfer Protocol)
* rlogin (Remote Login)
* Raw Socket Connections
* Powershell remoting

For a detailed feature list and general usage support, refer to the [Documentation](https://ShimanoRDP.readthedocs.io/en/latest/).

## Installation

### Supported Operating Systems

- [Windows 10](https://en.wikipedia.org/wiki/Windows_10)
- [Windows 8.1](https://en.wikipedia.org/wiki/Windows_8.1)
- [Windows Server 2019](https://en.wikipedia.org/wiki/Windows_Server_2019)
- [Windows Server 2016](https://en.wikipedia.org/wiki/Windows_Server_2016)
- [Windows Server 2012 R2](https://en.wikipedia.org/wiki/Windows_Server_2012_R2)

### Packaging

Downloads are provided in three different packages.

#### Binary package

The binary package of ShimanoRDP is a compiled version of ShimanoRDP which comes in an MSI installer.
This is the most common way to install ShimanoRDP and get up and running.

#### Portable package

The portable package contains a modified version of the executable which stores and loads all your settings from files in the application's directory.
This package can be used to run ShimanoRDP from a USB stick and preserve your configuration wherever you go.

#### Source package

This contains the source code from which ShimanoRDP is build.
You will need to compile it yourself using Visual Studio.

### Minimum Requirements

* [Microsoft Visual C++ Redistributable for Visual Studio 2015, 2017 and 2019](https://support.microsoft.com/en-us/help/2977003/the-latest-supported-visual-c-downloads)
* [Microsoft .NET Framework 4.0](https://www.microsoft.com/en-us/download/details.aspx?id=17851)
* Microsoft Terminal Service Client 6.0 or later
  * Needed if you use RDP. mstscax.dll and/or msrdp.ocx be registered.
* [Citrix ICA Client](https://www.citrix.com/downloads/citrix-receiver.html)
  * Needed if you use ICA. wfica.ocx must be registered

### Download

ShimanoRDP is available as a redistributable MSI package or as a portable ZIP package and can be downloaded from the following locations:
* [GitHub](https://github.com/ShimanoRDP/ShimanoRDP/releases)
* [Project Website](https://ShimanoRDP.org/download)

### Command line install

The MSI package of ShimanoRDP can be installed using the command line:

`msiexec /i C:\Path\To\ShimanoRDP-Installer.exe [INSTALLDIR=value] [IGNOREPREREQUISITES=value]`

| Property | Value | Description |
|-|-|-|
| INSTALLDIR | `folder path` | This allows you to set the installation directory from the command line. For paths that contain spaces, enclose the path in double quotes (""). This overrides any value found in the registry. |
| IGNOREPREREQUISITES | `0` or `1` | When set to `1`, the installer will not be halted if any prerequisite check is not met. You must still run the installer as administrator. |

#### Examples

**Install to a custom folder**

`msiexec /i C:\Path\To\ShimanoRDP-Installer.msi INSTALLDIR="D:\Work Apps\ShimanoRDP"`

**Ignore prerequisites during a normal install**

`msiexec /i C:\Path\To\ShimanoRDP-Installer.msi IGNOREPREREQUISITES=1`

**Ignore prerequisites during a silent install**

`msiexec /i C:\Path\To\ShimanoRDP-Installer.msi /qn IGNOREPREREQUISITES=1`

### Troubleshooting installation

Turn on verbose logging by using the `/lv* <log path>` argument at the command line.

`msiexec /i C:\Path\To\ShimanoRDP-Installer.msi /l*v C:\ShimanoRDP_install.log`

## Uninstall

### Standard Uninstall

ShimanoRDP basic binary package can be uninstalled with Windows Control Panel. If for some reason it does not work please
follow information provided below for Manual Uninstall.

### Manual Uninstall

_If you are using the Portable version, simply deleting the folder that contains ShimanoRDP should be sufficient. These uninstall instructions are only necessary for the normal binary .MSI installed version of ShimanoRDP_

* Delete the folder where ShimanoRDP was installed. By default, this is:
	`%PROGRAMFILES%\ShimanoRDP`

* Delete the ShimanoRDP install entry from one of the following locations. Search for "ShimanoRDP" in the DisplayName field:
  * x86: ``HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\``
  * x64: ``HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\``

* (Optional) If you would also like to delete user data remove `%LOCALAPPDATA%\ShimanoRDP`

## Featured Projects

* [PSShimanoRDP](https://github.com/realslacker/PSShimanoRDP) A module to create ShimanoRDP connection files from PowerShell.

## Contribute

If you find ShimanoRDP useful and would like to contribute, it would be greatly appreciated. When you contribute, you make it possible for the team to cover the costs of producing ShimanoRDP.

### Submit Code
Check out the [Wiki page](https://github.com/ShimanoRDP/ShimanoRDP/wiki) on how to configure your development environment and submit a pull request.

### Translate
Check out the [Wiki page](https://github.com/ShimanoRDP/ShimanoRDP/wiki) on how to help make ShimanoRDP a polyglot.

</br>
<p align="center">
  <img alt="Developed with ReSharper" src="https://raw.githubusercontent.com/ShimanoRDP/ShimanoRDP/develop/Tools/img/icon_ReSharper.png">
</p>
