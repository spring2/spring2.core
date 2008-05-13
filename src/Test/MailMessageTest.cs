using System;
using System.Collections;
using System.IO;
using NUnit.Framework;
using Spring2.Core.DAO;
using Spring2.Core.Mail.BusinessLogic;
using Spring2.Core.Mail.Dao;
using Spring2.Core.Mail.DataObject;
using Spring2.Core.Mail.Types;
using Spring2.Core.Types;

namespace Spring2.Core.Test {

    /// <summary>
    /// Tests for MailMessage
    /// </summary>
    [TestFixture]
    public class MailMessageTest {

	private static readonly String TEMP_DIRECTORY = @"c:\temp\MailMessageTest";
	private static readonly String TEXT_ATTACHMENT = Path.Combine(TEMP_DIRECTORY, "TextFile.txt");
	private static readonly String BINARY_ATTACHMENT1 = "test.pdf";
	private static readonly String BINARY_ATTACHMENT2 = "test.jpg";

        [Test]
        public void Temp() {
        }

	[Test]
	public void TextAttachment() {
	    try {
		if (!Directory.Exists(TEMP_DIRECTORY)) {
		    Directory.CreateDirectory(TEMP_DIRECTORY);
		}

		// create a text temp file
		String content = String.Format("This is a test content created at {0}", DateTime.Now);
		StreamWriter writer = File.CreateText(TEXT_ATTACHMENT);
		writer.Write(content);
		writer.Close();
	    
		Assert.IsTrue(File.Exists(TEXT_ATTACHMENT));
	    
		// create mail message with attached text temp file
	    	MailMessage message = MailMessage.Create("TextAttachmentTest", "foo@bar.xyz", "bar@foo.abc", "subject", "body", MailBodyFormatEnum.TEXT, new String[] { TEXT_ATTACHMENT });
		File.Delete(TEXT_ATTACHMENT);

		// check for queued message
		MailMessageList messages = MailMessage.GetMailMessagesToSend();
		Assert.IsTrue(messages.Contains(message.MailMessageId));

		// output attachment
		Assert.AreEqual(1, message.Attachments.Count);
		((MailAttachment)message.Attachments[0]).WriteAttachment(TEMP_DIRECTORY);
		Assert.IsTrue(File.Exists(TEXT_ATTACHMENT));

		// verify that it is the same as the original
		StreamReader reader = File.OpenText(TEXT_ATTACHMENT);
		String fileContents = reader.ReadToEnd();
		reader.Close();
		Assert.AreEqual(content, fileContents);
	    } finally {
	    	if (File.Exists(TEXT_ATTACHMENT)) {
	    	    File.Delete(TEXT_ATTACHMENT);
	    	}
	    }
	}

	[Test]
	public void BinaryAttachment_PDF() {
	    try {
		if (!Directory.Exists(TEMP_DIRECTORY)) {
		    Directory.CreateDirectory(TEMP_DIRECTORY);
		}

		Assert.IsTrue(File.Exists(BINARY_ATTACHMENT1));
		FileStream stream = File.OpenRead(BINARY_ATTACHMENT1);
		byte[] bytes = MailAttachment.ReadFully(stream, stream.Length);
		stream.Close();

		CRC32.Init();
		byte[] originalCrc = CRC32.Crc32(bytes);
	    
		// create mail message with attached text temp file
		MailMessage message = MailMessage.Create("TextAttachmentTest", "foo@bar.xyz", "bar@foo.abc", "subject", "body", MailBodyFormatEnum.TEXT, new String[] { BINARY_ATTACHMENT1 });

		// check for queued message
		MailMessageList messages = MailMessage.GetMailMessagesToSend();
		Assert.IsTrue(messages.Contains(message.MailMessageId));

		// output attachment
		Assert.AreEqual(1, message.Attachments.Count);
		//message = MailMessage.GetInstance(messages[0].MailMessageId);
		
		((MailAttachment)message.Attachments[0]).WriteAttachment(TEMP_DIRECTORY);
		Assert.IsTrue(File.Exists(Path.Combine(TEMP_DIRECTORY, BINARY_ATTACHMENT1)));

		// verify that it is the same as the original
		stream = File.OpenRead(Path.Combine(TEMP_DIRECTORY, BINARY_ATTACHMENT1));
		bytes = MailAttachment.ReadFully(stream, stream.Length);
		stream.Close();
		byte[] crc = CRC32.Crc32(bytes);
		Assert.AreEqual(originalCrc, crc);
	    } finally {
		if (File.Exists(Path.Combine(TEMP_DIRECTORY, BINARY_ATTACHMENT1))) {
		    File.Delete(Path.Combine(TEMP_DIRECTORY, BINARY_ATTACHMENT1));
		}
	    }
	}

