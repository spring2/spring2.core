using System;
using System.Collections;
using Spring2.Core.DAO;

namespace Spring2.Core.BusinessLogic {
    
    public abstract class BusinessEntity : IBusinessEntity {
        
        protected Boolean isNew = true;
        
        public Boolean IsNew {
            get {
                return this.isNew;
            }
        }
    }
}
