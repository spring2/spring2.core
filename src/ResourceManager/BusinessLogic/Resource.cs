using System;
using Spring2.Core.BusinessEntity;
using Spring2.Core.Message;
using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;
using Spring2.Core.ResourceManager.Dao;
using Spring2.Core.ResourceManager.DataObject;
using Spring2.Core.ResourceManager.Types;

namespace Spring2.Core.ResourceManager.BusinessLogic {
    
    
    public class Resource : Spring2.Core.BusinessEntity.BusinessEntity, IResource {
        
        [Generate()]
        private IdType resourceId = IdType.DEFAULT;
        
        [Generate()]
        private StringType entityName = StringType.DEFAULT;
        
        [Generate()]
        private StringType propertyName = StringType.DEFAULT;
        
        [Generate()]
        private IdType identity = IdType.DEFAULT;
        
        [Generate()]
        internal Resource() {
            
        }
        
        [Generate()]
        internal Resource(Boolean isNew) {
            this.isNew = isNew;
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
        public StringType EntityName {
            get {
                return this.entityName;
            }
            set {
                this.entityName = value;
            }
        }
        
        [Generate()]
        public StringType PropertyName {
            get {
                return this.propertyName;
            }
            set {
                this.propertyName = value;
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
        
        [Generate()]
        public static Resource NewInstance() {
            return new Resource();
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