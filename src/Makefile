RM		:= rm -rf
MKDIR		:= mkdir -p
NUNIT		:= nunit-console.exe

SRCDIR		:= ../src
LIBDIR		:= ../lib
BINDIR		:= ../bin
DOCDIR		:= ../docs
RELEASEDIR	:= ../release
BUILDDIR	:= ../build

all: build

.DELETE_ON_ERROR:

.PHONY: clean
clean:
	$(RM) $(BUILDDIR)/*
	$(RM) $(DOCDIR)/*

.PHONY: build
build:
	nant

.PHONY: regress
regress: build
	cd $(BUILDDIR); $(NUNIT) Spring2.Core.Test.dll

.PHONY: precommit
precommit: build regress

changelog:
	@updatechangelog.sh
	@cp ../ChangeLog.txt $(BUILDDIR)

docs:
	@chmod a+x ../bin/NDoc/*
	@rm -rf ../docs/api
	../bin/NDoc/NDocConsole.exe -documenter=MSDN -project=Spring2.Core.ndoc


package: clean build regress changelog docs
	rm -rf ${BUILDDIR}/Core
	cp $(DOCDIR)/api/Spring2.Core.chm $(BUILDDIR)
	jar -cMvf $(RELEASEDIR)/Spring2.Core.$(shell getversion.sh).zip -C $(BUILDDIR) .

release: package
	cvs tag RELEASE_$(subst .,_,$(shell ./getversion.sh))
	../bin/cvsadd.sh $(RELEASEDIR) Spring2.Core.$(shell getversion.sh).zip "new release version"
	$(BINDIR)/UpdateVersion -b Increment -i AssemblyVersionInfo.cs -o AssemblyVersionInfo.cs
	cvs commit -m "update to next version" AssemblyVersionInfo.cs


