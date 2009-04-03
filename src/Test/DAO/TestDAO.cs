using System;
using Spring2.Core.DAO;

namespace Spring2.Core.Test.DAO {

    /// <summary>
    /// Summary description for TestDAO.
    /// </summary>
    public class TestDAO : SqlEntityDAO {

	protected override String ConnectionStringKey {
	    get { return "Registry:HKLM:Software\\SeamlessWeb\\customDB"; }
	}

	public static void GetConnectionString() {
	    TestDAO dao = new TestDAO();
	    dao.GetConnectionString(dao.ConnectionStringKey);
	}

    }
}
