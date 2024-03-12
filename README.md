# Teste.Tecnico.Mottu.Matheus.Willock

Este é um projeto de exemplo que inclui várias funcionalidades, como gerenciamento de usuários e motocicletas, autenticação e muito mais.

## Pré-requisitos
Para executar este projeto, você precisará ter o seguinte instalado em seu ambiente de desenvolvimento:
•	.NET 5.0 ou superior
•	SQL Server (ou outro banco de dados suportado pelo Entity Framework Core)

## Como executar o projeto
1.	Clone o repositório para o seu ambiente local usando o comando git clone.
2.	Navegue até a pasta do projeto usando o comando cd.
3.	Restaure os pacotes NuGet usando o comando dotnet restore.
4.	Atualize a string de conexão do banco de dados no arquivo appsettings.json para apontar para o seu banco de dados.
5.	Execute as migrações do banco de dados usando o comando dotnet ef database update.
6.	Inicie o projeto usando o comando dotnet run.

   > Para ajudar com os testes na raiz do projeto há uma collection do endpoints

## Testes
Para executar os testes unitários, navegue até a pasta de testes e execute o comando dotnet test.
