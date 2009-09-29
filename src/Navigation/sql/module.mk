SQL_SERVER ?= .
SQL_DATABASE ?= Spring2Core
SQL_USER ?= sa
SQL_PASSWORD ?= 1qaz2wsx

# Path to SQL Server bin.
SQL := sqlcmd
BCP := bcp
SQL_FLAGS := -b -n -r -i
SQL_LOGIN := /S "${SQL_SERVER}" /U "${SQL_USER}" /P "${SQL_PASSWORD}" -d "${SQL_DATABASE}"

TABLELOGS_NAV := $(patsubst Navigation/sql/table/%.sql, Navigation/sql/table/%.log, $(wildcard Navigation/sql/table/*.sql))
VIEWLOGS_NAV  := $(patsubst Navigation/sql/view/%.sql, Navigation/sql/view/%.log, $(wildcard Navigation/sql/view/*.sql))
FUNCTIONLOGS_NAV  := $(patsubst Navigation/sql/function/%.sql, Navigation/sql/function/%.log, $(wildcard Navigation/sql/function/*.sql))
PROCLOGS_NAV  := $(patsubst Navigation/sql/proc/%.sql, Navigation/sql/proc/%.log, $(wildcard Navigation/sql/proc/*.sql))
DATALOGS_NAV  := $(patsubst Navigation/sql/data/%.sql, Navigation/sql/data/%.log, $(wildcard Navigation/sql/data/*.sql))
REPORTLOGS_NAV  := $(patsubst Navigation/sql/report/%.sql, Navigation/sql/report/%.log, $(wildcard Navigation/sql/report/*.sql))

.PHONY : db_info_nav clean_db_nav build_db_nav load_data_nav tables_nav views_nav procs_nav debug_db_nav

db_info_nav:
	@echo SQL_SERVER = $(SQL_SERVER)
	@echo SQL_DATABASE = $(SQL_DATABASE)
	@echo SQL_USER = $(SQL_USER)
	@echo SQL_PASSWORD = $(SQL_PASSWORD)
	@echo
	@echo Usage: db_info_nav clean_db_nav build_db_nav load_data_nav

clean_db_nav:
	rm -rf $(TABLELOGS_NAV) $(PROCLOGS_NAV) $(VIEWLOGS_NAV) $(DATALOGS_NAV) $(TESTDATALOGS) $(FUNCTIONLOGS_NAV) $(REPORTLOGS_NAV)

build_db_nav: $(TABLELOGS_NAV) $(VIEWLOGS_NAV) $(FUNCTIONLOGS_NAV) $(PROCLOGS_NAV) $(DATALOGS_NAV) $(REPORTLOGS_NAV)

load_data_nav: db_info_nav build_db_nav $(TESTDATALOGS)

tables_nav: $(TABLELOGS_NAV)

views_nav: $(VIEWLOGS_NAV)

functions: $(FUNCTIONLOGS_NAV)

procs_nav: $(PROCLOGS_NAV)

data: $(DATALOGS_NAV)

report: $(REPORTLOGS_NAV)

%.log: %.sql 
	$(SQL) $(SQL_LOGIN) $(SQL_FLAGS) $< > $@

.DELETE_ON_ERROR:

debug_db_nav:
	@echo TABLELOGS_NAV = $(TABLELOGS_NAV)
	@echo VIEWLOGS_NAV = $(VIEWLOGS_NAV)
	@echo FUNCTIONLOGS_NAV = $(FUNCTIONLOGS_NAV)
	@echo PROCLOGS_NAV = $(PROCLOGS_NAV)
	@echo DATALOGS_NAV = $(DATALOGS_NAV)
	@echo REPORTLOGS_NAV = $(REPORTLOGS_NAV)
	@echo TESTDATALOGS = $(TESTDATALOGS)


Navigation/sql/data/DirectSalesAgentBusiness.data.log: Navigation/sql/data/DirectSalesAgent.data.log
Navigation/sql/data/DirectSalesAgent.data.log:  Navigation/sql/data/Title.data.log Navigation/sql/data/Business.data.log 
Navigation/sql/data/DirectSalesAgentUpline.data.log:  Navigation/sql/data/User.data.log
Navigation/sql/data/ShippingOrderTypeLookup.data.log: Navigation/sql/data/ShippingOrderType.data.log
Navigation/sql/data/ShippingRegionLookup.data.log: Navigation/sql/data/ShippingRegion.data.log
Navigation/sql/data/ShippingRate.data.log:  Navigation/sql/data/ShippingOrderType.data.log Navigation/sql/data/ShippingRegion.data.log
Navigation/sql/data/CommissionRate_CharterLevelOverrides.data.log: Navigation/sql/data/Title.data.log
Navigation/sql/data/CommissionRate.data.log: Navigation/sql/data/Title.data.log
Navigation/sql/data/Resource.data.log: Navigation/sql/data/ResourceKey.data.log

Navigation/sql/table/MailAttachment.table.log:  Navigation/sql/table/MailMessage.table.log
