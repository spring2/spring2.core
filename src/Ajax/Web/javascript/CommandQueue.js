/* namespacing object */
var sp2Ajax=new Object();

sp2Ajax.debug = false;

sp2Ajax.READY_STATE_UNINITIALIZED=0;
sp2Ajax.READY_STATE_LOADING=1;
sp2Ajax.READY_STATE_LOADED=2;
sp2Ajax.READY_STATE_INTERACTIVE=3;
sp2Ajax.READY_STATE_COMPLETE=4;


/*--- content loader object for cross-browser requests ---*/
sp2Ajax.ContentLoader = new Class({
	Extends: Request,
	options: {
		onSuccess: function(){sp2Ajax.CommandQueue.onload(this.response.xml)},
		onFailure:  function(){sp2Ajax.CommandQueue.onerror(this.xhr.responseText)}
	}
});




sp2Ajax.cmdQueues=new Array();
sp2Ajax.Base;

sp2Ajax.CommandQueue=function(url,freq){
  sp2Ajax.Base = this;
  this.id = "1";
  sp2Ajax.cmdQueues["1"] = this;
  this.url = url;
  this.queued = new Array();
  this.sent = new Array();
  this.queryStringVariables = new Hashtable();
  if (freq){
    this.repeat(freq);
  }
}

sp2Ajax.CommandQueue.STATUS_QUEUED=-1;
sp2Ajax.CommandQueue.STATE_UNINITIALIZED=sp2Ajax.READY_STATE_UNINITIALIZED;
sp2Ajax.CommandQueue.STATE_LOADING=sp2Ajax.READY_STATE_LOADING;
sp2Ajax.CommandQueue.STATE_LOADED=sp2Ajax.READY_STATE_LOADED;
sp2Ajax.CommandQueue.STATE_INTERACTIVE=sp2Ajax.READY_STATE_INTERACTIVE;
sp2Ajax.CommandQueue.STATE_COMPLETE=sp2Ajax.READY_STATE_COMPLETE;
sp2Ajax.CommandQueue.STATE_PROCESSED=5;

sp2Ajax.CommandQueue.PRIORITY_NORMAL=0;
sp2Ajax.CommandQueue.PRIORITY_IMMEDIATE=1;

sp2Ajax.CommandQueue.TYPE_MULTIPROCESS=0;
sp2Ajax.CommandQueue.TYPE_SINGLEPROCESS=1;
sp2Ajax.CommandQueue.TYPE_SINGLEPROCESS_HOLDQAUEUE=2;

sp2Ajax.CommandQueue.holdQueue = false;


