/****************************** Cummins' new web appearance script *****/
/****************************** Copyright (c) 2007-2010 Cummins AG *****/
/***********************************************************************/
/******************************************** module LightboxLayer *****/
/***********************************************************************/
/************************************** author virtual identity AG *****/
/* $LastChangedDate: 2010-01-15 17:54:29 +0100 (Fr, 15 Jan 2010) $ *****/

var LightboxLayer = Class.create();

LightboxLayer.prototype = Object.extend(new Layer, {

	initialize: function(node, trigger) {
		this.node = node;
		this.initSuper(node, trigger);
		var closeButton = Helper.getCloseButton(this.node);

		closeButton.observe("click", function(){this.close();}.bindAsEventListener(this));

		trigger.href = 'javascript:void(0);';

		//create the curtain
		new Insertion.Before($('header-zone'), '<div id="lightbox-curtain">&nbsp;</div>');
		this.curtain = $('lightbox-curtain');
		this.resizeCurtain();

		if (Info.browser.isIEpre7) {
			this.iframeLining = new IframeLining(this.curtain);
		}

		//store the listener so it can be accessed by functions add and remove
		this.listener = {'augmentDone' : this.handleOpen.bind(this) };

		//add an event handler to resize the curtain when window is resized
		Event.observe(window, "resize", function(){this.resizeCurtain();}.bindAsEventListener(this));
	},

	open: function() {
		if(HeaderAnimation.diminishable && !HeaderAnimation.augmented) {
			HeaderAnimation.listenerQueue.add(this.listener);
			HeaderAnimation.augment();
		} else {
			Layer.prototype.open.call(this);
		}
	},

	show: function() {
		this.node.setStyle({'display': 'block'});
	},

	hide: function() {
		this.node.setStyle({'display': 'none'});
	},

	beforeOpen: function() {
		//calculate header height
		if(!this.headerHeight) this.headerHeight = $('header-zone').getDimensions().height + $('toolbar-zone').getDimensions().height;

		//position the curtain
		this.curtain.setStyle({'top': '0px'});

		//calculate layer position
		this.node.setStyle({'display': 'block'});
		if(!this.nodeTop) this.nodeTop = parseInt(this.node.getStyle('top'));
		if(!this.nodeLeft) this.nodeLeft = parseInt(this.node.getStyle('left'));
		if(!this.nodeHeight) this.nodeHeight = this.node.getDimensions().height;
		this.node.setStyle({'display': 'none'});

		//check whether layer is higher than current page
		var wrapper = $('footer-position-wrapper');
		this.diff = parseInt(wrapper.getDimensions().height) - this.nodeTop - this.nodeHeight;
		if(this.diff < 0) {
			//resize the content zone
			var layerOccupation = this.nodeHeight + this.nodeTop;
			layerOccupation = layerOccupation - this.headerHeight;

			//difference between layer height and content height
			var diff2 = $('content-zone').getDimensions().height - layerOccupation;
			$('content-zone').setStyle({'height': $('content-zone').getDimensions().height - diff2 + 'px'});
		}
		this.resizeCurtain();

		this.node.setStyle({'top': this.nodeTop+'px'});
		if(this.iframeLining)
			this.iframeLining.show();
		this.curtain.setStyle({'display': 'block'});
		return true;
	},

	beforeClose: function() {
		this.curtain.setStyle({'display': 'none'});
		if(this.iframeLining)
			this.iframeLining.hide();
		if(this.diff < 0) {
			if(Info.browser.isIE) {
				$('content-zone').setStyle({'height':'1%'});
			} else {
				$('content-zone').setStyle({'height':'auto'});
			}
		}
		return true;
	},

	handleOpen: function() {
		HeaderAnimation.listenerQueue.remove(this.listener);
		Layer.prototype.open.call(this);
	},

	resizeCurtain: function() {
		if(this.curtain) {
			var wrapper = $('footer-position-wrapper').getDimensions();
			this.contentHeight = parseInt(wrapper.height);
			this.contentWidth = $('content-zone').getDimensions().width;
			//982px is toolbar min-width (926px) plus its margins (34px left + 22px right)
			if(this.contentWidth < 982) {
				this.contentWidth = 982;
			}
			this.curtain.setStyle({'height': this.contentHeight+'px','width': this.contentWidth+'px'});
			if(this.iframeLining) {
				this.iframeLining.refresh();
			}
		}
	}
});
