USE [WorkWatch]
GO

/****** Object:  Table [WorkWatch].[User]    Script Date: 08/02/2019 8:06:09 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [WorkWatch].[Input](
	[InputID] [INT] IDENTITY(1,1) NOT NULL,
	[UserID] [INT] NOT NULL,
	[ApplicationID] [INT] NOT NULL,
	[StartTime] [DATETIME2] NOT NULL,
	[EndTime] [DATETIME2] NOT NULL,
 CONSTRAINT [PK_WorkWatch_Input] PRIMARY KEY CLUSTERED 
(
	[InputID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [WorkWatch].[Input]  WITH CHECK ADD  CONSTRAINT [FK_WorkWatch_Input_User_UserID] FOREIGN KEY([UserID])
REFERENCES [WorkWatch].[User] ([UserID])
GO

ALTER TABLE [WorkWatch].[Input]  WITH CHECK ADD  CONSTRAINT [FK_WorkWatch_Input_Application_ApplicationID] FOREIGN KEY([ApplicationID])
REFERENCES [WorkWatch].[Application] ([ApplicationID])
GO