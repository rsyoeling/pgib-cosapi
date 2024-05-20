FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80/tcp

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Api.csproj", "./"]
RUN dotnet restore "Api.csproj"
COPY /src .
WORKDIR "/src/."
RUN dotnet build "Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Api.csproj" -c Release -o /app/publish

FROM base AS final
# for health check
RUN apt-get update
RUN apt-get -y install procps
# end for health check
WORKDIR /app
COPY --from=publish /app/publish /app
RUN mkdir -p /app/Resources
COPY src/Resources /app/Resources
COPY DockerfileEntryPoint.sh /usr/local/bin/DockerfileEntryPoint.sh
RUN chmod 744 /usr/local/bin/DockerfileEntryPoint.sh
ENTRYPOINT ["DockerfileEntryPoint.sh"]
