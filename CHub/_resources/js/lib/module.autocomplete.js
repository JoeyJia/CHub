var AutoCompleteXmlLoader = Class.create();

AutoCompleteXmlLoader.prototype = Object.extend(new XmlLoader, {

	initialize: function(layer) {
		this.layer = layer;
	},
	
	display: function() {
		var layer = this.layer;
		this.layer.node.innerHTML = '<span></span>';
		this.insertXslTransformation($(this.layer.node.firstChild));
		var ul = $(this.layer.node.down('ul'));
		if (ul) { // a list exists and the suggest box will be displayed
			var links = $A(ul.getElementsByTagName('a'));
			links.each(function(a) {
				a = $(a);

				a.observe('focus', function(e) {
					$(this.parentNode).addClassName('active');
				}.bindAsEventListener(a));

				a.observe('blur', function(e) {
					$(this.parentNode).removeClassName('active');
				}.bindAsEventListener(a));

				window.setTimeout(function() { // delayed call to prevent focussing instantly after opening
					a.observe('mouseover', function(e) {
						a.focus();
					}.bindAsEventListener(a));
				}, 200);

				a.observe('mousemove', function(e) {
					a.focus();
				}.bindAsEventListener(a));

				a.observe('click', function(e) {
					layer.close();
					layer.setInput(a.innerHTML);
					// layer.form.submit();
				}.bindAsEventListener(a));
			});
			this.layer.open();
		} else {
			this.layer.close();
		}
	}
});

var AutoCompleteLayer = Class.create();

AutoCompleteLayer.prototype = Object.extend(new Layer, {
	
	initialize: function(input) {
		
		if(Info.browser.isIEpre6) return;

		this.input     = input;
		this.form      = this.input.up('form');
		this.content   = null;
		this.lastValue = this.input.value;
		this.xmlLoader = new AutoCompleteXmlLoader(this);
		
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
					this.xmlLoader.load();
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
					this.close();
					Event.stop(e);
					break;
			}
			
			if (newListElement) {
				$(newListElement).down('a').focus();
				this.setInput(newListElement.down('a').innerHTML);
			}
		} else {
			if (e.keyCode == Event.KEY_DOWN) {
				if (this.input.value) {
					this.xmlLoader.load();
				}
			}
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
	}

});
