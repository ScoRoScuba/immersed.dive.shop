#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["immersed.dive.shop.webapi/immersed.dive.shop.webapi.csproj", "immersed.dive.shop.webapi/"]
RUN dotnet restore "immersed.dive.shop.webapi/immersed.dive.shop.webapi.csproj"
COPY . .
WORKDIR "/src/immersed.dive.shop.webapi"
RUN dotnet build "immersed.dive.shop.webapi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "immersed.dive.shop.webapi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "immersed.dive.shop.webapi.dll"]