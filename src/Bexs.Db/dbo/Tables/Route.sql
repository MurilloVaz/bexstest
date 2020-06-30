CREATE TABLE [dbo].[Route](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdRouteEntryFrom] [int] NOT NULL,
	[IdRouteEntryTo] [int] NOT NULL,
	[Price] [decimal](19, 4) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_Route_Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [UQ_Route_IdRouteEntryFrom_IdRouteEntryTo_Priority] UNIQUE NONCLUSTERED 
(
	[IdRouteEntryFrom] ASC,
	[IdRouteEntryTo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Route] ADD  CONSTRAINT [DF_Route_Active]  DEFAULT ((1)) FOR [Active]
GO

ALTER TABLE [dbo].[Route]  WITH CHECK ADD  CONSTRAINT [FK_RouteIdRouteEntryFrom_RouteEntryId] FOREIGN KEY([IdRouteEntryFrom])
REFERENCES [dbo].[RouteEntry] ([Id])
GO

ALTER TABLE [dbo].[Route] CHECK CONSTRAINT [FK_RouteIdRouteEntryFrom_RouteEntryId]
GO

ALTER TABLE [dbo].[Route]  WITH CHECK ADD  CONSTRAINT [FK_RouteIdRouteEntryTo_RouteEntryId] FOREIGN KEY([IdRouteEntryTo])
REFERENCES [dbo].[RouteEntry] ([Id])
GO

ALTER TABLE [dbo].[Route] CHECK CONSTRAINT [FK_RouteIdRouteEntryTo_RouteEntryId]
GO