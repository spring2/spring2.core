using System;
using System.Data;
using System.IO;
using System.Text;
using System.Net.Mail;
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

	private void IncreaseAttempts() {
            int attempts = 0;
	    if(this.NumberOfAttempts.IsValid){
		attempts = this.NumberOfAttempts.ToInt32() + 1;
	    }else{
		attempts = 1;
	    }
	    this.NumberOfAttempts = attempts;
	}
        
        public void MarkSent() {
            if (this.MailMessageStatus.Equals(MailMessageStatusEnum.UNPROCESSED) || this.MailMessageStatus.Equals(MailMessageStatusEnum.RELEASED)) {
		this.MailMessageStatus = MailMessageStatusEnum.SENT;
		this.Store();
	    } else {
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
	    SqlFilter filter = new SqlFilter(new SqlEqualityPredicate(MailMessageRouteFields.MAILMESSAGE, EqualityOperatorEnum.Equal, mailMessage));
	    filter.And(new SqlEqualityPredicate(MailMessageRouteFields.STATUS, EqualityOperatorEnum.Equal, ActiveStatusEnum.ACTIVE.Code));
	    filter.And(new SqlEqualityPredicate(MailMessageRouteFields.ROUTINGTYPE, EqualityOperatorEnum.Equal, routingType.Code));
	    routes.AddRange(MailMessageRouteDAO.DAO.GetList(filter));
	    
	    StringBuilder sb = new StringBuilder();
	    if (initialAddress != null && !initialAddress.IsEmpty) {
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
        public static MailMessage Create(MailMessageData message) {
	    StringType messageType = StringType.EMPTY;
	    if (!message.MailMessageType.IsEmpty) {
	    	messageType = message.MailMessageType;
	    }
            return Create(messageType, message.From, message.To, message.Cc, message.Bcc, message.Subject, message.Body, message.BodyFormat, message.ScheduleTime, new String[]{});
        }
        
        /// <summary>
        /// Creates and persists a new MailMessage and signs it using a 'from' address from the message routing table
        /// </summary>
        public static MailMessage Create(MailMessageData message, StringType messageType) {
	    StringType from = message.From;
	    if (from.IsEmpty) {
		from = GetFromAddress(messageType);
	    }
	    return Create(messageType, from, message.To, message.Cc, message.Bcc, message.Subject, message.Body, message.BodyFormat, message.ScheduleTime, new String[]{});
        }
        
        /// <summary>
        /// Creates and persists a new MailMessage
        /// </summary>
        public static MailMessage Create(StringType messageType, StringType to, StringType subject, StringType body, MailBodyFormatEnum bodyFormat, DateTimeType scheduleTime) {
            StringType from = GetFromAddress(messageType);
	    return Create(messageType, from, to, StringType.DEFAULT, StringType.DEFAULT, subject, body, bodyFormat, scheduleTime, new String[]{});
        }
        
        /// <summary>
        /// Creates and persists a new MailMessage
        /// </summary>
        public static MailMessage Create(StringType messageType, StringType to, StringType subject, StringType body, MailBodyFormatEnum bodyFormat) {
            return Create(messageType, to, subject, body, bodyFormat, DateTimeType.DEFAULT);
        }
        
        /// <summary>
        /// Creates and persists a new MailMessage
        /// </summary>
        public static MailMessage Create(StringType messageType, StringType subject, StringType body, MailBodyFormatEnum bodyFormat) {
            return Create(messageType, StringType.EMPTY, subject, body, bodyFormat);
        }
        
        /// <summary>
        /// Creates and persists a new MailMessage
        /// </summary>
        public static MailMessage Create(StringType messageType, StringType from, StringType to, StringType subject, StringType body, MailBodyFormatEnum bodyFormat) {
            return Create(messageType, from, to, StringType.DEFAULT, StringType.DEFAULT, subject, body, bodyFormat, DateTimeType.DEFAULT, new string[]{});
        }
        
        /// <summary>
        /// Creates and persists a new MailMessage
        /// </summary>
        public static MailMessage Create(StringType messageType, StringType from, StringType to, StringType subject, StringType body, MailBodyFormatEnum bodyFormat, String[] attachmentFilenames) {
            return Create(messageType, from, to, StringType.DEFAULT, StringType.DEFAULT, subject, body, bodyFormat, DateTimeType.DEFAULT, attachmentFilenames);
        }
        
        /// <summary>
        /// Creates and persists a new MailMessage
        /// </summary>
        public static MailMessage Create(StringType messageType, StringType from, StringType to, StringType subject, StringType body, MailBodyFormatEnum bodyFormat, DateTimeType scheduleTime) {
            return Create(messageType, from, to, StringType.DEFAULT, StringType.DEFAULT, subject, body, bodyFormat, scheduleTime, new String[]{});
        }
        
        /// <summary>
        /// Creates and persists a new MailMessage
        /// This is THE method that really does the work
        /// </summary>
        public static MailMessage Create(StringType messageType, StringType from, StringType to, StringType cc, StringType bcc, StringType subject, StringType body, MailBodyFormatEnum bodyFormat, DateTimeType scheduleTime, String[] attachmentFilenames) {
            MailMessageData mailMessageData = new MailMessageData();
	    MailMessage mailMessage = new MailMessage();
	    mailMessage.SetInitialState();

	    mailMessage.From = from;

	    mailMessage.To = GetRoutingAddresses(messageType, RoutingTypeEnum.TO, to);
	    mailMessage.Cc = GetRoutingAddresses(messageType, RoutingTypeEnum.CC, cc);
	    mailMessage.Bcc = GetRoutingAddresses(messageType, RoutingTypeEnum.BCC, bcc);
	    mailMessage.Subject = subject;
	    mailMessage.Body = body;
	    mailMessage.BodyFormat = bodyFormat;
	    mailMessage.ScheduleTime = scheduleTime;
	    mailMessage.MailMessageType = messageType;

	    mailMessage.Store();

	    //persist attachments
	    foreach (String filename in attachmentFilenames) {
		mailMessage.Attachments.Add(MailAttachment.Create(mailMessage, filename));
	    }

	    return mailMessage;
        }
        
        private static StringType GetFromAddress(StringType messageType) {
            MailMessageRouteList addresses = new MailMessageRouteList();
	    SqlFilter filter = new SqlFilter(new SqlEqualityPredicate(MailMessageRouteFields.MAILMESSAGE, EqualityOperatorEnum.Equal, messageType));
	    filter.And(new SqlEqualityPredicate(MailMessageRouteFields.STATUS, EqualityOperatorEnum.Equal, ActiveStatusEnum.ACTIVE.Code));
	    filter.And(new SqlEqualityPredicate(MailMessageRouteFields.ROUTINGTYPE, EqualityOperatorEnum.Equal, RoutingTypeEnum.FROM.Code));
	    addresses.AddRange(MailMessageRouteDAO.DAO.GetList(filter));
	
	    if (addresses.Count == 0) {
		throw new ArgumentException("No FROM address is defined for " + messageType);
	    }
	    if (addresses.Count > 1) {
		throw new ArgumentException("More than 1 FROM address is defined for " + messageType);
	    }
	    return addresses[0].EmailAddress;
        }
        
        /// <summary>
        /// Sets all mail messages that are in the list of Ids to have Status of Released.
        /// </summary>
        /// <param name="list"></param>
        /// <param name="userId"></param>
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
        /// <param name="userId"></param>
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
	    SqlFilter filter = new SqlFilter(new SqlEqualityPredicate(MailMessageFields.MAILMESSAGESTATUS, EqualityOperatorEnum.Equal, MailMessageStatusEnum.UNPROCESSED.Code));
	    filter.And(new SqlLiteralPredicate(String.Format("(ScheduleTime is null or ScheduleTime <= '{0}')", DateTime.Now.ToString("yyyy-MM-dd HH:mm"))));
	    return MailMessageDAO.DAO.GetList(filter);
        }
        
        /// <summary>
        /// Get mail messages that are of the status that is passed to this function.
        /// </summary>
        public static MailMessageList GetMailMessagesByStatus(MailMessageStatusEnum status) {
            SqlFilter filter = new SqlFilter(new SqlEqualityPredicate(MailMessageFields.MAILMESSAGESTATUS, EqualityOperatorEnum.Equal, status.Code));
	    // order so that scheduled messages go first
	    OrderByClause sort = new OrderByClause("case when scheduletime is null then '12/31/2029' else scheduletime end, MailMessageId");
	    MailMessageList list = new MailMessageList();
	    list.AddRange(MailMessageDAO.DAO.GetList(filter, sort));
	    return list;
        }
        
        public void Send() {
            //make sure the attachment directory exists
	    // TODO: needs to handle ending on root path
	    String attachmentDirectory = ConfigurationProvider.Instance.Settings["TempPath"] + @"Attachments\";
	    if (!Directory.Exists(attachmentDirectory)){
		Directory.CreateDirectory(attachmentDirectory);
	    }

	    System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage();

	    message.From = new MailAddress(this.From.ToString());
	    message.To.Add(this.to.Replace(';', ',').ToString());
	    if (!this.Cc.IsEmpty) {
		message.CC.Add(new MailAddress(this.Cc.Replace(';', ',').ToString()));
	    }
	    if (!this.Bcc.IsEmpty) {
		message.Bcc.Add(this.Bcc.Replace(';', ',').ToString());
	    }
	    message.Subject = this.Subject.IsValid ? this.Subject.ToString() : String.Empty;

	    message.Body = this.Body.ToString();
	    message.IsBodyHtml = this.BodyFormat.Equals(MailBodyFormatEnum.HTML);
		    
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
	    foreach (IMailAttachment attachment in this.Attachments){
		//make sure the directory exists for this mail message's attachments
			
		if (!Directory.Exists(messageAttachmentPath)){
		    Directory.CreateDirectory(messageAttachmentPath);
		}
		attachment.WriteAttachment(messageAttachmentPath);

		String filename = Path.Combine(messageAttachmentPath, attachment.Filename);

		System.Net.Mail.Attachment mailAttachment = new System.Net.Mail.Attachment(filename);
		message.Attachments.Add(mailAttachment);
	    }

	    try {
		IncreaseAttempts();
		this.ProcessedTime = DateTimeType.Now;
		SmtpClient smtpClient = new SmtpClient(ConfigurationProvider.Instance.Settings["SMTPServer"]);
		smtpClient.Send(message);
		MarkSent();
	    } catch {
		IntegerType maxAttempts = IntegerType.Parse(ConfigurationProvider.Instance.Settings["MailMessage.MaxAttempts"]);
		IntegerType nextAttemptInMinutes = IntegerType.Parse(ConfigurationProvider.Instance.Settings["MailMessage.NextAttemptInMinutes"]);
		if (this.NumberOfAttempts.ToInt32() >= maxAttempts.ToInt32()) {
		    this.MailMessageStatus = MailMessageStatusEnum.FAILED;
		} else {
		    this.ScheduleTime = DateTimeType.Now.AddMinutes(nextAttemptInMinutes.ToInt32());
		}
		this.Store();
	    }

	    //remove any attachments that were temporarily saved to file
	    foreach (MailAttachment attachment in this.Attachments){
		String filename = Path.Combine(messageAttachmentPath, attachment.Filename);
		File.Delete(filename);
	    }
	    //remove the temporary directory for this message's attachments
	    if (Directory.Exists(messageAttachmentPath)){
		Directory.Delete(messageAttachmentPath);
	    }
        }
    }
}