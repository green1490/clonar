FROM mcr.microsoft.com/dotnet/sdk:7.0
WORKDIR /app
COPY . .
RUN dotnet restore

ENV ASPNETCORE_ENVIRONMENT=Development

EXPOSE 5246

ENTRYPOINT [ "dotnet","watch"]