	[Test]
	public void BinaryAttachment_JPG() {
	    try {
		if (!Directory.Exists(TEMP_DIRECTORY)) {
		    Directory.CreateDirectory(TEMP_DIRECTORY);
		}

		Assert.IsTrue(File.Exists(BINARY_ATTACHMENT2));
		FileStream stream = File.OpenRead(BINARY_ATTACHMENT2);
		byte[] bytes = MailAttachment.ReadFully(stream, stream.Length);
		stream.Close();

		CRC32.Init();
		byte[] originalCrc = CRC32.Crc32(bytes);
	    
		// create mail message with attached text temp file
		MailMessage message = MailMessage.Create("TextAttachmentTest", "foo@bar.xyz", "bar@foo.abc", "subject", "body", MailBodyFormatEnum.TEXT, new String[] { BINARY_ATTACHMENT2 });

		// check for queued message
		MailMessageList messages = MailMessage.GetMailMessagesToSend();
		Assert.IsTrue(messages.Contains(message.MailMessageId));

		// output attachment
		Assert.AreEqual(1, message.Attachments.Count);
		((MailAttachment)message.Attachments[0]).WriteAttachment(TEMP_DIRECTORY);
		Assert.IsTrue(File.Exists(Path.Combine(TEMP_DIRECTORY, BINARY_ATTACHMENT2)));

		// verify that it is the same as the original
		stream = File.OpenRead(Path.Combine(TEMP_DIRECTORY, BINARY_ATTACHMENT2));
		bytes = MailAttachment.ReadFully(stream, stream.Length);
		stream.Close();
		byte[] crc = CRC32.Crc32(bytes);
		Assert.AreEqual(originalCrc, crc);
	    } finally {
		if (File.Exists(Path.Combine(TEMP_DIRECTORY, BINARY_ATTACHMENT2))) {
		    File.Delete(Path.Combine(TEMP_DIRECTORY, BINARY_ATTACHMENT2));
		}
	    }
	}

	[Test]
	[Ignore("Need a mailbox for tests")]
	public void SendWithBinaryAttachment() {
	    if (!Directory.Exists(TEMP_DIRECTORY)) {
		Directory.CreateDirectory(TEMP_DIRECTORY);
	    }

	    Assert.IsTrue(File.Exists(BINARY_ATTACHMENT2));
	    
	    // create mail message with attached text temp file
	    MailMessage message = MailMessage.Create("TextAttachmentTest", "cort@spring2.com", "cort@spring2.com", "subject", "body", MailBodyFormatEnum.TEXT, new String[] { BINARY_ATTACHMENT2 });
	    message.Send();
	}

    	[Test]
	public void FindMailMessageRouteByMailMessageType() {
    	    MailMessageRouteList routes = MailMessageRouteDAO.DAO.FindByMailMessage("foo");
    	}
    	
	private static readonly StringType FROM = StringType.Parse("unittest@foo.bar");
	private static readonly StringType SUBJECT = StringType.Parse("Subject");
	private static readonly StringType TO_ADDRESS = StringType.Parse("to@spring2.com");
	private static readonly StringType FROM_ADDRESS = StringType.Parse("from@spring2.com");
	private static readonly StringType CC_ADDRESS = StringType.Parse("cc@spring2.com");
	private static readonly StringType BCC_ADDRESS = StringType.Parse("bcc@spring2.com");
	private static readonly StringType BODY = StringType.Parse("Body");
	private static readonly StringType SEMICOLON = StringType.Parse(";");

	private MailMessage message = null;
	private MailMessageRoute route = null;
    	
    	public struct MailMessageTypeEnum {
    	    public static String UNIT_TEST = "Unit Test";
    	}
    	
    	[SetUp]
	public void Setup() {
    	    SqlFilter filter = new SqlFilter(new SqlEqualityPredicate("MailMessage", EqualityOperatorEnum.Equal, "Unit Test"));
    	    filter.And(new SqlEqualityPredicate("RoutingType", EqualityOperatorEnum.Equal, RoutingTypeEnum.FROM.Code));
    	    MailMessageRouteList routes = MailMessageRouteDAO.DAO.GetList(filter);
    	    if (routes.Count == 0) {
    	    	route = MailMessageRoute.NewInstance();
    	    	route.MailMessage = MailMessageTypeEnum.UNIT_TEST;
    	    	route.RoutingType = RoutingTypeEnum.FROM;
    	    	route.EmailAddress = FROM;
    		route.Store();
    	    }
    	}

