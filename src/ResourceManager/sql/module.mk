SQL_SERVER ?= .
SQL_DATABASE ?= WeiderTest
SQL_USER ?= sa
SQL_PASSWORD ?= 1qaz2wsx

# Path to SQL Server bin.
SQL := isql
BCP := bcp
SQL_FLAGS := -b -n -r -i
SQL_LOGIN := /S "${SQL_SERVER}" /U "${SQL_USER}" /P "${SQL_PASSWORD}" -d "${SQL_DATABASE}"

TABLELOGS := $(patsubst sql/table/%.sql, sql/table/%.log, $(wildcard sql/table/*.sql))
VIEWLOGS  := $(patsubst sql/view/%.sql, sql/view/%.log, $(wildcard sql/view/*.sql))
PROCLOGS  := $(patsubst sql/proc/%.sql, sql/proc/%.log, $(wildcard sql/proc/*.sql))
DATALOGS  := $(patsubst sql/data/%.sql, sql/data/%.log, $(wildcard sql/data/*.sql))

.PHONY : db_info clean_db build_db load_daa tables views procs debug_db

db_info:
	@echo SQL_SERVER = $(SQL_SERVER)
	@echo SQL_DATABASE = $(SQL_DATABASE)
	@echo SQL_USER = $(SQL_USER)
	@echo SQL_PASSWORD = $(SQL_PASSWORD)
	@echo
	@echo Usage: db_info clean_db build_db load_data

clean_db:
	rm -rf $(TABLELOGS) $(PROCLOGS) $(VIEWLOGS) $(DATALOGS) $(TESTDATALOGS)

build_db: $(TABLELOGS) $(VIEWLOGS) $(PROCLOGS) $(DATALOGS)

load_data: db_info build_db $(TESTDATALOGS)

tables: $(TABLELOGS)

views: $(VIEWLOGS)

procs: $(PROCLOGS)

data: $(DATALOGS)

%.log: %.sql 
	$(SQL) $(SQL_LOGIN) $(SQL_FLAGS) $< > $@

.DELETE_ON_ERROR:

debug_db:
	@echo TABLELOGS = $(TABLELOGS)
	@echo VIEWLOGS = $(VIEWLOGS)
	@echo PROCLOGS = $(PROCLOGS)
	@echo DATALOGS = $(DATALOGS)
	@echo TESTDATALOGS = $(TESTDATALOGS)


