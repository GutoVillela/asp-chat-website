# Asp .NET Realtime Chat Website - English Description
Realtime chat website using ASP .NET and RabbitMQ

![CHAT](https://user-images.githubusercontent.com/32982475/156447659-d0fef3fc-f375-4bd3-801b-c9fca76120eb.gif)


## Overview
Simple browser-based realtime chat application using .NET, RabbitMQ, SignalR. 

## Prerequisites
You'll need an instance of [RabbitMQ](https://www.rabbitmq.com/download.html), [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads), [Visual Studio](https://visualstudio.microsoft.com/pt-br/downloads/) or other code editor you'd like to use.

## Instalation
Clone this repository running the following code in the selected folder:

```bash
git clone https://github.com/GutoVillela/asp-chat-website.git
```

Find the file "appsettings.json" and configure the appropriate Connection String and RabbitMQ settings:

![APP_SETTINGS](https://user-images.githubusercontent.com/32982475/156421018-33209161-9438-4ec8-b446-f89820123bb6.jpg)

![APPS](https://user-images.githubusercontent.com/32982475/156421247-4a2a9d8a-71c0-4915-86ea-bfecf53f3e3a.jpg)

If you have [SQLServerLocalDB](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver15) installed and RabbitMQ installed as default you can leave the file appsetting.json as it is.

When your appsettings file is configured:

### If you are using Visual Studio

Go to the Package Manager Console, select the project "Infrastructure" and run the following command:

```bash
Update-Database
```

![PackageManagerConsole](https://user-images.githubusercontent.com/32982475/156422159-ee289cb8-5dfe-4f01-b9bb-da24fa5ef38c.jpg)

### If you are using other code editor

If you don't have EF Tools enabled as default run the following command in the bash console:

```bash
dotnet tool install --global dotnet-ef
```

And then update the database using the following command:

```bash
dotnet ef database update
```

That's it, you are good to go! =D

Have fun!

# Asp .NET Realtime Chat Website - Descrição em Português
Chat em tempo real feito em ASP .NET Core com RabbitMQ.

![CHAT](https://user-images.githubusercontent.com/32982475/156447659-d0fef3fc-f375-4bd3-801b-c9fca76120eb.gif)

## Resumo
Aplicação de chat para navegador feito com .NET, RabbitMQ e SignalR.

## Pré-requisitos
Você vai precisar instalar o [RabbitMQ](https://www.rabbitmq.com/download.html), [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads), [Visual Studio](https://visualstudio.microsoft.com/pt-br/downloads/) ou algum editor de código da sua preferência.

## Instalação
Clone este repositório executando o seguinte comando dentro da pasta de escolha:

```bash
git clone https://github.com/GutoVillela/asp-chat-website.git
```

Encontre o arquivo "appsettings.json" e configure sua string de conexão com o banco de dados SQL e o seu servidor do RabbitMQ.

![APP_SETTINGS](https://user-images.githubusercontent.com/32982475/156421018-33209161-9438-4ec8-b446-f89820123bb6.jpg)

![APPS](https://user-images.githubusercontent.com/32982475/156421247-4a2a9d8a-71c0-4915-86ea-bfecf53f3e3a.jpg)

Se você tem o [SQLServerLocalDB](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver15) instalado na sua máquina e o RabbitMQ com as configurações padrões você pode deixar o arquivo appsetting.json como está.

Quando terminar de configurar seu arquivo appsettings:

### Se você está usando Visual Studio

Abra seu Console do Gerenciador de Pacotes, selecione o projeto "Infrastructure" e rode o seguinte comando:

```bash
Update-Database
```

![PackageManagerConsole](https://user-images.githubusercontent.com/32982475/156422159-ee289cb8-5dfe-4f01-b9bb-da24fa5ef38c.jpg)

### Se você está usando qualquer outro editor de texto

Se você não tem o EF Tolls instalado e habilitado no seu ambiente execute o seguinte comando na pasta raiz do projeto:

```bash
dotnet tool install --global dotnet-ef
```

Depois execute o seguinte comando para criar o banco de dados:

```bash
dotnet ef database update
```

Pronto, tudo no jeito! =D

Divirta-se!
