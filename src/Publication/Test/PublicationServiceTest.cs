using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Spring2.Core.Types;
using Spring2.Core.Publication.PublicationService;
using Spring2.Core.Publication.BusinessLogic;
using Spring2.Core.Publication.DataObject;
using Spring2.Core.Publication.Dao;

namespace Spring2.Core.Publication.Test {
    [TestFixture]
    public class PublicationServiceTest {
		
	[Test()]
	public void ShouldBeAbleToGetProvider() {
	    StringType clazz = new StringType("Spring2.Core.Publication.TestPublicationServiceProvider,Spring2.Core.Publication");
	    IPublisher provider = PublicationService.PublicationService.GetProvider(clazz);
	    Assert.IsInstanceOf<TestPublicationServiceProvider>(provider);
	}

	[Test()]
	[ExpectedException("System.ArgumentException")]
	public void ShouldThrowArgumentException() {
	    StringType defaultString = StringType.DEFAULT;
	    PublicationService.PublicationService.GetProvider(defaultString);
	}

	[Test()]
	[ExpectedException("Spring2.Core.Publication.PublicationTypeException")]
	public void ShouldThrowPublicationTypeException() {
	    StringType nonsense = new StringType("asdf");
	    PublicationService.PublicationService.GetProvider(nonsense);
	}

	[Test()]
	[ExpectedException("Spring2.Core.Types.InvalidTypeException")]
	public void ShouldThrowInvalidTypeException() {
	    StringType stringType = new StringType();
	    PublicationService.PublicationService.GetProvider(stringType.GetType().AssemblyQualifiedName);
	}

    }
}
