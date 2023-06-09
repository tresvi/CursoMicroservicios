GO
CREATE DATABASE Clientes;
GO
USE [Clientes]
GO
/****** Object:  Schema [Clientes]    Script Date: 24/05/2023 9:32:14 ******/
CREATE SCHEMA [Clientes]
GO
/****** Object:  Table [Clientes].[Cliente]    Script Date: 24/05/2023 9:32:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Clientes].[Cliente](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](250) NOT NULL,
	[Apellido] [nvarchar](250) NOT NULL,
	[Cuil] [nvarchar](32) NOT NULL,
	[TipoDocumento] [nvarchar](10) NOT NULL,
	[NroDocumento] [int] NOT NULL,
	[EsEmpleadoBNA] [bit] NOT NULL,
	[PaisOrigen] [nvarchar](64) NOT NULL,
 CONSTRAINT [PK_Cliente] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [Clientes].[Cliente] ON 

INSERT [Clientes].[Cliente] ([Id], [Nombre], [Apellido], [Cuil], [TipoDocumento], [NroDocumento], [EsEmpleadoBNA], [PaisOrigen]) VALUES (1, N'Homero', N'Simpson', N'20292666447', N'DNI', 29266644, 1, N'ARGENTINA')
INSERT [Clientes].[Cliente] ([Id], [Nombre], [Apellido], [Cuil], [TipoDocumento], [NroDocumento], [EsEmpleadoBNA], [PaisOrigen]) VALUES (2, N'Juan', N'Perez', N'20-29266644-7', N'DNI', 29266644, 1, N'Paraguay')
INSERT [Clientes].[Cliente] ([Id], [Nombre], [Apellido], [Cuil], [TipoDocumento], [NroDocumento], [EsEmpleadoBNA], [PaisOrigen]) VALUES (3, N'Marge', N'Simpson', N'27222222227', N'DNI', 22222222, 1, N'PARAGUAY')
SET IDENTITY_INSERT [Clientes].[Cliente] OFF
/****** Object:  Index [IX_Cliente]    Script Date: 29/05/2023 9:24:01 ******/
CREATE UNIQUE NONCLUSTERED INDEX [CUIL_Cliente_UNICO] ON [Clientes].[Cliente]
(
	[Cuil] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
GO
ALTER TABLE [Clientes].[Cliente] ADD  DEFAULT (CONVERT([bit],(0))) FOR [EsEmpleadoBNA]
GO
