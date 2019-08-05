USE [WorkWatch];
GO

/****** Object:  StoredProcedure [WorkWatch].[Input_I]    Script Date: 08/05/2019 8:54:33 AM ******/
SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO




/*
=============================================
EXEC WorkWatch.Report_ActiveTimeByUsers @StartTime = '2019-08-05', @EndTime = '2019-08-06'


CreatedBy	: BennettR, NicB
CreatedOn	: 20190805    
Description	: Get active time by user

ModifyOn	: 
ModifyBy	: 
Description	: 


=============================================
*/
CREATE OR ALTER PROCEDURE [WorkWatch].[Report_ActiveTimeByUsers]
    @StartTime DATETIME2,
    @EndTime DATETIME2
AS
SET NOCOUNT ON;
SET XACT_ABORT ON;


BEGIN

    --Beginning of Code
;
WITH GroupUserTimes_CTE (Username, TotalActiveTime)
AS (
	SELECT u.Username,
           SUM(DATEDIFF(SECOND, i.StartTime, i.EndTime)) [TotalActiveTime]
	FROM WorkWatch.Input i
        JOIN WorkWatch.[User] u
            ON u.UserID = i.UserID
    WHERE i.StartTime >= @StartTime
          AND i.EndTime <= @EndTime
	GROUP BY u.Username
),
StartTimes_CTE (Username, StartTime)
AS (
	SELECT u.Username,
		   MIN(i.StartTime) [StartTime]
	FROM WorkWatch.[User] u
	JOIN WorkWatch.Input i
	ON i.UserID = u.UserID
	WHERE i.StartTime >= @StartTime
	GROUP BY u.Username
),
EndTimes_CTE (Username, EndTime)
AS (
	SELECT u.Username,
		   MAX(i.EndTime) [EndTime]
	FROM WorkWatch.[User] u
	JOIN WorkWatch.Input i
	ON i.UserID = u.UserID
	WHERE i.EndTime <= @EndTime
	GROUP BY u.Username
),
TotalTimes_CTE (Username, TotalTime)
AS (
	SELECT st.Username,
		   DATEDIFF(SECOND, st.StartTime, et.EndTime) [TotalTime]
	FROM StartTimes_CTE st
	JOIN EndTimes_CTE et
	ON et.Username = st.Username
)
    SELECT gut.Username,
		   CONVERT(varchar, DATEADD(s, ( gut.TotalActiveTime % 86400 ), 0), 108) [TotalActiveTime],
		   CONVERT(varchar, DATEADD(s, ( tt.TotalTime % 86400 ), 0), 108) [TotalTime],
		   CONVERT(DECIMAL(16, 4), gut.TotalActiveTime) / tt.TotalTime * 100 [Percentage],
		   st.StartTime,
		   et.EndTime
    FROM GroupUserTimes_CTE gut
	JOIN TotalTimes_CTE tt
	ON tt.Username = gut.Username
	JOIN StartTimes_CTE st
	ON st.Username = gut.Username
	JOIN EndTimes_CTE et
	ON et.Username = gut.Username
	WHERE gut.TotalActiveTime > 0
	ORDER BY Percentage DESC


--End of Code
END;


GO


