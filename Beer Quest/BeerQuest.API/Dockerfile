FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["BeerQuest.API/BeerQuest.API.csproj", "BeerQuest.API/"]
RUN dotnet restore "BeerQuest.API/BeerQuest.API.csproj"
COPY . .
WORKDIR "/src/BeerQuest.API"
RUN dotnet build "BeerQuest.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BeerQuest.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BeerQuest.API.dll"]
