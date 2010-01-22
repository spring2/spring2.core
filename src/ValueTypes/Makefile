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

.PHONY: gen
gen:
	@chmod a+x $(DTGDIR)/*.exe
	@chmod a+x $(DTGDIR)/*.dll
	$(DTGDIR)/DataTierGenerator.exe DataTierGenerator.config.xml
