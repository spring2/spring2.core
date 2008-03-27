/*---- RegurgitationCommand ----*/
  
sp2Ajax.RegurgitationCommand = new Class({
    Extends: sp2Ajax.Command,

    initialize: function(options) {
	this.parent(options);
	this.parameters.set("Text",$('regurgitationInput').value);
    },

    parseResponse: function(returnObject) {
	$('RegurgitationResults').value = returnObject.message;
    }
});