<%@ Page %>
<div align="left">
	<% if(Context.Items["error"].GetType().FullName.Equals("System.Security.SecurityException")) { %>
	<h3><font color="red">Security Violation</font></h3>
	<% } %>
	<pre>
<%=Context.Items["error"]%>
</pre>
</div>
