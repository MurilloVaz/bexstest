while true
do
	echo "please enter the route:"
	read route
	readarray -d "-" -t array <<< "$route"
	from="${array[0]}"
	to=$(echo ${array[1]} | tr -d '\r')
	readarray -d " " -t url <<< "http://localhost:44380/Route/from/"$from"/to/"$to"/cheapest"
	response=$(curl -s -X GET $url -H "accept: */*" --insecure)
	totalPrice=$(echo $response | sed -e 's/[{}]/''/g' | awk -v RS=',"' -F: '/^totalPrice/ {print $2}')
	route=$(echo $response | sed -e 's/[{}]/''/g' | awk -v RS=',"' -F: '/^route/ {print $2}' | sed 's/\(^"\|"$\)//g')
	echo "best route: $route > $"$totalPrice
done