using System;
using System.Security.Principal;
using Spring2.Core.Types;

namespace Spring2.Core.Security
{

    public interface IUserPrincipal : IPrincipal {
	ILocale Locale{
	    get;
	}

	ILanguage Language{
	    get;
	}

	IdType UserId {
	    get;
	}
	
    }
}
