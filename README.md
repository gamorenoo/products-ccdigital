# products-ccdigital
Prueba técnica ccdigital


# products-ccdigital 

Este proyecto es una API de gestión de productos en al cual se pueden realizar operaciones CRUD (crear, editar, eliminar y obtener información sobre usuarios).

## 2. Solución

Se toma la decisión de trabajar la solución siguiendo los principios de Clean Architecture en .NET 8. 
El principal objetivo de esta arquitectura es la capacidad de separar el código en sus diferentes responsabilidades, hacer el código mucho más entendible en el presente y futuro, testeable y fácil de integrar.


## 2.1 Tecnologías
* NET 8 / C#
* ASP.NET Core 8
* Sql Server
* Entity Framework Core 8
* MediatR
* CQRS
* AutoMapper
* Visual studio Comunity 2022

## Requisitos Previos
Asegúrate de sql server instalado para el Back
Node y Angular cli para el Front

## Instrucciones para Ejecutar APi (Carpeta BackEnd)

### Paso 1: Configurar la Base de Datos
1. Eejutar el script contenido en el archivo llamdo ###products-ccdigital-db.sql para crear la base de datos del proyecto y la tabla de productos.

### Paso 2: Ejecucion del proyecto
1. asegruarse de que el appseting del api está correctamente configurado para la conetarme a sql server
2. Una vez se tenga la apicación en visual studio y haya sido ejecutada, si todo va bien mostrará el swagger en el navegador


## Instrucciones para Ejecutar FRONT (Carpeta FrontEnd)

Este proyecto hace uso del Api para realizar las operaciones CRUD mencionadas anterioemente

### Requisitos Previos

1. Asegúrate de tener Node.js y npm instalados en tu máquina.

### Configuración y Ejecución del Frontend (React)

1. Navega a la raíz del proyecto.

2. Ejecuta el siguiente comando para instalar las dependencias del frontend:

    ```bash
    npm install
    ```

3. Luego, ejecuta el siguiente comando para iniciar la aplicación React:

    ```bash
    npm start
    ```

4. Abre tu navegador y accede a `http://localhost:4200` para ver la aplicación en acción.


## Características del Proyecto
- **Operaciones CRUD de Productos:** La aplicación permite crear, leer productos a través de la API.


