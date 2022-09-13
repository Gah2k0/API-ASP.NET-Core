-include .env
TAG ?= ${TAG}
REPO = bempromotora/${PROJECT_NAME}

ifeq '$(findstring ;,$(PATH))' ';'
    OS := Windows
    PWD := cd
else
    OS := $(shell uname 2>/dev/null || echo Unknown)
    OS := $(patsubst CYGWIN%,Cygwin,$(OS))
    OS := $(patsubst MSYS%,MSYS,$(OS))
    OS := $(patsubst MINGW%,MSYS,$(OS))
    PWD := pwd
endif

.PHONY: build test push shell run start stop logs clean release

default: print

print:
	echo "PROJECT_NAME: ${PROJECT_NAME}"
	echo "DOTNET_VER: ${DOTNET_VER}"
	echo "BUILDCONFIG: ${BUILDCONFIG}"
	echo "ASPNETCORE_ENVIRONMENT: ${ASPNETCORE_ENVIRONMENT}"

build:
	docker build \
		--no-cache \
		--force-rm \
		--build-arg TAG=${TAG} \
		--build-arg DOTNET_VER=${DOTNET_VER} \
		--build-arg BUILDCONFIG=${BUILDCONFIG} \
		--build-arg ASPNETCORE_ENVIRONMENT=${ASPNETCORE_ENVIRONMENT} \
		-t $(REPO):$(TAG) . 

shell:
	docker-compose exec -T web bash

run:
	docker-compose up 

start:
	docker-compose up -d --remove-orphans

stop:
	docker-compose down --remove-orphans

logs:
	docker-compose logs

remove:
	docker image rm $(REPO):$(TAG) -f

release: build push