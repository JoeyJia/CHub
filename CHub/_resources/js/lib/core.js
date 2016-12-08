/****************************** Cummins' new web appearance script *****/
/****************************** Copyright (c) 2007-2010 Cummins AG *****/
/***********************************************************************/
/************************************************* javascript core *****/
/***********************************************************************/
/************************************** author virtual identity AG *****/
/* $LastChangedDate: 2010-02-02 13:16:56 +0100 (Di, 02 Feb 2010) $ *****/

// constants

var USE_FLASH_IN_HEADER = false; // could be overwritten in template

var WEBKIT_STYLESHEET_REFERENCE = RESOURCES_PATH + "css/styles-webkit.css";
var MACOS_STYLESHEET_REFERENCE  = RESOURCES_PATH + "css/styles-macos.css";
var SIFR_SLAB_PATH              = RESOURCES_PATH + "sifr/Cumminsslab.swf";

var LINK_REL_REGEX = /^jump-to-(.+)$/;

// global vars

var SIFR_IS_POSSIBLE = (typeof sIFR == "function" && !sIFR.UA.bIsIEMac && !sIFR.UA.bIsOpera && (!sIFR.UA.bIsWebKit || sIFR.UA.nWebKitVersion >= 100));

var zone     = {};   // global hash for standard zones
var pageType = null; // page type, set in initGlobals()

// serve special styles for Webkit and Mac OS

if (Info.browser.isWebkit) { document.write ('<link rel="stylesheet" type="text/css" media="screen,projection" href="' + WEBKIT_STYLESHEET_REFERENCE + '" />'); }
if (Info.os.isMac)         { document.write ('<link rel="stylesheet" type="text/css" media="screen,projection" href="' + MACOS_STYLESHEET_REFERENCE  + '" />'); }

/********************************************************************/
/* START: layout initalisation                                      */

var nwa = {
	version: "1.4.1"
};

// inits global vars as std zones and page type

function initGlobals() {
	var ptReg = /^page-type-/;
	pageType = $(document.body).classNames().find(function(className) {
		return ptReg.test(className);
	}).replace(ptReg, "");

	$A(["content", "header"]).each(function(zoneId) {
		try {
			var _zone = $(zoneId + "-zone");
			if (!_zone) {
				throw ("Implementation Exception: Zone " + zoneId + " is missing.");
			}
			zone[zoneId] = _zone;
		} catch (e) {
			alert(e);
			return false;
		}
	});
	return true;
}

// provides min-width/max-width for IE < 7

function initLayout_IEPre7() {

	var innerWidth = document.documentElement.clientWidth;
	if ($("toolbar-zone")) {
		if (innerWidth <= 996 && innerWidth > 982) {
			$("toolbar-zone").setStyle({"width": (innerWidth - 56) + "px"});
		} else if (innerWidth > 996) {
			$("toolbar-zone").setStyle({"width": "940px"});
		} else {
			$("toolbar-zone").setStyle({"width": "926px"});
		}
	}

	if (innerWidth > 960) {
		$("content-zone").style.width = "auto";
	} else {
		$("content-zone").style.width = "960px";
	}

	if ($("headervisual-zone") && $("fluid-zone") && pageType != "1" && pageType != "entry") {
		var realHeaderWidth = $("headervisual-zone").getWidth() + $("fluid-zone").getWidth();
		if (innerWidth <= realHeaderWidth) {
			var fluidWidth = $("fluid-zone").getWidth();
			var newWidth = innerWidth;
			newWidth = (newWidth - fluidWidth < 364) ? fluidWidth + 364 : newWidth;
			zone.header.setStyle({"width": newWidth + "px"});
		} else {
			zone.header.setStyle({"width": realHeaderWidth + "px"});
		}
	}
}

// inits sifr

