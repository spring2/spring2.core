using System;

namespace Spring2.Core.Types{
    /// <summary>
    /// ResourceManager is able to operate on instances of ILanguage.
    /// Use this interface to make your custom language enum compatibile with the resource manager
    /// </summary>
    public interface ILanguage {
	//void SetValue(Object newValue);
	String Code {
	    get;
	}

    }
}
