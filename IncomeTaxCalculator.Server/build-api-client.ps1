Write-Host "Generating TypeScript API client..." -ForegroundColor Cyan

try {
    dotnet tool update -g nswag.consolecore
    dotnet nswag run nswag.json /variables:Configuration=Debug

    if ($LASTEXITCODE -ne 0) {
        Write-Host "Failed to generate API client with NSwag. Error code: $LASTEXITCODE" -ForegroundColor Red
        exit 1
    }

    Write-Host "API client generated successfully." -ForegroundColor Green
} catch {
    Write-Host "An error occurred: $_" -ForegroundColor Red
    exit 1
}
