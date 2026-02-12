# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy solution and project files
COPY *.sln ./
COPY src/FamilyTracker.Domain/*.csproj ./src/FamilyTracker.Domain/
COPY src/FamilyTracker.Application/*.csproj ./src/FamilyTracker.Application/
COPY src/FamilyTracker.Infrastructure/*.csproj ./src/FamilyTracker.Infrastructure/
COPY src/FamilyTracker.API/*.csproj ./src/FamilyTracker.API/
COPY tests/FamilyTracker.UnitTests/*.csproj ./tests/FamilyTracker.UnitTests/

# Restore dependencies
RUN dotnet restore

# Copy everything else and build
COPY . .
WORKDIR /src/src/FamilyTracker.API
RUN dotnet build -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Install CA certificates
RUN apt-get update && apt-get install -y ca-certificates && update-ca-certificates

WORKDIR /app
EXPOSE 5000

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FamilyTracker.API.dll"]
