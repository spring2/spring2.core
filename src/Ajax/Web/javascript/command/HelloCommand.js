/*---- HelloCommand ----*/
  
sp2Ajax.HelloCommand = new Class({
    Extends: sp2Ajax.Command,

    initialize: function(options) {
	this.parent(options);
	this.parameters.set("helloText","Hello All!");
    },

    parseResponse: function(returnObject) {
	$('HelloResults').value = returnObject.message;
    }
});