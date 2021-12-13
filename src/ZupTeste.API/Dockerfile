FROM mcr.microsoft.com/dotnet/sdk:6.0 AS publish

WORKDIR /src
COPY . .
RUN dotnet publish src/ZupTeste.API/ZupTeste.API.csproj -v m -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS app

ARG ENVIRONMENT="DockerDev"
ENV ASPNETCORE_ENVIRONMENT=$ENVIRONMENT
ENV ASPNETCORE_URLS="http://+:5000"
WORKDIR /app

COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "ZupTeste.API.dll"]
EXPOSE 5000
EXPOSE 5001
