﻿using System;
using System.Collections.Generic;
using System.Text;

using NUnit.Framework;
using Spring2.Core.Shipping;
using Spring2.Core.Types;

namespace Spring2.Core.Shipping.Test {
    [TestFixture]
    public class UPSShippingManagerTest {
        [Test]
        public void ShouldBeAbleToCreateCorrectShippingProvider() {
            ShippingManager manager = new ShippingManager(StringType.Parse("Spring2.Core.Shipping.UPS.UPSShippingProvider"));
            Assert.IsNotNull(manager);
            Assert.IsNotNull(manager.ShippingProvider);
            Assert.IsTrue(manager.ShippingProvider.GetType().Name == "");
        }
    }
}
