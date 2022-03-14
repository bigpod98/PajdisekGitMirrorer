FROM mcr.microsoft.com/dotnet/sdk:6.0.100-rc.1-focal-amd64 AS BUILDER
RUN mkdir /build /buildOutput
COPY . /build
WORKDIR /build
RUN dotnet restore
RUN dotnet publish PajdisekGitMirrorer.MirroringScaledWorker/PajdisekGitMirrorer.MirroringScaledWorker.csproj --configuration Release --output /buildOutput --runtime linux-x64

FROM ubuntu:latest AS CERT
RUN apt update && apt upgrade -y && apt install curl -y
RUN curl https://gitlab.pajdisek.palocal/-/snippets/1/raw/main/ca.crt -k -o /ca.crt

FROM ubuntu:latest AS RUNTIME
RUN apt update && apt upgrade -y && DEBIAN_FRONTEND=noninteractive apt install libicu66 libssl1.1 ca-certificates git -y
COPY --from=CERT /ca.crt /usr/local/share/ca-certificates
RUN update-ca-certificates
COPY --from=BUILDER /buildOutput /opt/PajdisekGitMirrorer.MirroringScaledWorker
EXPOSE 80
ENV ASPNETCORE_URLS=http://+:80
ENV DOTNET_RUNNING_IN_CONTAINER=true
ENTRYPOINT ["/opt/PajdisekGitMirrorer.MirroringScaledWorker/PajdisekGitMirrorer.MirroringScaledWorker"]
