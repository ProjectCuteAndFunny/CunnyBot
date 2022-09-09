# **CunnyBot**

**CunnyBot** is a Discord bot written in **C#** using the [**Discord.Net**](https://github.com/discord-net/Discord.Net) library. The main usage and goal of the bot is to provide images from sites like Gelbooru, Safebooru, etc... with tags chosen by users.

**CunnyBot** makes use of the [**CunnyAPI**](https://github.com/ProjectCuteAndFunny/CunnyApi) to fetch images. 

# Commands
Currently **CunnyBot** has five commands.
```
/cunny site tags images
/blue-archive site character images
/genshin-impact site character images
/vtuber site vtuber images
/shutdown
```
The **`Site`** option currently provides the follow choices: **`Gelbooru`**, **`Safebooru`**, **`Danbooru`**, **`Konachan`** and **`Yandere`**. The user is only able to choose one website per query.

The **`Character`** option provides a choice of a character based on the command.

The **`Tags`** option needs to contain tags that are valid based on the site the user has chosen.

The **`Images`** option simply asks the user how many images the bot should post

The **``/shutdown``** command can only be successfully run by the owner of the bot, and as the name suggests, it shuts down the bot.

# Running the bot
The bot can simply be run by running **``dotnet run``** at the root directory of the bot file structure.

Another option is to use **``dotnet publish``** You can read more about this at [**dotnet publish**](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-publish)

# Notes
The bot token is kept as an environment variable named: ``CUNNY_TOKEN``.

The bot needs [CunnyAPI](https://github.com/ProjectCuteAndFunny/CunnyApi) to work fetch images, the bot gets the url from environment variable named: ``CUNNY_API_URL``

For registering a guild specific command, the id of the guild must be kept as an environment variable named: ``CUNNY_GUILD``

Every slash command has the **``ephemeral``** set to **``true``** this means that nobody else can see or know that a user has run a slash command; only the person who ran the command can see the response. 

# NuGet Packages
**CunnyBot** makes uses of the following **NuGet** packages:
```
Discord.NET
Discord.NET.Commands
Discord.NET.Core
Discord.NET.Interactions
Discord.NET.Rest
Discord.NET.WebSocket
Microsoft.Extensions.DependencyInjection
Microsoft.Extensions.Hosting
Newtonsoft.Json
```
