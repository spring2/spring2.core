/*---- ComplexPostCommand ----*/
  
sp2Ajax.ComplexPostCommand = new Class({
    Extends: sp2Ajax.Command,

    initialize: function(options) {
	this.parent(options);
	var lines = new Array();
	lines.include({'rowId':1, 'check':$('complexBox1').checked, 'text':$('complexIn1').value});
	lines.include({'rowId':2, 'check':$('complexBox2').checked, 'text':$('complexIn2').value});
	lines.include({'rowId':3, 'check':$('complexBox3').checked, 'text':$('complexIn3').value});
	this.parameters.set("rows",lines);
    },

    parseResponse: function(returnObject) {
	$('complexOut1').value = returnObject.rows[0].Text;
	$('complexBoxOut1').checked = returnObject.rows[0].Check;
	
	$('complexOut2').value = returnObject.rows[1].Text;
	$('complexBoxOut2').checked = returnObject.rows[1].Check;
	
	$('complexOut3').value = returnObject.rows[2].Text;
	$('complexBoxOut3').checked = returnObject.rows[2].Check;
    }
});