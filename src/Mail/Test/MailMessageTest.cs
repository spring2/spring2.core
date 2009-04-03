using System;
using System.Collections;
using System.Diagnostics;
using GoldCanyon.Dss.BusinessLogic;
using GoldCanyon.Dss.Dao;
using GoldCanyon.Dss.DataObject;
using GoldCanyon.Dss.Exceptions;
using GoldCanyon.Dss.Facade;
using GoldCanyon.Dss.Types;
using NUnit.Framework;
using Spring2.Core.DAO;
using Spring2.Core.Mail.BusinessLogic;
using Spring2.Core.Mail.Dao;
using Spring2.Core.Mail.DataObject;
using Spring2.Core.Mail.Types;
using Spring2.Core.Message;
using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;

namespace GoldCanyon.Dss.Test {
    
    
    /// <summary>
    /// Summary description for MailMessageTest.
    /// </summary>
    [TestFixture()]
    public class MailMessageTest : BaseTest {
        
        private ServiceFacade serviceFacade = new ServiceFacade();
        
        private IMailMessage message = null;
        
        private IMailMessageRoute route = null;
        
        // private static readonly String TEST_ADDRESS = "vbunit@spring2.com";
        // LONG_TO_ADDRESS is longer than 250 chars (original length prior to change to varchar(6000)
        // private static readonly String LONG_TO_ADDRESS = @"1aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa@makinganameformyself.com;2aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa@makinganameformyself.com;3aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa@makinganameformyself.com;4aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa@makinganameformyself.com;5aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa@makinganameformyself.com";
        private StringType SUBJECT = StringType.Parse("Subject");
        
        private StringType TO_ADDRESS = StringType.Parse("to@spring2.com");
        
        private StringType FROM_ADDRESS = StringType.Parse("from@spring2.com");
        
        private StringType CC_ADDRESS = StringType.Parse("cc@spring2.com");
        
        private StringType BCC_ADDRESS = StringType.Parse("bcc@spring2.com");
        
        private StringType BODY = StringType.Parse("Body");
        
        private StringType SEMICOLON = StringType.Parse(";");
        
        [SetUp()]
        protected void SetUp() {
            serviceFacade.Login();
        }
        
        [TearDown()]
        protected void TearDown() {
            if (message != null) {
		if (message.MailMessageId.IsValid) {
		    TestUtility.DeleteMailMessage((MailMessage)message);
		    message = null;
		}
	    }

	    if (route != null) {
		if (route.MailMessageRouteId.IsValid) {
		    MailMessageRouteDAO.DAO.Delete(route.MailMessageRouteId);
		    route = null;
		}
	    }
        }
        
        [Generate()]
        [Test()]
        public void ToStringTest() {
            IList list = MailMessageDAO.DAO.GetList(1);
	    foreach(IMailMessage entity in list) {
		Assert.AreNotEqual(entity.GetType().ToString(), entity.ToString());
	    }
        }
        
        [Test()]
        public void CreateMailMessage1() {
            message = serviceFacade.CreateMailMessage(MailMessageTypeEnum.UNIT_TEST, FROM_ADDRESS, TO_ADDRESS, SUBJECT, BODY, MailBodyFormatEnum.TEXT);

	    Assert.IsTrue(message.MailMessageId.IsValid);
	    Assert.IsTrue(message.MailMessageId.ToInt32() > 0);
	    Assert.AreEqual(DateTimeType.DEFAULT, message.ScheduleTime);
	    Assert.AreEqual(DateTimeType.DEFAULT, message.ProcessedTime);
	    Assert.AreEqual(MailPriorityEnum.NORMAL, message.Priority);
	    Assert.AreEqual(FROM_ADDRESS, message.From);
	    Assert.AreEqual(TO_ADDRESS, message.To.Replace(SEMICOLON, StringType.EMPTY));
	    Assert.AreEqual(StringType.EMPTY, message.Cc);
	    Assert.AreEqual(StringType.EMPTY, message.Bcc);
	    Assert.AreEqual(SUBJECT, message.Subject);
	    Assert.AreEqual(MailBodyFormatEnum.TEXT, message.BodyFormat);
	    Assert.AreEqual(BODY, message.Body);
	    Assert.AreEqual(MailMessageTypeEnum.UNIT_TEST.Code, message.MailMessageType.ToString());
	    Assert.AreEqual(MailMessageStatusEnum.UNPROCESSED, message.MailMessageStatus);
	    Assert.AreEqual(IdType.DEFAULT, message.ReleasedByUserId);
	    Assert.AreEqual(0, message.Attachments.Count);
        }
        
