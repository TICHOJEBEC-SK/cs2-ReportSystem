<h1 align="center">
  CS2 ReportSystem
</h1>

<p align="center">
<i>If you like the project, please consider <a href="https://paypal.com/paypalme/playpointsk">donating</a> 💸 to support further development!</i>
</p>

<p align="center">
<a href="https://www.paypal.com/paypalme/playpointsk"><img src="https://img.shields.io/badge/support-PayPal-blue?logo=PayPal&style=flat-square&label=Donate"/>
</a>
</p>

---

## 📜 About the Plugin

**ReportSystem** is a plugin for **Counter-Strike 2 (CounterStrikeSharp)** that allows players to easily report other players directly in-game via a menu.  
Its goal is to make report management **clear, efficient, and fully integrated with a database**, so reports can be handled outside the game as well (e.g. web panel, Discord bot, admin tools).

---

## 🔹 Features

- Report players directly in-game through an interactive menu  
- Custom reasons configurable in the config file  
- Cooldown to prevent spam reports  
- Self-report prevention (players cannot report themselves)  
- Limit of **1 active report per target** – new reports can only be submitted once the previous one is resolved  
- Clean database schema prepared for external integrations  
- Admin notifications in-game based on permissions  
- Multi-language support through JSON phrase files  

---

## 🛠 Installation

**Requirements**
- [CounterStrikeSharp](https://github.com/roflmuffin/CounterStrikeSharp)
- [MenuManagerCS2](https://github.com/NickFox007/MenuManagerCS2)
- MySQL database (with user & password)

**Steps**
1. Build the plugin (`dotnet build -c Release`) or download a prebuilt release.  
2. Copy the DLL and config file to:
   ```
   /game/csgo/addons/counterstrikesharp/plugins/ReportSystem/
   ```
3. Start or restart your server.  
4. On first run, the table `report_system_reports` will be automatically created.  

---

## ⚙️ Configuration

The config file is automatically generated on first run:

```json
{
  "Language": "sk",
  "ChatPrefix": "{lightred}[REPORT]",
  "ReportCommand": "css_report",
  "Database": {
    "Server": "127.0.0.1",
    "Port": 3306,
    "Database": "database_name",
    "User": "database_user",
    "Password": "database_password",
    "SslMode": "None"
  },
  "ServerName": "MIRAGE",
  "ServerAddress": "127.0.0.1:27015",
  "NotifyAdminsPermission": "@css/kick",
  "Reasons": [ "Cheating", "Toxic", "Voice spam", "Exploit" ],
  "CooldownSeconds": 30,
  "BlockSelfReport": true
}
```

---

## 🔗 Integration

This plugin is designed to be **fully ready for any external integration** based on the database, such as:
- Web-based admin panel  
- Discord bot with live notifications  
- Internal admin tools  

⚠️ **Discord module** is not included in this repository – it is part of my private Discord bot and therefore remains **private**.  
Nevertheless, the database schema is designed so anyone can implement their own web/Discord integration.

---

## 🎨 Chat Colors

You can use the following color tags in messages and prefixes:  
`{default}, {white}, {darkred}, {green}, {lightyellow}, {lightblue}, {olive}, {lime}, {red}, {lightpurple}, {purple}, {grey}, {yellow}, {gold}, {silver}, {blue}, {darkblue}, {bluegrey}, {magenta}, {lightred}, {orange}`

---

## 📩 Contact
- **Discord:** `tichotm`
