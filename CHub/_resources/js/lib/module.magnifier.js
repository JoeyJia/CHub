/****************************** Cummins' new web appearance script *****/
/****************************** Copyright (c) 2007-2010 Cummins AG *****/
/***********************************************************************/
/************************************************ module Magnifier *****/
/***********************************************************************/
/************************************** author virtual identity AG *****/
/* $LastChangedDate: 2010-01-12 16:33:21 +0100 (Di, 12 Jan 2010) $ *****/

var Magnifier = Class.create();

Magnifier.getInstance = function() {
	if (!Magnifier.instance) {
		Magnifier.instance = new Magnifier();
	}
	return Magnifier.instance;
}

Magnifier.prototype = {
	initialize: function() {

		$A(zone.content.getElementsByTagName("a")).findAll( function(link) {
			return $(link).hasClassName("magnifier");
		}).each( function(magnifier) {
			magnifier.observe("click", function(e) {Magnifier.getInstance().open(e); }.bindAsEventListener($(magnifier)));
		});
	},

	open: function(e) {
		var mLink     = Event.element(e);
		var smallImg  = mLink.up().previous();

		var miWrapper = $(document.createElement("div")); // mi stands for "magnified image"
		var miImg     = $(document.createElement("img"));
		var miClose   = $(document.createElement("a"));

		miWrapper.className   = "mi-wrapper";
		miImg.src             = mLink.getAttribute("href");
		miClose.href          = location.href;

		if (this.currentMiWrapper) {
			this.closeCurrent(true);
		}

		var offset = Position.cumulativeOffset(smallImg);

		miWrapper.style.left = offset[0] + "px";
		miWrapper.style.top =  offset[1] + "px";

		document.body.appendChild(miWrapper);
		this.currentMiWrapper = miWrapper;

		miWrapper.appendChild(miImg);
		miWrapper.appendChild(miClose);

		if (miImg.complete) {
			miClose.style.display = "block";
		} else {
			miImg.observe("load", function() { miClose.style.display = "block"; });
		}

		$A([miImg, miClose]).each(function (closeElt) {
			closeElt.observe("click", function(e) {
				Magnifier.getInstance().closeCurrent(false);
				Event.stop(e);
			});
		});
		Event.stop(e);
	},

	closeCurrent: function(reopen) {
		document.body.removeChild(this.currentMiWrapper);
		this.currentMiWrapper = null;
	}
}
