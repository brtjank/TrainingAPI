# TrainingAPI

## General info

The *TrainingAPI* is a simple REST API designed for personal trainers. It uses the SQLite database containing information about trainees and training, and it is intended to help managing them. *TrainingAPI* is documented by Swagger UI.

## Technologies

Project was created with:
* ASP .NET Core 7.0.2
* Entity Framework 7.0.2

## Requirements

The list of tools required to build and run the project:
* .NET SDK 7.0.x+

## Building

In order to create/update the database using migration files stored in the project, use the following command:

```
$ dotnet ef database update
```

Database is stored in the TrainingDatabase.db file.

In order to build the project use the following command:

```
$ dotnet build
```

## Running

In order to run the project use the following command:

```
$ dotnet run
```

You can access the API documented by Swagger UI using the right port on localhost and adding */swagger* at the end of the URL.

## License

Project is licensed under the [MIT](../master/LICENSE) license.
