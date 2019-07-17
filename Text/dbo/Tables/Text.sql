﻿CREATE TABLE [dbo].[Text](
	[DATA_ID] [int] IDENTITY(1,1) NOT NULL,
	[TEXT] [nvarchar](600) NOT NULL,
	[SOURCE_ID] [int] NOT NULL,
	[ADDED_ON] [datetime] NOT NULL,
	[LANG_ID] [int] NOT NULL,
	[DOMAIN_ID] [int] NOT NULL,
	[DATASET_ID] [int] NOT NULL,
 CONSTRAINT [PK_SENTENCES] PRIMARY KEY CLUSTERED 
(
	[DATA_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]