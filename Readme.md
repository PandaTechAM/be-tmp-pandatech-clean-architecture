﻿# Pandatech.CleanArchitecture - Monolith Application Using Clean Architecture

## Introduction

This project serves as a template for creating .NET Web API applications. It's pre-configured with essential
dependencies and a Docker setup for ease of development and deployment. The template is designed to be flexible,
allowing easy removal of unnecessary components and addition of new features. Template uses custom authentication and
authorization.

## Project Structure

**Root Directory:** Contains the solution file for the project.

**src Folder:** Houses all the source code

**tests Folder:** Contains all the test projects

**Docker File** Located at the root, used for creating a Docker container for the application. `Dockerfile.Local`
and `docker-compose` are for local development.

## External Dependencies

This project integrates various services and configurations:

**Postgres:** Used as the primary database.

**Redis:** Implemented for caching purposes.

**RabbitMQ (RMQ):** Utilized for messaging and event-driven architecture.

**Elasticsearch:** Employed for logging advanced search capabilities.

**appsettings{environment}.json:** Configuration settings for the application.

## Getting Started

### Pre-requisites:

- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/products/docker-desktop)

### Basic Commands

- `dotnet restore` - Restore dependencies
- `dotnet build` - Build the project
- `dotnet run` - Run the project
- `dotnet test` - Run tests
- `docker-compose up` - Run the project in Docker

## Features

List the key features of your application.