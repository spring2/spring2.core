using System;
using System.Data;
using System.IO;
using System.Text;
using System.Web.Mail;
using Spring2.Core.Configuration;
using Spring2.Core.DAO;
using Spring2.Core.BusinessEntity;
using Spring2.Core.Message;
using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;
using Spring2.Core.Mail.Dao;
using Spring2.Core.Mail.DataObject;
using Spring2.Core.Mail.Types;

namespace Spring2.Core.Mail.BusinessLogic {
    
    
    public class MailMessage : Spring2.Core.BusinessEntity.BusinessEntity, IMailMessage {
        
        [Generate()]
        private IdType mailMessageId = IdType.DEFAULT;
        
        [Generate()]
        private DateTimeType scheduleTime = DateTimeType.DEFAULT;
        
        [Generate()]
        private DateTimeType processedTime = DateTimeType.DEFAULT;
        
        [Generate()]
        private MailPriorityEnum priority = MailPriorityEnum.DEFAULT;
        
        [Generate()]
        private StringType mailMessageType = StringType.DEFAULT;
        
        [Generate()]
        private DateTimeType messageQueueDate = DateTimeType.DEFAULT;
        
        [Generate()]
        private StringType from = StringType.DEFAULT;
        
        [Generate()]
        private StringType to = StringType.DEFAULT;
        
        [Generate()]
        private StringType cc = StringType.DEFAULT;
        
        [Generate()]
        private StringType bcc = StringType.DEFAULT;
        
        [Generate()]
        private StringType subject = StringType.DEFAULT;
        
        [Generate()]
        private MailBodyFormatEnum bodyFormat = MailBodyFormatEnum.DEFAULT;
        
        [Generate()]
        private StringType body = StringType.DEFAULT;
        
        [Generate()]
        private MailMessageStatusEnum mailMessageStatus = MailMessageStatusEnum.DEFAULT;
        
        [Generate()]
        private IdType releasedByUserId = IdType.DEFAULT;
        
        [Generate()]
        private MailAttachmentList attachments = MailAttachmentList.DEFAULT;
        
        [Generate()]
        private IntegerType numberOfAttempts = IntegerType.DEFAULT;
        
        [Generate()]
        internal MailMessage() {
            
        }
        
        [Generate()]
        internal MailMessage(Boolean isNew) {
            this.isNew = isNew;
        }
        
        [Generate()]
        public IdType MailMessageId {
            get {
                return this.mailMessageId;
            }
            set {
                this.mailMessageId = value;
            }
        }
        
        [Generate()]
        public StringType MailMessageType {
            get {
                return this.mailMessageType;
            }
            set {
                this.mailMessageType = value;
            }
        }
        
        [Generate()]
        public DateTimeType MessageQueueDate {
            get {
                return this.messageQueueDate;
            }
            set {
                this.messageQueueDate = value;
            }
        }
        
        [Generate()]
        public DateTimeType ScheduleTime {
            get {
                return this.scheduleTime;
            }
            set {
                this.scheduleTime = value;
            }
        }
        
        [Generate()]
        public DateTimeType ProcessedTime {
            get {
                return this.processedTime;
            }
            set {
                this.processedTime = value;
            }
        }
        
        [Generate()]
        public MailPriorityEnum Priority {
            get {
                return this.priority;
            }
            set {
                this.priority = value;
            }
        }
        
        [Generate()]
        public StringType From {
            get {
                return this.from;
            }
            set {
                this.from = value;
            }
        }
        
        [Generate()]
        public StringType To {
            get {
                return this.to;
            }
            set {
                this.to = value;
            }
        }
        
        [Generate()]
        public StringType Cc {
            get {
                return this.cc;
            }
            set {
                this.cc = value;
            }
        }
        
        [Generate()]
        public StringType Bcc {
            get {
                return this.bcc;
            }
            set {
                this.bcc = value;
            }
        }
        
        [Generate()]
        public StringType Subject {
            get {
                return this.subject;
            }
            set {
                this.subject = value;
            }
        }
        
