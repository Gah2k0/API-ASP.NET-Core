# para rodar o build correto utilize a linha a abaixo
ARG DOTNET_VER 
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VER} AS dotnet-build
WORKDIR /src
#proxy ambiente local
ARG ASPNETCORE_ENVIRONMENT
ARG BUILDCONFIG
ENV BUILDCONFIG=${BUILDCONFIG} \
    ASPNETCORE_ENVIRONMENT=${ASPNETCORE_ENVIRONMENT}
# Copiar csproj e restaurar dependencias
COPY Projeto-BemPreparados/*.csproj ./Projeto-BemPreparados/
COPY Aplicacao/*.csproj ./Aplicacao/
COPY Dominio/*.csproj ./Dominio/
COPY Infraestrutura/*.csproj ./Infraestrutura/
COPY BemPreparadosTestes/*.csproj ./BemPreparadosTestes/
COPY Projeto-BemPreparados.sln ./
RUN dotnet restore
# Build da aplicacao
COPY . ./
RUN dotnet publish -c Release -o build
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VER}
WORKDIR /app
COPY --from=dotnet-build /src/build /app
ENTRYPOINT ["dotnet", "Api.dll"]