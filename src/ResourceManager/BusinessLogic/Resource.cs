using System;
using Spring2.Core.BusinessEntity;
using Spring2.Core.Message;
using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;
using Spring2.Core.ResourceManager.Dao;
using Spring2.Core.ResourceManager.DataObject;

namespace Spring2.Core.ResourceManager.BusinessLogic {
    
    
    public class Resource : IResource {
        
        [Generate()]
        private IdType resourceId = IdType.UNSET;
        
        [Generate()]
        private StringType context = StringType.UNSET;
        
        [Generate()]
        private StringType field = StringType.UNSET;
        
        [Generate()]
        private IdType identity = IdType.UNSET;
        
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
        
        public static StringTypeList GetContextList() {
            return ResourceDAO.DAO.FindUniqueContexts();
        }
        
        public static StringTypeList GetFieldList(StringType context) {
            return ResourceDAO.DAO.FindUniqueFields(context);
        }
        
        public static IdTypeList GetIdentityList(StringType context, StringType field) {
            return ResourceDAO.DAO.FindUniqueIdentities(context, field);
        }
        
        public static ResourceList GetListByContext(StringType context) {
            return ResourceDAO.DAO.FindByContext(context);
        }
        
        public static ResourceList GetListByContextAndField(StringType context, StringType field) {
            return ResourceDAO.DAO.FindListByContextAndField(context, field);
        }
        
        public static Resource Create(StringType resourceContext, StringType fieldName, IdType identity) {
            Resource resource = new Resource();
	    resource.context = resourceContext;
	    resource.field = fieldName;
	    resource.Identity = identity;
	    resource.Store();
	    return resource;
        }
        
        [Generate()]
        public static Resource GetInstance(IdType resourceId) {
            return ResourceDAO.DAO.Load(resourceId);
        }
        
        public static Resource GetInstance(StringType context, StringType field, IdType identity) {
            return ResourceDAO.DAO.FindByContextFieldAndIdentity(context, field, identity);
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
        
        public MessageList Validate() {
            MessageList errors = new MessageList();

	    if(Identity.IsUnset) {
		throw(new InvalidArgumentException(ResourceFields.IDENTITY));
	    }

	    if(isNew && ResourceDAO.DAO.FindUniqueIdentities(this.Context, this.Field).Contains(this.Identity)) {
		throw new InvalidValueException(ResourceFields.IDENTITY);
	    }

	    return errors;
        }
        
        public LocalizedResourceList CopyResourceAndLocalization(IdType newContextIdentity) {
            Resource newResouce = Resource.Create(this.Context, this.Field, newContextIdentity);
	    LocalizedResourceList list = LocalizedResourceDAO.DAO.FindByResourceId(this.ResourceId);
	    LocalizedResourceList newList = new LocalizedResourceList();
	    foreach(ILocalizedResource localizedResource in list) {
		newList.Add(LocalizedResource.Create(newResouce.ResourceId, localizedResource.Locale, localizedResource.Language, localizedResource.Content));
	    }
	    return newList;
        }
        
        [Generate()]
        public void Reload() {
            ResourceDAO.DAO.Reload(this);
        }
    }
}