        [Test()]
        public void CreateMailMessage2() {
            message = serviceFacade.CreateMailMessage(MailMessageTypeEnum.UNIT_TEST, SUBJECT, BODY, MailBodyFormatEnum.TEXT);

	    Assert.IsTrue(message.MailMessageId.IsValid);
	    Assert.IsTrue(message.MailMessageId.ToInt32() > 0);
	    Assert.AreEqual(DateTimeType.DEFAULT, message.ScheduleTime);
	    Assert.AreEqual(DateTimeType.DEFAULT, message.ProcessedTime);
	    Assert.AreEqual(MailPriorityEnum.NORMAL, message.Priority);
	    Assert.AreEqual(StringType.EMPTY, message.From);
	    Assert.AreEqual(StringType.EMPTY, message.To);
	    Assert.AreEqual(StringType.EMPTY, message.Cc);
	    Assert.AreEqual(StringType.EMPTY, message.Bcc);
	    Assert.AreEqual(SUBJECT, message.Subject);
	    Assert.AreEqual(MailBodyFormatEnum.TEXT, message.BodyFormat);
	    Assert.AreEqual(BODY, message.Body);
	    Assert.AreEqual(MailMessageTypeEnum.UNIT_TEST.Code, message.MailMessageType.ToString());
	    Assert.AreEqual(MailMessageStatusEnum.UNPROCESSED, message.MailMessageStatus);
	    Assert.AreEqual(IdType.DEFAULT, message.ReleasedByUserId);
	    Assert.AreEqual(0, message.Attachments.Count);
        }
        
        [Test()]
        public void CreateMailMessage3() {
            message = serviceFacade.CreateMailMessage(MailMessageTypeEnum.UNIT_TEST, TO_ADDRESS, SUBJECT, BODY, MailBodyFormatEnum.TEXT);

	    Assert.IsTrue(message.MailMessageId.IsValid);
	    Assert.IsTrue(message.MailMessageId.ToInt32() > 0);
	    Assert.AreEqual(DateTimeType.DEFAULT, message.ScheduleTime);
	    Assert.AreEqual(DateTimeType.DEFAULT, message.ProcessedTime);
	    Assert.AreEqual(MailPriorityEnum.NORMAL, message.Priority);
	    Assert.AreEqual(StringType.EMPTY, message.From);
	    Assert.AreEqual(TO_ADDRESS, message.To.Replace(SEMICOLON, StringType.EMPTY));
	    Assert.AreEqual(StringType.EMPTY, message.Cc);
	    Assert.AreEqual(StringType.EMPTY, message.Bcc);
	    Assert.AreEqual(SUBJECT, message.Subject);
	    Assert.AreEqual(MailBodyFormatEnum.TEXT, message.BodyFormat);
	    Assert.AreEqual(BODY, message.Body);
	    Assert.AreEqual(MailMessageTypeEnum.UNIT_TEST.Code, message.MailMessageType.ToString());
	    Assert.AreEqual(MailMessageStatusEnum.UNPROCESSED, message.MailMessageStatus);
	    Assert.AreEqual(IdType.DEFAULT, message.ReleasedByUserId);
	    Assert.AreEqual(0, message.Attachments.Count);
        }
        
