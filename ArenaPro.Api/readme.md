-- SEQ:
docker run --name seq -d --restart unless-stopped -e ACCEPT_EULA=Y -p 5341:80 datalust/seq:latest

-- BANCO DE DADOS:
docker run --name postgres-db -e POSTGRES_USER=bernava -e POSTGRES_PASSWORD=strongpassword1234 -e POSTGRES_DB=arenapro -p 5432:5432 -d postgres:latest
