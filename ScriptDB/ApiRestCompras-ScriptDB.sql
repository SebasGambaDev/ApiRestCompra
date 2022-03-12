USE master;  
GO  
CREATE DATABASE  ApiRestCompra
GO

USE ApiRestCompra

CREATE TABLE [dbo].[Compras](
	[Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[ClienteNombre1] [nvarchar](30) NOT NULL,
	[ClienteNombre2] [nvarchar](30) NULL,
	[ClienteApellido1] [nvarchar](30) NOT NULL,
	[ClienteApellido2] [nvarchar](30) NULL,
	[ClienteEmail] [nvarchar](30) NOT NULL,
	[ClienteDireccionDespacho] [nvarchar](100) NOT NULL,
	[CiudadDespacho] [nvarchar](15) NOT NULL,
	[ClienteDireccionFacturacion] [nvarchar](100) NOT NULL,
	[CiudadFacturacion] [nvarchar](15) NOT NULL,
	[ClienteTelefono1] [nvarchar](15) NOT NULL,
	[ClienteTelefono2] [nvarchar](15) NULL,
	[ValorFlete] [decimal](18, 2) NOT NULL,
	[NumeroFactura] [int] NULL,
	[TotalArticulos] [decimal](18, 2) NULL,
	[TotalImpuestosVenta] [decimal](18, 2) NULL,
	[TotalImpuestosFlete] [decimal](18, 2) NULL,
	[TotalImpuestosNetos] [decimal](18, 2) NULL,
	[ValorTotalFactura] [decimal](18, 2) NULL
) 
GO

CREATE TABLE [dbo].[Detalles](
	[Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[CodigoReferencia] [nvarchar](30) NOT NULL,
	[Cantidad] [int] NOT NULL,
	[ValorUnitario] [decimal](18, 2) NOT NULL,
	[Descripcion] [nvarchar](1000) NULL,
	[referencia] [nvarchar](30) NULL,
	[ValorTotal] [decimal](18, 2) NULL,
	[CompraId] [int] NOT NULL
) 
GO

ALTER TABLE [dbo].[Detalles]  WITH CHECK ADD  CONSTRAINT [FK_Detalles_Compras_CompraId] FOREIGN KEY([CompraId])
REFERENCES [dbo].[Compras] ([Id])
ON DELETE CASCADE
GO

CREATE TABLE [dbo].[Logs](
	[Id] [int] PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Message] [nvarchar](max) NULL,
	[MessageTemplate] [nvarchar](max) NULL,
	[Level] [nvarchar](max) NULL,
	[TimeStamp] [datetime] NULL,
	[Exception] [nvarchar](max) NULL,
	[Properties] [nvarchar](max) NULL
)
GO