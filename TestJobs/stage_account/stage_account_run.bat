%~d0
cd %~dp0
java -Xms256M -Xmx1024M -cp .;../lib/routines.jar;../lib/commons-io-2.4-6.0.0.jar;../lib/dom4j-1.6.1.jar;../lib/guava-20.0-6.0.0-SNAPSHOT.jar;../lib/log4j-1.2.16.jar;../lib/mssql-jdbc.jar;../lib/opencsv-3.10.jar;../lib/talend_DB_mssqlUtil-1.2-20171017.jar;stage_account_0_1.jar; local_project.stage_account_0_1.stage_account --context=Default %* 