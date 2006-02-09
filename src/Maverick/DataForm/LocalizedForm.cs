using System;
using System.Diagnostics;
using Spring2.Core.Maverick.Controller;
using Spring2.Core.Types;

namespace Spring2.Core.Maverick.DataForm {
    /// <summary>
    /// Summary description for LocalizedForm.
    /// </summary>
    public class LocalizedForm : PopulatedForm{

	public LocalizedForm(LocalizedController localizedController) : base(localizedController) {
	}

	public ILocale Locale{
	    get{ return ((LocalizedController)controller).Locale;}
	}

	public ILanguage Language{
	    get{ return ((LocalizedController)controller).Language;}
	}
    




    }
}
