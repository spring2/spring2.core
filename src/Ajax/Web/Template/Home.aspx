<%@ Page Language="C#" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Spring2.Core.Ajax" %>
<%@ Import Namespace="Spring2.Core.Ajax.SampleController.SampleCommand" %>

<%
    //Set up ajaxCommands for use on BaseTrim.aspx
    List<Command> ajaxCommands = new List<Command>();
    if(Context.Items["ajaxCommands"] != null) {
	ajaxCommands = Context.Items["ajaxCommands"] as List<Command>;
    }
    ajaxCommands.Add(HelloCommand.Instance);
    Context.Items["ajaxCommands"] = ajaxCommands;
    
    Context.Items["title"] = "Home";
%>

<div>
    <div>Click a command and see results on the right</div>
    <div style="border:solid 1px blue;">
	<input style="float:right;" type="text" id="HelloResults" value="">
	<div>
	    <div><a href="#" onclick="CreateCommand('<%=HelloCommand.Instance.Name%>', null)"><%=HelloCommand.Instance.Name%></a>&nbsp;<input id="HelloCommandInput" type="text" value="" /></div>
	</div>
    </div>
</div>