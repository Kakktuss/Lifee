FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Lifee.Api/Lifee.Api.csproj", "Lifee.Api/"]
RUN dotnet restore "Lifee.Api/Lifee.Api.csproj"
COPY . .
WORKDIR "/src/Lifee.Api"
RUN dotnet build "Lifee.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Lifee.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lifee.Api.dll"]
