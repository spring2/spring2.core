using System;
using System.Data;

using Spring2.Core.DataObject;
using Spring2.Core.Types;

namespace Spring2.Core.DAO {
    public abstract class EntityDAO {

	public EntityDAO() {
	}
//
//	public abstract IdType Create(IDbConnection conn, Spring2.Core.DataObject.DataObject data);
//	public abstract void Store(IDbConnection conn, Spring2.Core.DataObject.DataObject data);
//	public abstract Spring2.Core.DataObject.DataObject Load(IDbConnection conn, IdType id);
//	public abstract void Remove(IDbConnection conn, IdType id);
    }
}
//
//
//
//    public boolean findByPrimaryKey(int Id) {
//        boolean found = false;
//
//        try {
//            ArrayList args = new ArrayList();
//            args.add(new Integer(Id));
//            List result = executeFind(getFindByPrimaryKeyStatement(),args);
//
//            found = result.iterator().hasNext();
//        } catch (DAOException e) {
//            // Assumption that false is safe on any error.
//            found = false;
//        }
//
//        return found;
//    }
//
//    /**
//     * Convience methods to execute ANY SQL statement and return results in
//     * ScrollableRecordSet.
//     */
//    public ScrollableRecordSet getList(String sql, ArrayList args) {
//        ScrollableRecordSet rs = executeSelectRecordSet(sql, args);
//        return rs;
//    }
//
//    public ScrollableRecordSet getList(String sql) {
//        ScrollableRecordSet rs = executeSelectRecordSet(sql, new ArrayList());
//        return rs;
//    }
//
//    /**
//     * Lists that take a filter should also support ordering.
//     * @param filters a list of ListFilter objects to apply to the query.
//     * @param ordering represents the ordering to be applied to the
//     *        query.
//     * @return a ScrollableRecordSet containing the results of the query.
//     */
//    public ScrollableRecordSet getList(List filters, ListOrderBy ordering) {
//
//        String entityName = getEntity();
//        StringBuffer sql = new StringBuffer();
//	sql.append("SELECT * FROM ").append(entityName).append(" where 1=1 ");
//
//        ListIterator it = filters.listIterator();
//        while (it.hasNext()) {
//            Object o = it.next();
//            if (o instanceof ListFilter) {
//                ListFilter filter = (ListFilter)o;
//                sql.append(filter.formatSql());
//            }
//        }
//
//        if (ordering != null) {
//            ordering.setOrderByPrefix(entityName);
//            ordering.setAndThenByPrefix(entityName);
//            sql.append(ordering.formatSQL());
//        }
//
//        String queryString = sql.toString();
//        ScrollableRecordSet rs = executeSelectRecordSet(queryString, new ArrayList());
//        return rs;
//    }
//
//    public SQLClause getListSecurityFilter(Integer userId, Integer organizationId) {
//        // Default filter (1=2) in case a document type is not defined below.
//        SQLClause securityFilter = new SQLClause();
//        securityFilter.put("1", new Integer(2), SQLClause.MATCH_EXACT, SQLClause.USE_AND);
//        return securityFilter;
//    }
//
//    public List getEntityList(ScrollableRecordSet rs) {
//	return Collections.EMPTY_LIST;
//    }
//
//    public List getEntityList() {
//	return getEntityList(getList(Collections.EMPTY_LIST, null));
//    }
//
//    public EntityType getEntityType() {
//        throw new UnsupportedOperationException("getList is not supported for " + this.getClass().getName());
//    }
//
//    /**
//     * Abstract accessor methods to get SQL statments and parm columns
//     */
//    public abstract String getCreateStatement();
//    public abstract String[] getCreateFields();
//    public abstract String getUpdateStatement();
//    public abstract String[] getUpdateFields();
//    public abstract String getFindByPrimaryKeyStatement();
//    public abstract String getDeleteStatement();
//    public abstract String getLoadStatement();
//    public abstract String getEntity();
//
//    /**
//     * Executes the given SQL query against the database to find the
//     * primary keys of the EJBs specified by the query.
//     *
//     * @param statement - the query for finding a set of instances of
//     * the EJB with placeholders for the given parameters.
//     * @param params - the attributes to be inserted into the query.
//     * @return a list of primary keys of the EJBs matching the query.
//     * @exception DAOException when a database error occurs.
//     */
//    protected List executeFind(String statement, List args)
//        throws DAOException {
//
//        Connection conn = null;
//        PreparedStatement ps = null;
//        ResultSet rs = null;
//
//        try {
//            conn = getConnection(sqlPool);
//            ps = conn.prepareStatement(statement);
//
//            ListIterator it = args.listIterator();
//            while (it.hasNext()) {
//                Object o = it.next();
//                if (o == null) {
//                    ps.setNull(it.nextIndex(), java.sql.Types.VARCHAR);
//                } else if (o instanceof java.util.Date) {
//                    java.util.Date date = (java.util.Date)o;
//                    ps.setObject(it.nextIndex(), new java.sql.Timestamp(date.getTime()));
//                } else {
//                    ps.setObject(it.nextIndex(),o);
//                }
//            }
//
//            rs = ps.executeQuery();
//
//            ArrayList results = new ArrayList();
//
//            while (rs.next()) {
//                results.add(rs.getObject(1));
//            }
//
//            return results;
//
//        } catch (SQLException e) {
//            SimpleLogger.getInstance().log("executeFind(): " +
//					   formatException(e));
//            throw new DAOException(e);
//        } finally {
//            try {
//                if (rs != null) rs.close();
//                if (ps != null) ps.close();
//                if (conn != null) conn.close();
//            } catch (SQLException ignore) {
//            }
//        }
//    }
//
//    /**
//     * Executes a stored procedure against the database.
//     * @param statement - the stored procedure statement to be
//     * executed with placeholders for the given parameters.
//     * @param params - the parameters to be placed into the prepared
//     * statement before it is executed.
//     * @param Connection - Connection to use.  Useful for putting multiple
//     * statements in a transaction.
//     * @exception DAOException when a database error occurs.
//     */
//    protected int executeUpdateRecord(String statement, Object[] params, Connection conn)
//        throws DAOException {
//
//        CallableStatement cs = null;
//
//        try {
//            cs = conn.prepareCall(statement);
//            cs.registerOutParameter(1,java.sql.Types.INTEGER);
//
//            for (int i = 0; i < params.length; i++) {
//                if (params[i] == null) {
//                    cs.setNull(i+2,java.sql.Types.VARCHAR);
//                } else if (params[i] instanceof java.util.Date) {
//                    java.util.Date date = (java.util.Date)params[i];
//                    cs.setObject(i+2, new java.sql.Timestamp(date.getTime()));
//                } else {
//                    cs.setObject(i+2,params[i]);
//                }
//            }
//
//            cs.executeUpdate();
//            return cs.getInt(1);
//
//        } catch (SQLException e) {
//            SimpleLogger.getInstance().log("executeUpdate(): " +
//					   formatException(e));
//            SimpleLogger.getInstance().log(statement);
//            throw new DAOException(e);
//        } finally {
//            try {
//                if (cs != null) cs.close();
//            } catch (SQLException ignore) {
//            }
//        }
//    }
//
//    protected int executeUpdateRecord(String Statement, Object[] params)
//	throws DAOException {
//        Connection conn = null;
//
//        try {
//            conn = getConnection(sqlPool);
//            return executeUpdateRecord(Statement, params, conn);
//        } catch (Exception e) {
//            SimpleLogger.getInstance().log("executeUpdate(): " +
//					   formatException(e));
//            throw new DAOException(e);
//        } finally {
//            try {
//                if (conn != null) conn.close();
//            } catch (SQLException ignore) {
//            }
//        }
//    }
//
//    /**
//     * Deletes the records with the given ids from the database using
//     * the given statement.
//     * @param statement a 'delete' prepared statement with a single
//     *                  placeholder that will be filled in with a row
//     *                  id.
//     * @param rowIds an array of integer row ids indicating which
//     *		     records are to be deleted.
//     * @param rs a record set containing the fields and values to be
//     *           stored in the database.
//     * @exception DAOException when an error occurs.
//     */
//    protected void deleteRows(String statement, int[] rowIds)
//        throws DAOException {
//
//        Connection conn = null;
//        PreparedStatement ps = null;
//
//        try {
//            conn = getConnection(sqlPool);
//            ps = conn.prepareStatement(statement);
//
//            for (int i = 0; i < rowIds.length; i++) {
//                ps.setInt(i+1, rowIds[i]);
//                ps.execute();
//            }
//        } catch (SQLException e) {
//            SimpleLogger.getInstance().log("deleteRows(): " + formatException(e));
//            SimpleLogger.getInstance().log(statement);
//            throw new DAOException(e);
//        } finally {
//            try {
//                if (ps != null) ps.close();
//                if (conn != null) conn.close();
//            } catch (SQLException ignore) {
//            }
//        }
//    }
//
//    /**
//     * Executes a prepared SQL insert, update or delete statement
//     * against the database.
//     *
//     * @param statement - the SQL statement to be executed with
//     * placeholders for the given parameters.
//     * @param params - the parameters to be placed into the prepared
//     * statement before it is executed.
//     * @exception EJBException when a database error occurs.
//     */
//    protected void executeUpdate(String statement, List params)
//        throws DAOException {
//
//        Connection conn = null;
//        PreparedStatement ps = null;
//        try {
//            conn = getConnection(sqlPool);
//            ps = conn.prepareStatement(statement);
//
//            ListIterator it = params.listIterator();
//            while (it.hasNext()) {
//                Object o = it.next();
//                if (o == null) {
//                    ps.setNull(it.nextIndex(), java.sql.Types.VARCHAR);
//                } else if (o instanceof java.util.Date) {
//                    java.util.Date date = (java.util.Date)o;
//                    ps.setObject(it.nextIndex(), new java.sql.Timestamp(date.getTime()));
//                } else {
//                    ps.setObject(it.nextIndex(),o);
//                }
//            }
//
//
//            // changed to allow update of more than one row and not error
//            if (ps.executeUpdate() < 1) {
//                throw new DAOException("The following statement affect no rows: " + statement);
//            }
//        } catch (SQLException e) {
//            SimpleLogger.getInstance().log("executeUpdate(): " + formatException(e));
//            SimpleLogger.getInstance().log(statement);
//            throw new DAOException(e);
//        } finally {
//            try {
//                if (ps != null) ps.close();
//                if (conn != null) conn.close();
//            } catch (SQLException ignore) {
//            }
//        }
//    }
//
//    /**
//     * Methods to return meta data information about underlying SQL structure
//     */
//    public ScrollableRecordSet getMetaData() {
//        String sql = "SELECT * FROM " + getEntity() + " where 1=2 ";
//
//        ScrollableRecordSet rs = executeSelectRecordSet(sql, new ArrayList());
//        return rs;
//    }
//
//    private static Connection getConnection(String pool) throws DAOException {
//        return DAOHelper.getConnection(pool);
//    }
//
//    private static String formatException(Exception e) {
//        return DAOHelper.formatException(e);
//    }
//}
//}
