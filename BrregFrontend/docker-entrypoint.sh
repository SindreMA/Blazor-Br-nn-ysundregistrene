#!/bin/sh
set -e

echo "Generating config.json with API_URL: ${API_URL}"

# Generate config.json with the API_URL from environment variable
cat > /usr/share/nginx/html/config.json <<EOF
{
  "apiUrl": "${API_URL}"
}
EOF

echo "config.json generated successfully"

# Execute the CMD
exec "$@"
