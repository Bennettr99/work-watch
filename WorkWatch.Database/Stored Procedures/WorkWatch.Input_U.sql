USE [WorkWatch];
GO

/****** Object:  StoredProcedure [AgTech].[Company_I]    Script Date: 08/02/2019 8:39:45 AM ******/
SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO



/*
=============================================
EXEC WorkWatch.Input_U @InputID = 1


CreatedBy	: BennettR, NicB
CreatedOn	: 20190802    
Description	: Insert into Input table  

ModifyOn	: 
ModifyBy	: 
Description	: 


=============================================
*/
CREATE PROCEDURE [WorkWatch].[Input_U]
    @InputID INT

AS
SET NOCOUNT ON;
SET XACT_ABORT ON;


BEGIN



    --Beginning of Code

    UPDATE [WorkWatch].[Input] 
	SET EndTime = SYSDATETIME()
	WHERE InputID = @InputID;

--End of Code
END;

GO


