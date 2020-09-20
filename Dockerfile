FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /netcore-bank
COPY ["src/api/Application/Application.csproj", "src/api/Application/"]
COPY ["src/api/Domain/Domain.csproj", "src/api/Domain/"]
COPY ["src/api/Infra/Infra.csproj", "src/api/Infra/"]
RUN dotnet restore "src/api/Application/Application.csproj"
COPY . .
WORKDIR "/netcore-bank/src/api/Application"
RUN dotnet build "Application.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Application.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Application.dll"]