
function UpdateSelect(select, options, selectedOptionValue, valueXMLAttribute, textXMLAttribute) {
    var emptyOption = null;
    if(select.options[0].text.length == 0) {
	emptyOption = select.options[0];
    }

    while(select.options.length > 0) {
	select.remove(select.options.length-1);
    }

    if(emptyOption != null) {
	try {
	    select.add(emptyOption, null); // standards compliant
	} catch(ex) {
	    select.add(emptyOption); // IE only
	}
    }

    for(var i=0; i<options.length; i++) {
	var option = document.createElement('option');
	option.text = options[i].attributes.getNamedItem(textXMLAttribute).value;
	option.value = options[i].attributes.getNamedItem(valueXMLAttribute).value;
	if(selectedOptionValue == option.value) {
	    option.selected = true;
	}
	try {
	    select.add(option, null); // standards compliant
	} catch(ex) {
	    select.add(option); // IE only
	}
    }
}

function BuildDiv(id, cssClass) {
    var div = document.createElement('div');
    //Use of setAttribute because it is supported by all browsers
    div.setAttribute('id', id);
    div.className = cssClass;
    return div;
}

function BuildDivWithText(id, cssClass, text) {
    var div = document.createElement('div');
    //Use of setAttribute because it is supported by all browsers
    div.setAttribute('id', id);
    div.className = cssClass;
    div.appendChild(document.createTextNode(text));
    return div;
}

function BuildHiddenInput(id, name, value) {
    var input = document.createElement('input');
    //Use of setAttribute because it is supported by all browsers
    input.setAttribute('type', 'hidden');
    input.setAttribute('id', id);
    input.setAttribute('name', name);
    input.setAttribute('value', value);
    return input;
}

function BuildAnchor(href) {
    var anchor = document.createElement('A');
    anchor.href = href;
    return anchor;
}

function BuildImage(src, alt, cssClass) {
    var image = document.createElement('img');
    image.src = src;
    image.alt = alt;
    image.className = cssClass;
    return image;
}

function FindPosX(obj) {
    var curleft = 0;
    if(obj.offsetParent) {
	while(1) {
	    curleft += obj.offsetLeft;
	    if(!obj.offsetParent) {
		break;
	    }
	    obj = obj.offsetParent;
	}
    } else if(obj.x) {
	curleft += obj.x;
    }
    return curleft;
}

function FindPosY(obj) {
    var curtop = 0;
    if(obj.offsetParent) {
	while(1) {
	    curtop += obj.offsetTop;
	    if(!obj.offsetParent) {
		break;
	    }
	    obj = obj.offsetParent;
	}
    } else if(obj.y) {
	curtop += obj.y;
    }
    return curtop;
}

function GetElementById(element, id) {
    var nodeList = element.childNodes;
    for(var i=0; i<nodeList.length; i++) {
	if(nodeList[i].id && nodeList[i].id == id) {
	    return nodeList[i];
	}
    }
    return null;
}


var changeHeightTimeout = null;
function CollapseElementHeight(element) {
    clearTimeout(changeHeightTimeout);
    var height = parseInt(element.style.height);
    if(!height) {
	element.style.height = element.offsetHeight + "px";
    }
    ChangeHeight(element, 0);
}

function ExpandElementToChildHeight(element) {
    clearTimeout(changeHeightTimeout);
    var height = 0;
    for(var i=0; i<element.childNodes.length; i++) {
	var tempHeight = parseInt(element.childNodes[i].style.height);
	if(!tempHeight && element.childNodes[i].offsetHeight) {
	    tempHeight = element.childNodes[i].offsetHeight;
	}
	if(tempHeight > height) {
	    height = tempHeight;
	}
    }
    ChangeHeight(element, height);
}

function ChangeHeight(element, goalHeight) {
    var focusElement = null;
    for(var i=0; i<element.childNodes.length; i++) {
	element.childNodes[i].blur();
    }

    var height = parseInt(element.style.height);
    if(height == goalHeight) {
	return;
    }

    var deltaHeight = 10;
    if(height < goalHeight) {
	if(goalHeight-height<deltaHeight){
	    element.style.height = goalHeight + "px";
	} else {
	    element.style.height = parseInt(element.style.height) + deltaHeight + "px";
	}
    } else {
	if(height-goalHeight<deltaHeight){
	    element.style.height = goalHeight + "px";
	} else {
	    element.style.height = parseInt(element.style.height) - deltaHeight + "px";
	}
    }
    changeHeightTimeout = setTimeout("ChangeHeight(document.getElementById('" + element.id + "')," + goalHeight + ")", 0);
}

function DecodeText(text) {
    return unescape(text.replace(/\+/g, ' '));
}

//Set expireDays = null for a session cookie
function setCookie(c_name, value, expireDays) {
    var exdate = new Date();
    exdate.setDate(exdate.getDate() + expireDays);
    document.cookie = c_name + "=" + escape(value) + ((expireDays == null) ? "" : ";expires=" + exdate.toGMTString());
}

function getCookieValue(c_name) {
    if (document.cookie.length > 0) {
	c_start = document.cookie.indexOf(c_name + "=");
	if (c_start!=-1) {
	    c_start = c_start + c_name.length + 1;
	    c_end = document.cookie.indexOf(";", c_start);
	    if (c_end == -1) {
		c_end = document.cookie.length;
	    }
	    return unescape(document.cookie.substring(c_start, c_end));
	}
    }
    return "";
}