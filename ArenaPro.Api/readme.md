-- SEQ:
docker-compose up 

deve rodar esse comando dentro da caminho da ArenaPro.Api

"--"BANCP DE DADOS:

docker run --name postgres-db -e POSTGRES_USER=bernava -e POSTGRES_PASSWORD=strongpassword1234 -e POSTGRES_DB=arenapro -p 5432:5432 -d postgres:latest



