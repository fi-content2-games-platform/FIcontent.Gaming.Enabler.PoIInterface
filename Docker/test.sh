#!/bin/sh

HOST="localhost"
PORT="81"

echo "Entering FIC2Lab smoke test sequence. Vendor's validation procedure of the POI Data Provider GE engaged. Target host: $HOST:$PORT"

# run a radial search on the webservice
RETCODE=$(curl --max-time 5 -o /dev/null -sw '%{http_code}' "$HOST:$PORT/radial_search.php?lat=0&lon=0")

# get return code of command: 200 is ok, anything else is an error
if [ "$RETCODE" = "200" ]; then
	echo "Curl command for POI GE Data Provider is OK."
else
	echo "Curl command for POI GE Data Provider failed. Validation procedure terminated."
	echo "Debug information: HTTP code $RETCODE instead of expected 200 from SHOST"
	exit 1;
fi

echo "Smoke on the water and fire in the sky. Vendor component validation procedure succeeded. Over."
