﻿CREATE TABLE [dbo].[DOMAIN_ID_MAPPING](
	[DOMAIN_ID] [int] IDENTITY(1,1) NOT NULL,
	[VALUE] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_DOMAIN_ID_MAPPING] PRIMARY KEY CLUSTERED 
(
	[DOMAIN_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]