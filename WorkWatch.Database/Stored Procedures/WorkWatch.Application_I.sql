USE [WorkWatch];
GO

/****** Object:  StoredProcedure [AgTech].[Company_I]    Script Date: 08/02/2019 8:39:45 AM ******/
SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO



/*
=============================================
EXEC WorkWatch.Application_I @UserID = 1, @Name = 'Google Chrome'


CreatedBy	: BennettR, NicB
CreatedOn	: 20190802    
Description	: Insert into Application table and return new ApplicationID

ModifyOn	: 
ModifyBy	: 
Description	: 


=============================================
*/
CREATE OR ALTER PROCEDURE [WorkWatch].[Application_I]
    @UserID INT,
	@Name VARCHAR(256)

AS
SET NOCOUNT ON;
SET XACT_ABORT ON;


BEGIN



    --Beginning of Code

    INSERT INTO [WorkWatch].[Application]
    (
        UserID,
		Name,
		Time
    )
	OUTPUT INSERTED.ApplicationID
    VALUES
    (
		@UserID, 
		@Name,
		SYSDATETIME()
	);

--End of Code
END;

GO


