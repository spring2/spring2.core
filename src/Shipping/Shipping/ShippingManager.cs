using System;
using System.Collections.Generic;
using System.Text;
using Spring2.Core.Configuration;
using Spring2.Core;
using Spring2.Core.Types;

namespace Spring2.Core.Shipping {
    public class ShippingManager {

        public ShippingManager(StringType providerId) {
            CreateShippingProvider(providerId);
        }

        IShippingProvider shippingProvider = null;

        public IShippingProvider ShippingProvider {
            get { return shippingProvider; }
            set { shippingProvider = value; }
        }

        public void CreateShippingProvider(String shippingProviderClass) {
            Type t = Type.GetType(shippingProviderClass);
            if (t == null) {
                throw new ShippingConfigurationException(shippingProviderClass + " could not be created");
            }

            Object o = System.Activator.CreateInstance(t);
            if (o is IShippingProvider) {
                shippingProvider = o as IShippingProvider;
            } else {
                throw new ShippingConfigurationException(shippingProviderClass + " does not support IShippingProvider");
            }
        }
    }
}
