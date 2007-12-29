/* namespacing object */
var net=new Object();

var debug = false;

net.READY_STATE_UNINITIALIZED=0;
net.READY_STATE_LOADING=1;
net.READY_STATE_LOADED=2;
net.READY_STATE_INTERACTIVE=3;
net.READY_STATE_COMPLETE=4;


/*--- content loader object for cross-browser requests ---*/
net.ContentLoader=function(url,onload,onerror,method,params,contentType){
  this.req=null;
  this.onload=onload;
  this.onerror=(onerror) ? onerror : this.defaultError;
  this.loadXMLDoc(url,method,params,contentType);
}

net.ContentLoader.prototype={
 loadXMLDoc:function(url,method,params,contentType){
  if (!method){
    method="GET";
  }
  if (!contentType && method=="POST"){
    contentType='application/x-www-form-urlencoded';
  }
  if (window.XMLHttpRequest){
    this.req=new XMLHttpRequest();
  } else if (window.ActiveXObject){
    this.req=new ActiveXObject("Microsoft.XMLHTTP");
  }
  if (this.req){
    try{
      var loader=this;
      this.req.onreadystatechange=function(){
        loader.onReadyState.call(loader);
      }
      this.req.open(method,url,true);
      if (contentType){
        this.req.setRequestHeader('Content-Type', contentType);
      }
      if(debug){
	alert(params);
      }
      this.req.send(params);
    }catch (err){
      this.onerror.call(this);
    }
  }
 },

 onReadyState:function(){
  var req=this.req;
  var ready=req.readyState;
  if (ready==net.READY_STATE_COMPLETE){
  var httpStatus=req.status;
    if (httpStatus==200 || httpStatus==0){
      this.onload.call(this);
    }else{
      this.onerror.call(this);
    }
  }
 },

 defaultError:function(){
  alert("error fetching data!"
    +"\n\nreadyState:"+this.req.readyState
    +"\nstatus: "+this.req.status
    +"\nheaders: "+this.req.getAllResponseHeaders());
 }
}




net.cmdQueues=new Array();
net.Base;

net.CommandQueue=function(url,freq){
  net.Base = this;
  this.id = "1";
  net.cmdQueues["1"] = this;
  this.url = url;
  this.queued = new Array();
  this.sent = new Array();
  this.queryStringVariables = new Hashtable();
  if (freq){
    this.repeat(freq);
  }
}

net.CommandQueue.STATUS_QUEUED=-1;
net.CommandQueue.STATE_UNINITIALIZED=net.READY_STATE_UNINITIALIZED;
net.CommandQueue.STATE_LOADING=net.READY_STATE_LOADING;
net.CommandQueue.STATE_LOADED=net.READY_STATE_LOADED;
net.CommandQueue.STATE_INTERACTIVE=net.READY_STATE_INTERACTIVE;
net.CommandQueue.STATE_COMPLETE=net.READY_STATE_COMPLETE;
net.CommandQueue.STATE_PROCESSED=5;

net.CommandQueue.PRIORITY_NORMAL=0;
net.CommandQueue.PRIORITY_IMMEDIATE=1;

net.CommandQueue.TYPE_MULTIPROCESS=0;
net.CommandQueue.TYPE_SINGLEPROCESS=1;
net.CommandQueue.TYPE_SINGLEPROCESS_HOLDQAUEUE=2;

net.CommandQueue.holdQueue = false;


net.CommandQueue.prototype={
 addCommand:function(command){
  if (this.isCommand(command)){
    if(debug){
      alert("isCommand");
    }
    var added = false;
    if(command.type == net.CommandQueue.TYPE_SINGLEPROCESS || command.type == net.CommandQueue.TYPE_SINGLEPROCESS_HOLDQAUEUE) {
      if(debug){alert("Single Process Command");}
      for(var i=0; i<this.queued.length; i++) {
	if(this.queued[i].ajaxCommand == command.ajaxCommand) {
	  this.queued[i] = command;
	  if(debug){alert("Single Process Command UPDATED");}
	  added = true;
	  break;
	}
      }
    }
    if(!added) {
      this.queued.append(command,true);
      if(debug){alert("added command");}
    }
    if (command.priority==net.CommandQueue.PRIORITY_IMMEDIATE){
      if(debug){alert("PRIORITY_IMMEDIATE");}
      this.fireRequest();
    }
  }else{
    if(debug){
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
      if(cmd.type == net.CommandQueue.TYPE_SINGLEPROCESS || cmd.type == net.CommandQueue.TYPE_SINGLEPROCESS_HOLDQAUEUE) {
        for(var j=0; j<this.sent.length; j++) {
          if(this.sent[j] && this.sent[j].ajaxCommand == cmd.ajaxCommand) {
            newQueued.append(cmd);
            processCommand = false;
            break;
          }
        }
      }
      if(processCommand) {
        if(cmd.tpe == net.CommandQueue.TYPE_SINGLEPROCESS_HOLDQAUEUE) {
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
    if(debug) {
      alert("Query string = " + data);
    }
    this.loader = new net.ContentLoader(this.url, net.CommandQueue.onload, net.CommandQueue.onerror, "POST",data);
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
    var cmd="net.Base.fireRequest()";
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
     if(this.queued[i].priority == net.CommandQueue.PRIORITY_IMMEDIATE){
       net.Base.fireRequest();
       return;
     }
   }
 }
}

net.CommandQueue.onload=function(){
if(debug){
alert("OnLoad");
}
  var xmlDoc=this.req.responseXML;
  var myText = this.req.responseText;
  var elDocRoot=xmlDoc.getElementsByTagName("commands")[0];
  if (elDocRoot){
    if(debug){
    alert("Good XML");
    }
    var needFireQueue = false;
    for(var i=0;i<elDocRoot.childNodes.length;i++){
      elChild=elDocRoot.childNodes[i];
      if (elChild.nodeName=="command"){
        var attrs=elChild.attributes;
        var id=attrs.getNamedItem("id").value;
         //Get refence from myself here
            if(debug){
            alert(id);
            }
         command = net.Base.sent[id];   
       if (command){
           if(debug){
            alert("GotCommand");
            }
          var unhandledException = elChild.attributes.getNamedItem("unhandledException");
          if(unhandledException && unhandledException.value == 'true') {
	    alert(DecodeText(elChild.attributes.getNamedItem("message").value));
	  } else {
	    command.ParseResponse(elChild);
	    if(!needFireQueue && command.type == net.CommandQueue.TYPE_SINGLEPROCESS && command.priority == net.CommandQueue.PRIORITY_IMMEDIATE) {
	      for(var j=0; j<net.Base.queued.length; j++) {
		if(command.ajaxCommand == net.Base.queued[j].ajaxCommand) {
		  needFireQueue = true;
		  break;
		}
	      }
	    }
	    net.Base.sent[id] = null;
          }
        }
      }
      if(needFireQueue) {
	net.Base.fireRequest();
      }
    }
  }
}

net.CommandQueue.onerror=function(){
  alert("problem sending the data to the server");
}


Object.prototype.implementsProp=function(propName){
  return (this[propName]!=null);
}

Object.prototype.implementsFunc=function(funcName){
  return (this[funcName]!=null); /*&& this[funcName] instanceof Function;*/
}



