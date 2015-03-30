## POI Data Provider Dockerfile

This directory contains a Dockerfile of POI Data Provider GE (https://github.com/Chiru/FIWARE-POIDataProvider.git)


### Requirements

* Install [Docker](https://www.docker.com)


### Usage

The `poi` endpoint is exposed internally on the port 80.
The webservice can be launch and made accessible on the host's port 8080 with the command:

    docker run -p 81:80 -t -i poi_dp:latest

### Docker image's creation

* Use the following command inside the cloned repository:

    docker build -t 'fic2/poi_dp:latest' .
