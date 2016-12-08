/****************************** Cummins' new web appearance script *****/
/****************************** Copyright (c) 2007-2010 Cummins AG *****/
/***********************************************************************/
/********************************************* module HeaderVisual *****/
/***********************************************************************/
/************************************** author virtual identity AG *****/
/* $LastChangedDate: 2010-01-12 16:33:21 +0100 (Di, 12 Jan 2010) $ *****/

// swap header visuals

var HeaderVisual = Class.create();

HeaderVisual.initialize = function() {
	if ($("headervisual-zone") && $("headertext-zone")) {
		HeaderVisual.node = $("headervisual-zone");
		HeaderVisual.defaultContentNode = $("headertext-zone").down();

		$A($("content-zone").getElementsByTagName("ul")).findAll(function(elt) {
			return $(elt).hasClassName("js-swap-headervisual");
		}).each(function(elt) {
			$A(elt.getElementsByTagName("li")).each(function(elt) {
				new HeaderVisual($(elt));
			});
		});
	}
}

HeaderVisual.prototype = {
	initialize: function(listItem) {
		this.id     = listItem.id;
		this.source = headerVisualImages[this.id];
		this.loaded = false;
		this.active = false;

		this.initBgImage();
		listItem.observe("mouseover", function() {
			this.show();
		}.bindAsEventListener(this));

		listItem.observe("mouseout", function() {
			this.hide();
		}.bindAsEventListener(this));
	},

	initBgImage: function() {
		this.image  = new Image;

		this.image.onload = function() {
			this.loaded = true;
			if (this.active) {
				this.hideProgress();
				this.show();
			}
		}.bindAsEventListener(this);

		this.image.src = this.source;

		this.imageNode = document.createElement("div");
		this.imageNode.className = "swap-image-container";
		this.imageNode.style.backgroundImage = "url(" + this.source + ")";
		HeaderVisual.node.up().insertBefore(this.imageNode, HeaderVisual.node);
	},

	hide: function() {
		this.active = false;
		HeaderVisual.defaultContentNode.show();
		$("headertext-" + this.id).removeClassName("active");
		if (this.loaded) {
			this.imageNode.style.display = "none";
		} else {
			this.hideProgress();
		}
	},

	hideProgress: function() {
		// not implemented
	},

	show: function() {
		this.active = true;
		HeaderVisual.defaultContentNode.hide();

		/** ff 3.0 mac fix removing headertext-container **/
		if(Info.os.isMac && Info.browser.isMozilla) {
			$$('div.headertext-content').each(function(el) {
				$(el).removeClassName("active");
			});
			$$('div.swap-image-container').each(function(el) {
				$(el).style.display = "none";
			});
		}

		$("headertext-" + this.id).addClassName("active");
		if (this.loaded) {
			this.imageNode.style.display = "block";
		} else {
			this.showProgress();
		}
	},

	showProgress: function() {
		// not implemented
	}
}