function initLayout_sifr() {
	if (typeof sIFR != "function" || Info.browser.isOpera || Info.os.isLinux) { return; }

	sIFR.replaceElement(named({sSelector:"div.link-list span.sifr", sFlashSrc: SIFR_SLAB_PATH, sColor:"#666666", sHoverColor:"#990000", sWmode:"transparent"}));
	sIFR.replaceElement(named({sSelector:"div.sifr h3", sFlashSrc: SIFR_SLAB_PATH, sColor:"#333333", sWmode:"transparent"}));
	sIFR.replaceElement(named({sSelector:"div.sifr-h1 h1", sFlashSrc: SIFR_SLAB_PATH, sColor:"#333333", sWmode:"transparent"}));
	sIFR.replaceElement(named({sSelector:"div.sifr-h2 h2", sFlashSrc: SIFR_SLAB_PATH, sColor:"#999999", sWmode:"transparent"}));
	sIFR.replaceElement(named({sSelector:"div.sifr-header h1", sFlashSrc: SIFR_SLAB_PATH, sColor:"#ffffff", sWmode:"transparent"}));
	sIFR.replaceElement(named({sSelector:"div.sifr-header h2", sFlashSrc: SIFR_SLAB_PATH, sColor:"#ffffff", sWmode:"transparent"}));
	sIFR.replaceElement(named({sSelector:"div.sifr-header h3", sFlashSrc: SIFR_SLAB_PATH, sColor:"#ffffff", sHoverColor:"#990000", sWmode:"transparent"}));
}

nwa.SifrManager = {
	painted: false,
	paint: function(cSelector) {
		if(!SIFR_IS_POSSIBLE || !USE_SIFR) { return false; }
		if (this.painted) {
			if(cSelector){
				sIFR.rollback(named({sSelector:cSelector+" *"}));
			}else{
				sIFR.rollback();
			}
		}
		initLayout_sifr();
		this.painted = true;
	}
};

// replaces search button and generic buttons by a-elements to provide css styled buttons

function initLayout_buttons() {
	$$("button#site-search-button", "button.generic").each(function(elt) {
		if (!elt.form.id) {
			elt.form.id = Helper.getUniqueId();
		}

		linkAsButton = document.createElement('a');
		linkAsButton.className = (elt.id == "site-search-button") ? "search-button" : "generic-button";
		if ($(elt).hasClassName('inactive')) {
			$(linkAsButton).addClassName('inactive-generic-button');
			linkAsButton.observe('click', function(e){
				if($(this).hasClassName('inactive-generic-button')){
					e.stop();
				}
			 });
		}
		linkAsButton.href = "javascript:submitForm('" + elt.form.id + "');";

		if (elt.id == "site-search-button") {
			linkAsButton.innerHTML = $(elt).innerHTML;
		} else {
			linkAsButton.innerHTML = '<span><span>' + $(elt).innerHTML + '</span></span>';
		}

		$(elt).parentNode.replaceChild($(linkAsButton), $(elt));

	});
}
/* END: layout initalisation                                        */
/********************************************************************/
/* START: functional initalisation                                  */

function init_contentLayers() {
	if ($("toolbar-nav")) {
		$A($("toolbar-nav").getElementsByTagName("a")).each(
			function(trigger) {
				trigger = $(trigger);
				if (LINK_REL_REGEX.test(trigger.rel)) { // layer link
					var id = trigger.rel.replace(LINK_REL_REGEX, "$1");
					var node = $("toolbar-layer-" + id);
					new ContentLayer(node, trigger);
				} else { // standard link, probably external
					trigger.observe("click", function() {
						HeaderAnimation.animate = false;
						Layer.closeCurrent();
						HeaderAnimation.animate = true;
					}.bindAsEventListener(this));
				}
			}.bind(this)
		);
	}
}

function init_siteIdLayer(){
	if (Info.browser.isIEpre6) { return; }
	if ($("site-id-layer")) {
		trigger = $($("site-id").getElementsByTagName("a")[0]);
		new SiteIdLayer($("site-id-layer"), trigger);
	}
}

function init_siteExplorer() {
	if ($("site-explorer-layer")) {
		trigger = $($("sitemap-link").getElementsByTagName("a")[0]);
		SiteExplorer.layer = new SiteExplorerLayer($("site-explorer-layer"), trigger);
	}
}

