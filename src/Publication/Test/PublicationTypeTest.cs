using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Spring2.Core.Publication.BusinessLogic;
using Spring2.Core.Publication.DataObject;
using Spring2.Core.Types;
using Spring2.Core.Message;
using Spring2.Core.Publication.Dao;
using Spring2.Core.Security;
using Rhino.Mocks;
using System.Security.Principal;
using System.Threading;

namespace Spring2.Core.Publication.Test {
    [TestFixture]
    public class PublicationTypeTest {
	MockRepository repository;
	PublicationType type1;
	PublicationType type2;
	PublicationType type3;
	
	IPrincipal principal;
	IIdentity identity;

	[SetUp()]
	public void Setup() {
	    type1 = PublicationType.NewInstance();
	    type2 = PublicationType.NewInstance();
	    type3 = PublicationType.NewInstance();

	    repository = new MockRepository();
	    principal = repository.StrictMultiMock<IPrincipal>(typeof(IUserPrincipal));
	    identity = repository.StrictMock<IIdentity>();

	    using (repository.Record()) {
		Expect.Call(principal.Identity).Return(identity).Repeat.Any();
		Expect.Call((principal as IUserPrincipal).UserId).Return(new IdType(1)).Repeat.Any();
		Expect.Call(identity.Name).Return("Test").Repeat.Any();
	    }
	    Thread.CurrentPrincipal = principal;
	}

	[TearDown()]
	public void TearDown() {
	    DeletePublicationType(type1);
	    DeletePublicationType(type2);
	    DeletePublicationType(type3);

	    repository = null;
	    principal = null;
	    identity = null;

	    Thread.CurrentPrincipal = null;
	}

	private void DeletePublicationType(PublicationType publicationType) {
	    try {
		PublicationTypeDAO.DAO.Delete(publicationType.PublicationTypeId);
	    } catch { }
	}

	[Test()]
	public void ShouldBeAbleToRetreiveTheActivePublicationTypesToProcess() {
	    //effective and past the frequency for being processed.
	    PublicationTypeData data1 = new PublicationTypeData() {
		AllowSubscription = true,
		AutoSubscribe = false,
		EffectiveDate = DateTimeType.Now.AddMonths(-1),
		FrequencyInMinutes = 4,
		LastSentDate = DateTimeType.Now.AddMinutes(-5),
		Name = "One",
		Description = "Desc One",
		ProviderName = "Spring2.Core.Publication.TestPublicationServiceProvider,Spring2.Core.Publication"
	    };

	    //future effective date
	    PublicationTypeData data2 = new PublicationTypeData() {
		AllowSubscription = true,
		AutoSubscribe = false,
		EffectiveDate = DateTimeType.Now.AddMonths(1),
		FrequencyInMinutes = 4,
		LastSentDate = DateTimeType.Now.AddMinutes(-5),
		Name = "Two",
		Description = "Desc TwO",
		ProviderName = "Spring2.Core.Publication.TestPublicationServiceProvider,Spring2.Core.Publication"
	    };
	    
	    using (repository.Playback()) {
		type1.Update(data1);
		type2.Update(data2);
	    }

	    PublicationTypeList publications = PublicationType.GetActivePublicationsForProcessing();
	    Assert.AreEqual(1, publications.Count);
	    Assert.AreEqual("One", publications[0].Name.Display());
	    Assert.AreEqual("Desc One", publications[0].Description.Display());
	}

