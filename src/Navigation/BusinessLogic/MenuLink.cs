using System;
using Spring2.Core.BusinessEntity;
using Spring2.Core.Message;
using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;
using Spring2.Core.Navigation.Dao;
using Spring2.Core.Navigation.DataObject;

namespace Spring2.Core.Navigation.BusinessLogic {
    
    
    public class MenuLink : Spring2.Core.BusinessEntity.BusinessEntity, IMenuLink {
        
        [Generate()]
        private IdType menuLinkId = IdType.DEFAULT;
        
        [Generate()]
        private StringType name = StringType.DEFAULT;
        
        [Generate()]
        private StringType target = StringType.DEFAULT;
        
        [Generate()]
        private BooleanType active = BooleanType.DEFAULT;
        
        [Generate()]
        private IdType menuLinkGroupId = IdType.DEFAULT;
        
        [Generate()]
        private IdType parentMenuLinkId = IdType.DEFAULT;
        
        [Generate()]
        private MenuLinkList childMenuLinks = MenuLinkList.DEFAULT;
        
        [Generate()]
        private MenuLinkKeyList keys = MenuLinkKeyList.DEFAULT;
        
        [Generate()]
        private DateTimeType effectiveDate = DateTimeType.DEFAULT;
        
        [Generate()]
        private DateTimeType expirationDate = DateTimeType.DEFAULT;
        
        [Generate()]
        private IdType sequence = IdType.DEFAULT;
        
        [Generate()]
        private StringType targetWindow = StringType.DEFAULT;
        
        [Generate()]
        internal MenuLink() {
            
        }
        
        [Generate()]
        internal MenuLink(Boolean isNew) {
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
        public StringType Name {
            get {
                return this.name;
            }
            set {
                this.name = value;
            }
        }
        
        [Generate()]
        public StringType Target {
            get {
                return this.target;
            }
            set {
                this.target = value;
            }
        }
        
        [Generate()]
        public BooleanType Active {
            get {
                return this.active;
            }
            set {
                this.active = value;
            }
        }
        
        [Generate()]
        public IdType MenuLinkGroupId {
            get {
                return this.menuLinkGroupId;
            }
            set {
                this.menuLinkGroupId = value;
            }
        }
        
        [Generate()]
        public IdType ParentMenuLinkId {
            get {
                return this.parentMenuLinkId;
            }
            set {
                this.parentMenuLinkId = value;
            }
        }
        
        public MenuLinkList ChildMenuLinks {
            get {
                if(this.childMenuLinks.IsDefault) {
		    this.childMenuLinks = MenuLinkDAO.DAO.FindByParentMenuLink(this.MenuLinkId);
		}
                return this.childMenuLinks;
            }
        }
        
        public MenuLinkKeyList Keys {
            get {
                if(this.keys.IsDefault) {
		    this.keys = MenuLinkKeyDAO.DAO.FindByMenuLinkId(this.MenuLinkId);
		}
                return this.keys;
            }
        }
        
        [Generate()]
        public DateTimeType EffectiveDate {
            get {
                return this.effectiveDate;
            }
            set {
                this.effectiveDate = value;
            }
        }
        
        [Generate()]
        public DateTimeType ExpirationDate {
            get {
                return this.expirationDate;
            }
            set {
                this.expirationDate = value;
            }
        }
        
        [Generate()]
        public IdType Sequence {
            get {
                return this.sequence;
            }
            set {
                this.sequence = value;
            }
        }
        
        public BooleanType CanMoveUp {
            get {
                return MenuLinkDAO.DAO.FindNextHigherSibling(this) != null;
            }
        }
        
        public BooleanType CanMoveDown {
            get {
                return MenuLinkDAO.DAO.FindNextLowerSibling(this) != null;
            }
        }
        
        [Generate()]
        public StringType TargetWindow {
            get {
                return this.targetWindow;
            }
            set {
                this.targetWindow = value;
            }
        }
        
        [Generate()]
        public static MenuLink NewInstance() {
            return new MenuLink();
        }
        
        [Generate()]
        public static MenuLink GetInstance(IdType menuLinkId) {
            return MenuLinkDAO.DAO.Load(menuLinkId);
        }
        
        [Generate()]
        public void Update(MenuLinkData data) {
            name = data.Name.IsDefault ? name : data.Name;
	    target = data.Target.IsDefault ? target : data.Target;
	    active = data.Active.IsDefault ? active : data.Active;
	    menuLinkGroupId = data.MenuLinkGroupId.IsDefault ? menuLinkGroupId : data.MenuLinkGroupId;
	    parentMenuLinkId = data.ParentMenuLinkId.IsDefault ? parentMenuLinkId : data.ParentMenuLinkId;
	    effectiveDate = data.EffectiveDate.IsDefault ? effectiveDate : data.EffectiveDate;
	    expirationDate = data.ExpirationDate.IsDefault ? expirationDate : data.ExpirationDate;
	    sequence = data.Sequence.IsDefault ? sequence : data.Sequence;
	    targetWindow = data.TargetWindow.IsDefault ? targetWindow : data.TargetWindow;
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
		this.MenuLinkId = MenuLinkDAO.DAO.Insert(this);
		isNew = false;
            } else {
		MenuLinkDAO.DAO.Update(this);
            }
        }
        
