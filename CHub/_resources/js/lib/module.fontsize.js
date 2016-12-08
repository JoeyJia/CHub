/****************************** Cummins' new web appearance script *****/
/****************************** Copyright (c) 2007-2010 Cummins AG *****/
/***********************************************************************/
/************************************************* module fontsize *****/
/***********************************************************************/
/************************************** author virtual identity AG *****/
/* $LastChangedDate: 2010-01-12 16:33:21 +0100 (Di, 12 Jan 2010) $ *****/

function init_fontsize() {
	if ($$('p.pagetools-fontsize').length) {
		var font = new FontSize();
		var title = new Cookie('text-size').read(font.getDefaultStyleSheet());
		font.setActiveStyleSheet(title);
	}
}

var FontSize = Class.create();

FontSize.prototype = {

	initialize: function() {
		var that = this;
		$$("a.tools-fontsize-decrease").each(function(_link){
			_link.observe("click", function() {
				this.fontSizeDown();
			}.bindAsEventListener(that));			
		});
		$$("a.tools-fontsize-enlarge").each(function(_link){
			_link.observe("click", function() {
				this.fontSizeUp();
			}.bindAsEventListener(that));			
		});
	},

	fontSizeUp: function() {
		switch(this.getActiveStyleSheet()) {
			case 'A+':
				this.setActiveStyleSheet('A++');
				break;
			case 'A++':
				break;
			default:
				this.setActiveStyleSheet('A+');
				break;
		}
	},

	fontSizeDown: function() {
		switch(this.getActiveStyleSheet()){
			case 'A+':
				this.setActiveStyleSheet('A');
				break;
			case 'A++':
				this.setActiveStyleSheet('A+');
				break;
		}
	},

	setActiveStyleSheet: function(title) {
		var i, a;
		for(i = 0; (a = document.getElementsByTagName("link")[i]); i++) {
			if (/\bstylesheet\b/.test(a.rel) && a.title) {
				a.disabled = true; /* always set true first for IE7 */
				if (a.title == title) {
					a.disabled = false;
				}
			}
		}

		switch(title) {
			case 'A+':
				$$('p.pagetools-fontsize').each(function(elm){
					$(elm).removeClassName('smallest').removeClassName('largest');
				});
				break;
			case 'A++':
				$$('p.pagetools-fontsize').each(function(elm){
					$(elm).removeClassName('smallest').addClassName('largest');
				});
				break;
			default:
				$$('p.pagetools-fontsize').each(function(elm){
					$(elm).addClassName('smallest').removeClassName('largest');
				});
				break;
		}
		
		new Cookie('text-size').write(title, 365 * 24 * 3600);
		
	},

	getActiveStyleSheet: function() {
		var i, a;
		for (i = 0; (a = document.getElementsByTagName("link")[i]); i++) {
			if (/\bstylesheet\b/.test(a.rel) && a.title && !a.disabled) {
				return a.title;
			}
		}
		return this.getDefaultStyleSheet();
	},

	getDefaultStyleSheet: function() {
		return 'A';
	}
}

Cookie = Class.create();

Cookie.prototype = {

	initialize: function(name) {
		this.name = name;
	},
	
	write: function(value, seconds) {
		
		if (seconds) {
			var date = new Date();
			date.setTime(date.getTime() + (1000 * seconds));
			var expires = "; expires=" + date.toGMTString();
		} else {
			var expires = "";
		}
		document.cookie = this.name + "=" + value + expires + "; path=/";
	},

	read: function(defaultValue) {
		var parts = document.cookie.split(';');
		var reg   = new RegExp('^\\s*' + this.name + '=(.*)$');
		var part;
		while(part = parts.pop()) {
			var matches = part.match(reg);
			if (matches) {
				return matches[1];
			}
		}
		return defaultValue;
	},
	
	remove: function() {
		this.write('', -86400);
	}
}
