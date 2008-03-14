<%@ Page ValidateRequest="false" Language="C#"%>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Spring2.Core.Ajax" %>

<%
//Set up ajax commands
List<Command> ajaxCommands = new List<Command>();
if(Context.Items["ajaxCommands"] != null) {
    ajaxCommands = Context.Items["ajaxCommands"] as List<Command>;
}
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en" lang="en">
    <head>
	<title><%=Context.Items["title"]%> - Spring2 AJAX Framework&trade;</title>
	<meta http-equiv="content-type" content="text/html; charset=utf-8" />
	<% if(ajaxCommands.Count>0) { %>
	    <script src="<% =Page.ResolveClientUrl("~/javascript/arrayMethods.js") %>" type="text/javascript"></script>
	    <script src="<% =Page.ResolveClientUrl("~/javascript/Hashtable.js") %>" type="text/javascript"></script>
	    <script src="<% =Page.ResolveClientUrl("~/javascript/CommandQueue.js") %>" type="text/javascript"></script>
	    <script src="<% =Page.ResolveClientUrl("~/javascript/HelperFunctions.js") %>" type="text/javascript"></script>

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
	    <%foreach(Command command in ajaxCommands) { %>
		<script src="<% =Page.ResolveClientUrl("~/javascript/command/" + command.Name + ".js") %>" type="text/javascript"></script>
	    <% } %>
	<% } %>
    </head>
    <body>
        <div class="allContent">
	    <%=Context.Items["wrapped"]%>
	</div>
    </body>
</html>
