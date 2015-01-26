#!/bin/sh

get_buster_server_pid(){
    echo `ps aux|grep buster-server|grep node|awk '{ print $2 }'`
}

buster-server -c &
sleep 2
buster-test

server_pid=`get_buster_server_pid`

echo "Killing server at $server_pid"
kill $server_pid
