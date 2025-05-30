# WatchTower 

## Descrição do Projeto

O **WatchTower** é um serviço RESTful desenvolvido em .NET para gerenciamento de usuários e endereços. Ele faz parte de uma arquitetura modular, permitindo cadastro, atualização, consulta e remoção de usuários e seus respectivos endereços, com validação de dados e integração com outros serviços via mensageria.

---

## Tecnologias Utilizadas

- [.NET 8](https://dotnet.microsoft.com/)
- ASP.NET Core Web API
- Entity Framework Core
- AutoMapper
- MassTransit (mensageria)
- RabbitMQ (mensageria)
- SQL Server (ou outro banco relacional)
- Docker (opcional)
- WebSocket

---

## Como Executar o Projeto

1. **Pré-requisitos**
   - [.NET 8 SDK](https://dotnet.microsoft.com/download)
   - [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads) ou outro banco relacional
   - [RabbitMQ](https://www.rabbitmq.com/download.html) (opcional, para mensageria)
   - Docker (opcional)

2. **Clone o repositório**
   ```bash
   git clone https://github.com/Monty-Aletheia/GS-dotNet.git
   cd WatchTower
   ```

3. **Configure o appsettings.json**
   - Ajuste as strings de conexão do banco de dados e RabbitMQ conforme necessário.

4. **Rode as migrações do banco**
   ```bash
   dotnet ef database update --project UserService
   ```

5. **Execute o projeto**
   ```bash
   dotnet run --project UserService
   ```

6. **Acesse a API**
   - Por padrão, a API estará disponível em: `http://localhost:5000/api`

---

## Documentação dos Endpoints

### Usuários

| Método | Rota                        | Descrição                        | Body (DTO)                  | Resposta             |
|--------|-----------------------------|----------------------------------|-----------------------------|----------------------|
| POST   | `/api/user`                 | Cria um novo usuário             | `CreateUserDto`             | 201 Created, User    |
| POST   | `/api/user/withAddress`     | Cria usuário com endereço        | `CreateUserWithAddressDto`  | 201 Created, User    |
| GET    | `/api/user/{id}`            | Busca usuário por ID             | -                           | 200 OK, User         |
| GET    | `/api/user`                 | Lista todos os usuários          | -                           | 200 OK, List<User>   |
| PUT    | `/api/user/{id}`            | Atualiza usuário                 | `UpdateUserDto`             | 204 No Content       |
| DELETE | `/api/user/{id}`            | Remove usuário                   | -                           | 204 No Content       |

---

### Endereços

| Método | Rota                        | Descrição                        | Body (DTO)                  | Resposta             |
|--------|-----------------------------|----------------------------------|-----------------------------|----------------------|
| POST   | `/api/address`              | Cria um novo endereço            | `CreateAddressDto`          | 201 Created, Address |
| GET    | `/api/address/{id}`         | Busca endereço por ID            | -                           | 200 OK, Address      |
| GET    | `/api/address`              | Lista todos os endereços         | -                           | 200 OK, List<Address>|
| PUT    | `/api/address/{id}`         | Atualiza endereço                | `UpdateAddressDto`          | 204 No Content       |
| DELETE | `/api/address/{id}`         | Remove endereço                  | -                           | 204 No Content       |

> **Obs:** Consulte os DTOs para detalhes dos campos obrigatórios e exemplos de payload.

---