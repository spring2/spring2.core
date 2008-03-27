/*---- AutoFireCommand ----*/
  
sp2Ajax.AutoFireCommand = new Class({
    Extends: sp2Ajax.Command,

    initialize: function(options) {
	options.priority = sp2Ajax.CommandQueue.PRIORITY_NORMAL;
	options.type = sp2Ajax.CommandQueue.TYPE_SINGLEPROCESS;
	this.parent(options);
    },

    parseResponse: function(returnObject) {
	$('AutoFireResults').value = returnObject.message;
	CreateCommand('AutoFireCommand');
    }
});