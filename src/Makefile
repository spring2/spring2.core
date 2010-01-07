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
	cd Currency; ../$(BINDIR)/DTG/DataTierGenerator.exe DataTierGenerator.config.xml
	cd Navigation; ../$(BINDIR)/DTG/DataTierGenerator.exe DataTierGenerator.config.xml
	cd ResourceManager; ../$(BINDIR)/DTG/DataTierGenerator.exe DataTierGenerator.config.xml

.PHONY: build_db
build_db:
	cd Configuration; make build_db;
	cd Geocode; make build_db
	cd Currency; make build_db
	cd Mail; make build_db
	cd Navigation; make build_db
	cd PerformanceMonitor; make build_db
	cd ResourceManager; make build_db
	cd SeleniumLoadTester; make build_db

.PHONY: clean_db
clean_db: 	
	cd Configuration; make clean_db
	cd Geocode; make clean_db
	cd Currency; make clean_db
	cd Mail; make clean_db
	cd Navigation; make clean_db
	cd PerformanceMonitor; make clean_db
	cd ResourceManager; make clean_db
	cd SeleniumLoadTester; make clean_db


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

package_sql:
	@chmod a+x ../bin/*.exe
	cd Configuration; make package_sql
	mv $(RELEASEDIR)/Spring2.Core.Configuration.sql.zip $(RELEASEDIR)/Spring2.Core.Configuration.sql-$(shell $(GETVERSION)).zip
	cd Geocode; make package_sql
	mv $(RELEASEDIR)/Spring2.Core.Geocode.sql.zip $(RELEASEDIR)/Spring2.Core.Geocode.sql-$(shell $(GETVERSION)).zip
	cd Currency; make package_sql
	mv $(RELEASEDIR)/Spring2.Core.Currency.sql.zip $(RELEASEDIR)/Spring2.Core.Currency.sql-$(shell $(GETVERSION)).zip
	cd Mail; make package_sql
	mv $(RELEASEDIR)/Spring2.Core.Mail.sql.zip $(RELEASEDIR)/Spring2.Core.Mail.sql-$(shell $(GETVERSION)).zip
	cd Navigation; make package_sql
	mv $(RELEASEDIR)/Spring2.Core.Navigation.sql.zip $(RELEASEDIR)/Spring2.Core.Navigation.sql-$(shell $(GETVERSION)).zip
	cd PerformanceMonitor; make package_sql
	mv $(RELEASEDIR)/Spring2.Core.PerformanceMonitor.sql.zip $(RELEASEDIR)/Spring2.Core.PerformanceMonitor.sql-$(shell $(GETVERSION)).zip
	cd ResourceManager; make package_sql
	mv $(RELEASEDIR)/Spring2.Core.ResourceManager.sql.zip $(RELEASEDIR)/Spring2.Core.ResourceManager.sql-$(shell $(GETVERSION)).zip
	cd SeleniumLoadTester; make package_sql
	mv $(RELEASEDIR)/Spring2.Core.SeleniumLoadTester.sql.zip $(RELEASEDIR)/Spring2.Core.SeleniumLoadTester.sql-$(shell $(GETVERSION)).zip

	
	
.PHONY: ncover
ncover: 
	rm -rf ../docs/NCover
	mkdir -p ../docs/NCover
	../bin/NCover/NCover.Console.exe //reg //l ../docs/NCover/Coverage.log //x ../docs/NCover/Coverage.xml ../bin/nunit/nunit-console.exe "../build/Test/Spring2.Core.Test.dll"
	
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

cleandtgbackups:
	find -iname '*cs~' -exec rm {} \;
	find -iname '*sql~' -exec rm {} \;
	find -iname '*orig' -exec rm {} \;
	find -iname '*mk~' -exec rm {} \;
	
reallyclean: clean cleanlogs cleandtgbackups
	
package_cl:
	#-cvs log -r${BRANCH_TAG} | perl ../bin/cvs2cl.pl --stdin -f ../release/ChangeLog-$(shell $(GETVERSION)).txt
	#svn log --xml --verbose | xsltproc ../bin/svn2cl/svn2cl.xsl - > ChangeLog
	../bin/svn2cl/svn2cl.sh -r ${RELEASE_REVISION}:BASE -o ../release/ChangeLog-$(shell $(GETVERSION)).txt
	
