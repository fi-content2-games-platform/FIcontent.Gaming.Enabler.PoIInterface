FROM ubuntu:12.04

# apt-get stuff
RUN 	apt-get -y update && apt-get -y install --fix-missing && \
	apt-get -y install wget unzip git sudo netcat && \
	apt-get -y install postgresql postgresql-contrib postgis postgresql-9.1-postgis && \
	apt-get -y install mongodb apache2 php5 php5-pgsql php-pear php5-dev gcc make && \
	/etc/init.d/postgresql stop && \
	service mongodb stop

# get GE repo
# cloning an older version until the new one is fixed
RUN	git clone https://github.com/Chiru/FIWARE-POIDataProvider.git && \
	cd FIWARE-POIDataProvider && \
	git reset --hard c8bd33b001dfdc0139693dcce94c75044db278f6 && \
#	cp -R FIWARE-POIDataProvider/php/* /var/www/
	cp -R php/* /var/www/

# mongodb
RUN 	pecl install mongo && \
  	echo "extension=mongo.so" > /etc/php5/conf.d/mongo.ini


# configure postGIS
RUN	/etc/init.d/postgresql start && \
	pg_dropcluster --stop 9.1 main && \
	pg_createcluster --start -e UTF-8 9.1 main && \
 	sudo -u postgres createuser -s gisuser && \
	sudo -u postgres createdb --encoding=UTF8 --owner=gisuser poidatabase && \
	sudo -u postgres psql -d poidatabase -f /usr/share/postgresql/9.1/contrib/postgis-1.5/postgis.sql  && \
	sudo -u postgres psql -d poidatabase -f /usr/share/postgresql/9.1/contrib/postgis-1.5/spatial_ref_sys.sql && \
	sudo -u postgres psql -d poidatabase -f /usr/share/postgresql/9.1/contrib/postgis_comments.sql && \
	sudo -u postgres psql -d poidatabase -c "GRANT SELECT ON spatial_ref_sys TO PUBLIC;" && \
	sudo -u postgres psql -d poidatabase -c "GRANT ALL ON geometry_columns TO gisuser;" && \
	sudo -u postgres psql -d poidatabase -c 'create extension "uuid-ossp";' && \
	/etc/init.d/postgresql stop

ADD	pg_hba.conf /etc/postgresql/9.1/main/pg_hba.conf

RUN 	ln -s /dev/stdout /var/log/mongodb/mongodb.log && \
	mongod --fork --config '/etc/mongodb.conf' && \
	/etc/init.d/postgresql start && \
	cd FIWARE-POIDataProvider/install_scripts && \
	(timeout 100s sh -c 'until nc -z 127.0.0.1 27017; do echo "Waiting for mongodb"; sleep 1.2s; done') || (echo 'Timeout expired'; exit 1) && \
	sudo -u postgres ./create_tables.sh && \
	echo 'Stopping posgresql' && /etc/init.d/postgresql stop && \
	echo 'Stopping mongodb' && service mongodb stop

#get composer package
RUN 	php -r "readfile('https://getcomposer.org/installer');" | php && \
	cp composer.phar /var/www && cd /var/www && \
	php composer.phar require justinrainbow/json-schema:~1.3

# open port 80 for web service
EXPOSE 	80

# run services at start
ADD 	start.sh start.sh
CMD 	sh start.sh
    
