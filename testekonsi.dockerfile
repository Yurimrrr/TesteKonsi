#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TesteKonsi.WebApi/TesteKonsi.WebApi.csproj", "TesteKonsi.WebApi/"]
COPY ["TesteKonsi.Domain/TesteKonsi.Domain.csproj", "TesteKonsi.Domain/"]
COPY ["TesteKonsi.Infra/TesteKonsi.Infra.csproj", "TesteKonsi.Infra/"]
COPY ["TesteKonsi.Application/TesteKonsi.Application.csproj", "TesteKonsi.Application/"]
RUN dotnet restore "TesteKonsi.WebApi/TesteKonsi.WebApi.csproj"
COPY . .
WORKDIR "/src/TesteKonsi.WebApi"
RUN dotnet build "TesteKonsi.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TesteKonsi.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TesteKonsi.WebApi.dll"]