#!/bin/sh
cd `dirname $0`
ROOT_PATH=`pwd`
java -Xms256M -Xmx1024M -cp .:$ROOT_PATH:$ROOT_PATH/../lib/routines.jar:$ROOT_PATH/../lib/log4j-1.2.16.jar:$ROOT_PATH/../lib/dom4j-1.6.1.jar:$ROOT_PATH/../lib/guava-20.0-6.0.0-SNAPSHOT.jar:$ROOT_PATH/../lib/opencsv-3.10.jar:$ROOT_PATH/../lib/commons-io-2.4-6.0.0.jar:$ROOT_PATH/../lib/mssql-jdbc.jar:$ROOT_PATH/../lib/talend_DB_mssqlUtil.jar:$ROOT_PATH/source_contacts_0_1.jar: veeam.source_contacts_0_1.source_contacts --context=Default "$@" 