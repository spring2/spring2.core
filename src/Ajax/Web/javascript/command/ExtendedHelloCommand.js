/*---- ExtendedHelloCommand ----*/
  
sp2Ajax.ExtendedHelloCommand = new Class({
    Extends: sp2Ajax.Command,

    initialize: function(options) {
	this.parent(options);
	this.extended = new sp2Ajax.HelloCommand(options);
	this.parameters.combine(this.extended.parameters);
    },

    parseResponse: function(returnObject) {
	$('ExtendedHelloResults').value = returnObject.message;
	this.extended.parseResponse(returnObject);
    }
});