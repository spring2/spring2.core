using Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Spring2.Core.SeleniumLoadTest {
    public interface ISeleniumLoadTest {
        void SetSelenium(ISelenium selenium);
    }
}
