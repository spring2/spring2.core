using System;
using Spring2.Core.BusinessEntity;
using Spring2.Core.Message;
using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;
using Spring2.Core.ResourceManager.Dao;
using Spring2.Core.ResourceManager.DataObject;
using Spring2.Core.ResourceManager.Types;

namespace Spring2.Core.ResourceManager.BusinessLogic {
    
    
    public class Resource : IResource {
        
        [Generate()]
        private IdType resourceId = IdType.DEFAULT;
        
        [Generate()]
        private StringType context = StringType.DEFAULT;
        
        [Generate()]
        private StringType field = StringType.DEFAULT;
        
        [Generate()]
        private IdType identity = IdType.DEFAULT;
        
        protected Boolean isNew = true;
        
        [Generate()]
        internal Resource() {
            
        }
        
        [Generate()]
        internal Resource(Boolean isNew) {
            this.isNew = isNew;
        }
        
        public Boolean IsNew {
            get {
                return this.isNew;
            }
        }
        
        [Generate()]
        public IdType ResourceId {
            get {
                return this.resourceId;
            }
            set {
                this.resourceId = value;
            }
        }
        
        [Generate()]
        public StringType Context {
            get {
                return this.context;
            }
            set {
                this.context = value;
            }
        }
        
        [Generate()]
        public StringType Field {
            get {
                return this.field;
            }
            set {
                this.field = value;
            }
        }
        
        [Generate()]
        public IdType Identity {
            get {
                return this.identity;
            }
            set {
                this.identity = value;
            }
        }
        
        public static Resource NewInstance() {
            throw new ApplicationException("Not supported");
        }
        
        public static Resource Create(StringType resourceContext, StringType fieldName) {
            Resource resource = new Resource();
	    resource.context = resourceContext;
	    resource.field = fieldName;
	    resource.Store();
	    return resource;
        }
        
        [Generate()]
        public static Resource GetInstance(IdType resourceId) {
            return ResourceDAO.DAO.Load(resourceId);
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
		this.ResourceId = ResourceDAO.DAO.Insert(this);
		isNew = false;
            } else {
		ResourceDAO.DAO.Update(this);
            }
        }
        
        [Generate()]
        public MessageList Validate() {
            MessageList errors = new MessageList();

	    return errors;
        }
        
        [Generate()]
        public void Reload() {
            ResourceDAO.DAO.Reload(this);
        }
    }
}