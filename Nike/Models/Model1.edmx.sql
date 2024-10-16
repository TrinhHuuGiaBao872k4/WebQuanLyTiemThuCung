
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 07/08/2024 22:34:09
-- Generated from EDMX file: C:\Users\Admin\OneDrive\Documents\Github\SmartPhone\Nike\Models\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [QuanLySanPham];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__NhanVien__MaChuc__797309D9]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[NhanVien] DROP CONSTRAINT [FK__NhanVien__MaChuc__797309D9];
GO
IF OBJECT_ID(N'[dbo].[FK__Order__KhachHang__2BFE89A6]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Order] DROP CONSTRAINT [FK__Order__KhachHang__2BFE89A6];
GO
IF OBJECT_ID(N'[dbo].[FK__Order__Status__2CF2ADDF]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Order] DROP CONSTRAINT [FK__Order__Status__2CF2ADDF];
GO
IF OBJECT_ID(N'[dbo].[FK__Order_Det__ID_Or__2FCF1A8A]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Order_Detail] DROP CONSTRAINT [FK__Order_Det__ID_Or__2FCF1A8A];
GO
IF OBJECT_ID(N'[dbo].[FK__Order_Det__ID_Pr__30C33EC3]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Order_Detail] DROP CONSTRAINT [FK__Order_Det__ID_Pr__30C33EC3];
GO
IF OBJECT_ID(N'[dbo].[FK__Product__Catalog__29221CFB]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Product] DROP CONSTRAINT [FK__Product__Catalog__29221CFB];
GO
IF OBJECT_ID(N'[dbo].[FK_Order]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Order] DROP CONSTRAINT [FK_Order];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Catalog]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Catalog];
GO
IF OBJECT_ID(N'[dbo].[ChucVu]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ChucVu];
GO
IF OBJECT_ID(N'[dbo].[KhachHang]', 'U') IS NOT NULL
    DROP TABLE [dbo].[KhachHang];
GO
IF OBJECT_ID(N'[dbo].[NhanVien]', 'U') IS NOT NULL
    DROP TABLE [dbo].[NhanVien];
GO
IF OBJECT_ID(N'[dbo].[Order]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Order];
GO
IF OBJECT_ID(N'[dbo].[Order_Detail]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Order_Detail];
GO
IF OBJECT_ID(N'[dbo].[Order_Status]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Order_Status];
GO
IF OBJECT_ID(N'[dbo].[Product]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Product];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Catalogs'
CREATE TABLE [dbo].[Catalogs] (
    [ID] int  NOT NULL,
    [CatalogCode] nvarchar(50)  NULL,
    [CatalogName] nvarchar(250)  NULL,
    [ProductOrigin] nvarchar(50)  NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CatalogId] int  NULL,
    [Picture] nvarchar(max)  NULL,
    [ProductName] nvarchar(250)  NULL,
    [ProductCode] nvarchar(50)  NULL,
    [UnitPrice] float  NULL,
    [ProductSold] int  NULL,
    [ProductSale] nvarchar(50)  NULL,
    [PriceOld] float  NULL,
    [SoLuong] int  NULL,
    [NgayNhapHang] datetime  NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- Creating table 'ChucVus'
CREATE TABLE [dbo].[ChucVus] (
    [MaChucVu] int IDENTITY(1,1) NOT NULL,
    [ChucVu1] nvarchar(50)  NULL
);
GO

-- Creating table 'NhanViens'
CREATE TABLE [dbo].[NhanViens] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FullName] nvarchar(250)  NULL,
    [Email] nvarchar(50)  NULL,
    [Address] nvarchar(250)  NULL,
    [NgaySinh] datetime  NULL,
    [Password] nvarchar(50)  NULL,
    [MaChucVu] int  NULL,
    [Picture] nvarchar(max)  NULL,
    [Sex] bit  NULL,
    [Sdt] char(10)  NULL
);
GO

-- Creating table 'KhachHangs'
CREATE TABLE [dbo].[KhachHangs] (
    [idUser] int IDENTITY(1,1) NOT NULL,
    [FirstName] nvarchar(50)  NULL,
    [LastName] nvarchar(50)  NULL,
    [Email] nvarchar(50)  NULL,
    [Password] nvarchar(50)  NULL,
    [Picture] nvarchar(max)  NULL,
    [Address] nvarchar(250)  NULL,
    [NgaySinh] datetime  NULL,
    [CMT] nvarchar(50)  NULL,
    [Sdt] char(10)  NULL,
    [TichLuy] int  NULL
);
GO

-- Creating table 'Orders'
CREATE TABLE [dbo].[Orders] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [KhachHangID] int  NULL,
    [Status] nvarchar(50)  NULL,
    [Address] nvarchar(250)  NULL,
    [Payment] bit  NULL,
    [NgayDat] datetime  NULL,
    [NgayGiao] datetime  NULL,
    [ThanhTien] float  NULL,
    [TongSoLuong] int  NULL,
    [Id_NV] int  NULL
);
GO

-- Creating table 'Order_Status'
CREATE TABLE [dbo].[Order_Status] (
    [Status] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Order_Detail'
CREATE TABLE [dbo].[Order_Detail] (
    [ID_Order] int  NOT NULL,
    [ID_Product] int  NOT NULL,
    [SoLuong] int  NULL,
    [Price] float  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Catalogs'
ALTER TABLE [dbo].[Catalogs]
ADD CONSTRAINT [PK_Catalogs]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- Creating primary key on [MaChucVu] in table 'ChucVus'
ALTER TABLE [dbo].[ChucVus]
ADD CONSTRAINT [PK_ChucVus]
    PRIMARY KEY CLUSTERED ([MaChucVu] ASC);
GO

-- Creating primary key on [Id] in table 'NhanViens'
ALTER TABLE [dbo].[NhanViens]
ADD CONSTRAINT [PK_NhanViens]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [idUser] in table 'KhachHangs'
ALTER TABLE [dbo].[KhachHangs]
ADD CONSTRAINT [PK_KhachHangs]
    PRIMARY KEY CLUSTERED ([idUser] ASC);
GO

-- Creating primary key on [ID] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [PK_Orders]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [Status] in table 'Order_Status'
ALTER TABLE [dbo].[Order_Status]
ADD CONSTRAINT [PK_Order_Status]
    PRIMARY KEY CLUSTERED ([Status] ASC);
GO

-- Creating primary key on [ID_Order], [ID_Product] in table 'Order_Detail'
ALTER TABLE [dbo].[Order_Detail]
ADD CONSTRAINT [PK_Order_Detail]
    PRIMARY KEY CLUSTERED ([ID_Order], [ID_Product] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CatalogId] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK__Product__Catalog__47DBAE45]
    FOREIGN KEY ([CatalogId])
    REFERENCES [dbo].[Catalogs]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Product__Catalog__47DBAE45'
CREATE INDEX [IX_FK__Product__Catalog__47DBAE45]
ON [dbo].[Products]
    ([CatalogId]);
GO

-- Creating foreign key on [MaChucVu] in table 'NhanViens'
ALTER TABLE [dbo].[NhanViens]
ADD CONSTRAINT [FK__NhanVien__MaChuc__797309D9]
    FOREIGN KEY ([MaChucVu])
    REFERENCES [dbo].[ChucVus]
        ([MaChucVu])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__NhanVien__MaChuc__797309D9'
CREATE INDEX [IX_FK__NhanVien__MaChuc__797309D9]
ON [dbo].[NhanViens]
    ([MaChucVu]);
GO

-- Creating foreign key on [KhachHangID] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK__Order__KhachHang__01142BA1]
    FOREIGN KEY ([KhachHangID])
    REFERENCES [dbo].[KhachHangs]
        ([idUser])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Order__KhachHang__01142BA1'
CREATE INDEX [IX_FK__Order__KhachHang__01142BA1]
ON [dbo].[Orders]
    ([KhachHangID]);
GO

-- Creating foreign key on [Status] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK__Order__Status__04E4BC85]
    FOREIGN KEY ([Status])
    REFERENCES [dbo].[Order_Status]
        ([Status])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Order__Status__04E4BC85'
CREATE INDEX [IX_FK__Order__Status__04E4BC85]
ON [dbo].[Orders]
    ([Status]);
GO

-- Creating foreign key on [ID_Order] in table 'Order_Detail'
ALTER TABLE [dbo].[Order_Detail]
ADD CONSTRAINT [FK__Order_Det__ID_Or__0A9D95DB]
    FOREIGN KEY ([ID_Order])
    REFERENCES [dbo].[Orders]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [ID_Product] in table 'Order_Detail'
ALTER TABLE [dbo].[Order_Detail]
ADD CONSTRAINT [FK__Order_Det__ID_Pr__0B91BA14]
    FOREIGN KEY ([ID_Product])
    REFERENCES [dbo].[Products]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__Order_Det__ID_Pr__0B91BA14'
CREATE INDEX [IX_FK__Order_Det__ID_Pr__0B91BA14]
ON [dbo].[Order_Detail]
    ([ID_Product]);
GO

-- Creating foreign key on [Id_NV] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_Order]
    FOREIGN KEY ([Id_NV])
    REFERENCES [dbo].[NhanViens]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Order'
CREATE INDEX [IX_FK_Order]
ON [dbo].[Orders]
    ([Id_NV]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------