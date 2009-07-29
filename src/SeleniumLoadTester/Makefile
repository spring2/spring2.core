#
# Location of the source and destination folders.
#
RM		:= rm -rf
MKDIR		:= mkdir -p

SRCDIR		:= ../src
LIBDIR		:= ../lib
BINDIR		:= ../../bin
RELEASEDIR	:= ../release
BUILDDIR	:= ../build
TESTDIR		:= ../build/Test
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
