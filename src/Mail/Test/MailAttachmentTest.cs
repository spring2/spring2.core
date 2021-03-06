using System;
using System.Collections;
using NUnit.Framework;
using Spring2.Core.DAO;
using Spring2.Core.Mail.Dao;
using Spring2.Core.Mail.DataObject;
using Spring2.Core.Message;
using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;
using GoldCanyon.Dss.BusinessLogic;
using GoldCanyon.Dss.Dao;
using GoldCanyon.Dss.DataObject;
using GoldCanyon.Dss.Types;

namespace GoldCanyon.Dss.Test {
    
    
    /// <summary>
    /// Summary description for MailAttachmentTest.
    /// </summary>
    [TestFixture()]
    public class MailAttachmentTest : BaseTest {
        
        [Generate()]
        [Test()]
        public void ToStringTest() {
            IList list = MailAttachmentDAO.DAO.GetList(1);
	    foreach(IMailAttachment entity in list) {
		Assert.AreNotEqual(entity.GetType().ToString(), entity.ToString());
	    }
        }
    }
}