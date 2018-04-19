USE DvdLibrary
GO

IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdsSelectAll')
      DROP PROCEDURE DvdsSelectAll
GO

CREATE PROCEDURE DvdsSelectAll AS
BEGIN
	SELECT DvdId, Title, RealeaseYear, Director, Rating
	FROM DvdDetails
END

GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdInsert')
      DROP PROCEDURE DvdInsert
GO

CREATE PROCEDURE DvdInsert (
	@DvdId int output,
	@Title nvarchar(50),
	@RealeaseYear char(4),
	@Director nvarchar(100),
	@Rating varchar(5),
	@Notes varchar(255)
) AS
BEGIN
	INSERT INTO DvdDetails (Title, RealeaseYear, Director, Rating, Notes)
	VALUES (@Title, @RealeaseYear, @Director, @Rating, @Notes)

	SET @DvdId = SCOPE_IDENTITY();
END

GO



IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdUpdate')
      DROP PROCEDURE DvdUpdate
GO

CREATE PROCEDURE DvdUpdate (
	@DvdId int output,
	@Title nvarchar(50),
	@RealeaseYear char(4),
	@Director nvarchar(100),
	@Rating varchar(5),
	@Notes varchar(255)
) AS
BEGIN
	UPDATE DvdDetails SET 
		Title = @Title, 
		RealeaseYear = @RealeaseYear, 
		Director = @Director, 
		Rating = @Rating, 
		Notes = @Notes
	WHERE DvdId = @DvdId
END

GO



IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdDelete')
      DROP PROCEDURE DvdDelete
GO

CREATE PROCEDURE DvdDelete (
	@DvdId int
) AS
BEGIN
	BEGIN TRANSACTION

	DELETE FROM DvdDetails WHERE DvdId = @DvdId;

	COMMIT TRANSACTION
END
GO



IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdSelectById')
      DROP PROCEDURE DvdSelectById
GO

CREATE PROCEDURE DvdSelectById (
	@DvdId int
) AS
BEGIN
	SELECT DvdId, Title, RealeaseYear, Director, Rating, Notes
	FROM DvdDetails
	WHERE DvdId = @DvdId
END
GO


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdsSearchByDirector')
      DROP PROCEDURE DvdsSearchByDirector
GO

CREATE PROCEDURE DvdsSearchByDirector (
	@Director nvarchar(100)
) AS
BEGIN
	SELECT DvdId, Title, RealeaseYear, Director, Rating, Notes
	FROM DvdDetails
	WHERE Director LIKE @Director
END
GO



IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdsSearchByTitle')
      DROP PROCEDURE DvdsSearchByTitle
GO

CREATE PROCEDURE DvdsSearchByTitle (
	@Title nvarchar(50)
) AS
BEGIN
	SELECT DvdId, Title, RealeaseYear, Director, Rating, Notes
	FROM DvdDetails
	WHERE Title LIKE @Title
END
GO



IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdsSearchByRealeaseYear')
      DROP PROCEDURE DvdsSearchByRealeaseYear
GO

CREATE PROCEDURE DvdsSearchByRealeaseYear (
	@RealeaseYear char(4)
) AS
BEGIN
	SELECT DvdId, Title, RealeaseYear, Director, Rating, Notes
	FROM DvdDetails
	WHERE RealeaseYear = @RealeaseYear
END
GO




IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.ROUTINES
   WHERE ROUTINE_NAME = 'DvdsSearchByRating')
      DROP PROCEDURE DvdsSearchByRating
GO

CREATE PROCEDURE DvdsSearchByRating (
	@Rating varchar(5)
) AS
BEGIN
	SELECT DvdId, Title, RealeaseYear, Director, Rating, Notes
	FROM DvdDetails
	WHERE Rating LIKE @Rating
END
GO
