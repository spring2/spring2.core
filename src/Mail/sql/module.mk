SQL_SERVER ?= .
SQL_DATABASE ?= Spring2Core
SQL_USER ?= sa
SQL_PASSWORD ?= 1qaz2wsx

# Path to SQL Server bin.
SQL := sqlcmd
BCP := bcp
SQL_FLAGS := -b -n -r -i
SQL_LOGIN := /S "${SQL_SERVER}" /U "${SQL_USER}" /P "${SQL_PASSWORD}" -d "${SQL_DATABASE}"

TABLELOGS_MAIL := $(patsubst Mail/sql/table/%.sql, Mail/sql/table/%.log, $(wildcard Mail/sql/table/*.sql))
VIEWLOGS_MAIL  := $(patsubst Mail/sql/view/%.sql, Mail/sql/view/%.log, $(wildcard Mail/sql/view/*.sql))
FUNCTIONLOGS_MAIL  := $(patsubst Mail/sql/function/%.sql, Mail/sql/function/%.log, $(wildcard Mail/sql/function/*.sql))
PROCLOGS_MAIL  := $(patsubst Mail/sql/proc/%.sql, Mail/sql/proc/%.log, $(wildcard Mail/sql/proc/*.sql))
DATALOGS_MAIL  := $(patsubst Mail/sql/data/%.sql, Mail/sql/data/%.log, $(wildcard Mail/sql/data/*.sql))
REPORTLOGS_MAIL  := $(patsubst Mail/sql/report/%.sql, Mail/sql/report/%.log, $(wildcard Mail/sql/report/*.sql))

.PHONY : db_info_mail clean_db_mail build_db_mail load_data_mail tables_mail views_mail procs_mail debug_db_mail

db_info_mail:
	@echo SQL_SERVER = $(SQL_SERVER)
	@echo SQL_DATABASE = $(SQL_DATABASE)
	@echo SQL_USER = $(SQL_USER)
	@echo SQL_PASSWORD = $(SQL_PASSWORD)
	@echo
	@echo Usage: db_info_mail clean_db_mail build_db_mail load_data_mail

clean_db_mail:
	rm -rf $(TABLELOGS_MAIL) $(PROCLOGS_MAIL) $(VIEWLOGS_MAIL) $(DATALOGS_MAIL) $(TESTDATALOGS_MAIL) $(FUNCTIONLOGS_MAIL) $(REPORTLOGS_MAIL)

build_db_mail: $(TABLELOGS_MAIL) $(VIEWLOGS_MAIL) $(FUNCTIONLOGS_MAIL) $(PROCLOGS_MAIL) $(DATALOGS_MAIL) $(REPORTLOGS_MAIL)

load_data_mail: db_info_mail build_db_mail $(TESTDATALOGS_MAIL)

tables_mail: $(TABLELOGS_MAIL)

views_mail: $(VIEWLOGS_MAIL)

functions: $(FUNCTIONLOGS_MAIL)

procs_mail: $(PROCLOGS_MAIL)

data: $(DATALOGS_MAIL)

report: $(REPORTLOGS_MAIL)

%.log: %.sql 
	$(SQL) $(SQL_LOGIN) $(SQL_FLAGS) $< > $@

.DELETE_ON_ERROR:

debug_db_mail:
	@echo TABLELOGS_MAIL = $(TABLELOGS_MAIL)
	@echo VIEWLOGS_MAIL = $(VIEWLOGS_MAIL)
	@echo FUNCTIONLOGS_MAIL = $(FUNCTIONLOGS_MAIL)
	@echo PROCLOGS_MAIL = $(PROCLOGS_MAIL)
	@echo DATALOGS_MAIL = $(DATALOGS_MAIL)
	@echo REPORTLOGS_MAIL = $(REPORTLOGS_MAIL)
	@echo TESTDATALOGS_MAIL = $(TESTDATALOGS_MAIL)


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
