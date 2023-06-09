GO
CREATE DATABASE Transferencias;
GO
USE [Transferencias]
GO
/****** Object:  Table [dbo].[Transferencias]    Script Date: 24/05/2023 19:42:40 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transferencias](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CuilOriginante] [nvarchar](32) NOT NULL,
	[CuilDestinatario] [nvarchar](32) NOT NULL,
	[CbuOrigen] [nvarchar](64) NOT NULL,
	[CbuDestino] [nvarchar](64) NOT NULL,
	[Importe] [decimal](16, 2) NOT NULL,
	[Concepto] [nvarchar](64) NOT NULL,
	[Descripcion] [nvarchar](64) NULL,
 CONSTRAINT [PK_Transferencias22] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
