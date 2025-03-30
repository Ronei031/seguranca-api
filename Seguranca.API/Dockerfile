FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /app

COPY . ./

RUN dotnet restore

RUN dotnet publish -o out

RUN mkdir ./out/db

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /runtime/app
COPY --from=build /app/out .

# Define a porta correta para a API dentro do contêiner
ENV ASPNETCORE_URLS=http://+:7271

EXPOSE 7271

ENTRYPOINT ["dotnet", "Seguranca.API.dll"]
