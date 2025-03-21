# Bank Account
Esta aplicação consiste em dois módulos principais: **Bank Account** e **Transactions**. A comunicação entre esses módulos é feita via HTTP, mantendo-os desacoplados.

- Implementei todas as operações de busca, criação, edição e bloqueio de contas bancárias.
As transações foram implementadas com validações para crédito e débito, respeitando o status da conta.
- A busca de transações permite filtros por data, tipo e contraparte, com ordenação do mais recente para o mais antigo.
- Validei todos os formatos exigidos: número de agência, número de conta, nomes, e-mails e valores monetários.

- Garanti a unicidade do número da conta, independente de agência ou tipo.
A lógica de status das contas (ACTIVE, BLOCKED, FINISHED) foi aplicada corretamente.
Implementei as restrições para que uma pessoa não possa ter mais de uma conta do mesmo tipo.
Os tipos de conta e transação foram atendidos.


## Como rodar o projeto🚦
1. Clone o repositório:
   ```bash
   git clone https://github.com/CharlesMeloBC/System-Bank-API
   cd System-Bank-API
   ```

## Tecnologias Utilizadas

- C# .NET 9.0
- EntityFrameworkCore 9.0.3
- EntityFrameworkCore Tools 9.0.3
- EntityFrameworkCore Design 9.0.3
- EntityFrameworkCore SQL 9.0.3
- AutoMapper 12.0.01
- AutoMapper.Extensions.Microsoft.DependencyInjection 12.0.1
- FluentValidation 11.11.0
- SQL Server 
- HTTP para comunicação entre módulos


## Configure AccountBank e Transactions de forma individual ⚙️

### Instalação e Execução

1. Clone o repositório:
   ```bash
   git clone https://github.com/seu-usuario/seu-repositorio.git
   cd seu-repositorio
   ```
2. Configure o banco de dados no arquivo `appsettings.json`.
    ```json
    "ConnectionStrings": {
    "ConnectionDefault": "SERVER=SEU BANCO; DATABASE=SUA TABELA; TrustServerCertificate=True; Trusted_Connection=True;"}
    ```
3. Execute as migrações:
   ```bash
   dotnet ef migrations add MigrationsInicial
   dotnet ef database update
   ```
4. ⚠️Confiugure a rota do BankAccount no modulo Transactions para fazer a comunicação http entre as aplicações ⚠️
    ```
    System-Bank-API\Transactions\Domain\Services\BankAccountService.cs
    ```

   ```csharp
    private readonly string _bankAccountApiUrl = "https://localhost:"; 
    // Substitua URL 
   ```

5. Rode a aplicação:
   ```bash
   dotnet run
   ```

## Estrutura do Projeto 📂

        /seu-projeto
        ├── BankAccount
        │   ├── Controllers
        │   ├── Data
        │   ├── Domain
        │   │   ├── DTOs
        │   │   ├── Enums
        │   │   ├── Interfaces
        │   │   ├── Models            
        │   │   ├── Services
        │   │   └── Validators
        │   └── Mappings

        ├──Transactions
        │   ├── Controllers
        │   ├── Data
        │   ├── Domain
        │   │   ├── DTOs
        │   │   ├── Enums
        │   │   ├── Interfaces
        │   │   ├── Models            
        │   │   ├── Services
        │   │   └── Validators
        │   └── Mappings
        └── README.md


---


## Modelo de Arquitetura
![Arquitetura](../System-Bank-API/images/Arquitetura.png)

# Módulo: Bank Account 👥

Gerencia as contas bancárias dos usuários.

- **Endpoint:** `POST /Account`
- **Exemplo de Requisição:**
  ```json
  {
      "HolderName": "Barry Allen",
      "HolderEmail": "barry.allen@exemple.com",
      "HolderDocuments": "12345678910",
      "HolderType": "LEGAL",
      "TypeAccount": "PAYMENT"
  }
  ```
- **Resposta:**
  ```json
    {
	"id": 1005,
	"branch": "22523",
	"numberAccount": "76230771",
	"bankName": "DelFinance",
	"holderName": "Barry Allen",
	"holderEmail": "barry.allen@exemple.com",
	"holderDocuments": "12345678910",
	"holderType": "LEGAL",
	"typeAccount": "PAYMENT",
	"codeBank": "435",
	"status": "ACTIVE",
	"createdAt": "2025-03-21T02:40:08.2121927-03:00",
	"updatedAt": null
    }
  ```

