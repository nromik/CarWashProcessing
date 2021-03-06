USE [CarWashProcessing]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 04.09.2018 1:25:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DataPost] [datetime2](7) NOT NULL,
	[ClientName] [nvarchar](max) NULL,
	[CarNumber] [nvarchar](max) NULL,
	[OrderTypeId] [int] NOT NULL,
	[Price] [decimal](18, 2) NULL,
	[StartTime] [datetime] NULL,
	[EndTime] [datetime] NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderTask]    Script Date: 04.09.2018 1:25:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderTask](
	[OrderId] [int] NOT NULL,
	[TaskId] [int] NOT NULL,
 CONSTRAINT [PK_OrderTask] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC,
	[TaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderType]    Script Date: 04.09.2018 1:25:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderType](
	[Id] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](256) NULL,
 CONSTRAINT [PK_OrderType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaskType]    Script Date: 04.09.2018 1:25:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaskType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[NeedWorker] [bit] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Duration] [int] NOT NULL,
 CONSTRAINT [PK_TaskType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[vw_OrderTask]    Script Date: 04.09.2018 1:25:05 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


Create VIEW [dbo].[vw_OrderTask]
AS
SELECT   
o.Id as OrderTypeId, 
o.Name as OrderName,
o.Description as OrderDescription,
t.Id as TaskId,
t.Name as TaskName,
t.NeedWorker as NeedWorker,
t.Price as Price,
t.Duration as Duration

FROM            dbo.OrderType o (nolock) 
join dbo.OrderTask ot (nolock) on o.Id = ot.OrderId 
join dbo.TaskType t (nolock) on t.Id = ot.TaskId
GO
INSERT [dbo].[OrderTask] ([OrderId], [TaskId]) VALUES (1, 1)
INSERT [dbo].[OrderTask] ([OrderId], [TaskId]) VALUES (1, 2)
INSERT [dbo].[OrderTask] ([OrderId], [TaskId]) VALUES (2, 1)
INSERT [dbo].[OrderTask] ([OrderId], [TaskId]) VALUES (2, 2)
INSERT [dbo].[OrderTask] ([OrderId], [TaskId]) VALUES (2, 4)
INSERT [dbo].[OrderTask] ([OrderId], [TaskId]) VALUES (3, 1)
INSERT [dbo].[OrderTask] ([OrderId], [TaskId]) VALUES (4, 1)
INSERT [dbo].[OrderTask] ([OrderId], [TaskId]) VALUES (4, 4)
INSERT [dbo].[OrderTask] ([OrderId], [TaskId]) VALUES (5, 2)
INSERT [dbo].[OrderTask] ([OrderId], [TaskId]) VALUES (5, 3)
INSERT [dbo].[OrderTask] ([OrderId], [TaskId]) VALUES (5, 4)
INSERT [dbo].[OrderTask] ([OrderId], [TaskId]) VALUES (6, 3)
INSERT [dbo].[OrderTask] ([OrderId], [TaskId]) VALUES (6, 4)
INSERT [dbo].[OrderType] ([Id], [Name], [Description]) VALUES (1, N'OrderType1', N'desc1')
INSERT [dbo].[OrderType] ([Id], [Name], [Description]) VALUES (2, N'OrderType2', N'desc2')
INSERT [dbo].[OrderType] ([Id], [Name], [Description]) VALUES (3, N'OrderType3', N'desc3')
INSERT [dbo].[OrderType] ([Id], [Name], [Description]) VALUES (4, N'OrderType4', N'desc4')
INSERT [dbo].[OrderType] ([Id], [Name], [Description]) VALUES (5, N'OrderType5', N'desc5')
INSERT [dbo].[OrderType] ([Id], [Name], [Description]) VALUES (6, N'OrderType6', N'desc6')
SET IDENTITY_INSERT [dbo].[TaskType] ON 

INSERT [dbo].[TaskType] ([Id], [Name], [NeedWorker], [Price], [Duration]) VALUES (1, N'Task1', 0, CAST(10.00 AS Decimal(18, 2)), 2000)
INSERT [dbo].[TaskType] ([Id], [Name], [NeedWorker], [Price], [Duration]) VALUES (2, N'Task2', 1, CAST(20.00 AS Decimal(18, 2)), 5000)
INSERT [dbo].[TaskType] ([Id], [Name], [NeedWorker], [Price], [Duration]) VALUES (3, N'Task3', 0, CAST(5.00 AS Decimal(18, 2)), 1000)
INSERT [dbo].[TaskType] ([Id], [Name], [NeedWorker], [Price], [Duration]) VALUES (4, N'Task4', 1, CAST(7.00 AS Decimal(18, 2)), 1500)
SET IDENTITY_INSERT [dbo].[TaskType] OFF
