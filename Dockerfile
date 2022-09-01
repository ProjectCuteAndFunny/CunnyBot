FROM mcr.microsoft.com/dotnet/sdk:latest

RUN mkdir -p /app/src

WORKDIR /app/src

COPY . .

RUN cd /app/src

RUN dotnet restore

ENV DISCORD_TOKEN=
ENV DISCORD_GUILD=

ENTRYPOINT ["dotnet", "run"]
