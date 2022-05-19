
use [sep4_dwh]
/****** Load to stage ClimateDevice  ******/
truncate table [stage].[DimClimateDevice] 
insert into [stage].[DimClimateDevice] 
([ClimateDeviceId]
)
SELECT [ClimateDeviceId]
  FROM [sep4_source].[dbo].[Devices]


  /******  Load to stage Room  ******/
  truncate table  [stage].[DimRoom]
INSERT INTO [stage].[DimRoom]
      ([RoomId],
	   [RoomName])
SELECT [RoomId]
      ,[RoomName]
  FROM [sep4_source].[dbo].[Rooms]
      
 


  /****** Load to stage Settings  ******/
truncate table [stage].[DimSettings]
Insert into [stage].[DimSettings]
      ([SettingsId]
      ,[Co2Threshold]
      ,[HumidityThreshold]
      ,[TargetTemperature]
      ,[TemperatureMargin])
SELECT [SettingId]
      ,[Co2Threshold]
      ,[HumidityThreshold]
      ,[TargetTemperature]
      ,[TemperatureMargin]
  FROM [sep4_source].[dbo].[Settings]


  /****** Load to stage Measurement  ******/

truncate table [stage].[FactMeasurement]
insert into [stage].[FactMeasurement]
      ([RoomId]
	  ,[SettingsId]
	  ,[ClimateDeviceId]
	  ,[CO2_In_PPM]
	  ,[Temperature_In_C]
	  ,[Humidity_In_Percent]
	  ,[Measurement_Time]
	  ,[Measurement_Date]
      )
SELECT r.[RoomId]
	  ,d.[SettingsSettingId]
	  ,d.[ClimateDeviceId]
	  ,[Co2]
      ,[Temperature]
      ,[Humidity]
	  ,CAST([Timestamp] as time)  --Isolating time value from timestamp
	  ,CAST([Timestamp] as date)  --Isolating date value from timestamp

  FROM [sep4_source].[dbo].[Measurements] m join [sep4_source].[dbo].[Devices] d on m.ClimateDeviceId = d.ClimateDeviceId
  join [sep4_source].[dbo].[Rooms] r on d.RoomId = r.RoomId join [sep4_source].[dbo].[Settings] s on d.SettingsSettingId = s.SettingId
