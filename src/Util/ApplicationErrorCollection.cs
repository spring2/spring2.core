using System;
using System.Collections;

namespace Spring2.Core.Util {

    /// <summary>
    /// Summary description for ApplicationErrorCollection.
    /// </summary>
    public class ApplicationErrorCollection : CollectionBase {

	public ApplicationErrorCollection() {
	}

	// Indexer implementation.
	public ApplicationError this[int index] {
	    get { return (ApplicationError)List[index]; }
	}

	public void Add(ApplicationError error) {
            List.Add(error);
	}

	public void Remove(int index) {
	    List.RemoveAt(index); 
	}
    }
}