function init_logoLinking(){
	var $logo = $('logo');
	if($logo) init_contentLayer2($logo);
}

function init_shareLayer(){
	var sharetrigger = [];
	if ($("pagetools-zone")) {
		sharetrigger = sharetrigger.concat($("pagetools-zone").select("a.share-trigger"))
	}
	
	if ($("pagetools-footer-zone")) {
		sharetrigger = sharetrigger.concat($("pagetools-footer-zone").select("a.share-trigger"))
	}
	
	sharetrigger.each(
		function(trigger) {
			trigger = $(trigger);
			if (LINK_REL_REGEX.test(trigger.rel)) { // layer link
				var id = trigger.rel.replace(LINK_REL_REGEX, "$1");
				var node = $("simple-layer-" + id);
				new ShareLayer(node, trigger);
			}
		}.bind(this)
	);
}

function init_contextLayer(){
	var contexttrigger = [];
	if ($("content-zone")) {
		contexttrigger = contexttrigger.concat($("content-zone").select("a.context-trigger"))
	}
	
	contexttrigger.each(
		function(trigger) {
			trigger = $(trigger);
			if (LINK_REL_REGEX.test(trigger.rel)) { // layer link
				var id = trigger.rel.replace(LINK_REL_REGEX, "$1");
				var node = $("context-layer-" + id);
				new ContextLayer(node, trigger);
			}
		}.bind(this)
	);
}

function init_countryLinks() {
	if(Info.browser.isIEpre6) { return; }
	var links = [];
	if ($("site-id-layer")) {
		links = links.concat($("site-id-layer").select("div.toolbar-content a"));
	}
	if ($("lightbox-layer-logo")) {
		links = links.concat($("lightbox-layer-logo").select("ul.countries a"));
	}
	
	if(links.length){
		//initalize list element hovers
		links.filter(function(link) {
			return !$(link).hasClassName('c');
		}).each(function(link) {
			link.observe("mouseover", function() {
				$(link).up("li").addClassName("hover");
			});
			link.observe("mouseout", function() {
				$(link).up("li").removeClassName("hover");
			});
		});

		// workaround for missing adjacent sibling combinator
		// ("ul.countries li a.c:hover + a" and "ul.countries li a.worldwide:hover + a") and "ul.countries li a.mobile:hover + a") support in IE6 and Safari3
		if(Info.browser.isIEpre7 || Info.browser.isSafari3) {
			links.filter(function(link) {
				return $(link).hasClassName("c") || $(link).hasClassName("worldwide") || $(link).hasClassName("mobile");
			}).each(function(link) {
				link.observe("mouseover", function() {
					$(link).next("a").addClassName("hover");
				});
				link.observe("mouseout", function() {
					$(link).next("a").removeClassName("hover");
				});
			});
		}
	}
}

/**
 * initialize dynamic lightbox-layer with opaque curtain
 * 1st usage:
 * - use <a class="content-layer-2-trigger">
 *   to trigger the lightbox
 * - use as next (or next of parent) element <div class="content-layer-2"> 
 *   to place the lightbox content
 * 2nd usage:
 * - use <a class="content-layer-2-trigger" rel="my-id-somewhere"> 
 *   to trigger the lightbox
 * - define an element <div class="content-layer-2" id="my-id-somewhere">
 *   to place the lightbox somewhere in the html structure you like
 * 
 * @author astirn
 * @see module.lightbox-layer.js
 */
function init_contentLayer2(_context) {
	var $context = _context || $('content-zone');
	if ($context && typeof LightboxLayer == 'function') {
		$context.select('a.content-layer-2-trigger').each(
			function(trigger) {
				trigger = $(trigger);
				if (trigger.next() && trigger.next('div.content-layer-2')) {
					var node = $(trigger.next());
				}
				if (trigger.up().next() && trigger.up().next('div.content-layer-2')) {
					var node = $(trigger.up().next());
				}
				if (trigger.rel && $(trigger.rel)) {
					var node = $(trigger.rel);
				}
				if (node) {
					new LightboxLayer(node, trigger);
				}
			}.bind(this)
		);
	}
}
/* END: functional initalisation                                    */
/********************************************************************/
/* START: "abstract" layer - do not instantiate                     */

