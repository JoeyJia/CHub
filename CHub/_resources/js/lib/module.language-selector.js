/****************************** Cummins' new web appearance script *****/
/****************************** Copyright (c) 2007-2010 Cummins AG *****/
/***********************************************************************/
/************************************ module LanguageSelectorLayer *****/
/***********************************************************************/
/************************************** author virtual identity AG *****/
/* $LastChangedDate: 2010-01-12 16:33:21 +0100 (Di, 12 Jan 2010) $ *****/

function init_languageSwitchLayer() {
	if ($("multi-language-switch")) {
		var trigger = $($("language-switch").getElementsByTagName("a")[0]);
		new LanguageSelectorLayer($("language-list"), trigger);
	}
}

var LanguageSelectorLayer = Class.create();

LanguageSelectorLayer.prototype = Object.extend(new Layer, {

	initialize: function(node, trigger) {
		this.initSuper(node, trigger);
	},

	afterClose: function(newLayer) {
		if (!Layer.toggle) {
			HeaderAnimation.augment();
		}
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
		this.iframeLining.correction.left = -12;
	},

	hide: function() {
		$("multi-language-switch").removeClassName("active");
	},

	show: function() {
		$("multi-language-switch").addClassName("active");
		this.trigger.addClassName("clicked"); // avoids hover effect (only for the first time)
		this.trigger.observe("mouseout",
			function(e) {
				this.trigger.removeClassName("clicked");
				this.trigger.stopObserving("mouseout");
			}.bindAsEventListener(this)
		);
	}

});
