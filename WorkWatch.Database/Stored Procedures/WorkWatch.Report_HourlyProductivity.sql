USE [WorkWatch];
GO

/****** Object:  StoredProcedure [WorkWatch].[Input_I]    Script Date: 08/05/2019 8:54:33 AM ******/
SET ANSI_NULLS ON;
GO

SET QUOTED_IDENTIFIER ON;
GO




/*
=============================================
EXEC WorkWatch.Report_HourlyProductivity @Date = '2019-08-05'


CreatedBy	: BennettR, NicB
CreatedOn	: 20190805    
Description	: Get active versus inactive time for each hour in the given day

ModifyOn	: 
ModifyBy	: 
Description	: 


=============================================
*/
CREATE OR ALTER PROCEDURE [WorkWatch].[Report_HourlyProductivity]
    @Date DATE
AS
SET NOCOUNT ON;
SET XACT_ABORT ON;


BEGIN

    --Beginning of Code

DECLARE @TimePeriod TABLE (StartTime DATETIME2, EndTime DATETIME2);
DECLARE @HourlyProductivity TABLE (
	Username VARCHAR(256), 
	ActiveTime INT, 
	TotalTime INT, 
	Percentage DECIMAL
)
DECLARE @OutputHourlyProductivity TABLE (
	Username VARCHAR(256), 
	ActiveTime INT, 
	TotalTime INT, 
	Percentage DECIMAL,
	HourStartTime TIME,
	HourEndTime TIME
)
DECLARE @PeriodStartTime DATETIME2;
DECLARE @PeriodEndTime DATETIME2;

INSERT INTO @TimePeriod (StartTime, EndTime)
SELECT 
	DATEADD(HOUR, AddedTime.n, CAST(@Date AS DATETIME2)) [StartTime],
	DATEADD(HOUR, AddedTime.n + 1, CAST(@Date AS DATETIME2)) [EndTime]
FROM (VALUES(0),(1),(2),(3),(4),(5),(6),(7),(8),(9),(10),(11),(12),(13),(14),(15),(16),(17),(18),(19),(20),(21),(22),(23)) AddedTime(n)

DECLARE hour_cursor CURSOR  
    FOR SELECT StartTime, EndTime FROM @TimePeriod

OPEN hour_cursor  
FETCH NEXT FROM hour_cursor INTO @PeriodStartTime, @PeriodEndTime; 

WHILE @@FETCH_STATUS = 0  
BEGIN  
      INSERT INTO @HourlyProductivity
      (
          Username,
          ActiveTime,
          TotalTime,
          Percentage
      )
      EXEC WorkWatch.Report_PeriodProductivity @StartTime = @PeriodStartTime,
                                               @EndTime = @PeriodEndTime

	INSERT INTO @OutputHourlyProductivity
	(
	    Username,
	    ActiveTime,
	    TotalTime,
	    Percentage,
	    HourStartTime,
	    HourEndTime
	)
	SELECT
		php.Username,
		php.ActiveTime,
		php.TotalTime,
		php.Percentage,
	    @PeriodStartTime AS HourStarTime,
	    @PeriodEndTime AS HourEndTime
	FROM @HourlyProductivity php

	DELETE FROM @HourlyProductivity

    FETCH NEXT FROM hour_cursor INTO @PeriodStartTime, @PeriodEndTime;
END 
 
CLOSE hour_cursor  
DEALLOCATE hour_cursor 

SELECT ohp.Username,
       ohp.ActiveTime,
       ohp.TotalTime,
       ohp.Percentage,
       ohp.HourStartTime,
       ohp.HourEndTime
FROM @OutputHourlyProductivity ohp
ORDER BY ohp.Username, ohp.HourStartTime ASC

--End of Code
END;


GO


