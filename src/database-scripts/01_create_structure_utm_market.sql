/*
================================================================================
SCRIPT: 01_create_structure_utm_market.sql
AUTOR: Database Architect Senior (Gemini CLI)
FECHA: 2026-02-18
MOTOR: Microsoft SQL Server 2022 Express
OBJETIVO: Definir la estructura base para el sistema UTM Market.
================================================================================
*/

USE [develop_courses_javerage];
GO

SET NOCOUNT ON;
SET XACT_ABORT ON;

-- =============================================================================
-- 1. TABLA: Producto
-- Descripción: Almacena el catálogo de productos disponibles en el inventario.
-- =============================================================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Producto' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    CREATE TABLE dbo.Producto (
        ProductoID INT IDENTITY(1,1) NOT NULL,
        Nombre NVARCHAR(100) NOT NULL,
        SKU VARCHAR(20) NOT NULL,
        Marca NVARCHAR(50) NULL,
        Precio DECIMAL(19,4) NOT NULL,
        Stock INT NOT NULL,
        
        -- Restricciones de Integridad
        CONSTRAINT PK_Producto PRIMARY KEY CLUSTERED (ProductoID),
        CONSTRAINT UQ_Producto_SKU UNIQUE (SKU),
        CONSTRAINT CK_Producto_Precio_NoNegativo CHECK (Precio >= 0),
        CONSTRAINT CK_Producto_Stock_NoNegativo CHECK (Stock >= 0)
    );

    PRINT 'Tabla [dbo].[Producto] creada exitosamente.';
END
ELSE
BEGIN
    PRINT 'La tabla [dbo].[Producto] ya existe.';
END
GO

-- =============================================================================
-- 2. TABLA: Venta
-- Descripción: Registro maestro de transacciones de venta.
-- Estatus: 1 = Pendiente, 2 = Completada, 3 = Cancelada.
-- =============================================================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Venta' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    CREATE TABLE dbo.Venta (
        VentaID INT IDENTITY(1,1) NOT NULL,
        Folio VARCHAR(20) NOT NULL,
        FechaVenta DATETIME NOT NULL DEFAULT GETDATE(),
        TotalArticulos INT NOT NULL,
        TotalVenta DECIMAL(19,4) NOT NULL,
        Estatus TINYINT NOT NULL, -- Ver restricción CK_Venta_Estatus
        
        -- Restricciones de Integridad
        CONSTRAINT PK_Venta PRIMARY KEY CLUSTERED (VentaID),
        CONSTRAINT UQ_Venta_Folio UNIQUE (Folio),
        CONSTRAINT CK_Venta_Estatus CHECK (Estatus IN (1, 2, 3)),
        CONSTRAINT CK_Venta_TotalVenta_NoNegativo CHECK (TotalVenta >= 0),
        CONSTRAINT CK_Venta_TotalArticulos_NoNegativo CHECK (TotalArticulos >= 0)
    );

    PRINT 'Tabla [dbo].[Venta] creada exitosamente.';
END
ELSE
BEGIN
    PRINT 'La tabla [dbo].[Venta] ya existe.';
END
GO

-- =============================================================================
-- 3. TABLA: DetalleVenta
-- Descripción: Desglose de artículos por cada venta realizada.
-- Relaciones: N Detalles -> 1 Venta, N Detalles -> 1 Producto.
-- =============================================================================
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'DetalleVenta' AND schema_id = SCHEMA_ID('dbo'))
BEGIN
    CREATE TABLE dbo.DetalleVenta (
        DetalleID INT IDENTITY(1,1) NOT NULL,
        VentaID INT NOT NULL,
        ProductoID INT NOT NULL,
        PrecioUnitario DECIMAL(19,4) NOT NULL,
        Cantidad INT NOT NULL,
        TotalDetalle DECIMAL(19,4) NOT NULL,
        
        -- Restricciones de Integridad y Claves Primarias
        CONSTRAINT PK_DetalleVenta PRIMARY KEY CLUSTERED (DetalleID),
        CONSTRAINT CK_DetalleVenta_PrecioUnitario_NoNegativo CHECK (PrecioUnitario >= 0),
        CONSTRAINT CK_DetalleVenta_Cantidad_Positiva CHECK (Cantidad > 0),
        
        -- Relaciones Externas (Foreign Keys)
        CONSTRAINT FK_DetalleVenta_Venta FOREIGN KEY (VentaID) 
            REFERENCES dbo.Venta (VentaID) ON DELETE CASCADE,
        
        CONSTRAINT FK_DetalleVenta_Producto FOREIGN KEY (ProductoID) 
            REFERENCES dbo.Producto (ProductoID)
    );

    PRINT 'Tabla [dbo].[DetalleVenta] creada exitosamente.';
END
ELSE
BEGIN
    PRINT 'La tabla [dbo].[DetalleVenta] ya existe.';
END
GO
