dapr run \
    --app-id second \
    --app-port 5061 \
    --dapr-http-port 3502 \
    --components-path ./dapr-components \
    dotnet run