	[Test()]
	public void ShouldBeAbleToRetreiveTheActiveSubscribablePublicationTypes() {
	    //effective and subscribable
	    PublicationTypeData data1 = new PublicationTypeData() {
		AllowSubscription = true,
		AutoSubscribe = false,
		EffectiveDate = DateTimeType.Now.AddMonths(-1),
		FrequencyInMinutes = 4,
		LastSentDate = DateTimeType.Now.AddMinutes(-5),
		Name = "One",
		Description = "Desc One",
		ProviderName = "Spring2.Core.Publication.TestPublicationServiceProvider,Spring2.Core.Publication"
	    };

	    //not subscribable
	    PublicationTypeData data2 = new PublicationTypeData() {
		AllowSubscription = true,
		AutoSubscribe = false,
		EffectiveDate = DateTimeType.Now.AddMonths(1),
		FrequencyInMinutes = 4,
		LastSentDate = DateTimeType.Now.AddMinutes(-5),
		Name = "Two",
		Description = "Desc Two",
		ProviderName = "Spring2.Core.Publication.TestPublicationServiceProvider,Spring2.Core.Publication"
	    };

	    //expired
	    PublicationTypeData data3 = new PublicationTypeData() {
		AllowSubscription = true,
		AutoSubscribe = false,
		EffectiveDate = DateTimeType.Now.AddMonths(-2),
		ExpirationDate = DateTimeType.Now.AddMonths(-1),
		FrequencyInMinutes = 100,
		LastSentDate = DateTimeType.Now.AddMinutes(-5),
		Name = "Three",
		Description = "Desc Three",
		ProviderName = "Spring2.Core.Publication.TestPublicationServiceProvider,Spring2.Core.Publication"
	    };

	    using (repository.Playback()) {
		type1.Update(data1);
		type2.Update(data2);
		type3.Update(data3);
	    }

	    PublicationTypeList publications = PublicationType.GetActiveSubscribablePublications();
	    Assert.AreEqual(1, publications.Count);
	    Assert.AreEqual("One", publications[0].Name.Display());
	    Assert.AreEqual("Desc One", publications[0].Description.Display());
	}
	[Test()]
	public void ShouldBeAbleToRetreiveTheActiveAutoSubscribablePublicationTypes() {
	    //effective and subscribable
	    PublicationTypeData data1 = new PublicationTypeData() {
		AllowSubscription = true,
		AutoSubscribe = true,
		EffectiveDate = DateTimeType.Now.AddMonths(-1),
		FrequencyInMinutes = 4,
		LastSentDate = DateTimeType.Now.AddMinutes(-5),
		Name = "One",
		Description = "Desc One",
		ProviderName = "Spring2.Core.Publication.TestPublicationServiceProvider,Spring2.Core.Publication"
	    };

	    //not subscribable
	    PublicationTypeData data2 = new PublicationTypeData() {
		AllowSubscription = true,
		AutoSubscribe = true,
		EffectiveDate = DateTimeType.Now.AddMonths(1),
		FrequencyInMinutes = 4,
		LastSentDate = DateTimeType.Now.AddMinutes(-5),
		Name = "Two",
		Description = "Desc Two",
		ProviderName = "Spring2.Core.Publication.TestPublicationServiceProvider,Spring2.Core.Publication"
	    };

	    //expired
	    PublicationTypeData data3 = new PublicationTypeData() {
		AllowSubscription = true,
		AutoSubscribe = true,
		EffectiveDate = DateTimeType.Now.AddMonths(-2),
		ExpirationDate = DateTimeType.Now.AddMonths(-1),
		FrequencyInMinutes = 100,
		LastSentDate = DateTimeType.Now.AddMinutes(-5),
		Name = "Three",
		Description = "Desc Three",
		ProviderName = "Spring2.Core.Publication.TestPublicationServiceProvider,Spring2.Core.Publication"
	    };

	    using (repository.Playback()) {
		type1.Update(data1);
		type2.Update(data2);
		type3.Update(data3);
	    }

	    PublicationTypeList publications = PublicationType.GetActiveAutoSubscribablePublications();
	    Assert.AreEqual(1, publications.Count);
	    Assert.AreEqual("One", publications[0].Name.Display());
	    Assert.AreEqual("Desc One", publications[0].Description.Display());
	}

	[Test()]
	public void ValidationTestForPublicationTypeUpdate() {
	    PublicationTypeData data = new PublicationTypeData();
	    MessageList errors = new MessageList();

	    using (repository.Playback()) {
		try {
		    type1.Update(data);
		} catch (MessageListException ex) {
		    errors = ex.Messages;
		}
	    }

	    Assert.AreEqual(8, errors.Count);
	    for (Int32 i = 0; i < errors.Count; i++) {
		Assert.IsTrue(errors[i] is MissingRequiredFieldError);
	    }
	    Int32 index = 0;
	    Assert.AreEqual("Name is required.", errors[index++].Message);
	    Assert.AreEqual("Description is required.", errors[index++].Message);
	    Assert.AreEqual("LastSentDate is required.", errors[index++].Message);
	    Assert.AreEqual("FrequencyInMinutes is required.", errors[index++].Message);
	    Assert.AreEqual("ProviderName is required.", errors[index++].Message);
	    Assert.AreEqual("AllowSubscription is required.", errors[index++].Message);
	    Assert.AreEqual("AutoSubscribe is required.", errors[index++].Message);
	    Assert.AreEqual("EffectiveDate is required.", errors[index++].Message);
	}

    }
}
