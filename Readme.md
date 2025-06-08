# WatchTower 

## Descrição do Projeto

O **WatchTower** é um serviço RESTful desenvolvido em .NET para gerenciamento de usuários e endereços. Ele faz parte de uma arquitetura modular, permitindo cadastro, atualização, consulta e remoção de usuários e seus respectivos endereços, com validação de dados e integração com outros serviços via mensageria.

---

## Tecnologias Utilizadas

- [.NET 8](https://dotnet.microsoft.com/)
- ASP.NET Core Web API
- Entity Framework Core
- AutoMapper
- HATEOAS
- Ml.Net
- MongoDB
- MassTransit (mensageria)
- RabbitMQ (mensageria)
- Oracle Database
- Docker (opcional)
- WebSocket

---

## Arquitetura

- **API RESTful** para operações CRUD de usuários, endereços e dispositivos.
- **WebSocket** para ingestão e resposta em tempo real de dados de sensores.
- **Módulo de IA** (ML.NET) para previsão de eventos ambientais.
- **Mensageria** (RabbitMQ/MassTransit) para integração e escalabilidade.
- **Persistência** em OracleDB (relacional) e MongoDB (NoSQL).

## Diagrama da Arquitetura



---

## Como Executar o Projeto

1. **Pré-requisitos**
   - [.NET 8 SDK](https://dotnet.microsoft.com/download)
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
   dotnet run --project UserService & dotnet run --project MlnetService
   ```

---

## Inteligência Artificial e Funcionalidades Avançadas

O **WatchTower** integra um módulo de Inteligência Artificial desenvolvido com ML.NET, responsável por analisar dados de sensores ambientais e prever eventos críticos, como enchentes, tempestades, ondas de calor e incêndios. Essa IA é treinada com um dataset sintético de desastres ambientais, permitindo identificar padrões e antecipar situações de risco em tempo real.

### Como Funciona a IA

- **Entrada de Dados:** Recebe dados de sensores (temperatura, umidade, pressão, vento, chuva, nível de água, gases, luminosidade) via WebSocket.
- **Processamento:** Os dados são normalizados e enviados para o modelo de machine learning, que retorna a previsão do tipo de evento.
- **Geolocalização:** O sistema integra serviços de geocodificação reversa para associar coordenadas geográficas a endereços reais, enriquecendo as informações enviadas ao usuário.

### Exemplo de Fluxo

1. **Sensor envia dados** para o endpoint WebSocket.
2. **IA processa** e prevê o tipo de desastre.
3. **Serviço de geolocalização** converte coordenadas em endereço.
4. **Resposta** é enviada ao cliente com previsão e localização detalhada.

---

## Testes Utilizados

Para facilitar o desenvolvimento e os testes do projeto ML.NET de forma isolada, sem depender das demais partes do sistema, foram utilizados arquivos em JavaScript que simulam o funcionamento da aplicação. Esses arquivos estão disponíveis no seguinte repositório:

`https://github.com/QueijoQualho/Teste-Gs-Dotnet.git`

Esse repositório contém scripts que permitem executar e validar o comportamento do modelo de machine learning separadamente, garantindo maior agilidade e independência durante a fase de testes.

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


### Dispositivos

| Método | Rota                                 | Descrição                           | Body (DTO)           | Resposta                |
|--------|--------------------------------------|-------------------------------------|----------------------|-------------------------|
| POST   | `/api/device`                        | Cria um novo dispositivo            | `CreateDeviceDto`    | 201 Created, Device     |
| GET    | `/api/device/{id}`                   | Busca dispositivo por ID            | -                    | 200 OK, Device          |
| GET    | `/api/device`                        | Lista todos os dispositivos         | -                    | 200 OK, List<Device>    |
| GET    | `/api/device/byUser/{userId}`        | Lista dispositivos de um usuário    | -                    | 200 OK, List<Device>    |
| GET    | `/api/device/tokensByCity/{city}`    | Lista tokens por cidade             | -                    | 200 OK, List<string>    |
| PUT    | `/api/device/{id}`                   | Atualiza dispositivo                | `UpdateDeviceDto`    | 204 No Content          |
| DELETE | `/api/device/{id}`                   | Remove dispositivo                  | -                    | 204 No Content          |

> **Obs:** Consulte os DTOs para detalhes dos campos obrigatórios e exemplos de payload.

---

### Exemplo de Payload WebSocket

**Envio:**
```json
{
  "sensor": {
    "temperatura": 28.5,
    "umidade": 80,
    "pressao": 1012,
    "vento": 12,
    "chuva": 10,
    "nivelAgua": 2.5,
    "gases": 0.03,
    "luminosidade": 500
  },
  "localizacao": {
    "latitude": -23.5505,
    "longitude": -46.6333
  }
}
```

### Resposta: 
```json
{
  "prediction": "Enchente",
  "lat": -23.5505,
  "log": -46.6333,
}
```