        [Test()]
        public void CreateMailMessage4() {
            DateTimeType now = DateTimeType.Now;
	    message = serviceFacade.CreateMailMessage(MailMessageTypeEnum.UNIT_TEST, FROM_ADDRESS, TO_ADDRESS, SUBJECT, BODY, MailBodyFormatEnum.TEXT, now);

	    Assert.IsTrue(message.MailMessageId.IsValid);
	    Assert.IsTrue(message.MailMessageId.ToInt32() > 0);
	    Assert.AreEqual(now, message.ScheduleTime);
	    Assert.AreEqual(DateTimeType.DEFAULT, message.ProcessedTime);
	    Assert.AreEqual(MailPriorityEnum.NORMAL, message.Priority);
	    Assert.AreEqual(FROM_ADDRESS, message.From);
	    Assert.AreEqual(TO_ADDRESS, message.To.Replace(SEMICOLON, StringType.EMPTY));
	    Assert.AreEqual(StringType.EMPTY, message.Cc);
	    Assert.AreEqual(StringType.EMPTY, message.Bcc);
	    Assert.AreEqual(SUBJECT, message.Subject);
	    Assert.AreEqual(MailBodyFormatEnum.TEXT, message.BodyFormat);
	    Assert.AreEqual(BODY, message.Body);
	    Assert.AreEqual(MailMessageTypeEnum.UNIT_TEST.Code, message.MailMessageType.ToString());
	    Assert.AreEqual(MailMessageStatusEnum.UNPROCESSED, message.MailMessageStatus);
	    Assert.AreEqual(IdType.DEFAULT, message.ReleasedByUserId);
	    Assert.AreEqual(0, message.Attachments.Count);
        }
        
        [Test()]
        public void CreateMailMessage5() {
            route = TestUtility.CreateMailMessageRoute(RoutingTypeEnum.FROM, FROM_ADDRESS);
	    DateTimeType now = DateTimeType.Now;
            message = serviceFacade.CreateMailMessage(MailMessageTypeEnum.UNIT_TEST, TO_ADDRESS, SUBJECT, BODY, MailBodyFormatEnum.TEXT, now);

	    Assert.IsTrue(message.MailMessageId.IsValid);
	    Assert.IsTrue(message.MailMessageId.ToInt32() > 0);
	    Assert.AreEqual(now, message.ScheduleTime);
	    Assert.AreEqual(DateTimeType.DEFAULT, message.ProcessedTime);
	    Assert.AreEqual(MailPriorityEnum.NORMAL, message.Priority);
	    Assert.AreEqual(route.EmailAddress, message.From);
	    Assert.AreEqual(TO_ADDRESS, message.To.Replace(SEMICOLON, StringType.EMPTY));
	    Assert.AreEqual(StringType.EMPTY, message.Cc);
	    Assert.AreEqual(StringType.EMPTY, message.Bcc);
	    Assert.AreEqual(SUBJECT, message.Subject);
	    Assert.AreEqual(MailBodyFormatEnum.TEXT, message.BodyFormat);
	    Assert.AreEqual(BODY, message.Body);
	    Assert.AreEqual(MailMessageTypeEnum.UNIT_TEST.Code, message.MailMessageType.ToString());
	    Assert.AreEqual(MailMessageStatusEnum.UNPROCESSED, message.MailMessageStatus);
	    Assert.AreEqual(IdType.DEFAULT, message.ReleasedByUserId);
	    Assert.AreEqual(0, message.Attachments.Count);
        }
        