var Layer = Class.create();

Layer.current = null;
Layer.toggle = false;

Layer.initialize = function() {
	var closeEvt = Info.browser.isFirefox && Info.os.isMac ? 'mousedown' : 'mouseup';
	Event.observe(document, closeEvt, function(e) {
		if (Layer.current) {
			var element   = $(Event.element(e));
			var layerNode = Layer.current.node;
			if (element != layerNode && !element.descendantOf(layerNode)) {
				Layer.closeCurrent();
			}
		}
	});

	var escapeKeyEvt = Info.browser.isWebkit ? 'keydown' : 'keypress';

	Event.observe(document, escapeKeyEvt, function(e) {
		if (Layer.current) {
			var code = e.keyCode;
			if (code == Event.KEY_ESC) {
				Layer.closeCurrent();
				Event.stop(e);
			} else {
				if (Layer.current.onkeydown) {
					Layer.current.onkeydown(e);
				}
			}
		}
	});
}

Layer.closeCurrent = function(newLayer) {
	if (Layer.current) {
		return Layer.current.close(newLayer);
		Layer.current = null;
	}
	return true; /* nothing to close, but ok */
}

Layer.prototype = {

	initialize: function() {
	},

	initSuper: function(node, trigger) {
		this.node   = node;
		this.isOpen = false;
		this.trigger = trigger || null;

		if (this.trigger) {
			this.trigger.observe("click", function(e) {
				this.toggle();
				nwa.SifrManager.paint('.lightbox-layer');
				Event.stop(e);
			}.bindAsEventListener(this));
			var closeEvt = Info.browser.isFirefox && Info.os.isMac ? 'mousedown' : 'mouseup';
			this.trigger.observe(closeEvt, function(e) { Event.stop(e); });
		}

		if (Info.browser.isIEpre7) {
			this.iframeLining = new IframeLining(this.node);
			this.correctIframe();
		}

	},

	open: function() {
		Layer.toggle = !!Layer.current;
		if (Layer.closeCurrent(this)) {
			if (this.beforeOpen()) {
				this.show();
				if (this.iframeLining) {
					this.iframeLining.show();
				}
				this.isOpen = true;
				Layer.current = this;
				Layer.toggle = false;
				this.afterOpen();
				return true;
			}
		}

		return false;
	},

	close: function(newLayer) {
		if (this.beforeClose(newLayer)) {
			if (this.iframeLining) {
				this.iframeLining.hide();
			}
			this.hide(newLayer);
			this.isOpen = false;
			Layer.current = null;
			this.afterClose(newLayer);
			return true;
		}
		return false;
	},

	toggle: function() {
		if (this.isOpen) {
			this.close();
		} else {
			this.open();
		}
	},

	afterClose: function() {},

	afterOpen: function() {},

	beforeClose: function() {
		return true;
	},

	beforeOpen: function() {
		return true;
	},

	correctIframe: function() {},

	hide: function(newLayer) {},

	show: function() {},

	superSetOffset: function(offset) {
		this.setOffset(offset);
		if (this.iframeLining) {
			this.iframeLining.refresh();
		}
	},

	setOffset: function(offset) {}

}

/* END: "abstract" layer                                            */
/********************************************************************/
/* START: content layer e. g. contact layer                         */

var ContentLayer = Class.create();

