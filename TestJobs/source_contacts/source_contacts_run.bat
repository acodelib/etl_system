%~d0
cd %~dp0
java -Xms256M -Xmx1024M -cp .;../lib/routines.jar;../lib/log4j-1.2.16.jar;../lib/dom4j-1.6.1.jar;../lib/guava-20.0-6.0.0-SNAPSHOT.jar;../lib/opencsv-3.10.jar;../lib/commons-io-2.4-6.0.0.jar;../lib/mssql-jdbc.jar;../lib/talend_DB_mssqlUtil.jar;source_contacts_0_1.jar; veeam.source_contacts_0_1.source_contacts --context=Default %* 