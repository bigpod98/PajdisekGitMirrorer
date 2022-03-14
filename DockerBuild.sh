#!/bin/bash
REGISTRY=ghcr.io/bigpod98/pajdisekgitmirrorer

docker build . -t $REGISTRY/pajdisekgitmirrorer.api:latest -f Dockerfiles/PajdisekGitMirrorer.API/Dockerfile --no-cache
docker build . -t $REGISTRY/pajdisekgitmirrorer.dbmigrations:latest -f Dockerfiles/PajdisekGitMirrorer.DBMigrations/Dockerfile --no-cache
docker build . -t $REGISTRY/pajdisekgitmirrorer.frontend:latest -f Dockerfiles/PajdisekGitMirrorer.Frontend/Dockerfile --no-cache
docker build . -t $REGISTRY/pajdisekgitmirrorer.mirroringscaledworker:latest -f Dockerfiles/PajdisekGitMirrorer.MirroringScaledWorker/Dockerfile --no-cache
docker build . -t $REGISTRY/pajdisekgitmirrorer.mirroringtimeworker:latest -f Dockerfiles/PajdisekGitMirrorer.MirroringTimeWorker/Dockerfile --no-cache
docker push $REGISTRY/pajdisekgitmirrorer.api:latest
docker push $REGISTRY/pajdisekgitmirrorer.dbmigrations:latest
docker push $REGISTRY/pajdisekgitmirrorer.frontend:latest
docker push $REGISTRY/pajdisekgitmirrorer.mirroringscaledworker:latest
docker push $REGISTRY/pajdisekgitmirrorer.mirroringtimeworker:latest