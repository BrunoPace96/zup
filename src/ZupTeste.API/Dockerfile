FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/ZupTeste.API/ZupTeste.API.csproj", "ZupTeste.API/"]
RUN dotnet restore "src/ZupTeste.API/ZupTeste.API.csproj"
COPY . .
WORKDIR "/src/ZupTeste.API"
RUN dotnet build "ZupTeste.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ZupTeste.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ZupTeste.API.dll"]
