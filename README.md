# netcore-bank

Este projeto é um desafio técnico, descrição:


Implementar um sistema de controle de conta corrente bancária, processando solicitações de depósito, resgates e pagamentos. 
Um ponto extra seria rentabilizar o dinheiro parado em conta de um dia para o outro como uma conta corrente remunerada.

Como:

Criar um app de uma única página contendo informações do extrato da conta e os botões das ações esperadas (depósitos, retiradas e pagamentos). 
Mostrar também o histórico da conta.


## Stack utilizada

- .Net Core 3.1
- EF Core 3.1
- Mysql
- Angular 10
- Jwt Bearer Token Authentication
- Swagger 
- XUnit + Moq
- Docker

## Requisitos
- Docker
- Git
- Node.js

## Rodar a aplicação

# Backend

O Docker deve estar rodando na máquina.

Clone o projeto e vá até o diretório: /src/api/ e rode o comando abaixo:

- docker-compose up --build

Caso deseje rodar os testes unitarios:
- dotnet test

# Frontend

Já com o projeto clonado, vá até o diretório /src/front/ e rode o comando abaixo:
- npm start


## Acessar a aplicação

Após subir o backend e o front end, a documentação da api ficará disponível em:

Documentação da API: http://localhost:5000/swagger/index.html

API: http://localhost:5000/

App Angular: http://localhost:4200

OBS: dependendo de como esta configurado o docker, o localhost do mesmo poderá ser outro IP, então para acessar a aplicação deverá ir pelo ip do docker.



# Sobre o APP:

Não foi implementado uma tela de cadastro para criar usuarios / contas, apenas login/logout para validar o token. 
Então é necessário criar o primeiro usuario pela api (ex: via swagger / postman). (Ao criar o usuário, será criado automaticamente uma conta para o usuário)
