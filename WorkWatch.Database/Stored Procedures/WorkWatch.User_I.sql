USE [WorkWatch];
GO

/****** Object:  StoredProcedure [AgTech].[Company_I]    Script Date: 08/02/2019 8:39:45 AM ******/
SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO



/*
=============================================
EXEC WorkWatch.User_I @Username = 'Domain/User'
,@MachineName = 'MACHINE1234'

CreatedBy	: BennettR, NicB
CreatedOn	: 20190802    
Description	: Insert into User table  

ModifyOn	: 
ModifyBy	: 
Description	: 


=============================================
*/
CREATE PROCEDURE [WorkWatch].[User_I]
    @Username VARCHAR(256),
    @MachineName VARCHAR(256)
AS
SET NOCOUNT ON;
SET XACT_ABORT ON;


BEGIN



    --Beginning of Code

    INSERT INTO [WorkWatch].[User]
    (
        Username,
        MachineName
    )
    VALUES
    (
		@Username, 
		@MachineName
	);

--End of Code
END;

GO


