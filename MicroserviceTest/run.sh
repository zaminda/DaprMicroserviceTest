dapr run \
    --app-id myapp \
    --app-port 5230 \
    --dapr-http-port 3501 \
    --components-path ./dapr-components \
    dotnet run