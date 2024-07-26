-- GRAYLOG:
docker run --link mongo --link elasticsearch \ -p 9000:9000 -p 12201:12201 -p 1514:1514 -p 5555:5555 \ -e GRAYLOG_HTTP_EXTERNAL_URI="http://127.0.0.1:9000/" \ -d graylog/graylog:5.0

Depois de rodar o comando, deve se entrar dentro do container para ver qual senha ele deu para conseguir acessar ele pela URL.

"--"BANCP DE DADOS:

docker run --name postgres-db -e POSTGRES_USER=bernava -e POSTGRES_PASSWORD=strongpassword1234 -e POSTGRES_DB=arenapro -p 5432:5432 -d postgres:latest