        [Generate()]
        public MailBodyFormatEnum BodyFormat {
            get {
                return this.bodyFormat;
            }
            set {
                this.bodyFormat = value;
            }
        }
        
        [Generate()]
        public StringType Body {
            get {
                return this.body;
            }
            set {
                this.body = value;
            }
        }
        
        [Generate()]
        public MailMessageStatusEnum MailMessageStatus {
            get {
                return this.mailMessageStatus;
            }
            set {
                this.mailMessageStatus = value;
            }
        }
        
        [Generate()]
        public IdType ReleasedByUserId {
            get {
                return this.releasedByUserId;
            }
            set {
                this.releasedByUserId = value;
            }
        }
        
        public MailAttachmentList Attachments {
            get {
                if (this.attachments.IsDefault) {
		    attachments = new MailAttachmentList();
		    foreach(MailAttachment attachment in MailAttachmentDAO.DAO.FindByMailMessageId(this.mailMessageId)) {
			attachments.Add(attachment);
		    }
		}
		return this.attachments;
            }
            set {
                this.attachments = value;
            }
        }
        
        [Generate()]
        public IntegerType NumberOfAttempts {
            get {
                return this.numberOfAttempts;
            }
            set {
                this.numberOfAttempts = value;
            }
        }
        
        [Generate()]
        public static MailMessage NewInstance() {
            return new MailMessage();
        }
        
        [Generate()]
        public static MailMessage GetInstance(IdType mailMessageId) {
            return MailMessageDAO.DAO.Load(mailMessageId);
        }
        
        [Generate()]
        public void Update(MailMessageData data) {
            mailMessageId = data.MailMessageId.IsDefault ? mailMessageId : data.MailMessageId;
	    scheduleTime = data.ScheduleTime.IsDefault ? scheduleTime : data.ScheduleTime;
	    processedTime = data.ProcessedTime.IsDefault ? processedTime : data.ProcessedTime;
	    priority = data.Priority.IsDefault ? priority : data.Priority;
	    from = data.From.IsDefault ? from : data.From;
	    to = data.To.IsDefault ? to : data.To;
	    cc = data.Cc.IsDefault ? cc : data.Cc;
	    bcc = data.Bcc.IsDefault ? bcc : data.Bcc;
	    subject = data.Subject.IsDefault ? subject : data.Subject;
	    bodyFormat = data.BodyFormat.IsDefault ? bodyFormat : data.BodyFormat;
	    body = data.Body.IsDefault ? body : data.Body;
	    mailMessageStatus = data.MailMessageStatus.IsDefault ? mailMessageStatus : data.MailMessageStatus;
	    releasedByUserId = data.ReleasedByUserId.IsDefault ? releasedByUserId : data.ReleasedByUserId;
	    mailMessageType = data.MailMessageType.IsDefault ? mailMessageType : data.MailMessageType;
	    numberOfAttempts = data.NumberOfAttempts.IsDefault ? numberOfAttempts : data.NumberOfAttempts;
	    messageQueueDate = data.MessageQueueDate.IsDefault ? messageQueueDate : data.MessageQueueDate;
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
		this.MailMessageId = MailMessageDAO.DAO.Insert(this);
		isNew = false;
            } else {
		MailMessageDAO.DAO.Update(this);
            }
        }
        
        [Generate()]
        public MessageList Validate() {
            MessageList errors = new MessageList();

	    return errors;
        }
        
        [Generate()]
        public void Reload() {
            MailMessageDAO.DAO.Reload(this);
	    attachments = MailAttachmentList.DEFAULT;
        }
        
        public void MarkSent() {
            int attempts = 0;
	    if(this.NumberOfAttempts.IsValid){
		attempts = this.NumberOfAttempts.ToInt32() + 1;
	    }else{
		attempts = 1;
	    }
            if (this.MailMessageStatus.Equals(MailMessageStatusEnum.UNPROCESSED) || this.MailMessageStatus.Equals(MailMessageStatusEnum.RELEASED)) {
		this.ProcessedTime = new DateTimeType();
		this.MailMessageStatus = MailMessageStatusEnum.SENT;
		this.NumberOfAttempts  = attempts;
		this.Store();
	    } else {
		this.NumberOfAttempts  = attempts;
		this.Store();
		throw new InvalidOperationException("Message can only be marked Sent when it is the Unprocessed or Released state");
	    }
        }
        
