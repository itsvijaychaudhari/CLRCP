﻿CREATE TABLE [dbo].[ImageText](
	[USER_ID] [int] NOT NULL,
	[DATA_ID] [int] NOT NULL,
	[DOMAIN_ID] [int] NOT NULL,
	[OUTPUT_DATA] [nvarchar](max) NOT NULL,
	[OUTPUT_LANG_ID] [int] NOT NULL,
	[DATASET_ID] [int] NOT NULL,
	[ADDED_ON] [datetime] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]