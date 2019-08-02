USE [WorkWatch];
GO

/****** Object:  StoredProcedure [AgTech].[Company_I]    Script Date: 08/02/2019 8:39:45 AM ******/
SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO



/*
=============================================
EXEC WorkWatch.Input_I @UserID = 1, @ApplicationID = 1


CreatedBy	: BennettR, NicB
CreatedOn	: 20190802    
Description	: Insert into Input table and return new InputID

ModifyOn	: 
ModifyBy	: 
Description	: 


=============================================
*/
CREATE OR ALTER PROCEDURE [WorkWatch].[Input_I]
    @UserID INT,
	@ApplicationID INT

AS
SET NOCOUNT ON;
SET XACT_ABORT ON;


BEGIN



    --Beginning of Code

    INSERT INTO [WorkWatch].[Input]
    (
        UserID,
		ApplicationID,
		StartTime,
		EndTime
    )
	OUTPUT INSERTED.InputID
    VALUES
    (
		@UserID, 
		@ApplicationID,
		SYSDATETIME(),
		SYSDATETIME()
	);

--End of Code
END;

GO


