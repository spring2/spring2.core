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
	csc /target:library /out:$@ /debug+ $(SRC) AssemblyInfo.cs AssemblyVersionInfo.cs

$(TEST): $(TEST_SRC) $(CORE) $(NUNIT)
	csc /target:library /out:$@ /debug+ /reference:$(NUNIT),$(CORE) $(TEST_SRC) AssemblyVersionInfo.cs

$(WEBCONTROL): $(WEBCONTROL_SRC)
	csc /target:library /out:$@ /debug+ $(WEBCONTROL_SRC) AssemblyVersionInfo.cs

solution:
	@-rm build.log
	@-devenv Spring2.Core.sln /rebuild Debug /out build.log /nologo
	@cat build.log

gen:
	../../../../work/Spring2/DataTierGenerator/build/DataTierGenerator.exe DataTierGenerator.config.xml

dtgcompare:
	../../../../work/Spring2/DataTierGenerator/build/DataTierGenerator.exe dtg-dbcompare.config.xml

regress:
	../bin/nunit-console.exe /assembly:$(TEST)

clean:
	rm -rf $(BUILDDIR)/*

debug:
	@echo MODULES = $(MODULES)
	@echo SRC = $(SRC)
	@echo TEST_SRC = $(TEST_SRC)