ContentLayer.prototype = Object.extend(new Layer, {

	initialize: function(node, trigger) {
		this.initSuper(node, trigger);

		var closeButton = Helper.getCloseButton(this.node);

		if (!closeButton) alert("Implementation Error: no close button found");

		closeButton.observe("click",
			function() {
				this.close();
			}.bindAsEventListener(this)
		);
	},

	afterClose: function(newLayer) {
		if (!Layer.toggle) {
			HeaderAnimation.augment();
		}
	},

	afterOpen: function() {
		HeaderAnimation.diminish();
	},

	beforeClose: function() {
		HeaderAnimation.unregisterLayer();
		return true;
	},

	beforeOpen: function() {
		HeaderAnimation.registerLayer(this);
		return true;
	},

	hide: function() {
		this.node.removeClassName("active-layer");
		this.trigger.up().removeClassName("active");
	},

	show: function() {
		this.node.addClassName("active-layer");
		this.trigger.up().addClassName("active");
		this.trigger.addClassName("clicked"); // avoids hover effect (only for the first time)
		this.trigger.observe("mouseout",
			function(e) {
				var elm = Event.findElement(e, "a");
				elm.removeClassName("clicked");
				elm.stopObserving("mouseout");
			}
		);
	},

	setOffset:function(offset) {
		this.node.style.top = offset + "px";
	}

});

/* END: content layer                                               */
/********************************************************************/
/* START: site id                                                   */

var SiteIdLayer = Class.create();

SiteIdLayer.prototype = Object.extend(new Layer, {

	initialize: function(node, trigger) {
		this.initSuper(node, trigger);
		var closeButton = Helper.getCloseButton(this.node);
		closeButton.observe("click", function(){this.close();}.bindAsEventListener(this));
	},

	afterClose: function(newLayer) {
		if (!Layer.toggle) {
			HeaderAnimation.augment();
		}
	},

	afterOpen: function() {
		HeaderAnimation.diminish();
	},

	beforeClose: function() {
		HeaderAnimation.unregisterLayer();
		return true;
	},

	beforeOpen: function() {
		HeaderAnimation.registerLayer(this);
		return true;
	},

	correctIframe: function() {
		this.iframeLining.correction.left = -1;
		this.iframeLining.correction.top = -1;
	},

	hide: function() {
		$("site-id-wrapper").removeClassName("active");
	},

	show: function() {
		$("site-id-wrapper").addClassName("active");
		this.trigger.addClassName("clicked"); // avoids hover effect (only for the first time)
		this.trigger.observe("mouseout",
			function(e) {
				this.trigger.removeClassName("clicked");
				this.trigger.stopObserving("mouseout");
			}.bindAsEventListener(this)
		);
	}

});

/* END: site id                                                     */
/********************************************************************/
/* START: site explorer                                             */

var SiteExplorer = Class.create();

SiteExplorer.getContent = function() {
	// overwrite this to implement a custom method
	alert("Implementation Error: SiteExplorer.getContent is missing");
}

SiteExplorer.registerEvents = function(contentNode) {
	$A(contentNode.getElementsByTagName("a")).each(function(link) {
		link = $(link);
		if (link.hasClassName("page")) {
			link.observe("click", function(e) {
				SiteExplorer.followLink(link);
			});
		} else {
			link.observe("click", function(e) {
				SiteExplorer.toggleSubtree(link);
			});
		}
	});
}

SiteExplorer.toggleSubtree = function(node) {
	if (node.up().hasClassName("expanded")) {
		SiteExplorer.collapseSubtree(node);
	} else {
		SiteExplorer.expandSubtree(node);
	}
}

SiteExplorer.followLink = function(linkNode) {
	// not implemented
}

SiteExplorer.expandSubtree = function(linkNode) {
	// not implemented
}

SiteExplorer.collapseSubtree = function(linkNode) {
	// not implemented
}

/* END: site explorer                                               */
/********************************************************************/
/* START: site explorer layer                                       */
var SiteExplorerLayer = Class.create();

