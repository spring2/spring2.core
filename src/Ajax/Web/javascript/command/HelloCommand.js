/*---- HelloCommand ----*/
  
  Ajax.Commands.HelloCommand=function(elementId,ajaxCommand,cmdCounter){
  this.ajaxCommand = commandNumberMap.get(ajaxCommand);
  this.priority=sp2Ajax.CommandQueue.PRIORITY_IMMEDIATE;
  this.type=sp2Ajax.CommandQueue.TYPE_MULTIPROCESS;
  this.id=cmdCounter; 
  this.elementId = elementId;
}

Ajax.Commands.HelloCommand.prototype.QueryStringVariables=function(){
    var commandVariables = new Hashtable();
    return commandVariables;
}

Ajax.Commands.HelloCommand.prototype.ParseResponse=function(docEl){
  var attrs=docEl.attributes;
  var status = attrs.getNamedItem("status").value;
  var message = attrs.getNamedItem("message").value;

  document.getElementById('Results').innerHTML = message;
}
