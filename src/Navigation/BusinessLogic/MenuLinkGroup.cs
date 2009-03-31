using System;
using Spring2.Core.BusinessEntity;
using Spring2.Core.Message;
using Spring2.Core.Types;
using Spring2.DataTierGenerator.Attribute;
using Spring2.Core.Navigation.Dao;
using Spring2.Core.Navigation.DataObject;

namespace Spring2.Core.Navigation.BusinessLogic {
    
    
    public class MenuLinkGroup : Spring2.Core.BusinessEntity.BusinessEntity, IMenuLinkGroup {
        
        [Generate()]
        private IdType menuLinkGroupId = IdType.DEFAULT;
        
        [Generate()]
        private StringType name = StringType.DEFAULT;
        
        [Generate()]
        private MenuLinkList menuLinks = MenuLinkList.DEFAULT;
        
        [Generate()]
        internal MenuLinkGroup() {
            
        }
        
        [Generate()]
        internal MenuLinkGroup(Boolean isNew) {
            this.isNew = isNew;
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
        public StringType Name {
            get {
                return this.name;
            }
            set {
                this.name = value;
            }
        }
        
        public MenuLinkList MenuLinks {
            get {
                if(this.menuLinks.IsDefault) {
		    this.menuLinks = MenuLinkDAO.DAO.FindByMenuLinkGroup(this.MenuLinkGroupId);
		}
                return this.menuLinks;
            }
        }
        
        [Generate()]
        public static MenuLinkGroup NewInstance() {
            return new MenuLinkGroup();
        }
        
        [Generate()]
        public static MenuLinkGroup GetInstance(IdType menuLinkGroupId) {
            return MenuLinkGroupDAO.DAO.Load(menuLinkGroupId);
        }
        
        public static MenuLinkGroup GetInstance(StringType menuLinkName) {
            return MenuLinkGroupDAO.DAO.FindByName(menuLinkName);
        }
        
        [Generate()]
        public void Update(MenuLinkGroupData data) {
            name = data.Name.IsDefault ? name : data.Name;
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
		this.MenuLinkGroupId = MenuLinkGroupDAO.DAO.Insert(this);
		isNew = false;
            } else {
		MenuLinkGroupDAO.DAO.Update(this);
            }
        }
        
        [Generate()]
        public virtual MessageList Validate() {
            MessageList errors = new MessageList();
	    return errors;
        }
        
        [Generate()]
        public void Reload() {
            MenuLinkGroupDAO.DAO.Reload(this);
	    menuLinks = MenuLinkList.DEFAULT;
        }
        
        public static MenuLinkGroup Create(MenuLinkGroupData data) {
            MenuLinkGroup group = new MenuLinkGroup();
	    group.Update(data);
	    return group;
        }
        
        public static MenuLinkGroupList GetMenuLinkGroups() {
            return MenuLinkGroupDAO.DAO.GetList();
        }
        
        /// <summary>
        /// Returns the lowest active MenuLink
        /// </summary>
        /// <returns>The lowest active Menu Link</returns>
        public IMenuLink GetHighestActiveChild() {
            IMenuLink result = null;
			if(MenuLinks.Count > 0)
			{
				foreach(IMenuLink link in MenuLinks)
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