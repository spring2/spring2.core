using System;
using Spring2.Core.Configuration;
using Spring2.Core.ResourceManager.DataObject;
using Spring2.Core.Types;
using Spring2.Core.Security;
using Spring2.Core.ResourceManager.BusinessLogic;
using Spring2.Core.ResourceManager.Dao;
using Spring2.Core.DAO;
using System.Collections.Generic;

namespace Spring2.Core.ResourceManager.Facade {

    /// <summary>
    /// Fetches localized resources
    /// </summary>
    public class ResourceLocalizer {
	public static readonly String KEY_CACHETIMEOUT = "ResourceLocalizer.CacheTimeout";

	private static Int32 cacheTimeout = 0;
	private static DateTime lastRefresh = DateTime.MinValue;
	private static Int32 refreshes = 0;
    	private static Dictionary<ResourceKey, Resource> resources = new Dictionary<ResourceKey, Resource>();
	private static Dictionary<LocalizedResourceKey, LocalizedResource> localizedResources = new Dictionary<LocalizedResourceKey, LocalizedResource>();
        private static String resourceLock = "";

	public ResourceLocalizer(ILocaleFactory localeFactory, ILanguageFactory languageFactory) {
	    LocalizedResourceDAO.DAO.LanguageFactory = languageFactory;
	    LocalizedResourceDAO.DAO.LocaleFactory = localeFactory;

	    if (!String.IsNullOrEmpty(ConfigurationProvider.Instance.Settings[KEY_CACHETIMEOUT])) {
		cacheTimeout = Int32.Parse(ConfigurationProvider.Instance.Settings[KEY_CACHETIMEOUT]);
	    }

	    //Console.Out.WriteLine("cache timeout is " + cacheTimeout);
	    RefreshCache();
	}

    	public Int32 Refreshes {
	    get { return refreshes; }
    	}

	/// <summary>
	/// Method to reset all of the internal cache to initial values when class is created.  
	/// Should only be needed for testing purposes.
	/// </summary>
	public static void ResetCache() {
	    lock (resourceLock) {
		cacheTimeout = 0;
		lastRefresh = DateTime.MinValue;
		refreshes = 0;
		resources = new Dictionary<ResourceKey, Resource>();
		localizedResources = new Dictionary<LocalizedResourceKey, LocalizedResource>();
	    }
	}

	private static void RefreshCache() {
	    if (cacheTimeout > 0 && DateTime.Now > lastRefresh.AddSeconds(cacheTimeout)) {
		lock (resourceLock) {
		    //Console.Out.WriteLine("refresshing cache");
		    resources = new Dictionary<ResourceKey, Resource>();
		    foreach (Resource resource in ResourceDAO.DAO.GetList()) {
			ResourceKey key = new ResourceKey(resource.Context, resource.Field, resource.Identity);
			resources.Add(key, resource);
		    }

		    localizedResources = new Dictionary<LocalizedResourceKey, LocalizedResource>();
		    foreach (LocalizedResource localizedResource in LocalizedResourceDAO.DAO.GetList()) {
			LocalizedResourceKey key = new LocalizedResourceKey(localizedResource.ResourceId, localizedResource.Locale, localizedResource.Language);
			localizedResources.Add(key, localizedResource);
		    }
		    lastRefresh = DateTime.Now;
		    refreshes++;
		}
	    }
	}

