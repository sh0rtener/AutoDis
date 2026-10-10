FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY src/server-side/Core/AutoDis.Domain/*.csproj src/server-side/Core/AutoDis.Domain/
COPY src/server-side/Core/AutoDis.UseCases/*.csproj src/server-side/Core/AutoDis.UseCases/

COPY src/server-side/Infra/AutoDis.Caching/*.csproj src/server-side/Infra/AutoDis.Caching/
COPY src/server-side/Infra/AutoDis.Persistense/*.csproj src/server-side/Infra/AutoDis.Persistense/

COPY src/server-side/Presenters/AutoDis.WebApi/*.csproj src/server-side/Presenters/AutoDis.WebApi/

RUN dotnet restore src/server-side/Presenters/AutoDis.WebApi/*.csproj

COPY src/server-side/ src/server-side/

WORKDIR /src/src/server-side/Presenters/AutoDis.WebApi
RUN dotnet publish -c Release -o /app/publish --no-restore


FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "AutoDis.WebApi.dll"]