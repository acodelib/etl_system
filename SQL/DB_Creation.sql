USE master;
IF (EXISTS (SELECT name FROM master.dbo.sysdatabases WHERE (name = 'ETL_System')))
		DROP DATABASE ETL_System;
CREATE DATABASE [ETL_System] CONTAINMENT = NONE;

USE ETL_System;

CREATE TABLE dbo.Jobs ( 
	job_id               int NOT NULL   IDENTITY,
	last_instance_id     int    ,
	job_type_id          int    ,
	sys_change_id        int    ,
	last_instance_timestamp datetime    ,
	name                 varchar(250)    ,
	execution_path       varchar(2408)    ,
	max_try_count        int    ,
	is_failed            int    ,
	delay_seconds        int    ,
	latency_alert_seconds int    ,
	data_chceckpoint     bigint    ,
	time_checkpoint      datetime    ,
	CONSTRAINT Pk_Jobs_job_id PRIMARY KEY  ( job_id )
 );

 CREATE TABLE dbo.JobInstances ( 
	job_instance_id      int NOT NULL   IDENTITY,
	job_id               int    ,
	result               varchar(128)    ,
	start_timestamp      datetime    ,
	end_timestamp        datetime    ,
	from_time_checkpoint datetime    ,
	to_time_checkpoint   datetime    ,
	from_data_checkpoint bigint    ,
	to_data_chcekpoint   bigint    ,
	job_execution_path   varchar(2048)    ,
	worker               int    ,
	started_by           int    ,
	CONSTRAINT Pk_JobInstances_job_instance_id PRIMARY KEY  ( job_instance_id )
 );

 CREATE TABLE dbo.JobSchedules ( 
	job_schedule_id      int NOT NULL   IDENTITY,
	job_id               int    ,
	schedule_type_id     int    ,
	CONSTRAINT Pk_Job_Schedules_job_schedule_id PRIMARY KEY  ( job_schedule_id )
 );

 CREATE TABLE dbo.ScheduleTypes ( 
	schedule_type_id     int NOT NULL   IDENTITY,
	timestamp_anchor     datetime    ,
	frequency_seconds    bigint    ,
	execution_window_seconds int    ,
	CONSTRAINT Pk_Schedules_schedule_id PRIMARY KEY  ( schedule_type_id )
 );

 CREATE TABLE dbo.JobChanges ( 
	sys_change_id        int NOT NULL   IDENTITY,
	job_id               int    ,
	user_id              int    ,
	change_timestamp     datetime    ,
	CONSTRAINT Pk_JobChanges_sys_change_id PRIMARY KEY  ( sys_change_id )
 );

 CREATE TABLE dbo.[User] ( 
	user_id              int NOT NULL   IDENTITY,
	login                varchar(256)    ,
	password             varchar(64)    ,
	last_conn_timestamp  datetime    ,
	is_active            bit    ,
	CONSTRAINT Pk_User_user_id PRIMARY KEY  ( user_id )
 );

 CREATE TABLE dbo.UserRoles ( 
	user_role_id         int NOT NULL   IDENTITY,
	user_id              int    ,
	role_id              int    ,
	CONSTRAINT Pk_UserRoles_user_role_id PRIMARY KEY  ( user_role_id )
 );

 CREATE TABLE dbo.Roles ( 
	role_id              int NOT NULL   IDENTITY,
	name                 varchar(100)    ,
	description          varchar(256)    ,
	CONSTRAINT Pk_Roles_role_id PRIMARY KEY  ( role_id )
 );


 CREATE TABLE dbo.JobTypes ( 
	job_type_id          int NOT NULL   IDENTITY,
	type_name            varchar(100)    ,
	type_description     varchar(512)    ,
	CONSTRAINT Pk_JobTypes_job_type_id PRIMARY KEY  ( job_type_id )
 );

 CREATE TABLE dbo.JobDependency ( 
	job_dependency_id    int NOT NULL   IDENTITY,
	job_id               int    ,
	depending_job_id     int    ,
	dependency_type_id   int    ,
	CONSTRAINT Pk_Job_Dependency_job_dependency_id PRIMARY KEY  ( job_dependency_id )
 );

 CREATE TABLE dbo.DependencyTypes ( 
	dependency_type_id   int NOT NULL   IDENTITY,
	name                 varchar(100)    ,
	description          varchar(512)    ,
	CONSTRAINT Pk_Dependency_Types_dependency_type_id PRIMARY KEY  ( dependency_type_id )
 );

 --- CREATE default admin:
 INSERT INTO dbo.[User]
 SELECT 'admin',hashbytes('SHA1','admin'),GETDATE(),1; 

 INSERT INTO dbo.Roles
 SELECT 'admin','This is the master role for the system';

 INSERT INTO dbo.UserRoles
 SELECT 1,1;