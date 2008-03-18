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
    Extends: Request.JSON,
    options: {
	onSuccess: function(){sp2Ajax.CommandQueue.onload(JSON.decode(this.response.text))},
	onFailure:  function(){sp2Ajax.CommandQueue.onerror(this.xhr.responseText)}
    }
});






sp2Ajax.Base;

sp2Ajax.CommandQueue=function(url,freq){
  sp2Ajax.Base = this;
  this.url = url;
  this.queued = new Array();
  this.sent = new Hash();
  this.queryStringVariables = new Hash();
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
	if(this.queued[i].ajaxCommand == command.commandKey) {
	  this.queued[i] = command;
	  if(sp2Ajax.debug){alert("Single Process Command UPDATED");}
	  added = true;
	  break;
	}
      }
    }
    if(!added) {
      this.queued.include(command,true);
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
  if (this.holdQueue || this.queued.length == 0){
    return;
  }
  var commandMapToSend = new Hash();
  var commandsToSend = new Array();
  var newQueued = new Array();

  for(var i=0; i < this.queued.length; i++){
    var cmd = this.queued[i];
    if (this.isCommand(cmd)){
      var processCommand = true;
      if(cmd.type == sp2Ajax.CommandQueue.TYPE_SINGLEPROCESS || cmd.type == sp2Ajax.CommandQueue.TYPE_SINGLEPROCESS_HOLDQAUEUE) {
        Hash.each(this.sent, function(value, key){
	    if(value.commandKey == cmd.commandKey) {
		newQueued.push(cmd);
		processCommand = false;
	    }
	});
      }
      if(processCommand) {
        if(cmd.type == sp2Ajax.CommandQueue.TYPE_SINGLEPROCESS_HOLDQAUEUE) {
	  this.holdQueue = true;
	}
        commandMapToSend.set(cmd.commandKey, commandMap.get(cmd.commandKey));
        commandsToSend.include(cmd);
        this.sent.set(cmd.responseHandlerId, cmd);
      }
    }
  }
  if(commandsToSend.length > 0) {
    var data = "ajaxRequest={fullyQualifiedNames:" + JSON.encode(commandMapToSend) + ",AjaxCommands:[";
    for(var j=0; j<commandsToSend.length; j++) {
	data += commandsToSend[j].getJsonToSend() + (commandsToSend.length - 1 == j ? "" : ",");
    }
    data += "]}";
    if(sp2Ajax.debug) {
      alert("Data string = " + data);
    }
    this.loader = new sp2Ajax.ContentLoader({url: this.url, method : 'post', data : data}).send({});
  }
  this.queued = newQueued;
  this.queryStringVariables = new Hash();
 },

 isCommand:function(obj){
  return (
    obj.parent
    && obj.implementsProp("responseHandlerId")
    && obj.implementsProp("priority")
    && obj.implementsFunc("getJsonToSend")
    && obj.implementsFunc("parseResponse")
    && obj.implementsProp("type")
  );
 },

 isCommandResponse:function(obj){
  return (
    obj.implementsProp("responseHandlerId")
    && (obj.implementsProp("response") || obj.implementsProp("unhandledException"))
  );
 },

 isGoodResponse:function(obj){
  return (
    obj.implementsProp("commandResponses")
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

sp2Ajax.CommandQueue.onload=function(responseObject){
  if(sp2Ajax.debug){alert("OnLoad");}
  //var xmlDoc=sp2Ajax.CommandQueue.loader.responseXML;
  //var elDocRoot=xmlDoc.getElementsByTagName("commands")[0];
  if (sp2Ajax.Base.isGoodResponse(responseObject)){
    if(sp2Ajax.debug){alert("Good response");}
    var needFireQueue = false;
    for(var i=0;i<responseObject.commandResponses.length;i++){
      var commandResponse = responseObject.commandResponses[i];
      if(sp2Ajax.Base.isCommandResponse(commandResponse)) {
	if(sp2Ajax.debug) {alert(commandResponse.responseHandlerId);}
	var command = sp2Ajax.Base.sent[commandResponse.responseHandlerId];
	if(command) {
	    if(sp2Ajax.debug){alert("Got Command");}
	    if(commandResponse.unhandledException && commandResponse.unhandledExeption == 'true') {
		alert(DecodeText(commandResponse.message));
	    } else {
		command.parseResponse(commandResponse.response);
		if(!needFireQueue && command.type == sp2Ajax.CommandQueue.TYPE_SINGLEPROCESS && command.priority == sp2Ajax.CommandQueue.PRIORITY_IMMEDIATE) {
		    for(var j=0; j<sp2Ajax.Base.queued.length; j++) {
			if(command.commandKey == sp2Ajax.Base.queued[j].commandKey) {
			  needFireQueue = true;
			  break;
			}
		    }
		}
		sp2Ajax.Base.sent.remove(commandResponse.responseHandlerId);
	    }
	}
      }
    }
    if(needFireQueue) {
      sp2Ajax.Base.fireRequest();
    }
   }
}

sp2Ajax.CommandQueue.onerror=function(message){
  alert("problem sending the data to the server\n\n" + message);
}


/*--- Base Command Class ---*/
sp2Ajax.Command = new Class({
    Implements: [Options],
    options: {
	type: sp2Ajax.CommandQueue.TYPE_MULTIPROCESS,
	priority: sp2Ajax.CommandQueue.PRIORITY_IMMEDIATE,
	commandKey: '',
	responseHandlerId: '',
	parameters: new Hash()
    },
    initialize: function(options) {
	this.setOptions(options);
	this.type = this.options.type;
	this.priority = this.options.priority;
	this.commandKey = this.options.commandKey;
	this.responseHandlerId = this.options.responseHandlerId;
	this.parameters = this.options.parameters;
    },
    getJsonToSend: function() {
	return "{commandKey:" + this.commandKey + ",responseHandlerId:" + this.responseHandlerId + ",parameters:" + JSON.encode(this.parameters) + "}";
    }
});


Object.prototype.implementsProp=function(propName){
  return (this[propName]!=null);
}

Object.prototype.implementsFunc=function(funcName){
  return (this[funcName]!=null); /*&& this[funcName] instanceof Function;*/
}



