#!/bin/sh
set -e

echo "Injecting API_URL environment variable: ${API_URL}"

# Find all .dll files in the Blazor WASM app and replace the hardcoded API URL
# This searches for the compiled Statics class and replaces the URL
find /usr/share/nginx/html/_framework -type f -name "*.dll" -exec sed -i "s|https://brreg.sindrema.com/api|${API_URL}|g" {} \;

# Also replace in any .js files that might contain the URL
find /usr/share/nginx/html/_framework -type f -name "*.js" -exec sed -i "s|https://brreg.sindrema.com/api|${API_URL}|g" {} \;

echo "API_URL injection complete"

# Execute the CMD
exec "$@"
