using System;
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
        
        [Generate()]
        private StringType text = StringType.DEFAULT;
        
        [Generate()]
        internal MailAttachment() {
            
        }
        
        [Generate()]
        internal MailAttachment(Boolean isNew) {
            this.isNew = isNew;
        }
        
        [Generate()]
        public IdType MailAttachmentId {
            get {
                return this.mailAttachmentId;
            }
            set {
                this.mailAttachmentId = value;
            }
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
        public StringType Filename {
            get {
                return this.filename;
            }
            set {
                this.filename = value;
            }
        }
        
        [Generate()]
        public StringType Text {
            get {
                return this.text;
            }
            set {
                this.text = value;
            }
        }
        
        [Generate()]
        public static MailAttachment NewInstance() {
            return new MailAttachment();
        }
        
        [Generate()]
        public static MailAttachment GetInstance(IdType mailAttachmentId) {
            return MailAttachmentDAO.DAO.Load(mailAttachmentId);
        }
        
        [Generate()]
        public void Update(MailAttachmentData data) {
            mailAttachmentId = data.MailAttachmentId.IsDefault ? mailAttachmentId : data.MailAttachmentId;
	    mailMessageId = data.MailMessageId.IsDefault ? mailMessageId : data.MailMessageId;
	    filename = data.Filename.IsDefault ? filename : data.Filename;
	    text = data.Text.IsDefault ? text : data.Text;
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
        public MessageList Validate() {
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
    }
}