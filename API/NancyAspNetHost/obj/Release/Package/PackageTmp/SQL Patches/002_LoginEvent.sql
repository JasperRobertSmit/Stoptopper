ALTER TABLE [Event]
ADD [LoginID] int FOREIGN KEY REFERENCES [Login](Id) default 1

GO

UPDATE [Event] SET LoginID = 1
WHERE Id = 1

GO