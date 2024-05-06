# Scheduling API

This Coding Exercise was developed using MacOS Sonoma 14.4.1.

### Prerequisites

- **Visual Studio Community**: Install Visual Studio Community.
- **.NET 8.0**: Install version 8.0 of dotnet. The actual version used in this repository was: 8.0.100-rc.2.23502.2
- **Docker**: Install Docker for the connection with SQL Server for DB.
- **Database Management Tool**: Install DB Tool (For this tutorial we will use [DBeaver](https://dbeaver.io/download/) but you should have the same result at the end with any DB Management tool). 

## Installation

1. **DB Installation**

    * Create a file named: "docker-compose.yml" wherever you like, and paste the following code:

```sh
version: '3.8'
services:
    db-mssql:
        image: tx-apps-images-registry:5000/mcr.microsoft.com/mssql/server:2019-CU10-ubuntu-20.04
        ports:
            - "4320:1433"
        volumes:
            - db-mssql-data:/var/opt/mssql
        environment:
            - "ACCEPT_EULA=Y"
            - "SA_PASSWORD=1234567.a"
            - "MSSQL_PID=Express"
volumes:
    db-mssql-data:
```

* Modify the Docker Engine that's in the Docker -> Settings -> Docker Engine by adding the following instruction:

```sh
"insecure-registries": [
    "tx-apps-images-registry:5000"
  ] 
```

After the addition of the above command, then click Apply & Restart.


* Run the docker-compose.yml by typing the following command in the Terminal (make sure to be in the same directory as your docker-compose.yml):

```sh
$ docker-compose up
```

It will create a client ready to hear your connection SQL Server connection.

* Create a new connection in your DB Management Tool (In this case, DBeaver) and select the connection for SQL Server.

* Fill up the fields with the following data:

```sh
Host: localhost
Database/Schema: master
Port: 4320
username: sa
password: 1234567.a
```

It will connect succesfully.

* Create a new DB and run the scripts that are located in ** scheduling-api/Truextend/Scheduling/Data/Data Seeding ** openning a New SQL Script in DBeaver.

2. **Project Installation**

* Verify that the installation was succesfull:

```sh
$ dotnet --version
```

* Clone the repository:
```sh
$ git clone http://192.168.1.225/tx-internal-apps/rewards-api.git
$ cd rewards-api
$ git checkout develop
```

## Running the Project
**From Visual Studio Community**
* Open the **.sln** that's inside of Truextend/Scheduling and Visual Studio Community will deploy. 
* Once Visual Studio Community has been opened run the project using the run button.