        [Generate()]
        public virtual MessageList Validate() {
            MessageList errors = new MessageList();
	    return errors;
        }
        
        [Generate()]
        public void Reload() {
            MenuLinkDAO.DAO.Reload(this);
	    childMenuLinks = MenuLinkList.DEFAULT;
	    keys = MenuLinkKeyList.DEFAULT;
        }
        
        public static MenuLink Create(MenuLinkData data) {
            MenuLink menuLink = new MenuLink();
	    menuLink.Update(data);
	    return menuLink;
        }
        
        public BooleanType IsSelected(StringType key) {
            foreach(MenuLinkKey menuKey in Keys) {
		if(key.Equals(menuKey.Key)) {
		    return BooleanType.TRUE;
		}
	    }
	    return BooleanType.FALSE;
        }
        
        public void MoveUp() {
            MenuLink sibling = MenuLinkDAO.DAO.FindNextHigherSibling(this);
			IdType tmp = IdType.DEFAULT;
			if (sibling != null) 
			{
				tmp = this.Sequence;
				this.Sequence = sibling.Sequence;
				sibling.Sequence = tmp;

				this.Store();
				sibling.Store();
			}
        }
        
        public void MoveDown() {
            MenuLink sibling = MenuLinkDAO.DAO.FindNextLowerSibling(this);
			IdType tmp = IdType.DEFAULT;
			if (sibling != null) 
			{
				tmp = this.Sequence;
				this.Sequence = sibling.Sequence;
				sibling.Sequence = tmp;

				this.Store();
				sibling.Store();
			}
        }
        
        public static IdType FindNextSequenceByParentId(IdType parentMenuLinkId) {
            return MenuLinkDAO.DAO.FindNextSequenceByParentId(parentMenuLinkId);
        }
        
        public static IdType FindNextSequenceByGroupId(IdType menuLinkGroupId) {
            return MenuLinkDAO.DAO.FindNextSequenceByGroupId(menuLinkGroupId);
        }
        
        public IMenuLink GetHighestActiveChild() {
            IMenuLink result = null;
			if(ChildMenuLinks.Count > 0)
			{
				foreach(IMenuLink link in ChildMenuLinks)
				{
					if(link.Active.IsTrue)
					{
						if(result == null)
						{
							result = link;
							continue;
						}
						if(link.Sequence.ToInt32() > result.Sequence.ToInt32())
						{
							result = link;
						}
					}
				}
			}
			return result;
        }
    }
}