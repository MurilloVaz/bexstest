curl -F "fileInput=@$1;type=text/plain" https://localhost:44380/Administration/file/input \
	 -H 'Content-Type: multipart/form-data' \
	 -H "accept: */*" \
	 -X 'POST' \
	 --insecure
	 