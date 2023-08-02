FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 5099

ENV ASPNETCORE_URLS=http://+:5099

# Add this line to allow Swagger to work in Docker. Otherwise Swagger works only in development mode
ENV ASPNETCORE_ENVIRONMENT=Development

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Students.Api/Students.Api.csproj", "Students.Api/"]
RUN dotnet restore "Students.Api/Students.Api.csproj"
COPY . .
WORKDIR "/src/Students.Api"
RUN dotnet build "Students.Api.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "Students.Api.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Students.Api.dll"]
