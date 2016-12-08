/****************************** Cummins' new web appearance script *****/
/****************************** Copyright (c) 2007-2010 Cummins AG *****/
/***********************************************************************/
/****************************************** compability component: *****/
/* this version of AutoCompleteLayer is deprecated due to a new    *****/
/* one with a better ajax orientated interface using client side   *****/
/* xsl transformation to create html from the xml server response. *****/
/*                                                                 *****/
/* this file shouldn't be included per default but could provide   *****/
/* help if custom code relies on these deprecated componentes.     *****/
/***********************************************************************/
/************************************** author virtual identity AG *****/

var AutoCompleteLayer = Class.create();

AutoCompleteLayer.prototype = Object.extend(new Layer, {
	
	initialize: function(input) {
		
		if(Info.browser.isIEpre6) return;

		this.input     = input;
		this.form      = this.input.up('form');
		this.content   = null;
		this.lastValue = this.input.value;
		
		this.node = document.createElement('div');
		this.node.className = 'autocomplete-wrapper';
		
		input.insert({after: $(this.node)})
		input.setAttribute('autocomplete', 'off');
		
		this.node.observe('keydown', function(e) {
			this.onkeydown(e);
		}.bindAsEventListener(this));

		input.observe('keydown', function(e) {
			this.onkeydown(e);
		}.bindAsEventListener(this));

		input.observe('keyup', function(e) {
			if (this.input.value != this.lastValue) { // input value changed
				this.lastValue = this.input.value;
				if (this.input.value) {
					this.load();
				} else {
					this.close();
				}
			}
		}.bindAsEventListener(this));
	},

	onkeydown: function(e) {
		if(Info.browser.isOpera) return; // no key handling, since preventing default actions seems to be impossible in Opera

		if(this.isOpen) {
			var activeListElement = this.node.select('ul li.active');
			activeListElement = activeListElement.length ? activeListElement[0]: null;
			var newListElement = null;

			switch (e.keyCode) {
				case Event.KEY_UP:
					if (activeListElement && activeListElement.previous()) {
						newListElement = activeListElement.previous();
					}
					Event.stop(e);
					break;
				
				case Event.KEY_DOWN:
					if (activeListElement && activeListElement.next()) {
						newListElement = activeListElement.next();
					} else if (!activeListElement) {
						newListElement = this.node.select('ul li').first();
					}
					Event.stop(e);
					break;
					
				case Event.KEY_HOME:
					newListElement = this.node.select('ul li').first();
					Event.stop(e);
					break;

				case Event.KEY_END:
					newListElement = this.node.select('ul li').last();
					Event.stop(e);
					break;
					
				case Event.KEY_ESC:
					this.input.focus();
					break;
			}
			
			if (newListElement) {
				$(newListElement).down('a').focus();
				this.setInput(newListElement.down('a').innerHTML);
			}
		} else {
			if (e.keyCode == Event.KEY_DOWN) {
				if (this.input.value) {
					this.load();
				}
			}
		}
	},
	
	getSuggestions: function() {
		this.load();
	},
	
	display: function() {
		var that = this;
		if(!this.content) {
			this.close();
		} else {
			this.node.update(this.content);
			var ul = this.node.down('ul');
			if (ul) {
				var links = $A(ul.getElementsByTagName('a'));
				// add events to links to submit form with selected value
				links.each(function(a) {
					a = $(a);

					a.observe('focus', function(e) {
						$(this.parentNode).addClassName('active');
					}.bindAsEventListener(a));

					a.observe('blur', function(e) {
						$(this.parentNode).removeClassName('active');
					}.bindAsEventListener(a));

					a.observe('mouseover', function(e) {
						a.focus();
					}.bindAsEventListener(a));

					a.observe('mousemove', function(e) {
						a.focus();
					}.bindAsEventListener(a));

					a.observe('click', function(e) {
						that.close();
						that.setInput(a.innerHTML);
						that.form.submit();
					}.bindAsEventListener(a));
				});
			}
			this.open();
		}
	},
	
	setInput: function(value) {
		this.input.value = value;
		this.lastValue = value;
	},
	
	show: function() {
		this.node.setStyle({'display':'block'});
	},

	hide: function() {
		this.node.setStyle({'display':'none'});
	},

	load: function() {
		alert('Implementation Error: AutoComplete.load is missing');
	}
});

/*

	example code for a implementation of the load method

*/

// overwrites the same method in module.autocomplete.js

AutoCompleteLayer.prototype.load = function() {
	var value = this.input.value;

	// loading the suggestbox xhtml and save it in this.content
	
	if (/xx/.match(value)) { // for testing: no list when value contains xx
		this.content = '';
	} else {
		this.content = ('<ul>'
			+'	<li><a href="javascript:void(0);">%s adipiscing</a></li>'
			+'	<li><a href="javascript:void(0);">%s consectetuer</a></li>'
			+'	<li><a href="javascript:void(0);">%s dolor sit amet</a></li>'
			+'	<li><a href="javascript:void(0);">%s donec</a></li>'
			+'	<li><a href="javascript:void(0);">%s etiam</a></li>'
			+'	<li><a href="javascript:void(0);">%s fusce</a></li>'
			+'	<li><a href="javascript:void(0);">%s imperdiet</a></li>'
			+'	<li><a href="javascript:void(0);">%s lacus</a></li>'
			+'	<li><a href="javascript:void(0);">%s massa</a></li>'
			+'	<li><a href="javascript:void(0);">%s nonummy</a></li>'
			+'	<li><a href="javascript:void(0);">%s phasellus</a></li>'
			+'	<li><a href="javascript:void(0);">%s purus</a></li>'
			+'	<li><a href="javascript:void(0);">%s quisque</a></li>'
			+'	<li><a href="javascript:void(0);">%s suspendisse</a></li>'
			+'	<li><a href="javascript:void(0);">%s tempor</a></li>'
			+'	<li><a href="javascript:void(0);">%s torquent</a></li>'
			+'	<li><a href="javascript:void(0);">%s velit</a></li>'
			+'	<li><a href="javascript:void(0);">%s venenatis</a></li>'
			+'	<li><a href="javascript:void(0);">%s volutpat</a></li>'
			+'</ul>').replace(/%s/g, value.substr(0,1).toUpperCase() + value.substr(1))
		;
	}
		
	// display the filter
	// DEV-NOTE: when using ajax this method should be used as a callback

	var that = this;
	window.setTimeout(function() { that.display(); }, 400);
}
