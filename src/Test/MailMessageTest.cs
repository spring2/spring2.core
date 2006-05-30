using System;
using System.IO;
using NUnit.Framework;
using Spring2.Core.Mail.BusinessLogic;
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


    }
}
