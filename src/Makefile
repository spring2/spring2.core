#
# Makefile for Spring2.Core
#
RM		:= rm -rf
MKDIR		:= mkdir -p
NUNIT		:= ../bin/NUnit/nunit-console.exe

SRCDIR		:= ../src
LIBDIR		:= ../lib
BINDIR		:= ../bin
DOCDIR		:= ../docs
RELEASEDIR	:= ../release
BUILDDIR	:= ../build
NANT		:= ../bin/NAnt/nant.exe

RELEASE_REVISION	:= 2247
GETVERSION	:= $(BINDIR)/getversion.sh

include Mail/sql/*.mk
include Configuration/sql/*.mk
include Navigation/sql/*.mk
include PerformanceMonitor/sql/*.mk
include ResourceManager/sql/*.mk

all: build

.DELETE_ON_ERROR:

.PHONY: clean
clean:
	$(RM) $(BUILDDIR)/*
	$(RM) $(DOCDIR)/NCover
	$(RM) obj
	$(RM) bin

gen:
	cd Mail; ../$(BINDIR)/DTG/DataTierGenerator.exe DataTierGenerator.config.xml
	cd Geocode; ../$(BINDIR)/DTG/DataTierGenerator.exe DataTierGenerator.config.xml
	cd Navigation; ../$(BINDIR)/DTG/DataTierGenerator.exe DataTierGenerator.config.xml
	cd ResourceManager; ../$(BINDIR)/DTG/DataTierGenerator.exe DataTierGenerator.config.xml

.PHONY: build_db
build_db: build_db_mail build_db_config build_db_nav build_db_pm build_db_rscman

.PHONY: clean_db
clean_db: clean_db_mail clean_db_config clean_db_nav clean_db_pm clean_db_rscman

.PHONY: build
build:
	${NANT} build

clean_release:
	$(RM) ../release/*	

.PHONY: regress
regress: build

.PHONY: regressonly
regressonly:
	export COMPLUS_Version=net-1.1
	cp Payment/Test/App.config ../build/net-1.1/Spring2.Core.Payment.Test.dll.config
	mkdir -p ../build/net-1.1/certs
	cp ../lib/certs/*.0 ../build/net-1.1/certs
	$(BINDIR)/NUnit/nunit-console /xml=ccnet-nunit11-results.xml ../build/net-1.1/Spring2.Core.Test.dll 
	$(BINDIR)/NUnit/nunit-console /xml=ccnet-nunit11withtypes-results.xml ../build/net-1.1/WithTypes/Spring2.Core.WithTypesTest.dll
	#$(BINDIR)/NUnit/nunit-console /xml=ccnet-nunit11-AddressValidation-results.xml ../build/net-1.1/Spring2.Core.AddressValidation.Test.dll
	$(BINDIR)/NUnit/nunit-console /xml=ccnet-nunit11-Payment-results.xml ../build/net-1.1/Spring2.Core.Payment.Test.dll
	#$(BINDIR)/NUnit/nunit-console /xml=ccnet-nunit11-ResouceManager.AcceptanceTest-results.xml ../build/net-1.1/Spring2.Core.ResourceManager.AcceptanceTest.dll
	#$(BINDIR)/NUnit/nunit-console /xml=ccnet-nunit11-Tax-results.xml ../build/net-1.1/Spring2.Core.Tax.Test.dll

	export COMPLUS_Version=net-2.0
	$(BINDIR)/NUnit/nunit-console /xml=ccnet-nunit20-results.xml ../build/net-2.0/Spring2.Core.Test.dll
	$(BINDIR)/NUnit/nunit-console /xml=ccnet-nunit20withtypes-results.xml ../build/net-2.0/WithTypes/Spring2.Core.WithTypesTest.dll
	#$(BINDIR)/NUnit/nunit-console /xml=ccnet-nunit20-results.xml ../build/net-2.0/Spring2.Core.Test.Ajax.dll

.PHONY: precommit
precommit: build regress

changelog:
	@updatechangelog.sh
	@cp ../ChangeLog.txt $(BUILDDIR)

docs:
	@chmod a+x ../bin/NDoc2/*
	@rm -rf ../docs/api
	../bin/NDoc2/NDocConsole.exe -documenter=MSDN-CHM -project=Spring2.Core.ndoc -HtmlHelpName=Spring2.Core-$(shell $(GETVERSION)) -Version=$(shell $(GETVERSION))
	

package: clean build regress changelog docs packageonly

packageonly:
	rm -rf ${BUILDDIR}/Core
	#cp $(DOCDIR)/api/Spring2.Core.chm $(BUILDDIR)
	#cp $(DOCDIR)/api/Spring2.Core.chm $(RELEASEDIR)/Spring2.Core-$(shell getversion.sh).chm
	#cp ../ChangeLog.txt $(RELEASEDIR)/ChangeLog-$(shell getversion.sh).txt
	#jar -cMvf $(RELEASEDIR)/Spring2.Core.$(shell getversion.sh)-net-1.1.zip -C $(BUILDDIR)/net-1.1 .
	jar -cMvf $(RELEASEDIR)/Spring2.Core.$(shell getversion.sh)-net-2.0.zip -C $(BUILDDIR)/net-2.0 .
	${NANT} -D:version=$(shell ./getversion.sh) package_src

release: package
	cvs tag RELEASE_$(subst .,_,$(shell ./getversion.sh))
	../bin/cvsadd.sh $(RELEASEDIR) Spring2.Core.$(shell getversion.sh)-net-1.1.zip "new release version"
	../bin/cvsadd.sh $(RELEASEDIR) Spring2.Core.$(shell getversion.sh)-net-2.0.zip "new release version"
	../bin/cvsadd.sh $(RELEASEDIR) Spring2.Core-$(shell getversion.sh).chm "new release version"
	../bin/cvsadd.sh $(RELEASEDIR) ChangeLog-$(shell getversion.sh).txt "new release version"
	$(BINDIR)/UpdateVersion -b Increment -i AssemblyVersionInfo.cs -o AssemblyVersionInfo.cs
	cvs commit -m "update to next version" AssemblyVersionInfo.cs

package_sql: scriptorder combine_sql_scripts
	$(JAR) -cMvf $(RELEASEDIR)/${CLIENT_NAME}.Dss.sql-$(shell $(GETVERSION)).zip ScriptOrder.txt combined_sql_scripts.sql @ScriptOrder.txt
	$(RM) ScriptOrder.txt combined_sql_scripts.sql

.PHONY: scriptorder
scriptorder:
	$(RM) ScriptOrder.txt
	#CVS
	#cvs rdiff -s -r $(RELEASE_TAG) -r $(BRANCH_TAG) $(CVS_MODULE)/src/sql > s.txt
	## 26 is the lenght of the module to parse ouf of s.txt
	#gawk '/changed from/ {print substr($$2,25)} /is new/ {print substr($$2,25)}' s.txt > script.txt
	#SVN
	svn log -r ${RELEASE_REVISION}:BASE --verbose --xml --stop-on-copy ./ResourceManager/sql > svnlog.xml
	#svn log -r ${RELEASE_REVISION} --verbose ${SVN_URL}${CLIENT_NAME}/trunk/Dss/src/sql
	#chmod a+x ../bin/xml2csv.exe
	../bin/xml2csv.exe svnlog.xml svnlog.csv ",path" -N -S
	gawk -F"," "{print $2}" svnlog.csv | sed -e "s/,\/$(BRANCH_TAG)\/Dss\/src\///" | grep -v path > script.txt
	#ANY
	make -nB build_db > o.txt
	gawk '/sqlcmd/ {print $$14}' o.txt > order.txt
	-- for line in $$(cat order.txt); do grep $$line script.txt >> so.txt; done
	# strip out the blank lines 
	grep -v "^$$" so.txt > so1.txt
	# make sure the file has dos line endings,
	unix2dos -D so1.txt
	# strip off the last end of line
	awk 'NR > 1 { print h } { h = $$0 } END { ORS = ""; print h }' so1.txt > ScriptOrder.txt
	dos2unix ScriptOrder.txt
	$(RM) s.txt script.txt o.txt order.txt so.txt so1.txt svnlog.xml svnlog.csv

.PHONY: ncover
ncover: 
	rm -rf ../docs/NCover
	mkdir -p ../docs/NCover
	../bin/NCover/NCover.Console.exe /l ../docs/NCover/Coverage.log /o ../docs/NCover/Coverage.xml /c ../bin/nunit/nunit-console.exe "../build/Test/Spring2.Core.Test.dll /noshadow"
	
ncoverreports:
	@#../bin/nxslt ../docs/NCover/Coverage.xml ../docs/NCover/Coverage.xsl  -o ../docs/NCover/Coverage.html
	../bin/NCover/NCoverReport.exe ../docs/NCover/Coverage.xml
	cp ../bin/NCover/NCoverReport.css ../docs/NCover/

.PHONY: fxcop
fxcop:
	mkdir -p ../docs/FxCop
	-fxcopcmd /p:Spring2.Core.FxCop /out:../docs/FxCop/Spring2.Core.FxCop.xml

fixLineTermination:
	find . -type f -name '*.cs' -print -exec unix2dos -D --safe \{\} \;
	find . -type f -name '*.aspx' -print -exec unix2dos -D --safe \{\} \;
	find . -type f -name '*.ascx' -print -exec unix2dos -D --safe \{\} \;
	find . -type f -name '*.css' -print -exec unix2dos -D --safe \{\} \;
	find . -type f -name '*.js' -print -exec unix2dos -D --safe \{\} \;

cleanlogs:
	rm -rf *.log
	rm -rf nvelocity.log.*

package_cl:
	#-cvs log -r${BRANCH_TAG} | perl ../bin/cvs2cl.pl --stdin -f ../release/ChangeLog-$(shell $(GETVERSION)).txt
	#svn log --xml --verbose | xsltproc ../bin/svn2cl/svn2cl.xsl - > ChangeLog
	../bin/svn2cl/svn2cl.sh -r ${RELEASE_REVISION}:BASE -o ../release/ChangeLog-$(shell $(GETVERSION)).txt
	
