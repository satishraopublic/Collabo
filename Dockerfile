FROM microsoft/dotnet:2.2-sdk AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./collabo.api/collabo.api.csproj ./collabo.api/collabo.api.csproj
COPY ./collabo.data/collabo.data.csproj ./collabo.data/collabo.data.csproj
COPY ./collabo.common/collabo.common.csproj ./collabo.common/collabo.common.csproj
COPY ./collabo.api.test/collabo.api.test.csproj ./collabo.api.test/collabo.api.test.csproj
COPY ./collabo.app.sln.sln ./
RUN dotnet restore collabo.app.sln.sln

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM microsoft/dotnet:aspnetcore-runtime
WORKDIR /app
COPY --from=build-env /app/collabo.api/out .
ENTRYPOINT ["dotnet", "collabo.api.dll"]
