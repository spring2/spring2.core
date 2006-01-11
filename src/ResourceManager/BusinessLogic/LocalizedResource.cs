using System;
using Spring2.Core.BusinessEntity;
using Spring2.Core.Message;
using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;
using Spring2.Core.ResourceManager.Dao;
using Spring2.Core.ResourceManager.DataObject;
using Spring2.Core.ResourceManager.Types;

namespace Spring2.Core.ResourceManager.BusinessLogic {
    
    
    public class LocalizedResource : ILocalizedResource {
        
        [Generate()]
        private IdType localizedResourceId = IdType.DEFAULT;
        
        [Generate()]
        private IdType resourceId = IdType.DEFAULT;
        
        private Boolean isNew = true;
        
        [Generate()]
        private ILocale locale = null;
        
        [Generate()]
        private ILanguage language = null;
        
        [Generate()]
        private StringType content = StringType.DEFAULT;
        
        [Generate()]
        private Resource resource = new Resource();
        
        [Generate()]
        internal LocalizedResource() {
            
        }
        
        [Generate()]
        internal LocalizedResource(Boolean isNew) {
            this.isNew = isNew;
        }
        
        public Boolean IsNew {
            get {
                return this.isNew;
            }
        }
        
        [Generate()]
        public IdType LocalizedResourceId {
            get {
                return this.localizedResourceId;
            }
            set {
                this.localizedResourceId = value;
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
        public ILocale Locale {
            get {
                return this.locale;
            }
            set {
                this.locale = value;
            }
        }
        
        [Generate()]
        public ILanguage Language {
            get {
                return this.language;
            }
            set {
                this.language = value;
            }
        }
        
        [Generate()]
        public StringType Content {
            get {
                return this.content;
            }
            set {
                this.content = value;
            }
        }
        
        [Generate()]
        public Resource Resource {
            get {
                return this.resource as Resource;
            }
            set {
                this.resource = value;
            }
        }
        
        [Generate()]
        IResource ILocalizedResource.Resource {
            get {
                return this.Resource;
            }
        }
        
        public static LocalizedResource NewInstance() {
            throw new NotSupportedException();
        }
        
        public static LocalizedResource Create(IdType resourceId, ILocale newLocale, ILanguage newLanguage, StringType newContent) {
            LocalizedResource localResource = new LocalizedResource();
	    localResource.ResourceId = resourceId;
	    localResource.Content = newContent;
	    localResource.Locale = newLocale;
	    localResource.Language = newLanguage;
	    localResource.Store();
	    return localResource;
        }
        
        [Generate()]
        public static LocalizedResource GetInstance(IdType localizedResourceId) {
            return LocalizedResourceDAO.DAO.Load(localizedResourceId);
        }
        
        [Generate()]
        public void Update(LocalizedResourceData data) {
            content = data.Content.IsDefault ? content : data.Content;
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
		this.LocalizedResourceId = LocalizedResourceDAO.DAO.Insert(this);
		isNew = false;
            } else {
		LocalizedResourceDAO.DAO.Update(this);
            }
        }
        
        [Generate()]
        public MessageList Validate() {
            MessageList errors = new MessageList();

	    return errors;
        }
        
        [Generate()]
        public void Reload() {
            LocalizedResourceDAO.DAO.Reload(this);
	    resource = new Resource();
        }
    }
}