sp2Ajax.CommandQueue.prototype={
 addCommand:function(command){
  if (this.isCommand(command)){
    if(sp2Ajax.debug){
      alert("isCommand");
    }
    var added = false;
    if(command.type == sp2Ajax.CommandQueue.TYPE_SINGLEPROCESS || command.type == sp2Ajax.CommandQueue.TYPE_SINGLEPROCESS_HOLDQAUEUE) {
      if(sp2Ajax.debug){alert("Single Process Command");}
      for(var i=0; i<this.queued.length; i++) {
	if(this.queued[i].ajaxCommand == command.ajaxCommand) {
	  this.queued[i] = command;
	  if(sp2Ajax.debug){alert("Single Process Command UPDATED");}
	  added = true;
	  break;
	}
      }
    }
    if(!added) {
      this.queued.append(command,true);
      if(sp2Ajax.debug){alert("added command");}
    }
    if (command.priority==sp2Ajax.CommandQueue.PRIORITY_IMMEDIATE){
      if(sp2Ajax.debug){alert("PRIORITY_IMMEDIATE");}
      this.fireRequest();
    }
  }else{
    if(sp2Ajax.debug){
      alert("not command");
    }
  }
 },

 fireRequest:function(){
  var data = "";
  var commandStr = 'ajaxCommand=';
  var commandMapString = '&CommandMapString=';
  
  if (this.holdQueue || this.queued.length == 0){
    return;
  }
  var newQueued = new Array();
  for(var i=0; i < this.queued.length; i++){
    var cmd = this.queued[i];
    if (this.isCommand(cmd)){
      var processCommand = true;
      if(cmd.type == sp2Ajax.CommandQueue.TYPE_SINGLEPROCESS || cmd.type == sp2Ajax.CommandQueue.TYPE_SINGLEPROCESS_HOLDQAUEUE) {
        for(var j=0; j<this.sent.length; j++) {
          if(this.sent[j] && this.sent[j].ajaxCommand == cmd.ajaxCommand) {
            newQueued.append(cmd);
            processCommand = false;
            break;
          }
        }
      }
      if(processCommand) {
        if(cmd.tpe == sp2Ajax.CommandQueue.TYPE_SINGLEPROCESS_HOLDQAUEUE) {
	  this.holdQueue = true;
	}
        if(i > 0){
	  commandStr += ",";
	  if(commandMapString.indexOf(cmd.ajaxCommand + "^") < 0) {
	    commandMapString += "~";
	  }
        }
        
        if(commandMapString.indexOf(cmd.ajaxCommand + "^") < 0) {
	    commandMapString += cmd.ajaxCommand + "^" + commandMap.get(cmd.ajaxCommand);
        }
        var commandIdentifier = cmd.ajaxCommand + "~" + cmd.id;
        commandStr += commandIdentifier; 
        this.addCommandVariablesToQueryStringVariables(commandIdentifier, cmd.QueryStringVariables());
        this.sent[cmd.id] = cmd;
      }
    }
  }
  if(commandStr.length > 12) {
    data = commandStr + commandMapString + this.buildQueryStringVariables();
    if(sp2Ajax.debug) {
      alert("Query string = " + data);
    }
    this.loader = new sp2Ajax.ContentLoader({url: this.url, method : 'post', data : data}).send({});
  }
  this.queued = newQueued;
  this.queryStringVariables = new Hashtable();
 },
 
 addCommandVariablesToQueryStringVariables:function(commandIdentifier, commandVariables){
    commandVariables.moveFirst();
    while(commandVariables.next()) {
	this.queryStringVariables.put(commandIdentifier + commandVariables.getKey(), escape(commandVariables.getValue()).replace(/\+/g, "%2B"));
    }
    return 
 },
 
 buildQueryStringVariables:function(){
    var returnValue = "";
    this.queryStringVariables.moveFirst();
    while(this.queryStringVariables.next()) {
	returnValue += "&" + this.queryStringVariables.getKey() + "=" + this.queryStringVariables.getValue();
    }
    return returnValue;
 },

 isCommand:function(obj){
  return (
    obj.implementsProp("id")
    && obj.implementsProp("priority")
    && obj.implementsFunc("QueryStringVariables")
    && obj.implementsFunc("ParseResponse")
    && obj.implementsProp("type")
  );
 },

 repeat:function(freq){
  this.unrepeat();
  if (freq>0){
    this.freq=freq;
    var cmd="sp2Ajax.Base.fireRequest()";
    this.repeater=setInterval(cmd,freq*1000);
  }
 },

 unrepeat:function(){
  if (this.repeater){
    clearInterval(this.repeater);
  }
  this.repeater=null;
 },
 
 HoldQueue:function(){
  this.holdQueue = true;
 },
 
 ReleaseQueue:function(){
   this.holdQueue = false;
   for(var i=0; i<this.queued.length; i++){
     if(this.queued[i].priority == sp2Ajax.CommandQueue.PRIORITY_IMMEDIATE){
       sp2Ajax.Base.fireRequest();
       return;
     }
   }
 }
}

sp2Ajax.CommandQueue.onload=function(xmlDoc){
if(sp2Ajax.debug){
alert("OnLoad");
}
  //var xmlDoc=sp2Ajax.CommandQueue.loader.responseXML;
  var elDocRoot=xmlDoc.getElementsByTagName("commands")[0];
  if (elDocRoot){
    if(sp2Ajax.debug){
    alert("Good XML");
    }
    var needFireQueue = false;
    for(var i=0;i<elDocRoot.childNodes.length;i++){
      elChild=elDocRoot.childNodes[i];
      if (elChild.nodeName=="command"){
        var attrs=elChild.attributes;
        var id=attrs.getNamedItem("id").value;
         //Get refence from myself here
            if(sp2Ajax.debug){
            alert(id);
            }
         command = sp2Ajax.Base.sent[id];   
       if (command){
           if(sp2Ajax.debug){
            alert("GotCommand");
            }
          var unhandledException = elChild.attributes.getNamedItem("unhandledException");
          if(unhandledException && unhandledException.value == 'true') {
	    alert(DecodeText(elChild.attributes.getNamedItem("message").value));
	  } else {
	    command.ParseResponse(elChild);
	    if(!needFireQueue && command.type == sp2Ajax.CommandQueue.TYPE_SINGLEPROCESS && command.priority == sp2Ajax.CommandQueue.PRIORITY_IMMEDIATE) {
	      for(var j=0; j<sp2Ajax.Base.queued.length; j++) {
		if(command.ajaxCommand == sp2Ajax.Base.queued[j].ajaxCommand) {
		  needFireQueue = true;
		  break;
		}
	      }
	    }
	    sp2Ajax.Base.sent[id] = null;
          }
        }
      }
      if(needFireQueue) {
	sp2Ajax.Base.fireRequest();
      }
    }
  }
}

sp2Ajax.CommandQueue.onerror=function(message){
  alert("problem sending the data to the server\n\n" + message);
}


Object.prototype.implementsProp=function(propName){
  return (this[propName]!=null);
}

Object.prototype.implementsFunc=function(funcName){
  return (this[funcName]!=null); /*&& this[funcName] instanceof Function;*/
}



