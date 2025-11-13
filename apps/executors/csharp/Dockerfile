FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

# Copy csproj and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy source code and build
COPY . ./
RUN dotnet publish -c Release -o out

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0

WORKDIR /app

# Copy build output
COPY --from=build /app/out .

# Create a non-root user
RUN useradd -m -u 1000 executor && \
    mkdir -p /tmp/csharp-execution && \
    chown -R executor:executor /app /tmp/csharp-execution

USER executor

EXPOSE 4004

ENTRYPOINT ["dotnet", "CSharpExecutor.dll"]