        [Test()]
        public void CreateMailMessage6() {
            route = TestUtility.CreateMailMessageRoute(RoutingTypeEnum.FROM, FROM_ADDRESS);
	    DateTimeType now = DateTimeType.Now;
	    MailMessageData data = new MailMessageData();
	    data.Bcc = BCC_ADDRESS;
	    data.Body = BODY;
	    data.BodyFormat = MailBodyFormatEnum.TEXT;
	    data.Cc = CC_ADDRESS;
	    data.From = FROM_ADDRESS;
	    data.Priority = MailPriorityEnum.NORMAL;
	    data.ScheduleTime = now;
	    data.Subject = SUBJECT;
	    data.To = TO_ADDRESS;
            message = serviceFacade.CreateMailMessage(data, MailMessageTypeEnum.UNIT_TEST);

	    Assert.IsTrue(message.MailMessageId.IsValid);
	    Assert.IsTrue(message.MailMessageId.ToInt32() > 0);
	    Assert.AreEqual(data.ScheduleTime, message.ScheduleTime);
	    Assert.AreEqual(data.ProcessedTime, message.ProcessedTime);
	    Assert.AreEqual(data.Priority, message.Priority);
	    Assert.AreEqual(data.From, message.From);
	    Assert.AreEqual(data.To, message.To.Replace(SEMICOLON, StringType.EMPTY));
	    Assert.AreEqual(data.Cc, message.Cc);
	    Assert.AreEqual(data.Bcc, message.Bcc);
	    Assert.AreEqual(data.Subject, message.Subject);
	    Assert.AreEqual(data.BodyFormat, message.BodyFormat);
	    Assert.AreEqual(data.Body, message.Body);
	    Assert.AreEqual(MailMessageTypeEnum.UNIT_TEST.Code, message.MailMessageType.ToString());
	    Assert.AreEqual(MailMessageStatusEnum.UNPROCESSED, message.MailMessageStatus);
	    Assert.AreEqual(data.ReleasedByUserId, message.ReleasedByUserId);
	    Assert.AreEqual(0, message.Attachments.Count);
        }
        
        [Test()]
        public void CreateMailMessage7() {
            route = TestUtility.CreateMailMessageRoute(RoutingTypeEnum.FROM, FROM_ADDRESS);
	    DateTimeType now = DateTimeType.Now;
	    MailMessageData data = new MailMessageData();
	    data.Bcc = BCC_ADDRESS;
	    data.Body = BODY;
	    data.BodyFormat = MailBodyFormatEnum.TEXT;
	    data.Cc = CC_ADDRESS;
	    data.From = FROM_ADDRESS;
	    data.Priority = MailPriorityEnum.NORMAL;
	    data.ScheduleTime = now;
	    data.Subject = SUBJECT;
	    data.To = TO_ADDRESS;
	    data.MailMessageType = MailMessageTypeEnum.UNIT_TEST.Code;
	    message = serviceFacade.CreateMailMessage(data);

	    Assert.IsTrue(message.MailMessageId.IsValid);
	    Assert.IsTrue(message.MailMessageId.ToInt32() > 0);
	    Assert.AreEqual(data.ScheduleTime, message.ScheduleTime);
	    Assert.AreEqual(data.ProcessedTime, message.ProcessedTime);
	    Assert.AreEqual(data.Priority, message.Priority);
	    Assert.AreEqual(data.From, message.From);
	    Assert.AreEqual(data.To, message.To.Replace(SEMICOLON, StringType.EMPTY));
	    Assert.AreEqual(data.Cc, message.Cc);
	    Assert.AreEqual(data.Bcc, message.Bcc);
	    Assert.AreEqual(data.Subject, message.Subject);
	    Assert.AreEqual(data.BodyFormat, message.BodyFormat);
	    Assert.AreEqual(data.Body, message.Body);
	    Assert.AreEqual(MailMessageTypeEnum.UNIT_TEST.Code, message.MailMessageType.ToString());
	    Assert.AreEqual(MailMessageStatusEnum.UNPROCESSED, message.MailMessageStatus);
	    Assert.AreEqual(data.ReleasedByUserId, message.ReleasedByUserId);
	    Assert.AreEqual(0, message.Attachments.Count);
        }
        
        // [Test()]
        // public void GetMailMessage() {
        // 
        // }
        // 
        // [Test()]
        // public void GetMessageByStatus() {
        // 
        // }
        // 
        // [Test()]
        // public void MarkSent() {
        // 
        // }
        // 
        // [Test()]
        // public void Release() {
        // 
        // }
        // 
        // [Test()]
        // public void Reject() {
        // 
        // }
        // 
        // [Test()]
        // public void NotAllowed() {
        // 
        // }
        // 
        // [Test()]
        // public void Hold() {
        // 
        // }
        // 
        // [Test()]
        // public void AddRemoveAttachments() {
        // 
        // }
        private void Nothing() {
            
        }
    }
}