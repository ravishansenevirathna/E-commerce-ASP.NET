# Stage 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy only the .csproj files to take advantage of Docker layer caching
COPY *.csproj ./
RUN dotnet restore

# Copy the remaining source code and build the app
COPY . ./
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copy the built application from the previous stage
COPY --from=build /app/publish .

# Expose the application port
EXPOSE 80

# Command to run the application
ENTRYPOINT ["dotnet", "EcommerceApi.dll"]