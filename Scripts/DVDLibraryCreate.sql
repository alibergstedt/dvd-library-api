
USE DvdLibrary
GO

IF EXISTS(SELECT * FROM sys.tables WHERE name='DvdDetails')
	DROP TABLE DvdDetails
GO


CREATE TABLE DvdDetails (
	DvdId int identity(1,1) not null primary key,
	Title nvarchar(50) not null,
	RealeaseYear char(4) not null,
	Director nvarchar(100) not null,
	Rating varchar(5) not null,
	Notes varchar(255) null
)

GO
