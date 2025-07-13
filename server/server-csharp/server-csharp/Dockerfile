# Use the .NET SDK to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy project files and restore dependencies
COPY ./server-csharp.csproj ./
RUN dotnet restore

# Copy all source files and build the app
COPY . ./
RUN dotnet publish -c Release -o /out

# Use a smaller runtime image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /out .

# Expose port (if needed) and run
EXPOSE 5000
ENTRYPOINT ["dotnet", "server-csharp.dll"]