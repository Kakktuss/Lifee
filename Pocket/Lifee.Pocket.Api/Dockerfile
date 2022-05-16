FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Pocket/Lifee.Pocket.Api/Lifee.Pocket.Api.csproj", "Lifee.Pocket.Api/"]
RUN dotnet restore "Pocket/Lifee.Pocket.Api/Lifee.Pocket.Api.csproj"
COPY . .
WORKDIR "/src/Lifee.Pocket.Api"
RUN dotnet build "Lifee.Pocket.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Lifee.Pocket.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lifee.Pocket.Api.dll"]
