/*---- EvaluateCheckedCommand ----*/
  
sp2Ajax.EvaluateCheckedCommand = new Class({
    Extends: sp2Ajax.Command,

    initialize: function(options) {
	options.priority = sp2Ajax.CommandQueue.PRIORITY_NORMAL;
	options.type = sp2Ajax.CommandQueue.TYPE_MULTIPROCESS;
	this.parent(options);
	this.parameters.set("isChecked",$('checkBoxRow' + this.options.clientSideData.row).checked);
    },

    parseResponse: function(returnObject) {
	$('EvaluateChecked' + this.options.clientSideData.row).value = "Row " + this.options.clientSideData.row + " checked is " + returnObject.message;
    }
});