SiteExplorerLayer.prototype = Object.extend(new Layer, {

	initialize: function(node, trigger) {
		this.initSuper(node, trigger);

		var closeButton = Helper.getCloseButton(this.node);

		closeButton.observe("click",
			function() {
				this.close();
			}.bindAsEventListener(this)
		);

		this.content     = null;
		this.contentNode = $(document.createElement("div"));
		this.node.appendChild(this.contentNode);
	},

	afterClose: function(newLayer) {
		if (!Layer.toggle) {
			HeaderAnimation.augment();
		}
	},

	afterOpen: function() {
		if (Info.browser.isIE) { // correct float bug in all IE versions
			Helper.getCloseButton(this.node).setStyle(
				{
					position: "absolute",
					left: (this.contentNode.getDimensions().width - 20) + "px"
				}
			);
		}
		HeaderAnimation.diminish();
	},

	beforeClose: function() {
		HeaderAnimation.unregisterLayer();
		return true;
	},

	beforeOpen: function() {
		this.getContent();
		HeaderAnimation.registerLayer(this);
		return true;
	},

	hide: function() {
		$("site-explorer").removeClassName("active");
	},

	show: function() {
		$("site-explorer").addClassName("active");
		this.trigger.addClassName("clicked"); // avoids hover effect (only for the first time)
		this.trigger.observe("mouseout",
			function(e) {
				this.trigger.removeClassName("clicked");
				this.trigger.stopObserving("mouseout");
			}.bindAsEventListener(this)
		);
	},

	getContent: function() {
		if (!this.content) {
			this.content = SiteExplorer.getContent();
			this.contentNode.innerHTML = this.content;
			SiteExplorer.registerEvents(this.contentNode);
		}
	}

});

/* END: site explorer layer                                         */
/********************************************************************/
/* START: simple layer                                              */

var SimpleLayer = Class.create();

SimpleLayer.prototype = Object.extend(new Layer, {

	initialize: function(node, trigger) {
		this.initSuper(node, trigger);

		this.initCloseButton();
	},

	initCloseButton: function(){
		var closeButton = Helper.getCloseButton(this.node);

		if (!closeButton) alert("Implementation Error: no close button found");

		closeButton.observe("click",
			function() {
				this.close();
			}.bindAsEventListener(this)
		);
	},
	
	hide: function() {
		this.node.removeClassName("active-layer");
		this.trigger.up().removeClassName("active");
	},

	show: function() {
		this.node.addClassName("active-layer");
		this.trigger.up().addClassName("active");
	}
	
});

/* END: simple layer                                                */
/********************************************************************/
/* START: context layer                                             */

var ContextLayer = Class.create(SimpleLayer, {

	initialize: function(node, trigger) {
		this.initSuper(node, trigger);
		this.initCloseButton();
	},

	getPosition: function(){
		var pos = $(this.trigger).cumulativeOffset();
		var dim = this.node.getDimensions();
		return {
			left: (pos[0] + $(this.trigger).getWidth() - dim.width - 1) + 'px', // subtract white border width
			top: (pos[1] + $(this.trigger).getHeight()) + 'px'
		}
	},
	
	beforeOpen: function(){
		var pos = this.getPosition();
		this.node.setStyle(pos);
		return true;
	}	
});

/* END: context layer                                               */
/********************************************************************/
/* START: share layer                                              */

var ShareLayer = Class.create(SimpleLayer, {

	initialize: function(node, trigger) {
		this.initSuper(node, trigger);
		this.initCloseButton();
	},

	getPosition: function(){
		if($(this.trigger).up('div#footer-zone')){

			var dim = this.node.getDimensions();
			
			var pos = $(this.trigger).cumulativeOffset();
			return {
				left: (pos[0] - dim.width - 5) + 'px',
				top: (pos[1] - dim.height + $(this.trigger).getHeight()) + 'px'
			}
		} else {
			var pos = $('pagetools-zone').cumulativeOffset();
			return {
				left: (pos[0] - 1) + 'px', // subtract white border width
				top: (pos[1] + $('pagetools-zone').getHeight() + 7) + 'px'
			}
		}
	},
	
	beforeOpen: function(){
		var pos = this.getPosition();
		this.node.setStyle(pos);
		return true;
	}	
});

/* END: simple layer                                                */
/********************************************************************/
/* START: header zone animation                                     */

