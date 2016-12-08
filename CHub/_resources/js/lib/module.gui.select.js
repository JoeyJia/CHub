/****************************** Cummins' new web appearance script *****/
/****************************** Copyright (c) 2007-2010 Cummins AG *****/
/***********************************************************************/
/*********************************************** module gui select *****/
/***********************************************************************/
/************************************** author virtual identity AG *****/
/* $LastChangedDate: 2010-02-02 12:32:49 +0100 (Di, 02 Feb 2010) $ *****/

/********************************************************************/
/* START: functional initalisation                                  */

function init_guiSelect(transformer) {

	// register events for IE<6

	if(Info.browser.isIEpre6) {
		// add onchange handler and abandon initialization
		var selects = document.getElementById('content-zone').getElementsByTagName("select");
		for (var i = 0, l = selects.length; i < l; ++i) {
			var select = selects[i];
			if (select.className.match(/\bgui-select\b/)) {
				select.onchange = function() {
					this.parentNode.parentNode.submit();
				}
			}
		}
		return;
	}
	
	// build and init gui select boxes

	GuiSelect.build($("content-zone").getElementsByTagName("select"), transformer);

	// init mousewheel

	var wheel = new MouseWheelObserver;
	wheel.register(
		function(value) {
			if (Layer.current && Layer.current.onmousescroll && Layer.current.scrollbar) {
				return Layer.current.onmousescroll(value);
			} else {
				return true;
			}
		}
	);
}

/* END: functional initalisation                                    */
/********************************************************************/
/* START: GuiSelect class                                           */

var GuiSelect = Class.create();

GuiSelect.sliderCounter = 0;

