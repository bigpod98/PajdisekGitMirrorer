FROM mcr.microsoft.com/dotnet/sdk:6.0.100-rc.1-focal-amd64 AS BUILDER
RUN mkdir /build /buildOutput
COPY . /build
WORKDIR /build
RUN dotnet restore
RUN dotnet publish PajdisekGitMirrorer.MirroringTimeWorker/PajdisekGitMirrorer.MirroringTimeWorker.csproj --configuration Release --output /buildOutput --runtime linux-x64 -p:PublishTrimmed=true

FROM ubuntu:latest AS RUNTIME
RUN apt update && apt upgrade -y && apt install libicu66 libssl1.1 ca-certificates -y && update-ca-certificates
COPY --from=BUILDER /buildOutput /opt/PajdisekGitMirrorer.MirroringTimeWorker
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENTRYPOINT ["/opt/PajdisekGitMirrorer.MirroringTimeWorker/PajdisekGitMirrorer.MirroringTimeWorker"]