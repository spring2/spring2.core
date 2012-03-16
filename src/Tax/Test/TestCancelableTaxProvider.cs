using System;
using System.Collections.Generic;
using System.Text;
using Spring2.Core.Types;

namespace Spring2.Core.Tax.Test {
    public class TestCancelableTaxProvider : ICancelableTaxProvider {
        public TestCancelableTaxProvider(StringType profileKey) { 
        
        }

        public void Cancel(TaxOrder order) {
            
        }
    }
}
