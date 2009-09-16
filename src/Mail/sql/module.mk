SQL_SERVER ?= .
SQL_DATABASE ?= Spring2Core
SQL_USER ?= sa
SQL_PASSWORD ?= 1qaz2wsx

# Path to SQL Server bin.
SQL := sqlcmd
BCP := bcp
SQL_FLAGS := -b -n -r -i
SQL_LOGIN := /S "${SQL_SERVER}" /U "${SQL_USER}" /P "${SQL_PASSWORD}" -d "${SQL_DATABASE}"

TABLELOGS := $(patsubst Mail/sql/table/%.sql, Mail/sql/table/%.log, $(wildcard Mail/sql/table/*.sql))
VIEWLOGS  := $(patsubst Mail/sql/view/%.sql, Mail/sql/view/%.log, $(wildcard Mail/sql/view/*.sql))
FUNCTIONLOGS  := $(patsubst Mail/sql/function/%.sql, Mail/sql/function/%.log, $(wildcard Mail/sql/function/*.sql))
PROCLOGS  := $(patsubst Mail/sql/proc/%.sql, Mail/sql/proc/%.log, $(wildcard Mail/sql/proc/*.sql))
DATALOGS  := $(patsubst Mail/sql/data/%.sql, Mail/sql/data/%.log, $(wildcard Mail/sql/data/*.sql))
REPORTLOGS  := $(patsubst Mail/sql/report/%.sql, Mail/sql/report/%.log, $(wildcard Mail/sql/report/*.sql))

.PHONY : db_info clean_db build_db load_daa tables views procs debug_db

db_info:
	@echo SQL_SERVER = $(SQL_SERVER)
	@echo SQL_DATABASE = $(SQL_DATABASE)
	@echo SQL_USER = $(SQL_USER)
	@echo SQL_PASSWORD = $(SQL_PASSWORD)
	@echo
	@echo Usage: db_info clean_db build_db load_data

clean_db:
	rm -rf $(TABLELOGS) $(PROCLOGS) $(VIEWLOGS) $(DATALOGS) $(TESTDATALOGS) $(FUNCTIONLOGS) $(REPORTLOGS)

build_db: $(TABLELOGS) $(VIEWLOGS) $(FUNCTIONLOGS) $(PROCLOGS) $(DATALOGS) $(REPORTLOGS)

load_data: db_info build_db $(TESTDATALOGS)

tables: $(TABLELOGS)

views: $(VIEWLOGS)

functions: $(FUNCTIONLOGS)

procs: $(PROCLOGS)

data: $(DATALOGS)

report: $(REPORTLOGS)

%.log: %.sql 
	$(SQL) $(SQL_LOGIN) $(SQL_FLAGS) $< > $@

.DELETE_ON_ERROR:

debug_db:
	@echo TABLELOGS = $(TABLELOGS)
	@echo VIEWLOGS = $(VIEWLOGS)
	@echo FUNCTIONLOGS = $(FUNCTIONLOGS)
	@echo PROCLOGS = $(PROCLOGS)
	@echo DATALOGS = $(DATALOGS)
	@echo REPORTLOGS = $(REPORTLOGS)
	@echo TESTDATALOGS = $(TESTDATALOGS)


sql/data/DirectSalesAgentBusiness.data.log: sql/data/DirectSalesAgent.data.log
sql/data/DirectSalesAgent.data.log:  sql/data/Title.data.log sql/data/Business.data.log 
sql/data/DirectSalesAgentUpline.data.log:  sql/data/User.data.log
sql/data/ShippingOrderTypeLookup.data.log: sql/data/ShippingOrderType.data.log
sql/data/ShippingRegionLookup.data.log: sql/data/ShippingRegion.data.log
sql/data/ShippingRate.data.log:  sql/data/ShippingOrderType.data.log sql/data/ShippingRegion.data.log
sql/data/CommissionRate_CharterLevelOverrides.data.log: sql/data/Title.data.log
sql/data/CommissionRate.data.log: sql/data/Title.data.log
sql/data/Resource.data.log: sql/data/ResourceKey.data.log

sql/table/MailAttachment.table.log:  sql/table/MailMessage.table.log

deleteunittestdata:
	$(SQL) $(SQL_LOGIN) -r -Q "set ANSI_WARNINGS on; set ANSI_PADDING on; set CONCAT_NULL_YIELDS_NULL on; exec spDeleteTestData"