        private void SetInitialState() {
            this.Priority = MailPriorityEnum.NORMAL;
	    this.BodyFormat = MailBodyFormatEnum.TEXT;
	    this.MailMessageStatus = MailMessageStatusEnum.UNPROCESSED;
	    this.MessageQueueDate = DateTimeType.Now;
	    this.NumberOfAttempts = 0;
        }
        
        private static StringType GetRoutingAddresses(StringType mailMessage, RoutingTypeEnum routingType, StringType initialAddress) {
            MailMessageRouteList routes = new MailMessageRouteList();
	    WhereClause filter = new WhereClause(MailMessageRouteFields.MAILMESSAGE, mailMessage);
	    filter.And(MailMessageRouteFields.STATUS, ActiveStatusEnum.ACTIVE.Code);
	    filter.And(MailMessageRouteFields.ROUTINGTYPE, routingType.Code);
	    routes.AddRange(MailMessageRouteDAO.DAO.GetList(filter));
	    
	    StringBuilder sb = new StringBuilder();
	    if (initialAddress != null && initialAddress.IsValid) {
		sb.Append(initialAddress.ToString()).Append(";");
	    }

	    foreach(MailMessageRoute route in routes) {
		sb.Append(route.EmailAddress.ToString()).Append(";");
	    }

	    return StringType.Parse(sb.ToString());
        }
        
        /// <summary>
        /// Creates and persists a new MailMessage
        /// </summary>
        /// <param name="data"></param>
        public static MailMessage Create(MailMessageData message) {
            MailMessage mailMessage = new MailMessage();
	    mailMessage.SetInitialState();
	    mailMessage.Update(message);

	    //persist attachments
	    foreach (MailAttachmentData attachment in message.Attachments){
		mailMessage.Attachments.Add(MailAttachment.Create(attachment));
	    }
	    return mailMessage;
        }
        
        /// <summary>
        /// Creates and persists a new MailMessage and signs it using a 'from' address from the message routing table
        /// </summary>
        /// <param name="data"></param>
        public static MailMessage Create(MailMessageData message, StringType messageType) {
            MailMessage mailMessage = new MailMessage();
	    mailMessage.SetInitialState();

	    MailMessageRouteList addresses = new MailMessageRouteList();
	    WhereClause filter = new WhereClause(MailMessageRouteFields.MAILMESSAGE, messageType);
	    filter.And(MailMessageRouteFields.STATUS, ActiveStatusEnum.ACTIVE.Code);
	    filter.And(MailMessageRouteFields.ROUTINGTYPE, RoutingTypeEnum.FROM.Code);
	    addresses.AddRange(MailMessageRouteDAO.DAO.GetList(filter));

	    if (addresses.Count == 0) {
		throw new ArgumentException("No FROM address is defined for " + messageType.ToString());
	    }
	    if (addresses.Count > 1) {
		throw new ArgumentException("More than 1 FROM address is defined for " + messageType.ToString());
	    }

	    mailMessage.From = addresses[0].EmailAddress;
	    mailMessage.To = GetRoutingAddresses(messageType, RoutingTypeEnum.TO, mailMessage.To);
	    mailMessage.Cc = GetRoutingAddresses(messageType, RoutingTypeEnum.CC, mailMessage.Cc);
	    mailMessage.Bcc = GetRoutingAddresses(messageType, RoutingTypeEnum.BCC, mailMessage.Bcc);
	    mailMessage.MailMessageType = messageType;
	    mailMessage.Update(message);

	    //persist attachments
	    foreach (MailAttachmentData attachment in message.Attachments){
		mailMessage.Attachments.Add(MailAttachment.Create(attachment));
	    }
	    return mailMessage;
        }
        
