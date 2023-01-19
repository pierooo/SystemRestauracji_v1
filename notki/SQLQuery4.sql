USE RestaurantSystem_v0
GO

/****** Object:  Table [dbo].[Companies]    Script Date: 2023-01-18 23:09:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Companies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Address] [nvarchar](250) NULL,
	[City] [nvarchar](100) NULL,
	[PostalCode] [nvarchar](15) NULL,
	[Country] [nvarchar](100) NULL,
	[NIP] [nvarchar](20) NULL,
	[PhoneNumber] [nvarchar](30) NULL,
	[Mail] [nvarchar](100) NULL,
	[IsActive] [bit] NULL,
	[LastModified] [datetime] NULL,
	[PhotoUrl] [nvarchar](250) NULL
 CONSTRAINT [PK_Companies] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Description] [nvarchar](250) NULL,
	[EmployeeId] [int] NOT NULL,
	[RestaurantTableId] [int] NULL,
	[OrderDate] [datetime] NULL,
	[TotalPriceNet] [decimal](18, 2) NULL,
	[TotalPriceGross] [decimal](18, 2) NULL,
	[OrderStatus] [nvarchar](100) NULL,
	[PaymentId] [int]  NULL,
	[DocumentId] [int] NULL,
	[IsActive] [bit] NULL,
	[LastModified] [datetime] NULL,
	[PhotoUrl] [nvarchar](250) NULL
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

Go

CREATE TABLE [dbo].[OrdersDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[OrderId] [int] NOT NULL,
	[ProductId] [int] NOT NULL,
	[UnitPriceNetto] [decimal](18, 2) NULL,
	[Quantity] [int] NULL,
	[VAT] [decimal](18, 2) NULL,
	[Description] [nvarchar](250) NULL,
	[OrderDetailsStatus] [nvarchar](100) NULL,
	[IsActive] [bit] NULL,
	[LastModified] [datetime] NULL
 CONSTRAINT [PK_OrdersDetails] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

go

CREATE TABLE [dbo].[Payments](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Description] [nvarchar](250) NULL,
	[EmployeeId] [int] NULL,
	[PaymentDate] [datetime] NULL,
	[PaymentDateLimit] [datetime] NULL,
	[TotalAmountGross] [decimal](18, 2) NULL,
	[DeviceId] [int]  NULL,
	[PaymentStatus] [nvarchar](100) NULL,
	[PaymentType] [nvarchar](100) NULL,
	[IsActive] [bit] NULL,
	[LastModified] [datetime] NULL,
	[PhotoUrl] [nvarchar](250) NULL
 CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
go
CREATE TABLE [dbo].[Invoices](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Description] [nvarchar](250) NULL,
	[Title] [nvarchar](50) NULL,
	[Number] [nvarchar](10) NULL,
	[AuthorCompanyName] [nvarchar](250) NULL,
	[AuthorFullName] [nvarchar](250) NULL,
	[AuthorNIP] [nvarchar](50) NULL,
	[AuthorFullAddress] [nvarchar](350) NULL,
	[AuthorContact] [nvarchar](50) NULL,
	[RecipientFirstName] [nvarchar](250) NULL,
	[RecipientSecondName] [nvarchar](250) NULL,
	[RecipientFullName] [nvarchar](250) NULL,
	[RecipientNIP] [nvarchar](50) NULL,
	[RecipientFullAddress] [nvarchar](250) NULL,
	[RecipientContact] [nvarchar](50) NULL,
	[EmployeeFullName] [nvarchar](250) NULL,
	[PaymentStatus] [nvarchar](50) NULL,
	[PaymentDateLimit] [nvarchar](50) NULL,
	[InvoiceDate] [datetime] NULL,
	[TotalAmountGross] [decimal](18, 2) NULL,
	[TotalAmountNet] [decimal](18, 2) NULL,
	[TotalAmountVAT] [decimal](18, 2) NULL,
	[InvoiceStatus] [nvarchar](100) NULL,
	[Printed] [bit] NULL,
	[WorkstationDeviceLinkId] [int]  NULL,
	[IsActive] [bit] NULL,
	[LastModified] [datetime] NULL,
	[AuthorPhotoUrl] [nvarchar](250) NULL,
	[RecipientPhotoUrl] [nvarchar](250) NULL,
	[DocumentPhotoUrl] [nvarchar](250) NULL
 CONSTRAINT [PK_Invoices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
go

CREATE TABLE [dbo].[Documents](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NULL,
	[Description] [nvarchar](250) NULL,
	[AuthorFullName] [nvarchar](250) NULL,
	[AuthorNIP] [nvarchar](250) NULL,
	[RecipientFullName] [nvarchar](250) NULL,
	[RecipientNIP] [nvarchar](250) NULL,
	[EmployeeFullName] [nvarchar](250) NULL,
	[PaymentDateLimit] [datetime] NULL,
	[DocumentDate] [datetime] NULL,
	[InvoiceId] [int] NULL,
	[DocumentPositionsDetailsId] [int] NULL,
	[TotalAmountGross] [decimal](18, 2) NULL,
	[TotalAmountNet] [decimal](18, 2) NULL,
	[TotalAmountVAT] [decimal](18, 2) NULL,
	[DocumentStatus] [nvarchar](100) NULL,
	[Printed] [bit] NULL,
	[WorkstationDeviceLinkId] [int] NULL,
	[IsActive] [bit] NULL,
	[LastModified] [datetime] NULL,
	[AuthorPhotoUrl] [nvarchar](250) NULL,
	[RecipientPhotoUrl] [nvarchar](250) NULL,
	[DocumentPhotoUrl] [nvarchar](250) NULL,
 CONSTRAINT [PK_Documents] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
go
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nchar](50) NOT NULL,
	[CategoryId] [int] NULL,
	[UnitPriceNet] [decimal](18, 2) NULL,
	[UnitPriceGross] [decimal](18, 2) NULL,
	[UnitsInStock] [int]  NULL,
	[Discontinued] [bit] NULL,
	[VAT] [decimal](18, 2) NULL,
	[Description] [nvarchar](250) NULL,
	[IsActive] [bit] NULL,
	[LastModified] [datetime] NULL,
	[PhotoUrl] [nvarchar](250) NULL
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
go
CREATE TABLE [dbo].[DocumentPositionsDetails](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PositionName] [nchar](50) NOT NULL,
	[DucumentId] [int] NULL,
	[UnitPriceNet] [decimal](18, 2) NULL,
	[UnitPriceGross] [decimal](18, 2) NULL,
	[UnitsInStock] [int]  NULL,
	[VAT] [decimal](18, 2) NULL,
	[Quantity] [int] NULL,
	[Description] [nvarchar](250) NULL,
	[IsActive] [bit] NULL,
	[LastModified] [datetime] NULL
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]