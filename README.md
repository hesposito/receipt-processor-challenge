# Receipt Processor Challenge

Receipt processor API written in .NET.
API can be accessed at `http://localhost:8080`
Swagger UI located at `http://localhost:8080/swagger/index.html`

## Docker compose
`docker compose up --build`

## Docker CLI
    1. cd into repo directory.
    2. `docker build -t receipt-api-image .`
    3. `docker run -it --rm -p 8080:8080 --name receipt-processor-api receipt-api-image`