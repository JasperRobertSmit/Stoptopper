CREATE TABLE [Login] (
	Id int  IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Username] varchar(255) NOT NULL,
	[Password] varchar(255) NOT NULL,
	[Email] varchar(255) NULL
)

GO

CREATE TABLE [Vereniging] (
	Id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Afkorting] varchar(3) NOT NULL,
	[Naam] varchar(255) NOT NULL
)

GO

CREATE TABLE [Event] (
	Id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[EventGuid] varchar(255) NOT NULL,
	[Afkorting] varchar(255) NOT NULL,
	[Naam] varchar(255) NOT NULL,
	[OrganisatorID] int FOREIGN KEY REFERENCES [Vereniging](Id),
	[StartDatum] datetime NOT NULL,
	[EindDatum] datetime NOT NULL,
	[Afstand] int NOT NULL,
	[Plaats] varchar(255) NOT NULL
)

GO

CREATE TABLE [Blok] (
	Id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[EvenementID] int FOREIGN KEY REFERENCES [Event](Id),
	[Nummer] int NOT NULL,
	[StartTijd] datetime NOT NULL,
	[EindTijd] datetime NOT NULL,
	[Complete] bit NOT NULL
)

GO

CREATE TABLE [Veld] (
	Id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Afkorting] varchar(255) NOT NULL,
	[ANaam] varchar(255) NOT NULL,
	[BlokID] int FOREIGN KEY REFERENCES [Blok](Id)
)

GO

CREATE TABLE [Ploeg] (
	Id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Rugnummer] varchar(255) NOT NULL,
	[Naam] varchar(255) NOT NULL,
	[VerenigingID] int FOREIGN KEY REFERENCES [Vereniging](Id),
	[VeldID] int FOREIGN KEY REFERENCES [Veld](Id),
	[Bootnaam] varchar(255) NOT NULL
)

GO

CREATE TABLE [Persoon] (
	Id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Naam] varchar(255) NOT NULL,
	[VerenigingID] int FOREIGN KEY REFERENCES [Vereniging](Id),
	[GeboorteDatum] datetime NOT NULL,
	[KNRBID] varchar(255) NULL
)

GO

CREATE TABLE [Deelnemer] (
	Id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[PersoonID] int FOREIGN KEY REFERENCES [Persoon](Id),
	[Nummer] int NOT NULL
)

GO

CREATE TABLE [PloegDeelnemer] (
	Id int IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[DeelnemerID] int FOREIGN KEY REFERENCES [Deelnemer](Id),
	[PloegID] int FOREIGN KEY REFERENCES [Ploeg](Id),
)

GO

CREATE TABLE [Tijd] (
	Id int  IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[PloegID] int FOREIGN KEY REFERENCES [Ploeg](Id),
	[BlokID] int FOREIGN KEY REFERENCES [Blok](Id),
	[Tijd] datetime NOT NULL
)

GO

INSERT INTO Vereniging VALUES ('TST', 'TST')
GO
INSERT INTO Persoon VALUES('TST', 1, GETDATE(), 'TST')
GO
INSERT INTO Deelnemer VALUES(1, 1)
GO
INSERT INTO [Event] VALUES ('TST', 'TST', 'TST', 1, GETDATE(), GETDATE(), 1, 'TST')
GO
INSERT INTO Blok VALUES(1, 1, GETDATE(), GETDATE(), 1)
GO
INSERT INTO Veld VALUES('TST', 'TST', 1)
GO
INSERT INTO Ploeg VALUES('TST', 'TST', 1, 1, 'TST')
GO


