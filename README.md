
# ASP.NETCore_CRUD


CRUD utilizando Aplicativo Web ASP.NET Core :)

C = Create

R = Read

U = Update 

D = Delete

## Banco de Dados:

```SQL
USE [CRUD]
GO

/****** Object:  Table [dbo].[equipamento]    Script Date: 07/02/2023 21:07:06 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[equipamento](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nome] [varchar](50) NULL,
	[num_serie] [varchar](50) NULL,
 CONSTRAINT [PK_equip] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
```


## Referência

 - [Youtube: BoostMyTool](https://www.youtube.com/watch?v=T-e554Zt3n4&t=937s)
