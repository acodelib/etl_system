
USE ETL_System;

CREATE TABLE dbo.Jobs ( 
	job_id               int NOT NULL ,--  IDENTITY,
	last_instance_id     int    ,
	job_type_id          int    ,
	sys_change_id        int    ,
	last_instance_timestamp datetime    ,
	name                 varchar(250)  UNIQUE,
	executable_name      varchar(512)   ,
	max_try_count        int    ,
	current_failed_count int    ,	
	delay_seconds        int    ,
	latency_alert_seconds int    ,
	data_chceckpoint     bigint    ,
	time_checkpoint      datetime    ,
	notifications_list   varchar(4096),
	is_failed            bit    ,
	is_active            bit	,
	is_paused            bit
	CONSTRAINT Pk_Jobs_job_id PRIMARY KEY  ( job_id )
 );

 CREATE TABLE dbo.JobInstances ( 
	job_instance_id      int NOT NULL  IDENTITY,
	job_id               int    ,
	result               varchar(128),
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
	next_execution       int	,
	CONSTRAINT Pk_Job_Schedules_job_schedule_id PRIMARY KEY  ( job_schedule_id )
 );

 CREATE TABLE dbo.ScheduleTypes ( 
	schedule_type_id     int NOT NULL   IDENTITY,
	schedule_type_name   varchar(200),
	frequency_seconds    bigint  ,
	execution_window_seconds int ,
	CONSTRAINT Pk_Schedules_schedule_id PRIMARY KEY  ( schedule_type_id )
 );

 CREATE TABLE dbo.JobChanges ( 
	sys_change_id        int NOT NULL,--  IDENTITY,
	job_id               int    ,
	user_id              int    ,
	change_timestamp     datetime    ,
	CONSTRAINT Pk_JobChanges_sys_change_id PRIMARY KEY  ( sys_change_id )
 );

 CREATE TABLE dbo.[User] ( 
	user_id              int NOT NULL   IDENTITY,
	role_id				 int NOT NULL,
	login                varchar(256)   UNIQUE ,
	password             varchar(256)    ,
	last_conn_timestamp  datetime    ,
	is_active            bit    ,
	CONSTRAINT Pk_User_user_id PRIMARY KEY  ( user_id )
 );


 CREATE TABLE dbo.UserRoles ( 
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
 
 CREATE NONCLUSTERED INDEX IX_jobinstances_jid  ON ETL_System.dbo.JobInstances (job_id); 
 CREATE NONCLUSTERED INDEX IX_jobchanges_jid    ON ETL_System.dbo.JobChanges (job_id); 

 --- CREATE default admin:
 INSERT INTO dbo.[User]
 SELECT 1,'admin','208512264222772174181102151942010236531331277169151',GETDATE(),1
 UNION ALL
 SELECT 1,'sys','208512264222772174181102151942010236531331277169151',GETDATE(),1;

 INSERT INTO dbo.UserRoles
 SELECT 'admin','This is the master role for the system'
 UNION ALL
 SELECT 'developer','Standard access role';

 INSERT INTO dbo.JobChanges
 SELECT 1,1,1,GETDATE();

 

 INSERT INTO dbo.ScheduleTypes
 SELECT 'Hourly',3600,60
 UNION ALL
 SELECT 'Weekly',6004800,60
 UNION ALL
 SELECT 'Daily',86400,60;

 INSERT INTO JobTypes
 SELECT 'Schedule','Job is meant to be run by calendar schedle type. Types are defined in ScheduleTypes'
 UNION ALL
 SELECT 'Dependency','Job is to be run in the checkpoint dependencies system';


 INSERT INTO dbo.Jobs
 SELECT 1,11,1,NULL,'2018-01-12 20:00:00','dummy job','dummy_job.bat',5,0,0,20,NULL,'2018-01-11 22:00:00','andrei_gurguta@yahoo.com',0,0,0
 UNION ALL
 SELECT 2,12,2,NULL,'2018-01-12 20:00:00','dummy job 1','dummy_job_1.bat',5,0,0,20,2134324,NULL,'andrei_gurguta@yahoo.com',0,0,0;

 INSERT INTO dbo.DependencyTypes
 SELECT 'Data','Resolves Checkpoints for jobs that need to run prior to the target Job'
 UNION ALL
 SELECT 'Execution','Resolves Checkpoitns for jobs that need to run after the target Job';