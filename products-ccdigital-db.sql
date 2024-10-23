CREATE DATABASE Products;
GO
USE Products;

Create table [Product]
(
   Id  uniqueidentifier primary key not null,
   Code varchar(20) not null,
   Name varchar(200) not null,
   Description nvarchar(500) not null,
   Price DEcimal(18,2) not null,
   Stock int not null,
   Created datetime not null,
   CreatedBy varchar(100) null,
   LastModified datetime null,
   LastModifiedBy varchar(100) null,
   RowVersion  uniqueidentifier not null
)
INSERT INTO Product(Id, Code, Name, Description, Price, Stock, Created, RowVersion)
VALUES ('553e2e51-cf4d-4923-b2e9-e97575cf7a68', 'PROD001', 'Producto 1', 'Descripción producto 1', 1500, 10, GETDATE(), 'a8871de5-976b-48bc-b5dc-6c8f288bfd48')