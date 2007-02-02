using System;
using System.Text;
using Spring2.Core.Types;

using Spring2.Core.ResourceManager.DataObject;
using Spring2.Core.ResourceManager.Test;

namespace Spring2.Core.ResourceManager.TestUtility {
    public class TestUtil {
	
	Spring2.Core.ResourceManager.Test.TestUtility helper = new Spring2.Core.ResourceManager.Test.TestUtility();

	public void RemoveTestData() {
	    helper.CleanupTestResources();
	}

	public IResource CreateTestResource() {
	    return helper.CreateTestResource();
	}

	public ILocalizedResource CreateTestLocalizedResource() {
	    return helper.CreateTestLocalizedResource();
	}

    }
}
