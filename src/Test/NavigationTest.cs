using System;
using System.Collections;
using System.IO;
using NUnit.Framework;
using Spring2.Core.Types;

using Spring2.Core.Navigation.Dao;
using Spring2.Core.Navigation.DataObject;
using Spring2.Core.Navigation.BusinessLogic;

namespace Spring2.Core.Test {

    /// <summary>
    /// Tests for Navigation
    /// </summary>
    [TestFixture]
    public class NavigationTest {
	private const String MENULINKGROUP_NAME = "TestMenuLinkGroup";
	private const String MENULINK_NAME = "TestMenuLink";
	private const String MENULINK_TARGET = "TestMenuLinkTarget";

	[Test]
	public void ShouldBeAbleToGetMenuLinkGroupByName() {
	    MenuLinkGroup group = null;
	    try {
		group = GetTestMenuLinkGroup();
		MenuLinkGroup copy = MenuLinkGroup.GetInstance(MENULINKGROUP_NAME);

		Assert.AreEqual(group.MenuLinkGroupId, copy.MenuLinkGroupId);
	    } finally {
		this.DeleteMenuLinkGroup(group);
	    }
	}

	[Test]
	public void ShouldBeAbleToGetMenuLinkGroupWithOneMenuLinkSelected() {
	    MenuLinkGroup group = null;
	    MenuLink menuLink = null;
	    try {
		group = GetTestMenuLinkGroup();
		StringType key = new StringType("Key1");
		StringTypeList keys = new StringTypeList();
		keys.Add(key);
		menuLink = GetTestMenuLink(group, keys);

		group = MenuLinkGroup.GetInstance(group.Name);
		Assert.AreEqual(1, group.MenuLinks.Count);
		Assert.AreEqual(menuLink.MenuLinkId, group.MenuLinks[0].MenuLinkId);
		Assert.AreEqual(BooleanType.TRUE, group.MenuLinks[0].IsSelected(key));
	    } finally {
		this.DeleteMenuLinkGroup(group);
	    }
	}

	[Test]
	public void ShouldBeAbleToGetMenuLinkWithChildMenuLinks() {
	    MenuLinkGroup group = null;
	    MenuLink menuLink = null;
	    MenuLink childLink = null;
	    try {
		group = GetTestMenuLinkGroup();
		menuLink = GetTestMenuLink(group, new StringTypeList());
		childLink = GetTestMenuLink(menuLink, new StringTypeList());

		Assert.AreEqual(1, group.MenuLinks.Count);
		Assert.AreEqual(1, group.MenuLinks[0].ChildMenuLinks.Count);
	    } finally {
		this.DeleteMenuLinkGroup(group);
	    }
	}

	[Test]
	public void ShouldBeAbleToIndexIntoMenuLinkGroupListByName() {
	    MenuLinkGroup group1 = null;
	    MenuLinkGroup group2 = null;
	    try {
		group1 = GetTestMenuLinkGroup();
		MenuLinkGroupData data = new MenuLinkGroupData();
		data.Name = new StringType("bbb");
		group1.Update(data);
		group2 = GetTestMenuLinkGroup();
		data.Name = new StringType("aaa");
		group2.Update(data);

		MenuLinkGroupList list = MenuLinkGroup.GetMenuLinkGroups();

		Assert.AreEqual(2, list.Count);
		Assert.AreEqual(group2.MenuLinkGroupId, list[new StringType("aaa")].MenuLinkGroupId);
		Assert.AreEqual(group1.MenuLinkGroupId, list[new StringType("bbb")].MenuLinkGroupId);
	    } finally {
		this.DeleteMenuLinkGroup(group1);
		this.DeleteMenuLinkGroup(group2);
	    }
	}

	[Test,Ignore]
	public void ShouldHandleEffectiveAndExperationDates() {
	    Assert.Fail();
	}


	#region Helper Methods
	private MenuLinkGroup GetTestMenuLinkGroup() {
	    MenuLinkGroupData groupData = new MenuLinkGroupData();
	    groupData.Name = MENULINKGROUP_NAME;
	    return MenuLinkGroup.Create(groupData);
	}

	private MenuLink GetTestMenuLink(IMenuLinkGroup group, StringTypeList keys) {
	    MenuLinkData menuLinkData = new MenuLinkData();
	    menuLinkData.Name = MENULINK_NAME;
	    menuLinkData.Active = BooleanType.TRUE;
	    menuLinkData.EffectiveDate = DateTimeType.Now.AddDays(-1);
	    menuLinkData.ExpirationDate = DateTimeType.Now.AddDays(1);
	    menuLinkData.MenuLinkGroupId = group.MenuLinkGroupId;
	    menuLinkData.Target = MENULINK_TARGET;

	    MenuLink menuLink = MenuLink.Create(menuLinkData);

	    foreach(StringType key in keys) {
		MenuLinkKeyData keyData = new MenuLinkKeyData();
		keyData.Key = key;
		keyData.MenuLinkId = menuLink.MenuLinkId;
		MenuLinkKey.Create(keyData);
	    }
	    return menuLink;
	}

	private MenuLink GetTestMenuLink(IMenuLink parent, StringTypeList keys) {
	    MenuLinkData menuLinkData = new MenuLinkData();
	    menuLinkData.Name = MENULINK_NAME;
	    menuLinkData.Active = BooleanType.TRUE;
	    menuLinkData.EffectiveDate = DateTimeType.Now.AddDays(-1);
	    menuLinkData.ExpirationDate = DateTimeType.Now.AddDays(1);
	    menuLinkData.ParentMenuLinkId = parent.MenuLinkId;
	    menuLinkData.Target = MENULINK_TARGET;

	    MenuLink menuLink = MenuLink.Create(menuLinkData);

	    foreach(StringType key in keys) {
		MenuLinkKeyData keyData = new MenuLinkKeyData();
		keyData.Key = key;
		keyData.MenuLinkId = menuLink.MenuLinkId;
		MenuLinkKey.Create(keyData);
	    }
	    return menuLink;
	}

	private void DeleteMenuLinkGroup(IMenuLinkGroup group) {
		if (group != null) {
	    foreach(IMenuLink link in group.MenuLinks) {
		DeleteMenuLink(link);
	    }
	    MenuLinkGroupDAO.DAO.Delete(group.MenuLinkGroupId);
		}
	}

	private void DeleteMenuLink(IMenuLink menuLink) {
	    foreach(IMenuLinkKey key in menuLink.Keys) {
		MenuLinkKeyDAO.DAO.Delete(key.MenuLinkId, key.Key);
	    }
	    foreach(IMenuLink link in menuLink.ChildMenuLinks) {
		DeleteMenuLink(link);
	    }
	    MenuLinkDAO.DAO.Delete(menuLink.MenuLinkId);
	}
	#endregion End Helper Methods

    }
}
