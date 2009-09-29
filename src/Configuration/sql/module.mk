SQL_SERVER ?= .
SQL_DATABASE ?= Spring2Core
SQL_USER ?= sa
SQL_PASSWORD ?= 1qaz2wsx

# Path to SQL Server bin.
SQL := sqlcmd
BCP := bcp
SQL_FLAGS := -b -n -r -i
SQL_LOGIN := /S "${SQL_SERVER}" /U "${SQL_USER}" /P "${SQL_PASSWORD}" -d "${SQL_DATABASE}"

TABLELOGS_CONFIG := $(patsubst Configuration/sql/table/%.sql, Configuration/sql/table/%.log, $(wildcard Configuration/sql/table/*.sql))
VIEWLOGS_CONFIG  := $(patsubst Configuration/sql/view/%.sql, Configuration/sql/view/%.log, $(wildcard Configuration/sql/view/*.sql))
FUNCTIONLOGS_CONFIG  := $(patsubst Configuration/sql/function/%.sql, Configuration/sql/function/%.log, $(wildcard Configuration/sql/function/*.sql))
PROCLOGS_CONFIG  := $(patsubst Configuration/sql/proc/%.sql, Configuration/sql/proc/%.log, $(wildcard Configuration/sql/proc/*.sql))
DATALOGS_CONFIG  := $(patsubst Configuration/sql/data/%.sql, Configuration/sql/data/%.log, $(wildcard Configuration/sql/data/*.sql))
REPORTLOGS_CONFIG  := $(patsubst Configuration/sql/report/%.sql, Configuration/sql/report/%.log, $(wildcard Configuration/sql/report/*.sql))

.PHONY : db_info_config clean_db_config build_db_config load_data_config tables_config views_config procs_config debug_db_config

db_info_config:
	@echo SQL_SERVER = $(SQL_SERVER)
	@echo SQL_DATABASE = $(SQL_DATABASE)
	@echo SQL_USER = $(SQL_USER)
	@echo SQL_PASSWORD = $(SQL_PASSWORD)
	@echo
	@echo Usage: db_info_config clean_db_config build_db load_data_config

clean_db_config:
	rm -rf $(TABLELOGS_CONFIG) $(PROCLOGS_CONFIG) $(VIEWLOGS_CONFIG) $(DATALOGS_CONFIG) $(TESTDATALOGS_CONFIG) $(FUNCTIONLOGS_CONFIG) $(REPORTLOGS_CONFIG)

build_db_config: $(TABLELOGS_CONFIG) $(VIEWLOGS_CONFIG) $(FUNCTIONLOGS_CONFIG) $(PROCLOGS_CONFIG) $(DATALOGS_CONFIG) $(REPORTLOGS_CONFIG)

load_data_config: db_info_config build_db $(TESTDATALOGS_CONFIG)

tables_config: $(TABLELOGS_CONFIG)

views_config: $(VIEWLOGS_CONFIG)

functions: $(FUNCTIONLOGS_CONFIG)

procs_config: $(PROCLOGS_CONFIG)

data: $(DATALOGS_CONFIG)

report: $(REPORTLOGS_CONFIG)

%.log: %.sql 
	$(SQL) $(SQL_LOGIN) $(SQL_FLAGS) $< > $@

.DELETE_ON_ERROR:

debug_db_config:
	@echo TABLELOGS_CONFIG = $(TABLELOGS_CONFIG)
	@echo VIEWLOGS_CONFIG = $(VIEWLOGS_CONFIG)
	@echo FUNCTIONLOGS_CONFIG = $(FUNCTIONLOGS_CONFIG)
	@echo PROCLOGS_CONFIG = $(PROCLOGS_CONFIG)
	@echo DATALOGS_CONFIG = $(DATALOGS_CONFIG)
	@echo REPORTLOGS_CONFIG = $(REPORTLOGS_CONFIG)
	@echo TESTDATALOGS_CONFIG = $(TESTDATALOGS_CONFIG)


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
