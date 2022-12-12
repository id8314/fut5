﻿FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY fut5.csproj .
RUN dotnet restore fut5.csproj
COPY . .
RUN dotnet build fut5.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish fut5.csproj -c Release -o /app/publish

FROM nginx:alpine AS final
WORKDIR /usr/share/nginx/html
COPY --from=publish /app/publish/wwwroot .
COPY nginx.conf /etc/nginx/nginx.conf

# https://mindsers.blog/post/https-using-nginx-certbot-docker/
