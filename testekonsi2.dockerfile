#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#
#FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
#WORKDIR /app
#EXPOSE 80
#EXPOSE 443
#
#FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
#WORKDIR /src
#COPY ["TesteKonsi.WebApi/TesteKonsi.WebApi.csproj", "TesteKonsi.WebApi/"]
#COPY ["TesteKonsi.Domain/TesteKonsi.Domain.csproj", "TesteKonsi.Domain/"]
#COPY ["TesteKonsi.Infra.Services/TesteKonsi.Infra.Services.csproj", "TesteKonsi.Infra.Services/"]
#COPY ["TesteKonsi.Application/TesteKonsi.Application.csproj", "TesteKonsi.Application/"]
#RUN dotnet restore "TesteKonsi.WebApi/TesteKonsi.WebApi.csproj"
#COPY . .
#WORKDIR "/src/TesteKonsi.WebApi"
#RUN dotnet build "TesteKonsi.WebApi.csproj" -c Release -o /app/build-env
#
#FROM build AS publish
#RUN dotnet publish "TesteKonsi.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false
#
#FROM base AS final
#WORKDIR /app
#COPY --from=build-env /app/publish .

# Use a imagem do SDK .NET como base
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env

# Defina o diretório de trabalho
WORKDIR /app

# Copie o csproj e restaure os pacotes dependentes
COPY ["TesteKonsi.WebApi/TesteKonsi.WebApi.csproj", "TesteKonsi.WebApi/"]
COPY ["TesteKonsi.Domain/TesteKonsi.Domain.csproj", "TesteKonsi.Domain/"]
COPY ["TesteKonsi.Infra.Services/TesteKonsi.Infra.Services.csproj", "TesteKonsi.Infra.Services/"]
COPY ["TesteKonsi.Application/TesteKonsi.Application.csproj", "TesteKonsi.Application/"]
RUN dotnet restore "TesteKonsi.WebApi/TesteKonsi.WebApi.csproj"

# Copie todo o resto e construa o projeto
COPY . ./
RUN dotnet publish -c Release -o out

# Gere a imagem final
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "TesteKonsi.WebApi.dll"]