SQL_SERVER ?= .
SQL_DATABASE ?= Spring2Core
SQL_USER ?= sa
SQL_PASSWORD ?= 1qaz2wsx

# Path to SQL Server bin.
SQL := sqlcmd
BCP := bcp
SQL_FLAGS := -b -n -r -i
SQL_LOGIN := /S "${SQL_SERVER}" /U "${SQL_USER}" /P "${SQL_PASSWORD}" -d "${SQL_DATABASE}"

TABLELOGS_RSCMAN := $(patsubst ResourceManager/sql/table/%.sql, ResourceManager/sql/table/%.log, $(wildcard ResourceManager/sql/table/*.sql))
VIEWLOGS_RSCMAN  := $(patsubst ResourceManager/sql/view/%.sql, ResourceManager/sql/view/%.log, $(wildcard ResourceManager/sql/view/*.sql))
PROCLOGS_RSCMAN  := $(patsubst ResourceManager/sql/proc/%.sql, ResourceManager/sql/proc/%.log, $(wildcard ResourceManager/sql/proc/*.sql))
DATALOGS_RSCMAN  := $(patsubst ResourceManager/sql/data/%.sql, ResourceManager/sql/data/%.log, $(wildcard ResourceManager/sql/data/*.sql))

.PHONY : db_info_rscman clean_db_rscman build_db_rscman load_data_rscman tables_rscman views_rscman procs_rscman debug_db_rscman

db_info_rscman:
	@echo SQL_SERVER = $(SQL_SERVER)
	@echo SQL_DATABASE = $(SQL_DATABASE)
	@echo SQL_USER = $(SQL_USER)
	@echo SQL_PASSWORD = $(SQL_PASSWORD)
	@echo
	@echo Usage: db_info_rscman clean_db_rscman build_db_rscman load_data_rscman

clean_db_rscman:
	rm -rf $(TABLELOGS_RSCMAN) $(PROCLOGS_RSCMAN) $(VIEWLOGS_RSCMAN) $(DATALOGS_RSCMAN) $(TESTDATALOGS)

build_db_rscman: $(TABLELOGS_RSCMAN) $(VIEWLOGS_RSCMAN) $(PROCLOGS_RSCMAN) $(DATALOGS_RSCMAN)

load_data_rscman: db_info_rscman build_db_rscman $(TESTDATALOGS)

tables_rscman: $(TABLELOGS_RSCMAN)

views_rscman: $(VIEWLOGS_RSCMAN)

procs_rscman: $(PROCLOGS_RSCMAN)

data: $(DATALOGS_RSCMAN)

%.log: %.sql 
	$(SQL) $(SQL_LOGIN) $(SQL_FLAGS) $< > $@

.DELETE_ON_ERROR:

debug_db_rscman:
	@echo TABLELOGS_RSCMAN = $(TABLELOGS_RSCMAN)
	@echo VIEWLOGS_RSCMAN = $(VIEWLOGS_RSCMAN)
	@echo PROCLOGS_RSCMAN = $(PROCLOGS_RSCMAN)
	@echo DATALOGS_RSCMAN = $(DATALOGS_RSCMAN)
	@echo TESTDATALOGS = $(TESTDATALOGS)
