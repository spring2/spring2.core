using System;
using System.Collections;

namespace Spring2.Core.Drawing.Graphing {

    /// <summary>
    /// Summary description for LabelFormat.
    /// </summary>
    public abstract class LabelFormat {

	protected IList labels = new ArrayList();

	/// <summary>
	/// internal structure to keep track of the individual data points
	/// </summary>
	protected struct LabelPoint {
	    public float value;
	    public String text;
	}
	
	public LabelFormat() {
	}

	public virtual void AddLabel(float value, String text) {
	    LabelPoint label = new LabelPoint();
	    label.value = value;
	    label.text = text;
	    labels.Add(label);
	}

	public virtual String GetLabel(float value) {
	    return String.Empty;
	}

    }
}
