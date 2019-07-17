﻿CREATE TABLE [dbo].[LOGIN_DETAILS](
	[USER_ID] [int] IDENTITY(1,1) NOT NULL,
	[EMAIL_ID] [nvarchar](100) NOT NULL,
	[MOBILE_NO] [nvarchar](15) NOT NULL,
	[PASSWORD] [nvarchar](20) NOT NULL,
	[USER_TYPE] [int] NOT NULL,
 CONSTRAINT [PK_RegisterTable] PRIMARY KEY CLUSTERED 
(
	[USER_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]