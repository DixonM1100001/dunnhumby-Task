FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base

WORKDIR /app
EXPOSE 7030

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src

COPY . .

RUN dotnet restore "ProductManagementAPI/ProductManagementAPI.csproj"

COPY . .

WORKDIR "/src/ProductManagementAPI"
RUN dotnet build "ProductManagementAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM base AS final
WORKDIR /app
COPY --from=build /app/build .
ENTRYPOINT ["dotnet", "ProductManagementAPI.dll"] 