	/// <summary>
	/// Looks up the localized resource using the locale and language of the user on the thread
	/// </summary>
	/// <param name="context"></param>
	/// <param name="field"></param>
	/// <param name="identity"></param>
	/// <param name="locale"></param>
	/// <param name="language"></param>
	/// <returns></returns>
	public StringType Localize(StringType context, StringType field, IdType identity, ILocale locale, ILanguage language) {
	    RefreshCache();

	    //First, look up the resource id using the context, key, and identity.
	    try {
		ResourceKey rkey = new ResourceKey(context, field, identity);
	    	Resource resource;
		if (resources.ContainsKey(rkey)) {
		    resource = resources[rkey];
		} else {
	    	    //Console.Out.WriteLine("resource lookup for " + rkey.Key);
		    if (identity.IsValid) {
			resource = ResourceDAO.DAO.FindByContextFieldAndIdentity(context, field, identity);
		    } else {
			resource = ResourceDAO.DAO.FindByContextFieldAndIdentity(context, field, IdType.UNSET);
		    }
		}

		//now take that resource to the localized resource table
		LocalizedResourceKey lkey = new LocalizedResourceKey(resource.ResourceId, locale, language);
	    	LocalizedResource localizedResource;
		if (localizedResources.ContainsKey(lkey)) {
		    localizedResource = localizedResources[lkey];
		} else {
		    //Console.Out.WriteLine("localized resource lookup for " + rkey.Key);
		    localizedResource = LocalizedResourceDAO.DAO.FindByResourceIdLocaleAndLanguage(resource.ResourceId, locale, language);
		}
		return localizedResource.Content;

	    } catch (FinderException) {
		String unknownResouce = context.ToString();
		if (identity.IsValid) {
		    unknownResouce += "(" + identity.ToInt32().ToString() + ")";
		}
		unknownResouce += "." + field.ToString();
		return StringType.Parse(unknownResouce);
	    }
	}


	/// <summary>
	/// Looks up a localized resource using an identity, but returns the non identity specific version if none is found
	/// Uses the locale of the current user to find the appropriate resource
	/// </summary>
	/// <param name="context"></param>
	/// <param name="field"></param>
	/// <param name="identity"></param>
	/// <returns></returns>
	public StringType LocalizeWithDefault(StringType context, StringType field, IdType identity) {
	    if (System.Threading.Thread.CurrentPrincipal is IUserPrincipal) {
		IUserPrincipal currentPrincipal = (IUserPrincipal)System.Threading.Thread.CurrentPrincipal;
		return LocalizeWithDefault(context, field, identity, currentPrincipal.Locale, currentPrincipal.Language);
	    } else {
		throw new ApplicationException("Principal on thread does not implement IUserPrincipal");
	    }
	}

	/// <summary>
	/// Looks up a localized resource using an identity, but returns the non identity specific version if none is found
	/// </summary>
	/// <param name="context"></param>
	/// <param name="field"></param>
	/// <param name="identity"></param>
	/// <param name="locale"></param>
	/// <param name="language"></param>
	/// <returns></returns>
	public StringType LocalizeWithDefault(StringType context, StringType field, IdType identity, ILocale locale, ILanguage language) {
	    //First, look up the resource id using the context, key, and identity.
	    try {
		Resource resource;
		if (identity.IsValid) {
		    try {
			//try finding the identity specific version of this resource
			resource = ResourceDAO.DAO.FindByContextFieldAndIdentity(context, field, identity);
		    } catch (FinderException) {
			//if no identity specific version is found, try to find a default (non identity specific) version
			resource = ResourceDAO.DAO.FindByContextFieldAndIdentity(context, field, IdType.UNSET);
		    }
		} else {
		    resource = ResourceDAO.DAO.FindByContextFieldAndIdentity(context, field, IdType.UNSET);
		}
		//now take that resource to the localized resource table
		LocalizedResource localizedResource = LocalizedResourceDAO.DAO.FindByResourceIdLocaleAndLanguage(resource.ResourceId, locale, language);
		return localizedResource.Content;
	    } catch (FinderException) {
		String unknownResouce = context.ToString();
		if (identity.IsValid) {
		    unknownResouce += "(" + identity.ToInt32().ToString() + ")";
		}
		unknownResouce += "." + field.ToString();
		return StringType.Parse(unknownResouce);
	    }

	}

	/// <summary>
	/// Uses the locale of the current user to find the appropriate resource
	/// </summary>
	/// <param name="context"></param>
	/// <param name="field"></param>
	/// <param name="identity"></param>
	/// <returns></returns>
	public StringType Localize(StringType context, StringType field, IdType identity) {
	    if (System.Threading.Thread.CurrentPrincipal is IUserPrincipal) {
		IUserPrincipal currentPrincipal = (IUserPrincipal)System.Threading.Thread.CurrentPrincipal;
		return Localize(context, field, identity, currentPrincipal.Locale, currentPrincipal.Language);
	    } else {
		throw new ApplicationException("Principal on thread does not implement IUserPrincipal");
	    }
	}

