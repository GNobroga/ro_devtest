# Etapa 1: Usar a imagem base do SDK do .NET para construção
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

COPY /api . 

RUN dotnet restore RO.DevTest.sln
RUN dotnet build RO.DevTest.sln -c Release -o /app/build
RUN dotnet publish RO.DevTest.WebApi/RO.DevTest.WebApi.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 

WORKDIR /app

EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "RO.DevTest.WebApi.dll"]
