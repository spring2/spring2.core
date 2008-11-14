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
    ajaxCommands.Add(ComplexPostCommand.Instance);
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
	<div><a href="#" onclick="Queue.repeat(5);CreateCommand('<%=AutoFireCommand.Instance.Name%>')"><%=AutoFireCommand.Instance.Name%></a>&nbsp;&nbsp;<input type="button" onclick="Queue.unrepeat();" value="Stop Timer" /></div>
    </div>
    <div class="commandExample">
	<input style="float:right;" type="text" id="ExtendedHelloResults" value="">
	<div><a href="#" onclick="CreateCommand('<%=ExtendedHelloCommand.Instance.Name%>')"><%=ExtendedHelloCommand.Instance.Name%></a></div>
    </div>
    <div class="commandExample">
	Multiple Commands At Same time.
	<div style="margin-left:5px; width:75%">
	    <div style="clear:both;">
		<input style="float:right;" type="text" id="EvaluateChecked1" value="">
		<div>Evaluate Checked Row 1&nbsp;&nbsp;<input type="checkbox" id="checkBoxRow1" /><input type="button" onclick="CreateCommand('<%=EvaluateCheckedCommand.Instance.Name%>', {row:1});Queue.fireRequest();" value="Evaluate" /></div>
	    </div>
	    <div style="clear:both;">
		<input style="float:right;" type="text" id="EvaluateChecked2" value="">
		<div>Evaluate Checked Row 2&nbsp;&nbsp;<input type="checkbox" id="checkBoxRow2" /><input type="button" onclick="CreateCommand('<%=EvaluateCheckedCommand.Instance.Name%>', {row:2});Queue.fireRequest();" value="Evaluate" /></div>
	    </div>
	    <div style="clear:both;">
		<input style="float:right;" type="text" id="EvaluateChecked3" value="">
		<div>Evaluate Checked Row 3&nbsp;&nbsp;<input type="checkbox" id="checkBoxRow3" /><input type="button" onclick="CreateCommand('<%=EvaluateCheckedCommand.Instance.Name%>', {row:3});Queue.fireRequest();" value="Evaluate" /></div>
	    </div>
	    <div style="clear:both;"><input type="button" onclick="evaluateAll();" value="Evaluate All" /></div>
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
	Complex Posts.
	<div style="margin-left:5px; width:85%">
	    <div style="clear:both;">
		<input style="float:right;" type="text" id="complexOut1" value="">
		<input style="float:right;" type="checkbox" id="complexBoxOut1" />
		<div>Row 1&nbsp;&nbsp;<input type="checkbox" id="complexBox1" /><input type="text" id="complexIn1" value="" /></div>
	    </div>
	    <div style="clear:both;">
		<input style="float:right;" type="text" id="complexOut2" value="">
		<input style="float:right;" type="checkbox" id="complexBoxOut2" />
		<div>Row 2&nbsp;&nbsp;<input type="checkbox" id="complexBox2" /><input type="text" id="complexIn2" value="" /></div>
	    </div>
	    <div style="clear:both;">
		<input style="float:right;" type="text" id="complexOut3" value="">
		<input style="float:right;" type="checkbox" id="complexBoxOut3" />
		<div>Row 3&nbsp;&nbsp;<input type="checkbox" id="complexBox3" /><input type="text" id="complexIn3" value="" /></div>
	    </div>
	    <div style="clear:both;"><input type="button" onclick="CreateCommand('<%=ComplexPostCommand.Instance.Name%>');" value="Do Complex Post" /></div>
	</div>
    </div>
    <div class="commandExample">
	<div><a href="#" onclick="CreateCommand('<%=UnHandledMessageListExceptionCommand.Instance.Name%>')"><%=UnHandledMessageListExceptionCommand.Instance.Name%></a></div>
    </div>
    <div class="commandExample">
	<div><a href="#" onclick="CreateCommand('<%=UnHandledSystemExceptionCommand.Instance.Name%>')"><%=UnHandledSystemExceptionCommand.Instance.Name%></a></div>
    </div>
</div>