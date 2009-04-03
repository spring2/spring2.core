using System;
using System.Collections;

using Spring2.Core.ResourceManager.DataObject;

namespace Spring2.Core.ResourceManager.Facade {

    public class ResourceDictionary : DictionaryBase  {

	public ILocalizedResource this[ IResource key ]  {
	    get  {
		return( (ILocalizedResource) Dictionary[key] );
	    }
	    set  {
		Dictionary[key] = value;
	    }
	}

	public ICollection Keys  {
	    get  {
		return( Dictionary.Keys );
	    }
	}

	public ICollection Values  {
	    get  {
		return( Dictionary.Values );
	    }
	}

	public void Add( IResource key, ILocalizedResource value )  {
	    Dictionary.Add( key, value );
	}

	public bool Contains( IResource key )  {
	    return( Dictionary.Contains( key ) );
	}

	public void Remove( IResource key )  {
	    Dictionary.Remove( key );
	}

	protected override void OnInsert( Object key, Object value )  {
	    if ( key.GetType() != typeof(IResource) ) {
	    	throw new ArgumentException( "key must be of type IResource.", "key" );
	    }

	    if ( value.GetType() != typeof(ILocalizedResource) ) {
	    	throw new ArgumentException( "value must be of type ILocalizedResource.", "value" );
	    }
	}

	protected override void OnRemove( Object key, Object value )  {
	    if ( key.GetType() != typeof(IResource) ) {
		throw new ArgumentException( "key must be of type IResource.", "key" );
	    }
	}

	protected override void OnSet( Object key, Object oldValue, Object newValue )  {
	    if ( key.GetType() != typeof(IResource) ) {
		throw new ArgumentException( "key must be of type IResource.", "key" );
	    }

	    if ( newValue.GetType() != typeof(ILocalizedResource) ) {
		throw new ArgumentException( "newValue must be of type String.", "newValue" );
	    }
	}

	protected override void OnValidate( Object key, Object value )  {
	    if ( key.GetType() != typeof(IResource) ) {
		throw new ArgumentException( "key must be of type IResource.", "key" );
	    }

	    if ( value.GetType() != typeof(ILocalizedResource) ) {
		throw new ArgumentException( "value must be of type String.", "value" );
	    }
	}

    }
}