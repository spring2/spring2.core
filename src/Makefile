#
# Location of the source and destination folders.
#
SRCDIR :=	../src
LIBDIR :=	../lib
BUILDDIR :=     ../build
BINDIR :=	../bin

CORE := $(BUILDDIR)/Spring2.Core.dll
TEST := $(BUILDDIR)/Spring2.Core.Test.dll
WEBCONTROL := $(BUILDDIR)/Spring2.Core.WebControl.dll
NUNIT := $(LIBDIR)/nunit.framework.dll

MODULES := $(shell /usr/bin/find -name module.mk)
include $(MODULES)

.DELETE_ON_ERROR:

all: $(CORE) $(TEST) $(WEBCONTROL)

$(CORE): $(SRC) AssemblyInfo.cs
	csc /target:library /out:$@ /debug+ $(SRC) AssemblyInfo.cs

$(TEST): $(TEST_SRC) $(CORE) $(NUNIT)
	csc /target:library /out:$@ /debug+ /reference:$(NUNIT),$(CORE) $(TEST_SRC)

$(WEBCONTROL): $(WEBCONTROL_SRC)
	csc /target:library /out:$@ /debug+ $(WEBCONTROL_SRC)

solution:
	@-rm build.log
	@-devenv Scorecard.sln /rebuild Debug /out build.log /nologo
	@cat build.log

gen:
	../../../../work/Spring2/DataTierGenerator/build/DataTierGenerator.exe DataTierGenerator.config.xml

dtgcompare:
	../../../../work/Spring2/DataTierGenerator/build/DataTierGenerator.exe dtg-dbcompare.config.xml

regress:
	../bin/nunit-console.exe /assembly:../build/MacCall.Scorecard.Test.dll

debug:
	@echo MODULES = $(MODULES)
	@echo SRC = $(SRC)
	@echo TEST_SRC = $(TEST_SRC)
