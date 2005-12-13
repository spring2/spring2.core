using System;
using System.Collections;
using System.Collections.Specialized;
using System.Reflection;
using System.Web;
using Spring2.Core.Types;
using Maverick.Ctl;

namespace Spring2.Core.Maverick.Controller {

    /// <summary>
    /// Abstract base controller
    /// </summary>
    public abstract class AbstractController : Throwaway {

	private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
	private ArrayList properties = new ArrayList();

	public HttpResponse Response {
	    get { return this.ControllerContext.HttpContext.Response; }
	}

	public HttpRequest Request {
	    get { return this.ControllerContext.HttpContext.Request; }
	}

	public HttpContext Context {
	    get { return this.ControllerContext.HttpContext; }
	}

	public HttpServerUtility Server {
	    get { return this.ControllerContext.HttpContext.Server; }
	}

	/// <summary>
	/// This is the method you should override to implement application logic.
	/// Default implementation just returns "success".
	/// </summary>
	/// <returns></returns>
	public virtual string Perform() {
	    return SUCCESS;
	}

	public override sealed string Go() {
	    Populate(this, this.ControllerContext.HttpContext.Request.Params);
	    this.ControllerContext.Model = this;
	    return Perform();
	}

	public void Populate(Object target, NameValueCollection data) {
	    if (data == null) return;

	    // TODO: this seems like it is backwards in that the controller properties should be reflected and then check
	    // for values in the various scopes in a priority order.

	    foreach (string key in data) {
		this.Populate(target, key, data[key]);
	    }

	    // if the ControllerContext params collection exists and has anything in it, it was should be 
	    // mapped to controller properties.  If a forward is used, what used to be passed along in the query string 
	    // could be here.
	    if (this.ControllerContext != null && this.ControllerContext.Params != null && this.ControllerContext.Params.Count > 0) {
	    	foreach(String key in this.ControllerContext.Params.Keys) {
	    	    this.Populate(target, key, this.ControllerContext.Params[key].ToString());
	    	}
	    }

	    // check for required properties
	    PropertyInfo[] pis = target.GetType().GetProperties();
	    foreach(PropertyInfo property in pis) {
		Boolean required = property.GetCustomAttributes(typeof(RequiredAttribute), true).Length == 1;
		if (required && !properties.Contains(property)) {
		    HandleMissingRequiredProperty(property);
		}
	    }
	}

	/// <summary>
	/// Sets the property specified by name to the value val on
	/// the object target.
	/// </summary>
	/// <param name="target">The instance to set the property on.</param>
	/// <param name="name">The name of the property to set.</param>
	/// <param name="val">The new value for the property.</param>
	protected void Populate(object target, string name, string val) {
	    // only process the value of the name is not null
	    if (name!=null) {
		string firstChar = name.Substring(0, 1);
		firstChar = firstChar.ToUpper();
		string propertyName = firstChar + name.Substring(1);
		try {
		    Type t = target.GetType();
		    PropertyInfo property = t.GetProperty(propertyName);
		    properties.Add(property);
		    SetValue(target, property, val);
		} catch (Exception ex) {
		    log.Warn("Unable to populate property \"" + propertyName + "\" in: " + target.GetType().FullName, ex);
		}
	    }
	}

	/// <summary>
	/// Sets the value of property in the object target to the value val.
	/// </summary>
	/// <param name="target">The instance to set the property on.</param>
	/// <param name="property">The property to set.</param>
	/// <param name="val">The new value for the property.</param>
	protected void SetValue(object target, PropertyInfo property, string val) {
	    if (property != null) {
		Type ptype = property.PropertyType;
		Boolean required = property.GetCustomAttributes(typeof(RequiredAttribute), true).Length == 1;
		if (required && String.Empty.Equals(val)) {
		    HandleMissingRequiredProperty(property);
		} else {
		    try {
			Object o = ParseValue(val, ptype);
			property.SetValue(target, o, null);
		    } catch (FormatException) {
			HandleParseException(property, val);
		    } catch (ArgumentException) {
		    	// do nothing, this happens because a property is not mapped to the request value
		    }

		}
	    }
	}

	/// <summary>
	/// Attempts to parse the given string as one of the standard
	/// value types: bool, decimal, sbyte, byte, short, ushort,
	    /// int, uint, long, ulong, char, float, or double.
	/// Simply returns val on failure.
	/// </summary>
	/// <param name="val">The string containing the value to be parsed.</param>
	/// <param name="type">The target type of val after parsing.</param>
	/// <returns>
	///	A new object representing the string val parsed as the
	///	appropriate type or val if type is not one of the standard
	///	value types.
	/// </returns>
	protected object ParseValue(string val, Type type) {
	    // allow for other types not specified here to be handled
	    Object o = ParseCustomValue(val, type);
	    if (o != null) {
		return o;
	    }

