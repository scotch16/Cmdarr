# [scotch16/Cmdarr](https://github.com/scotch16/Cmdarr)

[Cmdarr](https://github.com/scotch16/Cmdarr) is an open-source chat bot to enable two way communication from Slack to Sonarr, Radarr, and NZBGet.

## Supported Architectures

| Architecture | Tag |
| :----: | --- |
| x86-64 | amd64-latest |

## Usage

Here is an example snippet to help you get started creating a container.

### docker

```
docker create \
  --name=cmdarr \
  -v <path to config>:/config \
  --restart unless-stopped \
  scotch16/cmdarr
```


### docker-compose

Compatible with docker-compose v2 schemas.

```
---
version: "2"
services:
  pixapop:
    image: scotch16/cmdarr
    container_name: cmdarr
    volumes:
      - <path to config>:/config
    restart: unless-stopped
```

## Parameters

Container images are configured using parameters passed at runtime (such as those above). These parameters are separated by a colon and indicate `<external>:<internal>` respectively. For example, `-p 8080:80` would expose port `80` from inside the container to be accessible from the host's IP on port `8080` outside the container.

| Parameter | Function |
| :----: | --- |
| `-v /config` | Stores config and logs for nginx base. |

## Environment variables from files (Docker secrets)

You can set any environment variable from a file by using a special prepend `FILE__`. 

As an example:

```
-e FILE__PASSWORD=/run/secrets/mysecretpassword
```

Will set the environment variable `PASSWORD` based on the contents of the `/run/secrets/mysecretpassword` file.

## User / Group Identifiers

When using volumes (`-v` flags) permissions issues can arise between the host OS and the container, we avoid this issue by allowing you to specify the user `PUID` and group `PGID`.

Ensure any volume directories on the host are owned by the same user you specify and any permissions issues will vanish like magic.

In this instance `PUID=1000` and `PGID=1000`, to find yours use `id user` as below:

```
  $ id username
    uid=1000(dockeruser) gid=1000(dockergroup) groups=1000(dockergroup)
```

## Support Info

* Shell access whilst the container is running: `docker exec -it cmdarr /bin/bash`
* To monitor the logs of the container in realtime: `docker logs -f cmdarr`
* container version number
  * `docker inspect -f '{{ index .Config.Labels "build_version" }}' cmdarr`
* image version number
  * `docker inspect -f '{{ index .Config.Labels "build_version" }}' scotch16/cmdarr`

## Updating Info

Most of our images are static, versioned, and require an image update and container recreation to update the app inside. We do not recommend or support updating apps inside the container.

Below are the instructions for updating containers:

### Via Docker Run/Create
* Update the image: `docker pull scotch16/cmdarr`
* Stop the running container: `docker stop cmdarr`
* Delete the container: `docker rm cmdarr`
* Recreate a new container with the same docker create parameters as instructed above (if mapped correctly to a host folder, your `/config` folder and settings will be preserved)
* Start the new container: `docker start cmdarr`
* You can also remove the old dangling images: `docker image prune`

### Via Docker Compose
* Update all images: `docker-compose pull`
  * or update a single image: `docker-compose pull cmdarr`
* Let compose update all containers as necessary: `docker-compose up -d`
  * or update a single container: `docker-compose up -d cmdarr`
* You can also remove the old dangling images: `docker image prune`

### Via Watchtower auto-updater (especially useful if you don't remember the original parameters)
* Pull the latest image at its tag and replace it with the same env variables in one run:
  ```
  docker run --rm \
  -v /var/run/docker.sock:/var/run/docker.sock \
  containrrr/watchtower \
  --run-once cmdarr
  ```

**Note:** We do not endorse the use of Watchtower as a solution to automated updates of existing Docker containers. In fact we generally discourage automated updates. However, this is a useful tool for one-time manual updates of containers where you have forgotten the original parameters. In the long term, we highly recommend using Docker Compose.

* You can also remove the old dangling images: `docker image prune`

## Building locally

If you want to make local modifications to these images for development purposes or just to customize the logic:
```
git clone https://github.com/scotch16/Cmdarr.git
cd cmdarr
docker build \
  --no-cache \
  --pull \
  -t scotch16/cmdarr:latest .
```

## Versions

* **1.04.20** - Initial release