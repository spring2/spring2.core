using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using Spring2.Core.Configuration;
using Spring2.Core.Shipping;
using Spring2.Core.Types;

namespace Spring2.Core.Shipping.Test {
    [TestFixture]
    public class UPSShippingProviderTest {
        [Test]
        public void ShouldBeAbleToCreateCorrectShippingProvider() {
            ShippingManager manager = new ShippingManager(StringType.Parse("Spring2.Core.Shipping.UPS.UPSShippingProvider"));
            Assert.IsNotNull(manager, "Manager Not Created");
            Assert.IsNotNull(manager.ShippingProvider, "Provider Not Found");
            Assert.IsTrue(manager.ShippingProvider.GetType().Name == "UPSShippingProvider", "Invalid Provider Type");
        }
    }
}
