SQL_SERVER ?= .
SQL_DATABASE ?= Spring2Core
SQL_USER ?= sa
SQL_PASSWORD ?= 1qaz2wsx

# Path to SQL Server bin.
SQL := sqlcmd
BCP := bcp
SQL_FLAGS := -b -n -r -i
SQL_LOGIN := /S "${SQL_SERVER}" /U "${SQL_USER}" /P "${SQL_PASSWORD}" -d "${SQL_DATABASE}"

TABLELOGS_PM := $(patsubst PerformanceMonitor/sql/table/%.sql, PerformanceMonitor/sql/table/%.log, $(wildcard PerformanceMonitor/sql/table/*.sql))
VIEWLOGS_PM  := $(patsubst PerformanceMonitor/sql/view/%.sql, PerformanceMonitor/sql/view/%.log, $(wildcard PerformanceMonitor/sql/view/*.sql))
FUNCTIONLOGS_PM  := $(patsubst PerformanceMonitor/sql/function/%.sql, PerformanceMonitor/sql/function/%.log, $(wildcard PerformanceMonitor/sql/function/*.sql))
PROCLOGS_PM  := $(patsubst PerformanceMonitor/sql/proc/%.sql, PerformanceMonitor/sql/proc/%.log, $(wildcard PerformanceMonitor/sql/proc/*.sql))
DATALOGS_PM  := $(patsubst PerformanceMonitor/sql/data/%.sql, PerformanceMonitor/sql/data/%.log, $(wildcard PerformanceMonitor/sql/data/*.sql))
REPORTLOGS_PM  := $(patsubst PerformanceMonitor/sql/report/%.sql, PerformanceMonitor/sql/report/%.log, $(wildcard PerformanceMonitor/sql/report/*.sql))

.PHONY : db_info_pm clean_db_pm build_db_pm load_data_pm tables_pm views_pm procs_pm debug_db_pm

db_info_pm:
	@echo SQL_SERVER = $(SQL_SERVER)
	@echo SQL_DATABASE = $(SQL_DATABASE)
	@echo SQL_USER = $(SQL_USER)
	@echo SQL_PASSWORD = $(SQL_PASSWORD)
	@echo
	@echo Usage: db_info_pm clean_db_pm build_db_pm load_data_pm

clean_db_pm:
	rm -rf $(TABLELOGS_PM) $(PROCLOGS_PM) $(VIEWLOGS_PM) $(DATALOGS_PM) $(TESTDATALOGS) $(FUNCTIONLOGS_PM) $(REPORTLOGS_PM)

build_db_pm: $(TABLELOGS_PM) $(VIEWLOGS_PM) $(FUNCTIONLOGS_PM) $(PROCLOGS_PM) $(DATALOGS_PM) $(REPORTLOGS_PM)

load_data_pm: db_info_pm build_db_pm $(TESTDATALOGS)

tables_pm: $(TABLELOGS_PM)

views_pm: $(VIEWLOGS_PM)

functions: $(FUNCTIONLOGS_PM)

procs_pm: $(PROCLOGS_PM)

data: $(DATALOGS_PM)

report: $(REPORTLOGS_PM)

%.log: %.sql 
	$(SQL) $(SQL_LOGIN) $(SQL_FLAGS) $< > $@

.DELETE_ON_ERROR:

debug_db_pm:
	@echo TABLELOGS_PM = $(TABLELOGS_PM)
	@echo VIEWLOGS_PM = $(VIEWLOGS_PM)
	@echo FUNCTIONLOGS_PM = $(FUNCTIONLOGS_PM)
	@echo PROCLOGS_PM = $(PROCLOGS_PM)
	@echo DATALOGS_PM = $(DATALOGS_PM)
	@echo REPORTLOGS_PM = $(REPORTLOGS_PM)
	@echo TESTDATALOGS = $(TESTDATALOGS)