- **Endpoint:** `GET /Account`
- **Exemplo de Requisição:**
- **Formas de busca**

    ```
        https://localhost:####/Account <- Todas as contas 
        https://localhost:####/Account/{1005} <-Id
        https://localhost:####/Account/number/{76230771}     
        https://localhost:####/Account/branch/{22523}       
        https://localhost:####/Account/holder/{Barry Allen}       
    ```
- **Resposta:**
  ```json
    {
	"id": 1005,
	"branch": "22523",
	"numberAccount": "76230771",
	"bankName": "DelFinance",
	"holderName": "Barry Allen",
	"holderEmail": "barry.allen@exemple.com",
	"holderDocuments": "12345678910",
	"holderType": "LEGAL",
	"typeAccount": "PAYMENT",
	"codeBank": "435",
	"status": "ACTIVE",
	"createdAt": "2025-03-21T02:40:08.2121927-03:00",
	"updatedAt": null
    }
  ```
- **Endpoint:** `PUT /Account/update-email/{id}`
- **Exemplo de Requisição:**
    ```json
    {"NewEmail": "flash@justiceLeague.com"}
    ```
- **Resposta**
    ```
    E-mail atualizado com sucesso.
    ```

- **Endpoint:** `PUT /Account/update-status/{id}`
- **Exemplo de Requisição:**
    ```json
    {"Status":"BLOCKED"} 
    // Possíveis status {"ACTIVE, BLOCKED, FINISHED"} 
    ```
    *futuramente implementar um jwt para essa rota*
- **Resposta**
    ```
    Status atualizado com sucesso.
    ```

# Módulo: Transactions

Gerencia as transações entre contas.

- **Endpoint:** `PUT /api/Transaction` 💸
- **Exemplo de Requisição:**
  ```json
    {
    "BankAccountId": 1007,
    "Amount": 500.00,
    "TransactionType": "CREDIT",
    "CounterpartyBankCode": "435",
    "CounterpartyBankName": "DelFinance",
    "CounterpartyBranch": "80283",
    "CounterpartyAccountNumber": "86513544",
    "CounterpartyHolderName": "Jesse Pinkman",
    "CounterpartyHolderType": "NATURAL",
    "CounterpartyAccountType": "SALARY",
    "CounterpartyHolderDocument": "45678901233"
    }
  ```
- **Resposta:**
  ```json
    {
	"id": 15,
	"transactionType": "CREDIT",
	"amount": 500,
	"bankAccountId": 1007,
	"counterpartyBankCode": "435",
	"counterpartyBankName": "DelFinance",
	"counterpartyBranch": "80283",
	"counterpartyAccountNumber": "86513544",
	"counterpartyAccountType": "SALARY",
	"counterpartyHolderName": "Jesse Pinkman",
	"counterpartyHolderType": "NATURAL",
	"counterpartyHolderDocument": "45678901233",
	"createdAt": "2025-03-21T04:11:49.7732498-03:00"
    }
  ```


- **Endpoint:** `GET /api/Transaction/{id}` 💸
- **Exemplo de Requisição:**
- **Formas de busca**

    ```
        https://localhost:####/Transaction/{15} <-Id 
        https://localhost:####/Transaction/account/{15} <- Todas as transações
        https://localhost:####/Transaction/counterparty/bank/{435}     
        https://localhost:####/Transaction/counterparty/account/{86513544}       
    ```       
- **Resposta:**

    ```json
    {
     "id": 15,
     "transactionType": "CREDIT",
     "amount": 500,
     "bankAccountId": 1007,
     "counterpartyBankCode": "435",
     "counterpartyBankName": "DelFinance",
     "counterpartyBranch": "80283",
     "counterpartyAccountNumber": "86513544",
     "counterpartyAccountType": "SALARY",
     "counterpartyHolderName": "Jesse Pinkman",
     "counterpartyHolderType": "NATURAL",
     "counterpartyHolderDocument": "45678901233",
     "createdAt": "2025-03-21T04:11:49.7732498"
    }
    ```

### Creditos
*Desenvolvida por: Charles Melo Bispo de Carvalho*