using System;
using Spring2.Core.BusinessEntity;
using Spring2.Core.Message;
using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;
using Spring2.Core.Navigation.Dao;
using Spring2.Core.Navigation.DataObject;

namespace Spring2.Core.Navigation.BusinessLogic {
    
    
    public class MenuLinkKey : Spring2.Core.BusinessEntity.BusinessEntity, IMenuLinkKey {
        
        [Generate()]
        private IdType menuLinkId = IdType.DEFAULT;
        
        [Generate()]
        private StringType key = StringType.DEFAULT;
        
        [Generate()]
        internal MenuLinkKey() {
            
        }
        
        [Generate()]
        internal MenuLinkKey(Boolean isNew) {
            this.isNew = isNew;
        }
        
        [Generate()]
        public IdType MenuLinkId {
            get {
                return this.menuLinkId;
            }
            set {
                this.menuLinkId = value;
            }
        }
        
        [Generate()]
        public StringType Key {
            get {
                return this.key;
            }
            set {
                this.key = value;
            }
        }
        
        [Generate()]
        public static MenuLinkKey NewInstance() {
            return new MenuLinkKey();
        }
        
        [Generate()]
        public static MenuLinkKey GetInstance(IdType menuLinkId, StringType key) {
            return MenuLinkKeyDAO.DAO.Load(menuLinkId, key);
        }
        
        [Generate()]
        public void Update(MenuLinkKeyData data) {
            menuLinkId = data.MenuLinkId.IsDefault ? menuLinkId : data.MenuLinkId;
	    key = data.Key.IsDefault ? key : data.Key;
	    Store();
        }
        
        public void Store() {
            MessageList errors = Validate();

	    if (errors.Count > 0) {
		if (!isNew) {
		    this.Reload();
		}
		throw new MessageListException(errors);
	    }

	    if (isNew) {
		MenuLinkKeyDAO.DAO.Insert(this);
		isNew = false;
	    } else {
		throw new NotSupportedException("Not allowed to just update a MenuLinkKey");
	    }
        }
        
        [Generate()]
        public virtual MessageList Validate() {
            MessageList errors = new MessageList();
	    return errors;
        }
        
        [Generate()]
        public void Reload() {
            MenuLinkKeyDAO.DAO.Reload(this);
        }
        
        public static MenuLinkKey Create(MenuLinkKeyData data) {
            MenuLinkKey menuLinkKey = new MenuLinkKey();
	    menuLinkKey.Update(data);
	    return menuLinkKey;
        }
    }
}