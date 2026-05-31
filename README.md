# Photon Network Checker for R.E.P.O

Displays Photon nameserver reachability directly on the main menu HUD.
Three check types per server — IP, HTTP, and WSS — so you know exactly where connectivity fails.

[![GitHub Release](https://img.shields.io/github/v/release/ToxesFoxes/repo-photon-network-checker?label=GitHub%20Release&color=orange)](https://github.com/ToxesFoxes/repo-photon-network-checker/releases)

![Version](https://img.shields.io/badge/version-1.0.0-blue)
![Game](https://img.shields.io/badge/game-R.E.P.O-green)
![Loader](https://img.shields.io/badge/loader-BepInEx-6aa84f)
![Status](https://img.shields.io/badge/status-working-brightgreen)

## 📖 Description

Having trouble connecting to lobbies or getting dropped mid-game?
Photon Network Checker adds a compact status line to the main menu HUD showing live reachability of all four Photon nameservers.
Each server is checked with three methods: DNS resolve, TCP port 80, and TLS port 443 (WSS).

## 🎯 Who Needs This Mod

Anyone who wants to quickly see whether Photon connectivity issues are on their end or the server side.
Client-side only — no host required.

## ✨ Features

- Live status line in the main menu HUD (below build version)
- Three independent checks per server: IP (DNS), HTTP (port 80), WSS (TLS port 443)
- All checks run in parallel per server, re-checked every 30 seconds
- Color-coded icons: ✅ green = reachable, ❌ red = unreachable, ❓ yellow = checking
- Font size scaled to match the existing HUD style (75% of Build Name)

## 📊 Status Display

Located in `Header` below the build version text:

```
Photon (IP|HTTP|WSS): ns ✓✓✓, eu ✓✓✓, us ✓✓✓, jp ✓✓✓
```

Each server shows three icons in order: **IP** → **HTTP** → **WSS**.

| Icon | Color  | Meaning     |
| ---- | ------ | ----------- |
| ✓    | green  | reachable   |
| ✗    | red    | unreachable |
| ?    | yellow | checking... |

## 🔍 Check Types

| #   | Name | Method                               |
| --- | ---- | ------------------------------------ |
| 1   | IP   | DNS resolve (`Dns.GetHostAddresses`) |
| 2   | HTTP | TCP connect to port 80               |
| 3   | WSS  | TLS handshake on port 443            |

## 🌐 Checked Servers

| Key | Host                  |
| --- | --------------------- |
| ns  | ns.photonengine.io    |
| eu  | ns-eu.photonengine.io |
| us  | ns-us.photonengine.io |
| jp  | ns-jp.photonengine.io |

## 📋 Requirements

- R.E.P.O (Steam)
- BepInEx pack for R.E.P.O
- Windows OS

## 📦 Installation via r2modman

1. Install r2modman from https://github.com/ebkr/r2modmanPlus/releases
2. Open r2modman and select R.E.P.O
3. Create or choose a profile
4. Search for `PhotonNetworkChecker` by ToxesFoxes
5. Install the mod

Dependencies are pulled automatically:

- BepInEx-BepInExPack-5.4.2305

## 🔧 Manual Installation

1. Install BepInEx for R.E.P.O
2. Download the latest release from:
	- GitHub: https://github.com/ToxesFoxes/repo-photon-network-checker/releases
3. Copy `TFS_PhotonNetworkChecker.dll` into your plugins folder:

```text
<REPO_PROFILE>/BepInEx/plugins/ToxesFoxes-PhotonNetworkChecker/TFS_PhotonNetworkChecker.dll
```

4. Launch the game

## 🛠️ Build from Source

1. Build:

```powershell
dotnet build TFS_PhotonNetworkChecker.csproj -c Release
```

2. Output DLL:

```text
bin/Release/TFS_PhotonNetworkChecker.dll
```

For one-command build and deploy, use `build-and-deploy.sh` / `build-and-deploy.cmd`.

## ⚠️ Disclaimer

- This mod is not affiliated with or endorsed by the developers of R.E.P.O
- Use at your own risk
- Always back up your saves before using any mods

## 🤖 AI Content Disclaimer

This project used AI-assisted tooling during development for code generation and iteration support.

- AI assistance was used as a development aid, not as an autonomous publisher.
- Final implementation decisions, integration, and testing were performed manually by the author.
- This notice is provided for transparency regarding the development workflow.

## 🤝 Contributing

Contributions are welcome! Please:
1. Fork the repository
2. Create a feature branch
3. Make your changes
4. Test thoroughly
5. Submit a pull request

## 🙏 Credits

- **Harmony** - [Harmony Patching Library](https://github.com/pardeike/Harmony)
- **BepInEx** - [BepInEx Mod Loader](https://github.com/bepinex/bepinex)
- **r2modman** - [r2modmanPlus Mod Manager](https://github.com/ebkr/r2modmanPlus)
- **R.E.P.O** - Game by semiwork
- **ToxesFoxes** - Mod development

## 📄 License

This project is licensed under the MIT License.

## 📞 Support

- 🐛 [Report Issues](https://github.com/ToxesFoxes/repo-photon-network-checker/issues)
- 📧 Contact: toxes_foxes@outlook.com