GuiSelect.build = function(elements, transformer, openImmediately) {
	transformer = transformer || GuiSelect.defaultTransformer;

	$A(elements).filter(
		function(select) {
			return $(select).hasClassName("gui-select");
		}).each(
		function(select) {
			select = $(select);

			// build html

			var options        = $A(select.getElementsByTagName('option'));
			var index          = select.selectedIndex;
			var titleClassName = (index) ? 'title selected-title' : 'title';
			var text           = transformer.replaceTitle(options[index].text);
			var id             = select.id + '-gui';
			var className      = select.className;
			var wrappedText    = (select.disabled) ? '<span class="a">' + text + '</span>' : '<a href="#">' + text + '</a>';

			var _html = '<div id="' + id + '" class="' + className + '"><p class="' + titleClassName + '">' + wrappedText + '</p>'
				+ '<input type="hidden" name="' + select.name + '" value="' + select.value + '" />'
				+ '<div class="content">';

			if (!select.disabled) {
				var isFirst = true;
				_html += '<ul>';
				options.each(
					function(option) {
						var classes = [];
						if (isFirst) {
							classes.push('first-child');
						}
						if (option.selected) {
							classes.push('selected');
						}
						var classSnippet = (classes.length) ? ' class="' + classes.join(' ') + '"' : '';

						var text = transformer.replaceOption(option.innerHTML);
						_html += '<li' + classSnippet +'><span class="value">' + option.value + '</span><a href="#">' + text + '</a></li>';
						isFirst = false;
					}
				);
				_html += '</ul>';
			}
			_html += '</div></div>';

			// replace html select element with new gui select element

			var wrapper = select.parentNode;
			Element.remove(select);
			wrapper.innerHTML += _html;

			if (select.disabled) {
				// if the select element is disabled, all is done
				return;
			}

			// init layer logic

			// optimized performance for IE6 and Cummins search

			var div        = $(id);
			var form       = $(div.parentNode.parentNode);
			var input      = $(div.childNodes[1]);
			var contentDiv = $(div.childNodes[2]);
			var linkList   = $(contentDiv.firstChild);
			var links      = $A(contentDiv.getElementsByTagName('a'));
			var titleLink  = $(div.firstChild.firstChild);
			this._clicked    = false;

			var that = this;
			var layer   = new GuiSelectLayer(contentDiv, titleLink);

			// add events to title

			titleLink.observe('mousedown', function(e) {
				that._clicked = true;
			});

			titleLink.observe('focus', function(e) {
				if (!that._clicked) {
					layer.open();
				}
			});

			titleLink.observe('mouseup', function(e) {
				that._clicked = false;
			});

			// add events to links to submit form with selected value

			links.each(function(a) {
				a = $(a);

				a.observe('focus', function(e) {
					$(this.parentNode).addClassName('active');
					if (layer != Layer.current) {
						layer.open();
						if (layer.scrollbar) {
							layer.scrollbar.scrollIntoView(a.up('li'));
						}
					}
				}.bindAsEventListener(a));

				a.observe('blur', function(e) {
					$(this.parentNode).removeClassName('active');
				}.bindAsEventListener(a));

				a.observe('mousemove', function(e) {
					if (layer.scrollbar) {
						if (!layer.scrollbar.slider.dragging) {
							a.focus();
						}
					} else {
						a.focus();
					}
				}.bindAsEventListener(a));

				a.observe('click', function(e) {
					if (this.up().hasClassName('selected')) {
						Layer.closeCurrent();
					} else {
						input.value = this.previous('span.value').innerHTML;
						form.submit();
					}
					Event.stop(e);
				}.bindAsEventListener(a));

				// show nicer link on status bar:
				a.href = '?' + input.name + '=' + encodeURIComponent(a.previousSibling.innerHTML);
			});

			// add scrollbar, if necessary

			if (linkList.getHeight() <= contentDiv.getHeight()) { // no slider needed

				contentDiv.style.height = 'auto'; // for IE6

			} else {

				GuiSelect.sliderCounter++;

				linkList.addClassName('has-scrollbar');

				/// add slider gui elements

				var sliderWrapper = document.createElement('div');
				sliderWrapper.className = 'slider-wrapper';
				sliderWrapper.innerHTML = '<div class="arrow-up"></div><div class="slider" id="slider' + GuiSelect.sliderCounter + '"><div class="handle" id="handle' + GuiSelect.sliderCounter + '"></div></div><div class="arrow-down"></div>';
				contentDiv.appendChild(sliderWrapper);

				// init scrollbar and add scrollbar object to layer

				layer.scrollbar = new GuiScrollbar("handle" + GuiSelect.sliderCounter, "slider" + GuiSelect.sliderCounter, layer);

				// add click events

				var elts = $(sliderWrapper).childElements();

				elts[0].observe("click", function() {
					layer.scrollbar.moveUp();
				});
				elts[2].observe("click", function() {
					layer.scrollbar.moveDown();
				});

				// add other events

				var handler = (Info.browser.isIE) ? "activate" : "focus";

				links.each(function(a) {
					$(a).observe(handler, function(e) {
						layer.scrollbar.scrollIntoView($(this).up("li"));
					}.bind(a));
				});

			}

			// open immediately if option is true

			if (openImmediately) {
				layer.open();
			}
		}
	);
}

GuiSelect.defaultTransformer = {
	replaceOption: Prototype.K,
	replaceTitle:  Prototype.K
}

/* END: GuiSelect class                                             */
/********************************************************************/
/* START: layer subclass for gui.select                             */

var GuiSelectLayer = Class.create();

