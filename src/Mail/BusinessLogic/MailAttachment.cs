using System;
using System.IO;
using Spring2.Core.BusinessEntity;
using Spring2.Core.Message;
using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;
using Spring2.Core.Mail.Dao;
using Spring2.Core.Mail.DataObject;
using Spring2.Core.Mail.Types;

namespace Spring2.Core.Mail.BusinessLogic {


    public class MailAttachment : Spring2.Core.BusinessEntity.BusinessEntity, IMailAttachment {

	[Generate()]
	private IdType mailAttachmentId = IdType.DEFAULT;
	[Generate()]
	private IdType mailMessageId = IdType.DEFAULT;
	[Generate()]
	private StringType filename = StringType.DEFAULT;

	private Byte[] buffer = null;

	[Generate()]
	internal MailAttachment() {
	}

	[Generate()]
	internal MailAttachment(Boolean isNew) {
	    this.isNew = isNew;
	}


	[Generate()]
	public IdType MailAttachmentId {
	    get { return this.mailAttachmentId; }
	    set { this.mailAttachmentId = value; }
	}

	[Generate()]
	public IdType MailMessageId {
	    get { return this.mailMessageId; }
	    set { this.mailMessageId = value; }
	}

	[Generate()]
	public StringType Filename {
	    get { return this.filename; }
	    set { this.filename = value; }
	}

	public Byte[] Buffer {
	    get { return buffer; }
	    set { buffer = value; }
	}

	[Generate()]
	public static MailAttachment NewInstance() {
	    return new MailAttachment();
	}

	[Generate()]
	public static MailAttachment GetInstance(IdType mailAttachmentId) {
	    return MailAttachmentDAO.DAO.Load(mailAttachmentId);
	}

	public void Update(MailAttachmentData data) {
	    mailAttachmentId = data.MailAttachmentId.IsDefault ? mailAttachmentId : data.MailAttachmentId;
	    mailMessageId = data.MailMessageId.IsDefault ? mailMessageId : data.MailMessageId;
	    filename = data.Filename.IsDefault ? filename : data.Filename;
	    buffer = data.Buffer;
	    Store();
	}

	[Generate()]
	public void Store() {
	    MessageList errors = Validate();

	    if (errors.Count > 0) {
		if (!isNew) {
		    this.Reload();
		}
		throw new MessageListException(errors);
	    }

	    if (isNew) {
		this.MailAttachmentId = MailAttachmentDAO.DAO.Insert(this);
		isNew = false;
	    } else {
		MailAttachmentDAO.DAO.Update(this);
	    }
	}

	[Generate()]
	public virtual MessageList Validate() {

	    MessageList errors = new MessageList();
	    return errors;
	}

	[Generate()]
	public void Reload() {
	    MailAttachmentDAO.DAO.Reload(this);
	}

	public static MailAttachment Create(MailAttachmentData data) {
	    MailAttachment attachment = new MailAttachment();
	    attachment.Update(data);

	    return attachment;
	}

	public static MailAttachment Create(MailMessage message, StringType filename) {
	    if (!File.Exists(filename)) {
		throw new ArgumentException(String.Format("File {0} does not exist", filename), "filename");
	    }

	    // read in the file
	    FileStream stream = File.OpenRead(filename);
	    Byte[] bytes = ReadFully(stream, stream.Length);
	    stream.Close();

	    // create the attachment
	    MailAttachment attachment = new MailAttachment();
	    attachment.Filename = Path.GetFileName(filename);
	    attachment.MailMessageId = message.MailMessageId;
	    attachment.Buffer = bytes;
	    attachment.Store();

	    return attachment;
	}

	public void WriteAttachment(String path) {
	    FileStream fs = File.Create(Path.Combine(path, Filename));
	    BinaryWriter bw = new BinaryWriter(fs);
	    bw.Write(Buffer);
	    bw.Close();
	    fs.Close();
	}

	/// <summary>
	/// Reads data from a stream until the end is reached. The
	/// data is returned as a byte array. An IOException is
	/// thrown if any of the underlying IO calls fail.
	/// </summary>
	/// <param name="stream">The stream to read data from</param>
	/// <param name="initialLength">The initial buffer length</param>
	public static Byte[] ReadFully(Stream stream, Int64 initialLength) {
	    // If we've been passed an unhelpful initial length, just
	    // use 32K.
	    if (initialLength < 1) {
		initialLength = 32768;
	    }

	    Byte[] buffer = new Byte[initialLength];
	    int read = 0;

	    int chunk;
	    while ((chunk = stream.Read(buffer, read, buffer.Length - read)) > 0) {
		read += chunk;

		// If we've reached the end of our buffer, check to see if there's
		// any more information
		if (read == buffer.Length) {
		    int nextByte = stream.ReadByte();

		    // End of stream? If so, we're done
		    if (nextByte == -1) {
			return buffer;
		    }

		    // Nope. Resize the buffer, put in the byte we've just
		    // read, and continue
		    Byte[] newBuffer = new Byte[buffer.Length * 2];
		    Array.Copy(buffer, newBuffer, buffer.Length);
		    newBuffer[read] = (Byte)nextByte;
		    buffer = newBuffer;
		    read++;
		}
	    }
	    // Buffer is now too big. Shrink it.
	    Byte[] ret = new Byte[read];
	    Array.Copy(buffer, ret, read);
	    return ret;
	}

	/// <summary>
	/// Convert a string to a byte array
	/// </summary>
	/// <param name="s">String to be converted</param>
	/// <returns></returns>
	private static Byte[] StringToByteArray(String s) {
	    System.Text.ASCIIEncoding encoding = new System.Text.ASCIIEncoding();
	    return encoding.GetBytes(s);
	}

	/// <summary>
	/// Convert a byte array to a string
	/// </summary>
	/// <param name="bytes"></param>
	/// <returns></returns>
	private static String ByteArrayToString(Byte[] bytes) {
	    System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();
	    return enc.GetString(bytes);
	}



	[Generate()]
	public override String ToString() {
	    return GetType().ToString() + "@" + MailAttachmentId.ToString();
	}
    }
}
