#
# Makefile for Spring2.Core
#
RM			:= rm -rf
CP			:= cp
MKDIR		:= mkdir -p
NUNIT		:= ../bin/NUnit/nunit-console.exe

SRCDIR		:= ../src
LIBDIR		:= ../lib
BINDIR		:= ../bin
DOCDIR		:= ../docs
RELEASEDIR	:= ../release
BUILDDIR	:= ../build
NANT		:= ../bin/NAnt/nant.exe

DOTNETFRAMEWORK	:= c:/WINDOWS/Microsoft.NET/Framework/v4.0.30319
MSBUILD			:= $(DOTNETFRAMEWORK)/MSBuild.exe

RELEASE_REVISION:= 1
GETVERSION	:= $(BINDIR)/getversion.sh
7ZIP		:= $(BINDIR)/7za.exe

PROJECTNAME := Spring2.Core

.DELETE_ON_ERROR:

all: build_db build

.PHONY: build
build: msbuild
	
msbuild:
	$(MSBUILD) $(PROJECTNAME).sln /target:Build
	
.PHONY: clean
clean:
	$(RM) $(BUILDDIR)/*
	$(RM) $(DOCDIR)/NCover
	$(RM) obj
	$(RM) bin

.PHONY: precommit
precommit: build build_db 

#regress_UnitTests unitTestsCoverage unitTestsCoverageReports

gen:
	cd Mail; ../$(BINDIR)/DTG/DataTierGenerator.exe --file=DataTierGenerator.config.xml
	cd Geocode; ../$(BINDIR)/DTG/DataTierGenerator.exe --file=DataTierGenerator.config.xml
	cd Currency; ../$(BINDIR)/DTG/DataTierGenerator.exe --file=DataTierGenerator.config.xml
	cd Navigation; ../$(BINDIR)/DTG/DataTierGenerator.exe --file=DataTierGenerator.config.xml
	cd ResourceManager; ../$(BINDIR)/DTG/DataTierGenerator.exe --file=DataTierGenerator.config.xml
	cd Publication; ../$(BINDIR)/DTG/DataTierGenerator.exe --file=DataTierGenerator.config.xml

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
	cd Publication; make build_db

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
	cd Publication; make clean_db

clean_release:
	$(RM) ../release/*	

.PHONY: regress
regress: build

.PHONY: regressonly
regressonly:
	export COMPLUS_Version=net-4.0
	$(BINDIR)/NUnit/nunit-console /xml=ccnet-nunit40-results.xml ../build/net-4.0/Spring2.Core.Test.dll
	$(BINDIR)/NUnit/nunit-console /xml=ccnet-nunit40withtypes-results.xml ../build/net-4.0/WithTypes/Spring2.Core.WithTypesTest.dll

.PHONY: precommit
precommit: build regress

changelog:
	@updatechangelog.sh
	@cp ../ChangeLog.txt $(BUILDDIR)

docs:
	@chmod a+x ../bin/NDoc3/*
	@rm -rf ../docs/api
	../bin/NDoc3/NDoc3Console.exe -documenter=MSDN -project=Spring2.Core.ndoc -HtmlHelpName=Spring2.Core-$(shell $(GETVERSION)) -Version=$(shell $(GETVERSION))
	
package: clean build regress changelog docs packageonly

packageonly:
	rm -rf ${BUILDDIR}/Core
	$(7ZIP) a $(RELEASEDIR)/Spring2.Core.$(shell $(GETVERSION))-net-4.0.7z $(BUILDDIR)/net-4.0/*
	#${NANT} -D:version=$(shell ./getversion.sh) package_src

release: package
	cvs tag RELEASE_$(subst .,_,$(shell $(GETVERSION)))
	../bin/cvsadd.sh $(RELEASEDIR) Spring2.Core.$(shell $(GETVERSION))-net-4.0.7z "new release version"
	../bin/cvsadd.sh $(RELEASEDIR) Spring2.Core-$(shell $(GETVERSION)).chm "new release version"
	../bin/cvsadd.sh $(RELEASEDIR) ChangeLog-$(shell $(GETVERSION)).txt "new release version"
	$(BINDIR)/UpdateVersion -b Increment -i AssemblyVersionInfo.cs -o AssemblyVersionInfo.cs
	cvs commit -m "update to next version" AssemblyVersionInfo.cs

package_sql:
	@chmod a+x ../bin/*.exe
	cd Configuration; make package_sql
	mv $(RELEASEDIR)/Spring2.Core.Configuration.sql.7z $(RELEASEDIR)/Spring2.Core.Configuration.sql-$(shell $(GETVERSION)).7z
	cd Geocode; make package_sql
	mv $(RELEASEDIR)/Spring2.Core.Geocode.sql.7z $(RELEASEDIR)/Spring2.Core.Geocode.sql-$(shell $(GETVERSION)).7z
	cd Currency; make package_sql
	mv $(RELEASEDIR)/Spring2.Core.Currency.sql.7z $(RELEASEDIR)/Spring2.Core.Currency.sql-$(shell $(GETVERSION)).7z
	cd Mail; make package_sql
	mv $(RELEASEDIR)/Spring2.Core.Mail.sql.7z $(RELEASEDIR)/Spring2.Core.Mail.sql-$(shell $(GETVERSION)).7z
	cd Navigation; make package_sql
	mv $(RELEASEDIR)/Spring2.Core.Navigation.sql.7z $(RELEASEDIR)/Spring2.Core.Navigation.sql-$(shell $(GETVERSION)).7z
	cd PerformanceMonitor; make package_sql
	mv $(RELEASEDIR)/Spring2.Core.PerformanceMonitor.sql.7z $(RELEASEDIR)/Spring2.Core.PerformanceMonitor.sql-$(shell $(GETVERSION)).7z
	cd ResourceManager; make package_sql
	mv $(RELEASEDIR)/Spring2.Core.ResourceManager.sql.7z $(RELEASEDIR)/Spring2.Core.ResourceManager.sql-$(shell $(GETVERSION)).7z
	cd SeleniumLoadTester; make package_sql
	mv $(RELEASEDIR)/Spring2.Core.SeleniumLoadTester.sql.7z $(RELEASEDIR)/Spring2.Core.SeleniumLoadTester.sql-$(shell $(GETVERSION)).7z
	cd Publication; make package_sql
	mv $(RELEASEDIR)/Spring2.Core.Publication.sql.7z $(RELEASEDIR)/Spring2.Core.Publication.sql-$(shell $(GETVERSION)).7z

	
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
	find . -type f -name '*.cs' -print -exec unix2dos --safe \{\} \;
	find . -type f -name '*.aspx' -print -exec unix2dos --safe \{\} \;
	find . -type f -name '*.ascx' -print -exec unix2dos --safe \{\} \;
	find . -type f -name '*.css' -print -exec unix2dos --safe \{\} \;
	find . -type f -name '*.js' -print -exec unix2dos --safe \{\} \;

cleanlogs:
	rm -rf *.log
	rm -rf nvelocity.log.*

cleandtgbackups:
	find -iname '*cs~' -exec rm {} \;
	find -iname '*sql~' -exec rm {} \;
	find -iname '*orig' -exec rm {} \;
	find -iname '*mk~' -exec rm {} \;
	
reallyclean: clean cleanlogs cleandtgbackups	
