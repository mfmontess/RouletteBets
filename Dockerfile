FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Services/Roulette.Api/Roulette.Api.csproj", "Services/Roulette.Api/"]
RUN dotnet restore "Services/Roulette.Api/Roulette.Api.csproj"
COPY . .
WORKDIR "/src/Services/Roulette.Api"
RUN dotnet build "Roulette.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Roulette.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Roulette.Api.dll"]
