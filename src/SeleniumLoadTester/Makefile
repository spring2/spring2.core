#
# Location of the source and destination folders.
#
RM		:= rm -rf
MKDIR		:= mkdir -p

SRCDIR		:= ..
LIBDIR		:= ../../lib
BINDIR		:= ../../bin
RELEASEDIR	:= ../../release
BUILDDIR	:= ../../build
TESTDIR		:= ../../build/Test
DTGDIR		:= $(BINDIR)/DTG

NANT		:= $(BINDIR)/NAnt/nant
NUNIT		:= $(BINDIR)/NUnit/nunit-console

all:

modules := $(shell /usr/bin/find . -name *.mk)
include $(modules)

.DELETE_ON_ERROR:

all: build_db build

.PHONY: build
build: Web/version.aspx Web/AssemblyVersionInfo.cs
	$(NANT)

.PHONY: clean
clean:
	$(RM) $(BUILDDIR)/*

.PHONY: precommit
precommit: gen regress

.PHONY: regress
regress: build_db build

.PHONY: release
release: package tagversion

.PHONY: package
package: clean build
	$(MKDIR) $(RELEASEDIR)

.PHONY: tagversion
tagversion:
	cd ..; cvs tag RELEASE_$(subst .,_,$(shell ./getversion.sh))

.PHONY: updateversion
updateversion: 
	$(BINDIR)/UpdateVersion -b Increment -i AssemblyVersionInfo.cs -o AssemblyVersionInfo.cs
	cp AssemblyVersionInfo.cs Web/AssemblyVersionInfo.cs
	cp AssemblyVersionInfo.cs Win/AssemblyVersionInfo.cs
	cp AssemblyVersionInfo.cs Util/PostBatches/AssemblyVersionInfo.cs
	cp AssemblyVersionInfo.cs Util/Encrypt/AssemblyVersionInfo.cs
	cvs commit -m "update to next version" AssemblyVersionInfo.cs
	cvs commit -m "update to next version" Web/AssemblyVersionInfo.cs
	cvs commit -m "update to next version" Win/AssemblyVersionInfo.cs
	cvs commit -m "update to next version" Util/PostBatches/AssemblyVersionInfo.cs
	cvs commit -m "update to next version" Util/Encrypt/AssemblyVersionInfo.cs

Web/version.aspx: AssemblyVersionInfo.cs
	sed -e "s/##_DATE_##/$(shell date)/" -e "s/##_BUILD_##/$(shell getversion.sh)/" -e "s/##_COMPUTER_##/$(COMPUTERNAME)/" -e "s/##_USER_##/$(USERNAME)/" Web/version.thtml > $@

%AssemblyVersionInfo.cs: AssemblyVersionInfo.cs
	cp $< $@

$(BUILDDIR):
	$(MKDIR) $(BUILDDIR)

.PHONY: debug
debug:
	@echo MODULES = $(MODULES)

gen_xsd:
	xsd DataObject/*.xsd /d /o:DataObject

JAR		?= $(BINDIR)/jar.exe
RELEASE_REVISION	:= 1
BRANCH_TAG 		:= trunk

package_sql: scriptorder combine_sql_scripts
	$(JAR) -cMvf $(RELEASEDIR)/Spring2.Core.SeleniumLoadTester.sql.zip ScriptOrder.txt combined_sql_scripts.sql @ScriptOrder.txt
	$(RM) ScriptOrder.txt combined_sql_scripts.sql

.PHONY: scriptorder
scriptorder:
	$(RM) ScriptOrder.txt combined_sql_scripts.sql
	$(RM) s.txt script.txt o.txt order.txt so.txt so1.txt svnlog.xml svnlog.csv
	svn log -r ${RELEASE_REVISION}:BASE --verbose --xml --stop-on-copy sql > svnlog.xml
	../../bin/xml2csv.exe svnlog.xml svnlog.csv ",path" -N -S
	gawk -F"," "{print $2}" svnlog.csv | sed -e "s/,\/$(BRANCH_TAG)\/Core\/src\/SeleniumLoadTester\///" | grep -v path > script.txt
	#ANY
	make -nB build_db > o.txt
	gawk '/sqlcmd/ {print $$14}' o.txt > order.txt
	dos2unix order.txt
	-- for line in $$(cat order.txt); do grep $$line script.txt >> so.txt; done
	# strip out the blank lines 
	grep -v "^$$" so.txt > so1.txt
	# make sure the file has dos line endings,
	unix2dos -D so1.txt
	# strip off the last end of line
	awk 'NR > 1 { print h } { h = $$0 } END { ORS = ""; print h }' so1.txt > ScriptOrder.txt
	dos2unix ScriptOrder.txt
	$(RM) s.txt script.txt o.txt order.txt so.txt so1.txt svnlog.xml svnlog.csv

combine_sql_scripts:
	$(RM) combined_sql_scripts.sql
	-- for line in $$(cat ScriptOrder.txt); do printf '\n-- START OF: ' >> combined_sql_scripts.sql; echo $$line >> combined_sql_scripts.sql; printf '\n' >> combined_sql_scripts.sql; cat $$line >> combined_sql_scripts.sql; printf '\n-- END OF: ' >> combined_sql_scripts.sql; echo $$line >> combined_sql_scripts.sql; printf '\nGO\n' >> combined_sql_scripts.sql; done
	cp combined_sql_scripts.sql $(RELEASEDIR)/Spring2.Core.SeleniumLoadTester.sql
