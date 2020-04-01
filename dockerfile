# Build 
FROM mcr.microsoft.com/dotnet/core/sdk:3.1 as build-env
WORKDIR /app
COPY *.csproj ./
RUN dotnet restore
COPY . ./
RUN dotnet publish -c Release -o output

# Runtime image
FROM mcr.microsoft.com/dotnet/core/runtime:3.1
WORKDIR /app
COPY --from=build-env /app/output .
ENTRYPOINT ["dotnet", "cmdarr.dll"]