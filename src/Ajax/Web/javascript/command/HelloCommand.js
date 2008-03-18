/*---- HelloCommand ----*/
  
/sp2Ajax.HelloCommand = new Class({
    Extends: sp2Ajax.Command,
    initialize: function(options) {
	this.parent(options);
	this.parameters.set("helloText",$('HelloCommandInput').value);
    },
    parseResponse: function(returnObject) {
	$('Results').innerHTML = returnObject.message;
    }
});