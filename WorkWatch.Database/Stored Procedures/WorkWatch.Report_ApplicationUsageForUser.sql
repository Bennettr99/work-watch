USE [WorkWatch];
GO

/****** Object:  StoredProcedure [WorkWatch].[Input_I]    Script Date: 08/05/2019 8:54:33 AM ******/
SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO




/*
=============================================
EXEC WorkWatch.Report_ApplicationUsageForUser @Username= 'FCSAMERICA\broszn', @StartTime = '2019-08-05', @EndTime = '2019-08-06'


CreatedBy	: BennettR, NicB
CreatedOn	: 20190805    
Description	: Get percentage and time used per application for user

ModifyOn	: 
ModifyBy	: 
Description	: 


=============================================
*/
CREATE OR ALTER PROCEDURE [WorkWatch].[Report_ApplicationUsageForUser]
    @Username VARCHAR(256),
    @StartTime DATETIME2,
    @EndTime DATETIME2
AS
SET NOCOUNT ON;
SET XACT_ABORT ON;


BEGIN

    --Beginning of Code
;
WITH GroupAppTimes_CTE (Name, Username, TotalAppLength)
AS (
	SELECT a.Name,
           u.Username,
           SUM(DATEDIFF(SECOND, i.StartTime, i.EndTime)) [TotalAppLength]
	FROM WorkWatch.Application a
        JOIN WorkWatch.Input i
            ON i.ApplicationID = a.ApplicationID
        JOIN WorkWatch.[User] u
            ON u.UserID = a.UserID
    WHERE u.Username = @Username
          AND i.StartTime >= @StartTime
          AND i.EndTime <= @EndTime
	GROUP BY u.Username, a.Name 
),
GroupUserTimes_CTE (Username, TotalAppLength)
AS (
	SELECT gat.Username, 
			SUM(gat.TotalAppLength) [TotalAppLength]
	FROM GroupAppTimes_CTE gat
	GROUP BY gat.Username
)
    SELECT gat.Name,
           gat.Username,
		   CONVERT(DECIMAL(16, 4), gat.TotalAppLength) / gut.TotalAppLength * 100 [Percentage],
		   CONVERT(varchar, gat.TotalAppLength / 86400 ) + ':' + -- Days
		   CONVERT(varchar, DATEADD(s, ( gat.TotalAppLength % 86400 ), 0), 108) [TotalAppTime]
    FROM GroupAppTimes_CTE gat
	JOIN GroupUserTimes_CTE gut
	ON gut.Username = gat.Username
	WHERE gat.TotalAppLength > 0
	ORDER BY Percentage DESC


--End of Code
END;


GO


