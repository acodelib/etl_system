USE master;
IF (EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE name = 'ETL_System')) 
	BEGIN
		DROP DATABASE [ETL_System];
	END
ELSE 
	CREATE DATABASE [ETL_System] CONTAINMENT = NONE;

