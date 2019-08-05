USE [WorkWatch];
GO

/****** Object:  StoredProcedure [WorkWatch].[Input_I]    Script Date: 08/05/2019 8:54:33 AM ******/
SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO




/*
=============================================
EXEC WorkWatch.Report_PeriodProductivity @StartTime = '2019-08-05 9:00:00', @EndTime = '2019-08-05 10:00:00'


CreatedBy	: BennettR, NicB
CreatedOn	: 20190805    
Description	: Get active versus inactive time within a time period.

ModifyOn	: 
ModifyBy	: 
Description	: 


=============================================
*/
CREATE OR ALTER PROCEDURE [WorkWatch].[Report_PeriodProductivity]
    @StartTime DATETIME2,
    @EndTime DATETIME2
AS
SET NOCOUNT ON;
SET XACT_ABORT ON;


BEGIN

    --Beginning of Code
;
WITH InputsInTimePeriod_CTE (InputID, UserID, StartTime, EndTime)
AS (
	SELECT 
		i.InputID,
		i.UserID,
		IIF(@StartTime > i.StartTime, @StartTime, i.StartTime) [StartTime],
		IIF(@EndTime < i.EndTime, @EndTime, i.EndTime) [EndTime]
	FROM WorkWatch.Input i
		WHERE (i.StartTime >= @StartTime AND i.StartTime <= @EndTime)
		OR (i.EndTime >= @StartTime AND i.EndTime <= @EndTime)
		OR (i.StartTime < @StartTime AND i.EndTime > @EndTime)
),
InputsForUserInTimePeriod_CTE (Username, ActiveTime)
AS (
	SELECT 
		u.Username,
		SUM(DATEDIFF(SECOND, iitp.StartTime, iitp.EndTime)) [ActiveTime]
	FROM InputsInTimePeriod_CTE iitp
	JOIN WorkWatch.[User] u ON u.UserID = iitp.UserID
	GROUP BY u.Username
)
SELECT
	ifuitp.Username,
	ifuitp.ActiveTime,
	DATEDIFF(SECOND, @StartTime, @EndTime) [TotalTime],
	CONVERT(DECIMAL(16, 4), ifuitp.ActiveTime) / DATEDIFF(SECOND, @StartTime, @EndTime) * 100 [Percentage]
FROM InputsForUserInTimePeriod_CTE ifuitp

--End of Code
END;


GO


