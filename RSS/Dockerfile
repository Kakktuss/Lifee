FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY Lifee.RSS.Api Lifee.RSS.Api
COPY Lifee.RSS.Application Lifee.RSS.Application

RUN dotnet restore "Lifee.RSS.Api/Lifee.RSS.Api.csproj"

FROM build AS publish
RUN ls
RUN dotnet publish ./Lifee.RSS.Api/Lifee.RSS.Api.csproj --no-restore -c Release -r linux-x64 -o /app --self-contained

FROM base as final
WORKDIR /app
COPY --from=publish /app .

ENV ASPNETCORE_URLS http://*:80

ENTRYPOINT ["dotnet ./Lifee.RSS.Api.exe"]