﻿CREATE TABLE [dbo].[Table]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Slug] VARCHAR(100) NOT NULL, 
    [Summary] VARCHAR(500) NULL, 
    [DateRan] DATETIME NOT NULL DEFAULT Now(), 
    [Lean] TINYINT NOT NULL DEFAULT 1, 
    [Source] VARCHAR(50) NOT NULL, 
    [Byline] VARCHAR(50) NULL
)
