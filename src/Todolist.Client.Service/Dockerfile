FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Your existing base stage...

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS node-install
RUN apt-get update \
    && apt-get install -y curl \
    && curl -sL https://deb.nodesource.com/setup_lts.x | bash - \
    && apt-get install -y nodejs \
    && apt-get install -y npm

# Verify Node.js and npm installation
RUN node -v
RUN npm -v

FROM node-install AS build
WORKDIR /src
COPY ["Todolist.Client.Service/Todolist.Client.Service.csproj", "Todolist.Client.Service/"]
RUN dotnet restore "Todolist.Client.Service/Todolist.Client.Service.csproj"
COPY . .
WORKDIR "/src/Todolist.Client.Service"
RUN dotnet build "Todolist.Client.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Todolist.Client.Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Todolist.Client.Service.dll"]