        /// <summary>
        /// Creates and persists a new MailMessage
        /// </summary>
        /// <param name="data"></param>
        public static MailMessage Create(StringType messageType, StringType to, StringType subject, StringType body, MailBodyFormatEnum bodyFormat, DateTimeType scheduleTime) {
            MailMessageData mailMessageData = new MailMessageData();
	    MailMessage mailMessage = new MailMessage();
	    mailMessage.SetInitialState();

	    MailMessageRouteList addresses = new MailMessageRouteList();
	    WhereClause filter = new WhereClause(MailMessageRouteFields.MAILMESSAGE, messageType);
	    filter.And(MailMessageRouteFields.STATUS, ActiveStatusEnum.ACTIVE.Code);
	    filter.And(MailMessageRouteFields.ROUTINGTYPE, RoutingTypeEnum.FROM.Code);
	    addresses.AddRange(MailMessageRouteDAO.DAO.GetList(filter));

	    if (addresses.Count == 0) {
		throw new ArgumentException("No FROM address is defined for " + mailMessage.ToString());
	    }
	    if (addresses.Count > 1) {
		throw new ArgumentException("More than 1 FROM address is defined for " + mailMessage.ToString());
	    }

	    mailMessageData.From = addresses[0].EmailAddress;

	    mailMessageData.To = GetRoutingAddresses(messageType, RoutingTypeEnum.TO, to);
	    mailMessageData.Cc = GetRoutingAddresses(messageType, RoutingTypeEnum.CC, null);
	    mailMessageData.Bcc = GetRoutingAddresses(messageType, RoutingTypeEnum.BCC, null);
	    mailMessageData.Subject = subject;
	    mailMessageData.Body = body;
	    mailMessageData.BodyFormat = bodyFormat;
	    mailMessageData.ScheduleTime = scheduleTime;
	    mailMessageData.MailMessageType = messageType;

	    mailMessage.Update(mailMessageData);

	    return mailMessage;
        }
        
        /// <summary>
        /// Creates and persists a new MailMessage
        /// </summary>
        /// <param name="data"></param>
        public static MailMessage Create(StringType messageType, StringType from, StringType to, StringType subject, StringType body, MailBodyFormatEnum bodyFormat, DateTimeType scheduleTime) {
            MailMessageData mailMessageData = new MailMessageData();
	    MailMessage mailMessage = new MailMessage();
	    mailMessage.SetInitialState();

	    mailMessageData.From = from;

	    mailMessageData.To = GetRoutingAddresses(messageType, RoutingTypeEnum.TO, to);
	    mailMessageData.Cc = GetRoutingAddresses(messageType, RoutingTypeEnum.CC, null);
	    mailMessageData.Bcc = GetRoutingAddresses(messageType, RoutingTypeEnum.BCC, null);
	    mailMessageData.Subject = subject;
	    mailMessageData.Body = body;
	    mailMessageData.BodyFormat = bodyFormat;
	    mailMessageData.ScheduleTime = scheduleTime;
	    mailMessageData.MailMessageType = messageType;

	    mailMessage.Update(mailMessageData);

	    return mailMessage;
        }
        
        /// <summary>
        /// Creates and persists a new MailMessage
        /// </summary>
        /// <param name="data"></param>
        public static MailMessage Create(StringType messageType, StringType to, StringType subject, StringType body, MailBodyFormatEnum bodyFormat) {
            MailMessageData mailMessageData = new MailMessageData();
	    MailMessage mailMessage = new MailMessage();
	    mailMessage.SetInitialState();

	    mailMessageData.To = GetRoutingAddresses(messageType, RoutingTypeEnum.TO, to);
	    mailMessageData.From = GetRoutingAddresses(messageType, RoutingTypeEnum.FROM, null);
	    mailMessageData.Cc = GetRoutingAddresses(messageType, RoutingTypeEnum.CC, null);
	    mailMessageData.Bcc = GetRoutingAddresses(messageType, RoutingTypeEnum.BCC, null);
	    mailMessageData.Subject = subject;
	    mailMessageData.Body = body;
	    mailMessageData.BodyFormat = bodyFormat;
	    mailMessageData.MailMessageType = messageType;

	    mailMessage.Update(mailMessageData);

	    return mailMessage;
        }
        