GuiSelectLayer.prototype = Object.extend(new Layer, {

	list: null,
	scrollbar: null,

	initialize: function(node, trigger) {
		this.name = node.up().id.match(/^filter-(.*)-gui$/)[1];
		this.initSuper(node, trigger);
		this.list = node.firstChild;
	},
	
	afterClose: function() {
		this.node.up().removeClassName("active-gui-select");

		var activeFilter = $('active-filter-' + this.name);
		if (activeFilter) {
			activeFilter.show();
			var form = $('filter-form-' + this.name);
			if (form) {
				form.remove();
			}
		}

	},

	beforeOpen: function() {
		this.node.up().addClassName("active-gui-select");
		if (this.scrollbar) {
			this.scrollbar.setValue(0);
		}
		return true;
	},

	hide: function() {
		this.node.removeClassName("active-content");
	},

	onkeydown: function(e) {
		var ITEMS_PER_PAGE = 9;

		if (Info.browser.isOpera) {
			// no key handling, since preventing default actions doesn't work with Opera
			// see: http://www.quirksmode.org/dom/events/keys.html
			return;
		}

		var activeListElement = this.getListElement();

		switch (e.keyCode) {

			case Event.KEY_UP:
				if (activeListElement && activeListElement.previous()) {
					this.setListElement(activeListElement.previous());
				}
				Event.stop(e);
				break;

			case Event.KEY_DOWN:
				if (activeListElement && activeListElement.next()) {
					this.setListElement(activeListElement.next());
				} else if (!activeListElement) {
					this.setListElement(this.node.select('ul li').first());
				}
				Event.stop(e);
				break;

			case Event.KEY_PAGEUP:
				if (activeListElement) {
					if (activeListElement.previous(ITEMS_PER_PAGE - 1)) {
						this.setListElement(activeListElement.previous(ITEMS_PER_PAGE - 1));
					} else {
						this.setListElement(this.node.select('ul li').first());
					}
				} else {
					this.setListElement(this.node.select('ul li').first());
				}
				Event.stop(e);
				break;

			case Event.KEY_PAGEDOWN:
				if (activeListElement) {
					if (activeListElement.next(ITEMS_PER_PAGE - 1)) {
						this.setListElement(activeListElement.next(ITEMS_PER_PAGE - 1));
					} else {
						this.setListElement(this.node.select('ul li').last());
					}
				} else {
					var length = this.node.select('ul li').length;
					this.setListElement(this.node.select('ul li')[Math.min(ITEMS_PER_PAGE, length)]);
				}
				Event.stop(e);
				break;

			case Event.KEY_HOME:
				this.setListElement(this.node.select('ul li').first());
				Event.stop(e);
				break;

			case Event.KEY_END:
				this.setListElement(this.node.select('ul li').last());
				Event.stop(e);
				break;
		}
	},

	getListElement: function() {
		var activeListElement = this.node.select('ul li.active');
		return activeListElement.length ? activeListElement[0]: null;
	},

	setListElement: function(newListElement) {
		if (newListElement) {
			$(newListElement).down('a').focus();
			if (this.scrollbar) {
				this.scrollbar.scrollIntoView(newListElement);
			}
		}
	},

	onmousescroll: function(value) {
		var activeListElement = this.getListElement();
		if (value < 0) {
			this.scrollbar.moveUp();
			if (activeListElement && activeListElement.previous()) {
				this.setListElement(activeListElement.previous());
			}
		} else if (value > 0) {
			this.scrollbar.moveDown();
			if (activeListElement && activeListElement.next()) {
				this.setListElement(activeListElement.next());
			}
		}
		return false;
	},

	scrollTo: function(offsetTop) {
		this.list.style.top = offsetTop + "px";
	},

	show: function() {
		this.node.addClassName("active-content");
	}

});

/* END: layer subclass for gui.select                               */
/********************************************************************/
/* START: scrollbar class                                           */

var GuiScrollbar = Class.create();

GuiScrollbar.prototype = {

	initialize: function(handle, slider, layer) {

		this.outerHeight  = layer.node.getHeight();
		this.innerHeight  = $(layer.node.firstChild).getHeight();
		this.maxScroll    = this.innerHeight - this.outerHeight;
		this.itemHeight   = $(layer.node.firstChild.firstChild.nextSibling).getHeight();
		this.sliderHeight = $(slider).getHeight();

		$(handle).style.height = Math.round(this.sliderHeight * this.outerHeight / this.innerHeight) + "px";

		this.slider = new Control.Slider(handle, slider, {
			axis: 'vertical',
			range: $R(0, this.maxScroll),
			onSlide: function(value) {
				layer.scrollTo(-value);
			},
			onChange: function(value) {
				layer.scrollTo(-value);
			}
		});
	},

	moveDown: function() {
		this.setValue(this.slider.value + this.itemHeight);
	},

	moveUp: function() {
		this.setValue(this.slider.value - this.itemHeight);
	},

	scrollIntoView: function(liNode) {
		var offset = $(liNode).offsetTop;
		var minValue = this.slider.value;
		var maxValue = minValue + this.outerHeight;
		if (offset + this.itemHeight > maxValue) {
			this.setValue(offset + this.itemHeight - this.outerHeight);
		} else if (offset < minValue) {
			this.setValue(offset);
		}
	},

	setValue: function(value) {
		this.slider.setValue(value);
	}

}

/* END: scrollbar class                                             */
/********************************************************************/
