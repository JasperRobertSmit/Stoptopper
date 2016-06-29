CREATE TABLE [LoginToken] (
	Id int  IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Token] uniqueidentifier NOT NULL,
	[IPAdress] varchar(50) NOT NULL,
	[LoginID] int FOREIGN KEY REFERENCES [Login](Id),
	[Timestamp] datetime NOT NULL
)

GO