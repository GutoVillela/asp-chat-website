# Asp .NET Realtime Chat Website - English Description
Realtime chat website using ASP .NET and RabbitMQ

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
