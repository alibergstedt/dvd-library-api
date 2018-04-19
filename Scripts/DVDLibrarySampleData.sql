USE DvdLibrary

GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DbReset')
      DROP PROCEDURE DbReset
GO

CREATE PROCEDURE DbReset AS
BEGIN
	DELETE FROM DvdDetails;
	DELETE FROM AspNetUsers WHERE id IN ('00000000-0000-0000-0000-000000000000');

	DBCC CHECKIDENT ('DvdDetails', RESEED, 0)

	SET IDENTITY_INSERT DvdDetails ON;

	INSERT INTO DvdDetails(DvdId, Title, RealeaseYear, Director, Rating, Notes)
	VALUES (0, 'A Great Tale', 2015, 'Jones', 'PG', 'This is such a great tale!'),
	 (1, 'A Good Tale', 2012, 'Smith', 'PG-13',null),
	 (2, 'An OK Tale', 2009, 'Bryan', 'G', 'Ehhh. It was ok.'),
	 (3, 'A Riveting Tale', 2014, 'Simpson', 'R', 'Exceptional and riveting to the core.'),
	 (4, 'A Rotten Tale', 2005, 'Jones', 'PG-13', null),
	 (5, 'A Boring Tale', 2011, 'Baker', 'G', 'Underwhelming.'),
	 (6, 'A Bad Tale', 2010, 'Smith', 'PG', 'So, so bad.'),
	 (7, 'A Scary Tale', 2012, 'Jones', 'G', null);

	SET IDENTITY_INSERT DvdDetails OFF;

	INSERT INTO AspNetUsers(Id, EmailConfirmed, PhoneNumberConfirmed, Email, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, UserName)
	VALUES('00000000-0000-0000-0000-000000000000', 0, 0, 'test@test.com', 0, 0, 0, 'DvdLibraryApp');

END