

USE ETL_System

CREATE VIEW dbo.JobsCatalogueDisplay AS

WITH topsinstances as (SELECT job_id,max(job_instance_id) job_instance_id FROM ETL_System.dbo.JobInstances GROUP BY job_id)
,topschanges as (SELECT max(sys_change_id) sys_change_id, job_id from ETL_System.dbo.JobChanges GROUP BY job_id)
,lastinstance as (SELECT * from ETL_System.dbo.JobInstances ji WHERE EXISTS (SELECT 1 FROM topsinstances t WHERE t.job_instance_id = ji.job_instance_id))
,lastchange as (SELECT * FROM ETL_System.dbo.JobChanges jc WHERE EXISTS (SELECT 1 FROM topschanges t WHERE t.sys_change_id = jc.sys_change_id))
,nextexec as (SELECT max(next_execution) next_execution,job_id FROM ETL_System.dbo.JobSchedules GROUP BY job_id)
SELECT
	 j.Job_Id
	,j.name as Name
	,jt.job_type_id
	,jt.type_name as Job_Type
	,CASE WHEN j.job_type_id = 1 THEN convert(varchar(25),n.next_execution,121) ELSE cast('Checkpoint' as varchar(25)) END as Next_Execution
	,CASE WHEN cast(j.data_chceckpoint as varchar(25)) IS NULL THEN convert(varchar(25),j.time_checkpoint,121) ELSE cast(j.data_chceckpoint as varchar(25)) END AS [Checkpoint]
	,CASE j.is_failed WHEN 1 THEN 'TRUE' ELSE 'FALSE' END AS Is_Failed
	,li.start_timestamp as When_Last_Executed
	,DATEDIFF(second,li.end_timestamp,li.start_timestamp) Duration_Last_Executed
	,j.max_try_count as Max_Attempts
	,j.current_failed_count as Current_Failed_Attempts
	,u.login as Last_Changed_By
	,li.started_by Last_Started_By
FROM ETL_System.dbo.Jobs j
LEFT JOIN lastinstance li on li.job_id = j.job_id
LEFT JOIN lastchange lc on lc.job_id = j.job_id
LEFT JOIN ETL_System.dbo.[User] u on u.user_id = lc.user_id
LEFT JOIN ETL_System.dbo.JobTypes jt on j.job_type_id = jt.job_type_id
LEFT JOIN nextexec n ON j.job_id = n.job_id

SELECT 