var HeaderAnimation = Class.create();

HeaderAnimation.initialize = function() {
	this.layer         = null;
	this.slide         = {}
	this.animate       = true;
	this.augmented     = true;
	this.listenerQueue = new ListenerQueue('');

	if (pageType == "1" || pageType == "2" || pageType == "entry") {
		if (USE_FLASH_IN_HEADER && !Info.hasTransparencySupport) {
			// deactivate animation on browsers that have problems with wmode=transparent */
			this.diminishable = false;
		} else {
			this.diminishable = true;
		}
	} else {
		this.diminishable = false;
	}

	this.diminish      = (this.diminishable) ? this.diminish_393 : function() {};
	this.augment       = (this.diminishable) ? this.augment_393 : function() {};

	this.toolbarNode   = $("toolbar-nav");
	this.toolbarHeight = (this.toolbarNode) ? this.toolbarNode.up().getHeight() : 0;
}

HeaderAnimation.augment_393 = function() {
	if (!this.augmented) {
		this.listenerQueue.fire('augmentBegin');
		if (this.animate) {
			this._toggleAnimated([154, 174, 204, 244, 284, 324, 354, 385, 393]);
		} else {
			this._toggle(393);
		}
		this.augmented = true;
	}
}

HeaderAnimation.diminish_393 = function() {
	if (this.augmented) {
		this.listenerQueue.fire('diminishBegin');
		if (this.animate) {
			this._toggleAnimated([363, 313, 263, 213, 183, 163, 152, 144]);
		} else {
			this._toggle(144);
		}
		this.augmented = false;
		
	}
}

HeaderAnimation.registerLayer = function(layer) {
	this.layer = layer;
	if (this.layer) {
		this.layer.superSetOffset(this.toolbarHeight + Position.cumulativeOffset(this.toolbarNode)[1]);
	}
}

HeaderAnimation.unregisterLayer = function() {
	this.layer = null;
}

HeaderAnimation._toggle = function(offset) {
	this.diminished = !this.diminished;
	this._setOffsets(offset);
	if (this.diminished) {
		this.listenerQueue.fire('diminishDone');
	} else {
		this.listenerQueue.fire('augmentDone');
	}
}

HeaderAnimation._toggleAnimated = function(offsets) {
	this.slide.offsets = offsets;
	this.slide.length = offsets.length;
	this.slide.index = 1;

	this._toggle(this.slide.offsets[0]);
	new PeriodicalExecuter(function(pe) {
		if (this.slide.index >= this.slide.length) {
			this.diminished = !this.diminished;
			if (this.diminished) {
				this.listenerQueue.fire('diminishDone');
			} else {
				this.listenerQueue.fire('augmentDone');
			}
			pe.stop();
		} else {
			this._setOffsets(this.slide.offsets[this.slide.index]);
			this.slide.index++;
		}
	}.bind(this), .06);
}

HeaderAnimation._setOffsets = function(offset) {
	$(document.body).style.backgroundPosition = "0 " + (offset - 393) + "px";
	zone.header.style.height = offset + "px";
	if (this.layer) {
		this.layer.superSetOffset(this.toolbarHeight + 1 + offset);
	}
}

/* END: header zone animation                                       */
/********************************************************************/
/* START: Listener queue                                            */

ListenerQueue = Class.create();

ListenerQueue.prototype = {
	initialize: function(wildcardHandlerName) {
		this._listeners = [];
		this._wildcardHandlerName = wildcardHandlerName;
	},

	add: function(listener) {
		this._listeners.push(listener);
	},

	remove: function(listener) {
		var listeners = this._listeners;
		for (var i = 0; i < listeners.length; i++) {
			if (listeners[i] == listener) {
				listeners.splice(i, 1);
				break;
			}
		}
	},

	fire: function(handlerName, args) {
		var listeners = [].concat(this._listeners);
		for (var i = 0; i < listeners.length; i++) {
			var listener = listeners[i];
			try {
				listener[handlerName].call()
			} catch(e) {
				//if there is no handler with handlerName
				if (this._wildcardHandlerName)listener[this._wildcardHandlerName].call();
			}
		}
	}
}

