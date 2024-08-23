# Project Details

This is a multi-language product. The Database and API were developed using C#, and the console app to parse the CSV was developed using Java.

Frameworks:
- Java: JDK 21
- C#: .Net 8.0

## Database Setup
This project uses Entity Framework for a SQL database. A migration has been provided which will incorporate seed data into the database, as well as a connection string to use.

The connection string is saved in the appsettings.json file for both the Database and the Test projects and follows this format:

```
    "sql_db": "Server=localhost;Database=esg-customer;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
```

Before setting up the database please make sure you have Entity Framework installed on your machine. The version used in this project is v8.0.8

To deploy the migration from visual studio:
- Navigate to the Package Manager Console
- Set both the Default Project in the console window, and the Startup Project for the solution to be the Database Project
- Input the command Update-Database
- The migration should run and setup a database in your localhost called fourteen-fish which has some seed data in the tables

To deploy the migration from a bash or cmd window:
- Navigate to the Database project in your window
- Run the command `dotnet ef database update`
- The migration should run and setup a database in your localhost called fourteen-fish which has some seed data in the tables



## Set Up Localhost Certificate

If the application fails to run on the Java side, then you need to set up localhost to be trusted. A localhost certificate has been provided to use. If this does not work, then generate your own with the command:

```
    openssl s_client -connect localhost:1701 < /dev/null 2>/dev/null | openssl x509 -out localhost.crt
```

1. Locate your Java truststore:

The Java truststore is usually located in the JRE/lib/security directory of your Java installation. For example:

    On Windows: C:\Program Files\Java\jdk-15\jre\lib\security
    On Linux: /usr/lib/jvm/java-15-openjdk/jre/lib/security

2. Identify the truststore file:

The truststore file is usually named cacerts. This file contains the trusted certificates for your Java application.

3. Open a command prompt or terminal:

Open a command prompt or terminal and navigate to the directory where your truststore file is located.

4. Run the keytool command:

Run the following command to import the server's certificate into your truststore:

```
    keytool -importcert -alias <alias> -file <certificate_file> -keystore <truststore_file>
```

Replace:

    <alias> with a unique alias for the certificate (e.g., myservercert)
    <certificate_file> with the path to the server's certificate file (e.g., server.crt)
    <truststore_file> with the path to your truststore file (e.g., cacerts)

Example command:

```
    keytool -importcert -alias myservercert -file server.crt -keystore cacerts
```

5. Enter the truststore password:

You will be prompted to enter the truststore password. The default password is changeit. If you've changed the password, enter the new password.

6. Confirm the certificate import:

You will be asked to confirm the certificate import. Type yes to confirm.

7. Verify the certificate import:

Run the following command to verify that the certificate has been imported successfully:

```
    keytool -list -v -keystore <truststore_file>
```

Replace <truststore_file> with the path to your truststore file (e.g., cacerts).

This command will list all the certificates in your truststore, including the one you just imported.