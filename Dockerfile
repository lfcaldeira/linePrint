# Estágio 1: Compilação
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia o projeto e restaura as dependências
COPY . .
RUN dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -o out

# Estágio 2: Imagem final (apenas para manter o contentor leve e extrairmos o ficheiro)
FROM build AS export
COPY --from=build /app/out .