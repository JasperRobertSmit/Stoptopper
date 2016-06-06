CREATE TABLE [Login] (
	Id int  IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Username] varchar(255) NOT NULL,
	[Password] varchar(255) NOT NULL,
	[Email] varchar(255) NULL
)

GO

CREATE TABLE [Time] (
	Id int  IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Team] varchar(255) NOT NULL,
	[Time] DateTime NOT NULL
)

GO

CREATE TABLE [Team] (
	Id int  IDENTITY(1,1) PRIMARY KEY NOT NULL,
	TeamId varchar(255) NOT NULL,
	TeamName varchar(255) NOT NULL,
	TeamNumber int NOT NULL
)

GO