using System;
using Spring2.Core.Types;
using Spring2.Core.Security;
using Spring2.Core.ResourceManager.BusinessLogic;
using Spring2.Core.ResourceManager.Dao;
using Spring2.Core.ResourceManager.DataObject;
using Spring2.Core.DAO;

namespace Spring2.Core.ResourceManager {
    /// <summary>
    /// Fetches localized resources
    /// </summary>
    public class ResourceLocalizer {

	public static ResourceLocalizer Localizer = new ResourceLocalizer();
	private ResourceLocalizer() {}

	/// <summary>
	/// Looks up the localized resource using the locale and language of the user on the thread
	/// </summary>
	/// <param name="context"></param>
	/// <param name="field"></param>
	/// <param name="identity"></param>
	/// <returns></returns>
	public StringType Localize(StringType context, StringType field, IdType identity, ILocale locale, ILanguage language){

	    //First, look up the resource id using the context, key, and identity.
	    try{
		Resource resource = ResourceDAO.DAO.FindByContextFieldAndIdentity(context, field, identity);
		//now take that resource to the localized resource table
		LocalizedResource localizedResource = LocalizedResourceDAO.DAO.FindByResourceIdLocaleAndLanguage(resource.ResourceId, locale, language);
		return localizedResource.Content;

	    }catch (FinderException){
		String unknownResouce = context.ToString();
		if (identity.IsValid){
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
	public StringType Localize(StringType context, StringType field, IdType identity){
	    if (System.Threading.Thread.CurrentPrincipal is ISpring2Principal) {
		ISpring2Principal currentPrincipal = (ISpring2Principal)System.Threading.Thread.CurrentPrincipal;
		return Localize(context, field, identity, currentPrincipal.Locale, currentPrincipal.Language);
	    } else {
		throw new ApplicationException("Principal on thread is not the correct type");
	    }
	}

	public StringType Localize(StringType context, StringType field){
	    return Localize(context, field, IdType.UNSET);
	}

	public StringType Localize(StringType context, StringType field, ILocale locale, ILanguage language){
	    return Localize(context, field, IdType.UNSET, locale, language);
	}
    }
}

namespace Spring2.Core.ResourceManager.Types{
    //The dtg templates are currently inserting this namespace into the 'uses' headers on each module. 
    //This is here just so we don't have to switch to a custom template.  
}