	public StringType Localize(StringType context, StringType field) {
	    return Localize(context, field, IdType.UNSET);
	}

	public StringType Localize(StringType context, StringType field, ILocale locale, ILanguage language) {
	    return Localize(context, field, IdType.UNSET, locale, language);
	}

	private class ResourceKey : IEquatable<ResourceKey>, IEqualityComparer<ResourceKey> {
	    private readonly StringType context;
	    private readonly StringType field;
	    private readonly IdType identity;

	    public ResourceKey(StringType context, StringType field) {
		this.context = context;
		this.field = field;
	    	this.identity = IdType.UNSET;
	    }

	    public ResourceKey(StringType context, StringType field, IdType identity) {
	    	this.context = context;
	    	this.field = field;
	    	this.identity = identity;
	    }

	    public StringType Context {
		get { return context; }
	    }

	    public StringType Field {
		get { return field; }
	    }

	    public IdType Identity {
		get { return identity; }
	    }

	    public string Key {
		get { return context + "%" + field + "%" + identity; }
	    }

	    public bool Equals(ResourceKey that) {
		return this.Context == that.Context && this.Field == that.Field && this.Identity==that.Identity;
	    }

	    public bool Equals(ResourceKey x, ResourceKey y) {
		return x.Equals(y);
	    }

	    public Int32 GetHashCode(ResourceKey obj) {
		return obj.GetHashCode();
	    }

	    public override int GetHashCode() {
		unchecked {
		    var result = 0;
		    result = (result * 397) ^ this.Context.GetHashCode();
		    result = (result * 397) ^ this.Field.GetHashCode();
		    result = (result * 397) ^ this.Identity.GetHashCode();
		    return result;
		}
	    }

	    public override bool Equals(object obj) {
		ResourceKey that = obj as ResourceKey;
		if (obj != null) {
		    return this.Equals(that);
		} else {
		    return false;
		}
	    }
	}

	private class LocalizedResourceKey : IEquatable<LocalizedResourceKey>, IEqualityComparer<LocalizedResourceKey> {
	    private readonly IdType resourceId;
	    private readonly ILocale locale;
	    private readonly ILanguage language;

	    public LocalizedResourceKey(IdType resourceId, ILocale locale, ILanguage language) {
		this.resourceId = resourceId;
		this.locale = locale;
		this.language = language;
	    }

	    public IdType ResourceId {
		get { return resourceId; }
	    }

	    public ILocale Locale {
		get { return locale; }
	    }

	    public ILanguage Language {
		get { return language; }
	    }

	    public string Key {
		get { return ResourceId + "%" + Locale.Code + "%" + Language.Code; }
	    }

	    public bool Equals(LocalizedResourceKey that) {
		return this.ResourceId == that.ResourceId && this.Locale == that.Locale && this.Language == that.Language;
	    }

	    public bool Equals(LocalizedResourceKey x, LocalizedResourceKey y) {
		return x.Equals(y);
	    }

	    public Int32 GetHashCode(LocalizedResourceKey obj) {
		return obj.GetHashCode();
	    }

	    public override int GetHashCode() {
		unchecked {
		    var result = 0;
		    result = (result * 397) ^ this.ResourceId.GetHashCode();
		    result = (result * 397) ^ this.Locale.Code.GetHashCode();
		    result = (result * 397) ^ this.Language.Code.GetHashCode();
		    return result;
		}
	    }

	    public override bool Equals(object obj) {
		LocalizedResourceKey that = obj as LocalizedResourceKey;
		if (obj != null) {
		    return this.Equals(that);
		} else {
		    return false;
		}
	    }
	}


    }
}

namespace Spring2.Core.ResourceManager.Types {
    //The dtg templates are currently inserting this namespace into the 'uses' headers on each module. 
    //This is here just so we don't have to switch to a custom template.  
}
