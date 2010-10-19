using System;
#if (NET_1_1)
#else
using System.Collections.Generic;
#endif

using Spring2.Core.Types;
using Spring2.Core.DAO;

using Spring2.Core.ResourceManager.BusinessLogic;
using Spring2.Core.ResourceManager.Dao;
using Spring2.Core.ResourceManager.DataObject;

namespace Spring2.Core.ResourceManager.Facade {
    /// <summary>
    /// Allows editing of resources
    /// </summary>
    public class ResourceEditor {

	public ResourceEditor(ILocaleFactory localeFactory, ILanguageFactory languageFactory) {
	    LocalizedResourceDAO.DAO.LanguageFactory = languageFactory;
	    LocalizedResourceDAO.DAO.LocaleFactory = localeFactory;
	}

	/// <summary>
	/// Returns a StringTypeList of all contexts in the DB.
	/// </summary>
	/// <returns></returns>
	public StringTypeList GetContexts() {
	    return Resource.GetContextList();
	}

	/// <summary>
	/// Returns a StringTypeList of all fields in the DB for given context.
	/// </summary>
	/// <returns></returns>
	public StringTypeList GetFields(StringType context) {
	    return Resource.GetFieldList(context);
	}

	/// <summary>
	/// Returns an IdTypeList of all ContextIdenties in DB for given context and field.
	/// </summary>
	/// <param name="context"></param>
	/// <param name="field"></param>
	/// <returns></returns>
	public IdTypeList GetIdentities(StringType context, StringType field) {
	    return Resource.GetIdentityList(context, field);
	}

	/// <summary>
	/// Returns all resources by context
	/// </summary>
	/// <param name="context"></param>
	/// <returns></returns>
	public ResourceList GetResources(StringType context) {
	    return Resource.GetListByContext(context);
	}

	/// <summary>
	/// Returns all resources by context and field
	/// </summary>
	/// <param name="context"></param>
	/// <param name="field"></param>
	/// <returns></returns>
	public ResourceList GetResources(StringType context, StringType field) {
	    return Resource.GetListByContextAndField(context, field);
	}

	public ResourceList SearchResources(StringType searchText) {
	    return Resource.GetListBySearchText(searchText);
	}

#if (NET_1_1)
    public ResourceDictionary GetPossibleLocalizedResources(StringType context, ILocale locale, ILanguage language) {
#else    	
	public Dictionary<IResource, ILocalizedResource> GetPossibleLocalizedResources(StringType context, ILocale locale, ILanguage language) {
#endif
	    ResourceList resources = this.GetResources(context);
#if (NET_1_1)
    	    ResourceDictionary list = new ResourceDictionary();
#else    	
	    Dictionary<IResource, ILocalizedResource> list = new Dictionary<IResource, ILocalizedResource>();
#endif
	    foreach(IResource resource in resources) {
		try {
		    list[resource] = LocalizedResource.GetInstance(resource.ResourceId, locale, language);
		} catch(FinderException) {
		    list[resource] = null;
		}
	    }
	    return list;
	}

#if (NET_1_1)
	public ResourceDictionary GetPossibleLocalizedResources(StringType context, StringType field, ILocale locale, ILanguage language) {
#else    	
	public Dictionary<IResource, ILocalizedResource> GetPossibleLocalizedResources(StringType context, StringType field, ILocale locale, ILanguage language) {
#endif
	    ResourceList resources = this.GetResources(context, field);
#if (NET_1_1)
	    ResourceDictionary list = new ResourceDictionary();
#else    	
	    Dictionary<IResource, ILocalizedResource> list = new Dictionary<IResource, ILocalizedResource>();
#endif

	    foreach(IResource resource in resources) {
		try {
		    list[resource] = LocalizedResource.GetInstance(resource.ResourceId, locale, language);
		} catch(FinderException) {
		    list[resource] = null;
		}
	    }
	    return list;
	}

#if (NET_1_1)
	public ResourceDictionary GetPossibleLocalizedResources(StringType context, StringType field, IdType identity, ILocale locale, ILanguage language) {
#else    	
	public Dictionary<IResource, ILocalizedResource> GetPossibleLocalizedResources(StringType context, StringType field, IdType identity, ILocale locale, ILanguage language) {
#endif
	    Resource resource = Resource.GetInstance(context, field, identity);
#if (NET_1_1)
	    ResourceDictionary list = new ResourceDictionary();
#else    	
	    Dictionary<IResource, ILocalizedResource> list = new Dictionary<IResource, ILocalizedResource>();
#endif

	    try {
		list[resource] = LocalizedResource.GetInstance(resource.ResourceId, locale, language);
	    } catch(FinderException) {
		list[resource] = null;
	    }
	    return list;
	}

#if (NET_1_1)
	public ResourceDictionary GetPossibleLocalizedResources(StringType context, StringType field, IdType identity, ILocale locale, ILanguage language) {
#else 
	public Dictionary<IResource, ILocalizedResource> SearchPossibleLocalizedResources(StringType searchTerm, ILocale locale, ILanguage language) {
#endif
#if (NET_1_1)
	    ResourceDictionary list = new ResourceDictionary();
#else
	    Dictionary<IResource, ILocalizedResource> list = new Dictionary<IResource, ILocalizedResource>();
#endif
	    ResourceList resources = SearchResources(searchTerm);
	    foreach (IResource resource in resources) {
		try {
		    list[resource] = LocalizedResource.GetInstance(resource.ResourceId, locale, language);
		} catch (FinderException) {
		    list[resource] = null;
		}
	    }

	    LocalizedResourceList localizedList = LocalizedResource.SearchContent(searchTerm, locale, language);
	    foreach (LocalizedResource localized in localizedList) {
		try {
		    if (!resources.Contains(localized.ResourceId)) {
			Resource resource = Resource.GetInstance(localized.ResourceId);
			list[resource] = localized;
		    }
		} catch (Exception) {}
	    }

	    return list;
	}

	public ILocalizedResource CreateLocalizedResource(IdType resourceId, ILocale locale, ILanguage language, StringType content) {
	    return LocalizedResource.Create(resourceId, locale, language, content);
	}

	public ILocalizedResource UpdateLocalizedResource(ILocalizedResource localizedResource, StringType newContent) {
	    LocalizedResourceData data = new LocalizedResourceData();
	    data.Content = newContent;
	    ((LocalizedResource)localizedResource).Update(data);
	    return localizedResource;
	}

	public ILocalizedResource UpdateLocalizedResource(IdType localizedResourceId, StringType newContent) {
	    return this.UpdateLocalizedResource(LocalizedResource.GetInstance(localizedResourceId), newContent);
	}

	public LocalizedResourceList CopyResourceAndLocalization(StringType context, StringType field, IdType originalIdentity, IdType newIdentity) {
	    Resource resource = Resource.GetInstance(context, field, originalIdentity);
	    return resource.CopyResourceAndLocalization(newIdentity);
	}

    }
}
