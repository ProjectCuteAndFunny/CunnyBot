# **CunnyBot**

**CunnyBot** is a Discord bot written in **[C#](https://learn.microsoft.com/en-us/dotnet/csharp/)** using the [**Discord.Net**](https://github.com/discord-net/Discord.Net) library. The main usage and goal of the bot is to provide images from sites like Gelbooru, Safebooru, etc... with tags chosen by the user.

**CunnyBot** makes use of the [**CunnyAPI**](https://github.com/ProjectCuteAndFunny/CunnyApi) to fetch images.

# Commands
Currently **CunnyBot** has four commands.
```
/cunny images tags skip site
/blue-archive images character skip site
/genshin-impact images character skip site
/vtuber images vtuber skip site
```

The **`Images`** option simply asks the user how many images the bot should post

The **`Character`** option provides a choice of a character based on the command.

The **`Tags`** option needs to contain tags that are valid based on the site the user has chosen. (e.g. `1girl blonde_hair blue_eyes looking_at_viewer`)

The **`Skip`** option is used to *skip* to a page (e.g. if you want to skip to page 5, you would type `skip 4`). Skipping allows you to start indexing from that page.

The **`Site`** option currently provides the following choices: **`Gelbooru`**, **`Safebooru`**, **`Danbooru`**, **`Konachan`** and **`Yandere`**. The user is only able to choose one website per query.

# Running the bot
The bot can simply be ran by running **``dotnet run``** at the root directory of the bot file structure.

Another option is to use **``dotnet publish``** You can read more about this at [**dotnet publish**](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-publish)

# Notes
The bot token is kept as an environment variable named: ``CUNNY_TOKEN``.

**CunnyBot** requires [CunnyAPI](https://github.com/ProjectCuteAndFunny/CunnyApi) in order to function. **CunnyBot** gets the URL for **CunnyAPI** from an environment variable named: ``CUNNY_API_URL``

For registering a guild specific command, the id of the guild must be kept as an environment variable named: ``CUNNY_GUILD``

# NuGet Packages
**CunnyBot** makes uses of the following **NuGet** packages:
```
Discord.NET
Discord.NET.Core
Discord.NET.WebSocket
Microsoft.Extensions.DependencyInjection
Microsoft.Extensions.Hosting
Serilog
Serilog.Sinks.Console
```