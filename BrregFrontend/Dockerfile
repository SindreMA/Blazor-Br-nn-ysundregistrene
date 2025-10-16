# Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

# Copy csproj and restore dependencies
COPY BrregFrontend.csproj .
RUN dotnet restore "BrregFrontend.csproj"

# Copy everything else and build
COPY . .
RUN dotnet build "BrregFrontend.csproj" -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish "BrregFrontend.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime stage - use nginx to serve the static files
FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html

# Copy the published Blazor WASM files
COPY --from=publish /app/publish/wwwroot .

# Copy nginx configuration
COPY nginx.conf /etc/nginx/nginx.conf

# Copy entrypoint script for environment variable injection and fix line endings
COPY docker-entrypoint.sh /docker-entrypoint.sh
RUN dos2unix /docker-entrypoint.sh || sed -i 's/\r$//' /docker-entrypoint.sh
RUN chmod +x /docker-entrypoint.sh

# Set default API_URL
ENV API_URL=https://brreg.sindrema.com/api

EXPOSE 80

ENTRYPOINT ["/docker-entrypoint.sh"]
CMD ["nginx", "-g", "daemon off;"]
