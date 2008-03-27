<%@ Page Language="C#" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Spring2.Core.Ajax" %>
<%@ Import Namespace="Spring2.Core.Ajax.SampleController.SampleCommand" %>
<%@ Import Namespace="Spring2.Core.Ajax.SampleController2.SampleCommand" %>

<%
    //Set up ajaxCommands for use on BaseTrim.aspx
    List<Command> ajaxCommands = new List<Command>();
    if(Context.Items["ajaxCommands"] != null) {
	ajaxCommands = Context.Items["ajaxCommands"] as List<Command>;
    }
    ajaxCommands.Add(HelloCommand.Instance);
    ajaxCommands.Add(RegurgitationCommand.Instance);
    ajaxCommands.Add(AutoFireCommand.Instance);
    ajaxCommands.Add(ExtendedHelloCommand.Instance);
    ajaxCommands.Add(EvaluateCheckedCommand.Instance);
    ajaxCommands.Add(UnHandledMessageListExceptionCommand.Instance);
    ajaxCommands.Add(UnHandledSystemExceptionCommand.Instance);
    Context.Items["ajaxCommands"] = ajaxCommands;
    
    Context.Items["title"] = "Home";
%>

<div>
    <div style="font-size:15pt; width:100%; border-bottom:solid 1px #303030; margin-bottom:10px;">List of Examples</div>
    <div class="commandExample">
	<input style="float:right;" type="text" id="HelloResults" value="">
	<div><a href="#" onclick="CreateCommand('<%=HelloCommand.Instance.Name%>')"><%=HelloCommand.Instance.Name%></a></div>
    </div>
    <div class="commandExample">
	<input style="float:right;" type="text" id="RegurgitationResults" value="">
	<div><a href="#" onclick="CreateCommand('<%=RegurgitationCommand.Instance.Name%>')"><%=RegurgitationCommand.Instance.Name%></a>&nbsp;<input type="text" id="regurgitationInput" value="" /></div>
    </div>
    <div class="commandExample">
	<input style="float:right;" type="text" id="AutoFireResults" value="">
	<div><a href="#" onclick="Queue.repeat(5);CreateCommand('<%=AutoFireCommand.Instance.Name%>')"><%=AutoFireCommand.Instance.Name%></a>&nbsp;&nbsp;<input type="button" onclick="Queue.unrepeat();" value="Stop Timmer" /></div>
    </div>
    <div class="commandExample">
	<input style="float:right;" type="text" id="ExtendedHelloResults" value="">
	<div><a href="#" onclick="CreateCommand('<%=ExtendedHelloCommand.Instance.Name%>')"><%=ExtendedHelloCommand.Instance.Name%></a></div>
    </div>
    <div class="commandExample">
	Multiple Commands At Same time.
	<div style="margin-left:5px; width:75%">
	    <div>
		<input style="float:right;" type="text" id="EvaluateChecked1" value="">
		<div>Evaluate Checked Row 1&nbsp;&nbsp;<input type="checkbox" id="checkBoxRow1" /><input type="button" onclick="CreateCommand('<%=EvaluateCheckedCommand.Instance.Name%>', {row:1});Queue.fireRequest();" value="Evaluate" /></div>
	    </div>
	    <div>
		<input style="float:right;" type="text" id="EvaluateChecked2" value="">
		<div>Evaluate Checked Row 2&nbsp;&nbsp;<input type="checkbox" id="checkBoxRow2" /><input type="button" onclick="CreateCommand('<%=EvaluateCheckedCommand.Instance.Name%>', {row:2});Queue.fireRequest();" value="Evaluate" /></div>
	    </div>
	    <div>
		<input style="float:right;" type="text" id="EvaluateChecked3" value="">
		<div>Evaluate Checked Row 3&nbsp;&nbsp;<input type="checkbox" id="checkBoxRow3" /><input type="button" onclick="CreateCommand('<%=EvaluateCheckedCommand.Instance.Name%>', {row:3});Queue.fireRequest();" value="Evaluate" /></div>
	    </div>
	    <div><input type="button" onclick="evaluateAll();" value="Evaluate All" /></td></div>
	    <script type="text/javascript">
		function evaluateAll() {
		    CreateCommand('<%=EvaluateCheckedCommand.Instance.Name%>', {row:1});
		    CreateCommand('<%=EvaluateCheckedCommand.Instance.Name%>', {row:2});
		    CreateCommand('<%=EvaluateCheckedCommand.Instance.Name%>', {row:3});
		    Queue.fireRequest();
		}
	    </script>
	</div>
    </div>
    <div class="commandExample">
	<div><a href="#" onclick="CreateCommand('<%=UnHandledMessageListExceptionCommand.Instance.Name%>')"><%=UnHandledMessageListExceptionCommand.Instance.Name%></a></div>
    </div>
    <div class="commandExample">
	<div><a href="#" onclick="CreateCommand('<%=UnHandledSystemExceptionCommand.Instance.Name%>')"><%=UnHandledSystemExceptionCommand.Instance.Name%></a></div>
    </div>
</div>