
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/13/2017 03:25:19
-- Generated from EDMX file: C:\Users\abdulhamid\Desktop\University\Level3\Semester2\Others\MaerskLineDDAC-master\MaerskLineDDAC\Models\CustomerBookCargoModel.edmx
-- --------------------------------------------------
CREATE DATABASE MaerskLine;
GO

SET QUOTED_IDENTIFIER OFF;
GO
USE [MaerskLine];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Book_Cargos]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Book] DROP CONSTRAINT [FK_Book_Cargos];
GO
IF OBJECT_ID(N'[dbo].[FK_Book_Ships]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Book] DROP CONSTRAINT [FK_Book_Ships];
GO
IF OBJECT_ID(N'[dbo].[FK_Book_Warehouses]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Book] DROP CONSTRAINT [FK_Book_Warehouses];
GO
IF OBJECT_ID(N'[dbo].[FK_Cargos_Customers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Cargos] DROP CONSTRAINT [FK_Cargos_Customers];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Book]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Book];
GO
IF OBJECT_ID(N'[dbo].[Cargos]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Cargos];
GO
IF OBJECT_ID(N'[dbo].[Customers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customers];
GO
IF OBJECT_ID(N'[dbo].[Ships]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Ships];
GO
IF OBJECT_ID(N'[dbo].[Warehouses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Warehouses];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Books'
CREATE TABLE [dbo].[Books] (
    [BookId] int IDENTITY(1,1) NOT NULL,
    [Agent] varchar(25)  NOT NULL,
    [Cargo] int  NOT NULL,
    [Ship] int  NOT NULL,
    [Warehouse] int  NOT NULL
);
GO

-- Creating table 'Cargos'
CREATE TABLE [dbo].[Cargos] (
    [CargoId] int IDENTITY(1,1) NOT NULL,
    [CargoName] varchar(25)  NOT NULL,
    [CargoLength] float  NULL,
    [CargoWidth] float  NULL,
    [CargoHeight] float  NULL,
    [CargoWeight] float  NULL,
    [CargoStatus] varchar(25)  NULL,
    [Customer] int  NOT NULL
);
GO

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [CustomerId] int IDENTITY(1,1) NOT NULL,
    [CustomerName] varchar(15)  NOT NULL,
    [CustomerContact] varchar(15)  NULL,
    [CustomerAddress] varchar(60)  NULL
);
GO

-- Creating table 'Ships'
CREATE TABLE [dbo].[Ships] (
    [ShipId] int IDENTITY(1,1) NOT NULL,
    [ShippedDate] datetime  NULL,
    [ShipName] nvarchar(40)  NOT NULL,
    [ShipAddress] varchar(200)  NOT NULL
);
GO

-- Creating table 'Warehouses'
CREATE TABLE [dbo].[Warehouses] (
    [WarehouseId] int IDENTITY(1,1) NOT NULL,
    [WarehouseName] nvarchar(40)  NOT NULL,
    [WarehouseAddress] nvarchar(60)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [BookId] in table 'Books'
ALTER TABLE [dbo].[Books]
ADD CONSTRAINT [PK_Books]
    PRIMARY KEY CLUSTERED ([BookId] ASC);
GO

-- Creating primary key on [CargoId] in table 'Cargos'
ALTER TABLE [dbo].[Cargos]
ADD CONSTRAINT [PK_Cargos]
    PRIMARY KEY CLUSTERED ([CargoId] ASC);
GO

-- Creating primary key on [CustomerId] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([CustomerId] ASC);
GO

-- Creating primary key on [ShipId] in table 'Ships'
ALTER TABLE [dbo].[Ships]
ADD CONSTRAINT [PK_Ships]
    PRIMARY KEY CLUSTERED ([ShipId] ASC);
GO

-- Creating primary key on [WarehouseId] in table 'Warehouses'
ALTER TABLE [dbo].[Warehouses]
ADD CONSTRAINT [PK_Warehouses]
    PRIMARY KEY CLUSTERED ([WarehouseId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Cargo] in table 'Books'
ALTER TABLE [dbo].[Books]
ADD CONSTRAINT [FK_Book_Cargos]
    FOREIGN KEY ([Cargo])
    REFERENCES [dbo].[Cargos]
        ([CargoId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Book_Cargos'
CREATE INDEX [IX_FK_Book_Cargos]
ON [dbo].[Books]
    ([Cargo]);
GO

-- Creating foreign key on [Ship] in table 'Books'
ALTER TABLE [dbo].[Books]
ADD CONSTRAINT [FK_Book_Ships]
    FOREIGN KEY ([Ship])
    REFERENCES [dbo].[Ships]
        ([ShipId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Book_Ships'
CREATE INDEX [IX_FK_Book_Ships]
ON [dbo].[Books]
    ([Ship]);
GO

-- Creating foreign key on [Warehouse] in table 'Books'
ALTER TABLE [dbo].[Books]
ADD CONSTRAINT [FK_Book_Warehouses]
    FOREIGN KEY ([Warehouse])
    REFERENCES [dbo].[Warehouses]
        ([WarehouseId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Book_Warehouses'
CREATE INDEX [IX_FK_Book_Warehouses]
ON [dbo].[Books]
    ([Warehouse]);
GO

-- Creating foreign key on [Customer] in table 'Cargos'
ALTER TABLE [dbo].[Cargos]
ADD CONSTRAINT [FK_Cargos_Customers]
    FOREIGN KEY ([Customer])
    REFERENCES [dbo].[Customers]
        ([CustomerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Cargos_Customers'
CREATE INDEX [IX_FK_Cargos_Customers]
ON [dbo].[Cargos]
    ([Customer]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------