/* END: Listener queue                                              */
/********************************************************************/
/* START: iframe to hide select elements in IE<7                    */

var IframeLining = Class.create();

IframeLining.prototype = {

	initialize: function(layer) {
		this.layer = layer;
		this.active = false;
		this.correction = {
			width:  0,
			height: 0,
			left:   0,
			top:    0
		} // since cumulativeOffset doesn't include borders

		this.div = $(document.createElement("div"));
		this.div.className = 'iframe-lining';
		this.div.innerHTML = '<iframe src="javascript:false" frameborder="0" style="position:absolute; left:0; top:0; width:100%; height:100%; margin:0; padding:0; background:transparent; filter:alpha(opacity=0);"></iframe>';
		this.hide();
		document.body.appendChild(this.div);
	},

	show: function() {
		this.refresh();
		this.div.show();
		this.active = true;
	},

	refresh: function() {
		var dimension = $(this.layer).getDimensions();
		var position  = Position.cumulativeOffset(this.layer);
		this.div.setStyle(
			{
				height: (dimension.height + this.correction.height) + "px",
				left:   (position[0] + this.correction.left)        + "px",
				top:    (position[1] + this.correction.top)         + "px",
				width:  (dimension.width + this.correction.width)   + "px",
				position: "absolute",
				zIndex:   "1"
			}
		);
	},

	setOffset: function(offset) {
		this.div.style.top = offset + "px";
	},

	hide: function() {
		this.div.hide();
		this.active = false;
	}
}

/* END: iframe to hide select elements in IE<7                      */
/********************************************************************/
/* START: xml loader to insert xml (transformed to html by xsl)     */

var XmlLoader = Class.create();

XmlLoader.prototype = {
	
	initialize: function() {},
	
	insertXslTransformation: function(node) {
		if (document.implementation && document.implementation.createDocument) { // code for std browsers
			xsltProcessor = new XSLTProcessor();
			xsltProcessor.importStylesheet(this.xsl);
			content = xsltProcessor.transformToFragment(this.xml, document);
			node.parentNode.insertBefore(content, node);
			return true;
		} else if (window.ActiveXObject) { // code for IE
			var content = this.xml.transformNode(this.xsl);
			node.insert({after: content});
			return true;
		}
		return false;
	},
	
	load: function() {
		that = this;
		that.loadXml(
			function(xml) {
				that.xml = xml.responseXML;
				that.loadXsl(
					function(xsl) {
						that.xsl = xsl.responseXML;
						that.display();
					}
				);
			}
		);
	},
	
	loadXml: function() {
		// overwrite this to implement a custom method
		alert("Implementation Error: XmlLoader.loadXml is missing");
	},
	
	loadXsl: function() {
		// overwrite this to implement a custom method
		alert("Implementation Error: XmlLoader.loadXsl is missing");
	}
}

/* END: xml loader to insert xml                                    */
/********************************************************************/
/* START: misc. helper                                              */

var Helper = Class.create();

Helper._uniqueIdInt = 0;

Helper.getUniqueId = function() {
	Helper._uniqueIdInt++;
	return "unique-" + Helper._uniqueIdInt;
}

Helper.getCloseButton = function(layer) {
	var closeButton = layer.down("div.close");
	if (!closeButton) { // could be IE<6
		$A(layer.getElementsByTagName("div")).each(
			function(div) {
				if ($(div).hasClassName("close")) {
					closeButton = $(div);
				}
			}
		);
	}
	return closeButton;
}

// a wrapper to show a nicer function call in the status bar

function submitForm(id) {
	$(id).submit();
}

function init_newWindow(domChunk) {
	$$(domChunk).each(function(element) {
		element.observe('click', function(e) {
			window.open(this.href);
			Event.stop(e);
		}.bindAsEventListener(element));
	});
}

/* END: misc. helper                                                */
/********************************************************************/