	    // check for .Net types
	    if (typeof(bool).Equals(type)) {
		return bool.Parse(val);

	    } else if (typeof(decimal).Equals(type)) {
		return decimal.Parse(val);

	    } else if (typeof(sbyte).Equals(type)) {
		return sbyte.Parse(val);

	    } else if (typeof(byte).Equals(type)) {
		return byte.Parse(val);

	    } else if (typeof(short).Equals(type)) {
		return short.Parse(val);

	    } else if (typeof(ushort).Equals(type)) {
		return ushort.Parse(val);

	    } else if (typeof(int).Equals(type)) {
		return int.Parse(val);

	    } else if (typeof(uint).Equals(type)) {
		return uint.Parse(val);

	    } else if (typeof(long).Equals(type)) {
		return long.Parse(val);

	    } else if (typeof(ulong).Equals(type)) {
		return ulong.Parse(val);

	    } else if (typeof(char).Equals(type)) {
		return char.Parse(val);

	    } else if (typeof(float).Equals(type)) {
		return float.Parse(val);

	    } else if (typeof(double).Equals(type)) {
		return double.Parse(val);
	    }

	    return val;
	}

	/// <summary>
	/// Extendeds types that ParseValue can parse
	/// </summary>
	/// <param name="val" type="string"></param>
	/// <param name="type" type="System.Type"></param>
	/// <returns>The parsed value, or null indicating that this type was not parsable by this method</returns>
	protected virtual object ParseCustomValue(String val, Type type) {
	    // check for Spring2.Core.Types
	    if (typeof(StringType).Equals(type)) {
		return StringType.Parse(val);

	    } else if (typeof(DateType).Equals(type)) {
		return DateType.Parse(val);

	    } else if (typeof(IdType).Equals(type)) {
		return IdType.Parse(val);

	    } else if (typeof(IntegerType).Equals(type)) {
		return IntegerType.Parse(val);

	    } else if (typeof(CurrencyType).Equals(type)) {
		return CurrencyType.Parse(val);

	    } else if (typeof(QuantityType).Equals(type)) {
		return QuantityType.Parse(val);

	    } else if (typeof(DecimalType).Equals(type)) {
		return DecimalType.Parse(val);

	    } else if (typeof(BooleanType).Equals(type)) {
		return BooleanType.GetInstance(val);
	    } else if (typeof(PhoneNumberType).Equals(type)) {
		return PhoneNumberType.Parse(val);
	    } else if (typeof(IdTypeList).Equals(type)) {
		IdTypeList ids = new IdTypeList();
		foreach (String key in val.Split(',')){
		    if (!key.Trim().Equals(String.Empty)) {
			ids.Add(IdType.Parse(key));
		    }
		}
		return ids;
	    } else if (type.IsSubclassOf(typeof(EnumDataType))) {
		// The GetInstance method is expected from anything that inherits from EnumDataType.  If it is missing, values cannot be parsed.
		return InvokeMethod(type, "GetInstance", new Type[] { typeof(object) }, new object[] { val });
	    }

	    // not one of the known types, return null
	    return null;
	}

	protected object InvokeMethod(Type type, string methodName, Type[] types, object[] values){
	    if(types.Length != values.Length){
		throw new ArgumentException("The parameters types and values must correspond.  The lengths of the arrays do not match.");
	    }

	    MethodInfo method = type.GetMethod(methodName, types);
	    if(null == method) {
		return null;
	    }
	    object instance = null;
	    if(!method.IsStatic) {
		instance = Activator.CreateInstance(type);
	    }

	    // If the object supports IDisposable, then we'll dispose of it after method invocation.
	    IDisposable disposable = instance as IDisposable;
	    try {
		return method.Invoke(instance, values);
	    }
	    finally {
		if(null != disposable) {
		    disposable.Dispose();
		}
	    }
	}

	/// <summary>
	/// Handle parsing exceptions
	/// </summary>
	protected virtual void HandleParseException(PropertyInfo property, object val) {
	}

	/// <summary>
	/// Handle parsing exceptions
	/// </summary>
	protected virtual void HandleMissingRequiredProperty(PropertyInfo property) {
	}

	/// <summary>
	/// Get a cookie for getting data. 
	/// </summary>
	/// <param name="cookieName"></param>
	/// <returns></returns>
	public HttpCookie GetCookie(String cookieName) {
	    HttpCookie returnCookie = ControllerContext.HttpContext.Request.Cookies[cookieName];
	    if (returnCookie == null) {
		returnCookie = new HttpCookie(cookieName);
	    }
	    return returnCookie;
	}

	/// <summary>
	/// Persists a cookie.  This can only be expected to work once for each cookie between post.
	/// </summary>
	/// <param name="cookie"></param>
	public void UpdateCookie(HttpCookie cookie) {
	    //cookie.Path = controllerContext.HttpContext.Request.ApplicationPath;
	    // TODO: set explicitly to / for problems with enrollment process
	    cookie.Path = "/";
	    ControllerContext.HttpContext.Response.Cookies.Add(cookie);
	}


    }
}