        /// <summary>
        /// Creates and persists a new MailMessage
        /// </summary>
        /// <param name="data"></param>
        public static MailMessage Create(StringType messageType, StringType subject, StringType body, MailBodyFormatEnum bodyFormat) {
            MailMessageData mailMessageData = new MailMessageData();
	    MailMessage mailMessage = new MailMessage();
	    mailMessage.SetInitialState();

	    mailMessageData.To = GetRoutingAddresses(messageType, RoutingTypeEnum.TO, null);
	    mailMessageData.From = GetRoutingAddresses(messageType, RoutingTypeEnum.FROM, null);
	    mailMessageData.Cc = GetRoutingAddresses(messageType, RoutingTypeEnum.CC, null);
	    mailMessageData.Bcc = GetRoutingAddresses(messageType, RoutingTypeEnum.BCC, null);
	    mailMessageData.Subject = subject;
	    mailMessageData.Body = body;
	    mailMessageData.BodyFormat = bodyFormat;

	    mailMessageData.MailMessageType = messageType;
	    mailMessage.Update(mailMessageData);

	    return mailMessage;
        }
        
        /// <summary>
        /// Creates and persists a new MailMessage
        /// </summary>
        /// <param name="data"></param>
        public static MailMessage Create(StringType messageType, StringType from, StringType to, StringType subject, StringType body, MailBodyFormatEnum bodyFormat) {
            MailMessageData mailMessageData = new MailMessageData();
	    MailMessage mailMessage = new MailMessage();
	    mailMessage.SetInitialState();

	    mailMessageData.From = from;

	    mailMessageData.To = GetRoutingAddresses(messageType, RoutingTypeEnum.TO, to);
	    mailMessageData.Cc = GetRoutingAddresses(messageType, RoutingTypeEnum.CC, null);
	    mailMessageData.Bcc = GetRoutingAddresses(messageType, RoutingTypeEnum.BCC, null);
	    mailMessageData.Subject = subject;
	    mailMessageData.Body = body;
	    mailMessageData.BodyFormat = bodyFormat;
	    mailMessageData.MailMessageType = messageType;
	    mailMessage.Update(mailMessageData);

	    return mailMessage;
        }
        
        /// <summary>
        /// Sets all mail messages that are in the list of Ids to have Status of Released.
        /// </summary>
        /// <param name="list"></param>
        public static void Release(IdTypeList list, IdType userId) {
            MailMessage mailMessage;
	    foreach (IdType mailMessageId in list) {
		mailMessage = MailMessageDAO.DAO.Load(mailMessageId);
		mailMessage.MailMessageStatus = MailMessageStatusEnum.RELEASED;
		mailMessage.ReleasedByUserId = userId;
		
		mailMessage.Store();
	    }
        }
        
        /// <summary>
        /// Sets all mail messages that are in the list of Ids to have Status of Rejected and set Processed time.
        /// </summary>
        /// <param name="list"></param>
        public static void Reject(IdTypeList list, IdType userId) {
            MailMessage mailMessage;
	    foreach (IdType mailMessageId in list) {
		mailMessage = MailMessageDAO.DAO.Load(mailMessageId);
		mailMessage.MailMessageStatus = MailMessageStatusEnum.REJECTED;
		mailMessage.ReleasedByUserId = userId;
		mailMessage.Store();
	    }
        }
        
        /// <summary>
        /// Sets all mail messages that are in the list of Ids to have Status of NotAllowedFromTestServer and set Processed time.
        /// </summary>
        /// <param name="list"></param>
        public static void NotAllowed(IdTypeList list) {
            MailMessage mailMessage;
	    foreach (IdType mailMessageId in list) {
		mailMessage = MailMessageDAO.DAO.Load(mailMessageId);
		mailMessage.MailMessageStatus = MailMessageStatusEnum.NOT_ALLOWED_FROM_TEST_SERVER;
		mailMessage.Store();
	    }
        }
        
