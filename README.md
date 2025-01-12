# Examen API C#

Este repositorio muestra un ejemplo de una API sencilla sobre una tienda.


## Descripción

Este proyecto es una Web API, usando REST API para las peticiones CRUD.


## Requisitos

- .NET SDK (versión 8.0 o superior)


## Instrucciones de Instalación

1. **Clona el repositorio:**
   ```sh
   git clone https://github.com/ManuFinn/ribbitTest.git

2. **Restaurar las dependencias**
   ```sh
   dotnet restore

3. **Compila y ejecuta el proyecto**
   ```sh
   dotnet run


## Base de Datos.

La Web API hace uso de SQLite para el almacenamiento de los datos de la API, por defecto viene la BD con datos predefenidos para su uso de muestra.

Para crear una nueva migración, puede usar el siguiente comando:
   ```sh
   dotnet ef migrations add InitialCreate
   ```
Si presenta el mensaje de error de "no proyecto seleccionado", intente colocar ``` --project nombreProjecto ``` al final del comando antes mostrado.

En caso de que se realizen cambios, puede actualizar con el siguiente comando:
   ```sh
   dotnet ef database update 
   ```
Si presenta el mensaje de error de "no proyecto seleccionado", intente colocar ``` --project nombreProjecto ``` al final del comando antes mostrado.


## Problemas con la instalación.

En caso de presentar problemas con la ejecución, vuelva a restaurar las dependencias. Si el problema persiste, pruebe reconstruir la solución.
   ```
   dotnet clean
   dotnet build
   ```

## Ejemplos de peticiones

La API REST actualmente presenta un CRUD completo, estas son las peticiones:

**Método**: GET

Peticion GET para obtener todos los Productos en la Base de Datos.
**URL**: `/api/Productos`

```http
GET /api/Productos HTTP/1.1
Host: localhost:1111/swagger/index.html
Accept: application/json
```
El resultado seria el siguiente:

```json
{
  "id": 1,
  "nombre": "Producto Generico",
  "descripcion": "Descripcion del Producto generico",
  "precio": 64,
  "stock": 32,
  "fechaCreacion": "2025-01-12T14:29:27"
}
```

**Método**: GET

Petición GET para obtener un Producto especifico por su Id en la Base de Datos.
**URL**: `/api/Productos/id`
```http
GET /api/Productos/{id} HTTP/1.1
Host: localhost:1111/swagger/index.html
Accept: application/json
```
El resultado seria el siguiente:

```json
{
  "id": 1,
  "nombre": "Producto Generico",
  "descripcion": "Descripcion del Producto generico",
  "precio": 64,
  "stock": 32,
  "fechaCreacion": "2025-01-12T14:29:27"
}
```

**Método**: POST

Petición POST para crear un nuevo registro Producto en la Base de Datos. 
**URL**: `/api/Productos`
```http
POST /api/Productos HTTP/1.1
Host: localhost:1111/swagger/index.html
Content-Type: application/json
{
  "nombre": "Producto Generico 1",
  "descripcion": "Descripcion del Producto generico 1",
  "precio": 64,
  "stock": 32,
}
```
El resultado deberia ser OK (200)

**Método**: PUT

Petición PUT para editar un registro Producto ya existente en la Base de Datos. 
**URL**: `/api/Productos/id`
```http
PUT /api/Productos/{id} HTTP/1.1
Host: localhost:1111/swagger/index.html
Content-Type: application/json
{
  "nombre": "Producto Generico 2",
  "descripcion": "Descripcion del Producto generico 2",
  "precio": 64,
  "stock": 32,
}
```
El resultado deberia ser OK (200)

**Método**: DELETE

Petición DELETE para eliminar un registro Producto especifico por su Id en la Base de Datos.
**URL**: `/api/Productos/id`
```http
DELETE /api/Productos/{id} HTTP/1.1
Host: localhost:1111/swagger/index.html
Accept: application/json
```
El resultado deberia ser OK (200)
