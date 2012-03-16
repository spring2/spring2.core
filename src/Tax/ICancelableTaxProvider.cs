using System;
using System.Collections.Generic;
using System.Text;

namespace Spring2.Core.Tax {
    public interface ICancelableTaxProvider {
        void Cancel(TaxOrder order);
    }
}