	[TearDown()]
	protected void TearDown() {
	    if (message != null) {
		if (message.MailMessageId.IsValid) {
		    MailMessageDAO.DAO.Delete(message.MailMessageId);
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

	[Test()]
	public void ToStringTest() {
	    IList list = MailMessageDAO.DAO.GetList(1);
	    foreach(IMailMessage entity in list) {
		Assert.AreNotEqual(entity.GetType().ToString(), entity.ToString());
	    }
	}

	[Test()]
	public void CreateMailMessage1() {
	    message = MailMessage.Create(MailMessageTypeEnum.UNIT_TEST, FROM_ADDRESS, TO_ADDRESS, SUBJECT, BODY, MailBodyFormatEnum.TEXT);

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
	    Assert.AreEqual(MailMessageTypeEnum.UNIT_TEST, message.MailMessageType.ToString());
	    Assert.AreEqual(MailMessageStatusEnum.UNPROCESSED, message.MailMessageStatus);
	    Assert.AreEqual(IdType.DEFAULT, message.ReleasedByUserId);
	    Assert.AreEqual(0, message.Attachments.Count);
	}

	[Test()]
	public void CreateMailMessage2() {
	    message = MailMessage.Create(MailMessageTypeEnum.UNIT_TEST, SUBJECT, BODY, MailBodyFormatEnum.TEXT);

	    Assert.IsTrue(message.MailMessageId.IsValid);
	    Assert.IsTrue(message.MailMessageId.ToInt32() > 0);
	    Assert.AreEqual(DateTimeType.DEFAULT, message.ScheduleTime);
	    Assert.AreEqual(DateTimeType.DEFAULT, message.ProcessedTime);
	    Assert.AreEqual(MailPriorityEnum.NORMAL, message.Priority);
	    Assert.AreEqual(FROM, message.From);
	    Assert.AreEqual(StringType.EMPTY, message.To);
	    Assert.AreEqual(StringType.EMPTY, message.Cc);
	    Assert.AreEqual(StringType.EMPTY, message.Bcc);
	    Assert.AreEqual(SUBJECT, message.Subject);
	    Assert.AreEqual(MailBodyFormatEnum.TEXT, message.BodyFormat);
	    Assert.AreEqual(BODY, message.Body);
	    Assert.AreEqual(MailMessageTypeEnum.UNIT_TEST, message.MailMessageType.ToString());
	    Assert.AreEqual(MailMessageStatusEnum.UNPROCESSED, message.MailMessageStatus);
	    Assert.AreEqual(IdType.DEFAULT, message.ReleasedByUserId);
	    Assert.AreEqual(0, message.Attachments.Count);
	}

	[Test()]
	public void CreateMailMessage3() {
	    message = MailMessage.Create(MailMessageTypeEnum.UNIT_TEST, TO_ADDRESS, SUBJECT, BODY, MailBodyFormatEnum.TEXT);

	    Assert.IsTrue(message.MailMessageId.IsValid);
	    Assert.IsTrue(message.MailMessageId.ToInt32() > 0);
	    Assert.AreEqual(DateTimeType.DEFAULT, message.ScheduleTime);
	    Assert.AreEqual(DateTimeType.DEFAULT, message.ProcessedTime);
	    Assert.AreEqual(MailPriorityEnum.NORMAL, message.Priority);
	    Assert.AreEqual(FROM, message.From);
	    Assert.AreEqual(TO_ADDRESS, message.To.Replace(SEMICOLON, StringType.EMPTY));
	    Assert.AreEqual(StringType.EMPTY, message.Cc);
	    Assert.AreEqual(StringType.EMPTY, message.Bcc);
	    Assert.AreEqual(SUBJECT, message.Subject);
	    Assert.AreEqual(MailBodyFormatEnum.TEXT, message.BodyFormat);
	    Assert.AreEqual(BODY, message.Body);
	    Assert.AreEqual(MailMessageTypeEnum.UNIT_TEST, message.MailMessageType.ToString());
	    Assert.AreEqual(MailMessageStatusEnum.UNPROCESSED, message.MailMessageStatus);
	    Assert.AreEqual(IdType.DEFAULT, message.ReleasedByUserId);
	    Assert.AreEqual(0, message.Attachments.Count);
	}

	[Test()]
	public void CreateMailMessage4() {
	    DateTimeType now = DateTimeType.Now;
	    message = MailMessage.Create(MailMessageTypeEnum.UNIT_TEST, FROM_ADDRESS, TO_ADDRESS, SUBJECT, BODY, MailBodyFormatEnum.TEXT, now);

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
	    Assert.AreEqual(MailMessageTypeEnum.UNIT_TEST, message.MailMessageType.ToString());
	    Assert.AreEqual(MailMessageStatusEnum.UNPROCESSED, message.MailMessageStatus);
	    Assert.AreEqual(IdType.DEFAULT, message.ReleasedByUserId);
	    Assert.AreEqual(0, message.Attachments.Count);
	}

	[Test()]
	public void CreateMailMessage5() {
	    //route = TestUtility.CreateMailMessageRoute(RoutingTypeEnum.FROM, FROM_ADDRESS);
	    DateTimeType now = DateTimeType.Now;
	    message = MailMessage.Create(MailMessageTypeEnum.UNIT_TEST, TO_ADDRESS, SUBJECT, BODY, MailBodyFormatEnum.TEXT, now);

	    Assert.IsTrue(message.MailMessageId.IsValid);
	    Assert.IsTrue(message.MailMessageId.ToInt32() > 0);
	    Assert.AreEqual(now, message.ScheduleTime);
	    Assert.AreEqual(DateTimeType.DEFAULT, message.ProcessedTime);
	    Assert.AreEqual(MailPriorityEnum.NORMAL, message.Priority);
	    //Assert.AreEqual(route.EmailAddress, message.From);
	    Assert.AreEqual(FROM, message.From);
	    Assert.AreEqual(TO_ADDRESS, message.To.Replace(SEMICOLON, StringType.EMPTY));
	    Assert.AreEqual(StringType.EMPTY, message.Cc);
	    Assert.AreEqual(StringType.EMPTY, message.Bcc);
	    Assert.AreEqual(SUBJECT, message.Subject);
	    Assert.AreEqual(MailBodyFormatEnum.TEXT, message.BodyFormat);
	    Assert.AreEqual(BODY, message.Body);
	    Assert.AreEqual(MailMessageTypeEnum.UNIT_TEST, message.MailMessageType.ToString());
	    Assert.AreEqual(MailMessageStatusEnum.UNPROCESSED, message.MailMessageStatus);
	    Assert.AreEqual(IdType.DEFAULT, message.ReleasedByUserId);
	    Assert.AreEqual(0, message.Attachments.Count);
	}

	[Test()]
	public void CreateMailMessage6() {
	    //route = TestUtility.CreateMailMessageRoute(RoutingTypeEnum.FROM, FROM_ADDRESS);
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
	    message = MailMessage.Create(data, MailMessageTypeEnum.UNIT_TEST);

	    Assert.IsTrue(message.MailMessageId.IsValid);
	    Assert.IsTrue(message.MailMessageId.ToInt32() > 0);
	    Assert.AreEqual(data.ScheduleTime, message.ScheduleTime);
	    Assert.AreEqual(data.ProcessedTime, message.ProcessedTime);
	    Assert.AreEqual(data.Priority, message.Priority);
	    Assert.AreEqual(data.From, message.From);
	    Assert.AreEqual(data.To, message.To.Replace(SEMICOLON, StringType.EMPTY));
	    Assert.AreEqual(new StringType(data.Cc + SEMICOLON), message.Cc);
	    Assert.AreEqual(new StringType(data.Bcc + SEMICOLON), message.Bcc);
	    Assert.AreEqual(data.Subject, message.Subject);
	    Assert.AreEqual(data.BodyFormat, message.BodyFormat);
	    Assert.AreEqual(data.Body, message.Body);
	    Assert.AreEqual(MailMessageTypeEnum.UNIT_TEST, message.MailMessageType.ToString());
	    Assert.AreEqual(MailMessageStatusEnum.UNPROCESSED, message.MailMessageStatus);
	    Assert.AreEqual(data.ReleasedByUserId, message.ReleasedByUserId);
	    Assert.AreEqual(0, message.Attachments.Count);
	}

	[Test()]
	public void CreateMailMessage7() {
	    //route = TestUtility.CreateMailMessageRoute(RoutingTypeEnum.FROM, FROM_ADDRESS);
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
	    data.MailMessageType = MailMessageTypeEnum.UNIT_TEST;
	    message = MailMessage.Create(data);

	    Assert.IsTrue(message.MailMessageId.IsValid);
	    Assert.IsTrue(message.MailMessageId.ToInt32() > 0);
	    Assert.AreEqual(data.ScheduleTime, message.ScheduleTime);
	    Assert.AreEqual(data.ProcessedTime, message.ProcessedTime);
	    Assert.AreEqual(data.Priority, message.Priority);
	    Assert.AreEqual(data.From, message.From);
	    Assert.AreEqual(data.To, message.To.Replace(SEMICOLON, StringType.EMPTY));
	    Assert.AreEqual(new StringType(data.Cc + SEMICOLON), message.Cc);
	    Assert.AreEqual(new StringType(data.Bcc + SEMICOLON), message.Bcc);
	    Assert.AreEqual(data.Subject, message.Subject);
	    Assert.AreEqual(data.BodyFormat, message.BodyFormat);
	    Assert.AreEqual(data.Body, message.Body);
	    Assert.AreEqual(MailMessageTypeEnum.UNIT_TEST, message.MailMessageType.ToString());
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
