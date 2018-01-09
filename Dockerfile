# Build binaries
FROM microsoft/dotnet:2.0-sdk AS build-env
WORKDIR /app

# Do restore as separate layer
COPY *.csproj ./
RUN dotnet restore

COPY *.cs ./
RUN dotnet publish -c Release -o out

FROM microsoft/dotnet:2.0-runtime
WORKDIR /app
COPY --from=build-env /app/out/* ./
ENTRYPOINT [ "dotnet", "./pubg-drop-randomiser.dll" ]