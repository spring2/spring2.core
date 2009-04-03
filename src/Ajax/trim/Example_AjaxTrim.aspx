<%@ Page Language="C#" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Spring2.Core.Ajax" %>

<%
//Set up ajax commands
List<Command> ajaxCommands = new List<Command>();
if(Context.Items["ajaxCommands"] != null) {
    ajaxCommands = Context.Items["ajaxCommands"] as List<Command>;
}
%>

<% if(ajaxCommands.Count>0) { %>
    <script src="<% =Page.ResolveClientUrl("~/javascript/ajax/arrayMethods.js") %>" type="text/javascript"></script>
    <script src="<% =Page.ResolveClientUrl("~/javascript/ajax/Hashtable.js") %>" type="text/javascript"></script>
    <script src="<% =Page.ResolveClientUrl("~/javascript/ajax/CommandQueue.js") %>" type="text/javascript"></script>
    <script src="<% =Page.ResolveClientUrl("~/javascript/ajax/HelperFunctions.js") %>" type="text/javascript"></script>

    <script language="javascript">
	var Ajax = new Object();
	Ajax.Commands=new Object();
	var Queue = null;
	function GetQueue(){
	    Queue = new net.CommandQueue('<%=Page.ResolveClientUrl("~/Ajax.m")%>');
	}
	this.onload = new Function(" GetQueue(); ");
    	    	          
	function CreateCommand(id, elementId){
	    switch(id){
		<%foreach(Command command in ajaxCommands) { %>
		    case '<%=command.Name%>':
			var command = new Ajax.Commands.<%=command.Name%>(elementId,id,counter);
			net.Base.addCommand(command);
			break;
		<%}%>
	    }
	    IncCounter();
	 }
                     
	 var counter = 0;
	 function IncCounter(){
	     counter++;
	 }
		 
	var commandMap = new Hashtable();
	var commandNumberMap = new Hashtable();
	<%for(Int32 i = 0; i < ajaxCommands.Count; i++) { %>
	    commandNumberMap.put("<%=ajaxCommands[i].Name%>", "<%=i%>");
	    commandMap.put("<%=i%>", "<%=ajaxCommands[i].QualifiedName%>");
	<% } %>
    </script>
<% } %>

<%foreach(Command command in ajaxCommands) { %>
    <script src="<% =Page.ResolveClientUrl("~/javascript/ajax/command/" + command.Name + ".js") %>" type="text/javascript"></script>
<% } %>

<!--*** This is AjaxTrim.aspx  Used when a page needs ajax functionality ***-->
<%=Context.Items["wrapped"]%>