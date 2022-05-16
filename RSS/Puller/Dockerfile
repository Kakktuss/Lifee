FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

COPY Lifee.RSS.Puller.Api Lifee.RSS.Puller.Api
COPY Lifee.RSS.Puller.Application Lifee.RSS.Puller.Application

RUN dotnet restore "Lifee.RSS.Puller.Api/Lifee.RSS.Puller.Api.csproj"

FROM build AS publish
RUN ls
RUN dotnet publish ./Lifee.RSS.Puller.Api/Lifee.RSS.Puller.Api.csproj --no-restore -c Release -r linux-x64 -o /app

FROM base as final
WORKDIR /app
COPY --from=publish /app .

ENV ASPNETCORE_URLS http://*:80

ENTRYPOINT ["dotnet ./Lifee.RSS.Puller.Api.exe"]