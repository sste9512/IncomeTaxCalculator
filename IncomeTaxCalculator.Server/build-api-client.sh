#!/bin/bash
# Make this script executable with: chmod +x build-api-client.sh

echo "Generating TypeScript API client..."

# Make script exit on error
set -e

# Install or update NSwag
dotnet tool update -g nswag.consolecore

# Run NSwag to generate client
dotnet nswag run nswag.json /variables:Configuration=Debug

if [ $? -eq 0 ]; then
  echo "API client generated successfully."
else
  echo "Failed to generate API client with NSwag. Error code: $?"
  exit 1
fi
