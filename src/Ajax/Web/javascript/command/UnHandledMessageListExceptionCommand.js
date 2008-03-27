/*---- UnHandledMessageListExceptionCommand ----*/
  
sp2Ajax.UnHandledMessageListExceptionCommand = new Class({
    Extends: sp2Ajax.Command,

    initialize: function(options) {
	this.parent(options);
    },

    parseResponse: function(returnObject) {
	alert('Should not see this alert');
    }
});