        /// <summary>
        /// Mark a message a held
        /// </summary>
        public static void Hold(IdType mailMessageId) {
            MailMessage mailMessage = MailMessageDAO.DAO.Load(mailMessageId);
	    mailMessage.MailMessageStatus = MailMessageStatusEnum.HELD;
	    mailMessage.Store();
        }
        
        public static MailMessageList GetMailMessagesToSend() {
            return MailMessageDAO.DAO.FindByStatus(MailMessageStatusEnum.UNPROCESSED);
        }
        
        /// <summary>
        /// Get mail messages that are of the status that is passed to this function.
        /// </summary>
        public static MailMessageList GetMailMessagesByStatus(MailMessageStatusEnum status) {
            WhereClause filter = new WhereClause(MailMessageFields.MAILMESSAGESTATUS, status.Code);
	    // order so that scheduled messages go first
	    OrderByClause sort = new OrderByClause("case when scheduletime is null then '12/31/2029' else scheduletime end, MailMessageId");
	    MailMessageList list = new MailMessageList();
	    list.AddRange(MailMessageDAO.DAO.GetList(filter, sort));
	    return list;
        }
        
        public void Send() {
            //make sure the attachment directory exists
	    String attachmentDirectory = ConfigurationProvider.Instance.Settings["TempPath"] + @"Attachments\";
	    if (!Directory.Exists(attachmentDirectory)){
		Directory.CreateDirectory(attachmentDirectory);
	    }

	    System.Web.Mail.MailMessage message = new System.Web.Mail.MailMessage();

	    message.From = this.From.ToString();
	    message.To = this.To.ToString();
	    if (this.Cc.IsValid) {
		message.Cc = this.Cc.ToString();
	    }
	    if (this.Bcc.IsValid) {
		message.Bcc = this.Bcc.ToString();
	    }
	    message.Subject = this.Subject.IsValid ? this.Subject.ToString() : String.Empty;

	    message.Body = this.Body.ToString();
	    if (this.BodyFormat.Equals(MailBodyFormatEnum.HTML)) {
		message.BodyFormat = MailFormat.Html;
	    } else {
		message.BodyFormat = MailFormat.Text;
	    }

		    
	    if (this.Priority.Equals(MailPriorityEnum.HIGH)) {
		message.Priority = MailPriority.High;
	    } else if (this.Priority.Equals(MailPriorityEnum.LOW)) {
		message.Priority = MailPriority.Low;
	    } else {
		message.Priority = MailPriority.Normal;
	    }

	    //If the mail message has attachments, 
	    //write them out to the <SystemSetting.TempPath>/Attachments/<MailMessageId>/<attachment.Filename>
		    
	    String messageAttachmentPath = attachmentDirectory + this.MailMessageId.ToInt32().ToString() + @"\";
	    foreach (MailAttachment attachment in this.Attachments){
		//make sure the directory exists for this mail message's attachments
			
		if (!Directory.Exists(messageAttachmentPath)){
		    Directory.CreateDirectory(messageAttachmentPath);
		}
		String filename = messageAttachmentPath + attachment.Filename;
		StreamWriter fileStream = new StreamWriter(filename, false); 
		if (attachment.Text.IsValid){
		    fileStream.WriteLine(attachment.Text.ToString()); 
		}
		fileStream.Close();
		System.Web.Mail.MailAttachment mailAttachment = new System.Web.Mail.MailAttachment(filename, MailEncoding.UUEncode);
		message.Attachments.Add(mailAttachment);
	    }

	    SmtpMail.SmtpServer = ConfigurationProvider.Instance.Settings["SMTPServer"];
	    SmtpMail.Send(message);
	    MarkSent();

	    //remove any attachments that were temporarily saved to file
	    foreach (MailAttachment attachment in this.Attachments){
		String filename = messageAttachmentPath + attachment.Filename;
		File.Delete(filename);
	    }
	    //remove the temporary directory for this message's attachments
	    if (Directory.Exists(messageAttachmentPath)){
		Directory.Delete(messageAttachmentPath);
	    }
        }
    }
}