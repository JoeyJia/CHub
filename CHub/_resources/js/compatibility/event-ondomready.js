/****************************** Cummins' new web appearance script *****/
/****************************** Copyright (c) 2007-2010 Cummins AG *****/
/***********************************************************************/
/****************************************** compability component: *****/
/* this file contains the deprecated prototype extension           *****/
/* Event.onDOMReady. This method is obsolete, because Prototype    *****/
/* 1.6 or higher provides the dom:loaded event.                    *****/
/*                                                                 *****/
/* this file shouldn't be included per default but could provide   *****/
/* help if custom code relies on these deprecated componentes.     *****/
/***********************************************************************/
/************************************** author virtual identity AG *****/

Object.extend(Event, {
	_domReady : function() {
		if (arguments.callee.done) return;
		arguments.callee.done = true;

		if (this._timer)  clearInterval(this._timer);

		this._readyCallbacks.each(function(f) { f() });
		this._readyCallbacks = null;
	},
	onDOMReady : function(f) {
		if (!this._readyCallbacks) {
			var domReady = this._domReady.bind(this);

			if (document.addEventListener)
				document.addEventListener("DOMContentLoaded", domReady, false);

				/*@cc_on @*/
				/*@if (@_win32)
					document.write("<script id=__ie_onload defer src=//:><\/script>");
					document.getElementById("__ie_onload").onreadystatechange = function() {
						if (this.readyState == "complete") domReady();
					};
				/*@end @*/

				if (/WebKit/i.test(navigator.userAgent)) {
					this._timer = setInterval(function() {
						if (/loaded|complete/.test(document.readyState)) domReady();
					}, 10);
				}

				Event.observe(window, "load", domReady);
				Event._readyCallbacks =  [];
			}
		Event._readyCallbacks.push(f);
	}
});
