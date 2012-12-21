RM			:= rm -rf
BINDIR		:= ../../bin
RELEASEDIR	:= ../../release

JAR		?= $(BINDIR)/jar.exe
RELEASE_REVISION	:= 1
BRANCH_TAG 		:= trunk
7ZIP		:= $(BINDIR)/7za.exe

include sql/*.mk

package_sql: scriptorder combine_sql_scripts
	$(7ZIP) a $(RELEASEDIR)/Spring2.Core.Configuration.sql.7z ScriptOrder.txt combined_sql_scripts.sql @ScriptOrder.txt
	$(RM) ScriptOrder.txt combined_sql_scripts.sql

.PHONY: scriptorder
scriptorder:
	$(RM) ScriptOrder.txt combined_sql_scripts.sql
	$(RM) s.txt script.txt o.txt order.txt so.txt so1.txt svnlog.xml svnlog.csv
	svn log -r ${RELEASE_REVISION}:BASE --verbose --xml --stop-on-copy sql > svnlog.xml
	../../bin/xml2csv.exe svnlog.xml svnlog.csv ",path" -N -S
	gawk -F"," "{print $2}" svnlog.csv | sed -e "s/,\/$(BRANCH_TAG)\/Core\/src\/Configuration\///" | grep -v path > script.txt
	#ANY
	make -nB build_db > o.txt
	gawk '/sqlcmd/ {print $$14}' o.txt > order.txt
	dos2unix order.txt
	-- for line in $$(cat order.txt); do grep $$line script.txt >> so.txt; done
	# strip out the blank lines 
	grep -v "^$$" so.txt > so1.txt
	# make sure the file has dos line endings,
	unix2dos so1.txt
	# strip off the last end of line
	awk 'NR > 1 { print h } { h = $$0 } END { ORS = ""; print h }' so1.txt | uniq > ScriptOrder.txt
	dos2unix ScriptOrder.txt
	#$(RM) s.txt script.txt o.txt order.txt so.txt so1.txt svnlog.xml svnlog.csv

combine_sql_scripts:
	$(RM) combined_sql_scripts.sql
	-- for line in $$(cat ScriptOrder.txt); do printf '\n-- START OF: ' >> combined_sql_scripts.sql; echo $$line >> combined_sql_scripts.sql; printf '\n' >> combined_sql_scripts.sql; cat $$line >> combined_sql_scripts.sql; printf '\n-- END OF: ' >> combined_sql_scripts.sql; echo $$line >> combined_sql_scripts.sql; printf '\nGO\n' >> combined_sql_scripts.sql; done
	cp combined_sql_scripts.sql $(RELEASEDIR)/Spring2.Core.Configuration.sql
