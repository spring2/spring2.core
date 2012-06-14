using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;
using System.Web;

using Newtonsoft.Json;

using Spring2.Core.Ajax;
using Spring2.Core.Ajax.Json;
using Spring2.Core.Ajax.PopulateBehavior;
using Spring2.Core.Message;
using Spring2.Core.PropertyPopulator;

namespace Spring2.Core.Ajax.SampleController.SampleCommand {
    public class ComplexPostCommand : Command {
	private List<Row> rows = new List<Row>();

	public static readonly ComplexPostCommand Instance = new ComplexPostCommand();

	public ComplexPostCommand(Int32 responseHandlerId, NameValueCollection values, IMessageFormatter formatter, MessageList errors, HttpCookieCollection cookies) : base(responseHandlerId, values, formatter, errors, cookies) {}
	protected ComplexPostCommand() { }

	protected override IPopulateBehavior PopulateBehavior {
	    get { return ComplexPopulate.Instance; }
	}
	

	protected override String Execute() {
	    String result = Rows.Count.ToString();
	    String status = "success";

            JsonAjaxUtility util = new JsonAjaxUtility();
            Hashtable response = new Hashtable();
            response.Add("status", status);
            response.Add("message", result);
	    response.Add("rows", this.Rows);
            return util.SerializeResponse(this.responseHandlerId, response);
	}

	public List<Row> Rows {
	    get { return rows; }
	    set { rows = value; }
	}
    }

    public class ComplexPopulate : IPopulateBehavior {
	public static ComplexPopulate Instance = new ComplexPopulate();

	private ComplexPopulate() {}

	public MessageList Populate(Object target, NameValueCollection data) {
	    String tempJson = data["rows"];
	    List<Dictionary<String, String>> tempObj = (List<Dictionary<String, String>>)JsonConvert.DeserializeObject(tempJson, typeof(List<Dictionary<String, String>>));
	    MessageList errors = new MessageList();
	    foreach(Dictionary<String, String> dict in tempObj) {
		NameValueCollection nvData = new NameValueCollection();
		Row tempRow = new Row();
		foreach(KeyValuePair<String, String> kvp in dict) {
		    nvData.Add(kvp.Key, kvp.Value);
		}
		errors.AddRange(Populator.Instance.Populate(tempRow, nvData));
		((ComplexPostCommand)target).Rows.Add(tempRow);
	    }
	    return errors;
	}
    }

    public class Row {
	private Int32 rowId;
	private bool check;
	private String text;

	public Int32 RowId {
	    get { return rowId; }
	    set { rowId = value; }
	}

	public bool Check {
	    get { return check; }
	    set { check = value; }
	}

	public String Text {
	    get { return text; }
	    set { text = value; }
	}
    }
}
