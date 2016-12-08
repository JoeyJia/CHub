// DEVEL NOTE: this file provides some patches testing the templates in the
// local file system.
// do not use this file in any productive environment!

USE_LOCAL = window.USE_LOCAL || true; // per default USE_LOCAL is true when using this file

var ExampleNS = {}; // namespace for example code

if (USE_LOCAL) {

	/**
	 * changes the precedence of ActiveXObject vs XMLHttpRequest to support
	 * ajax on local files in IE7 and IE8
	 *
	 * do not use this patch in any productive environment!
	 * side effects could be possible
	 */

	Ajax.getTransport = function() {
	    return window.ActiveXObject ? new ActiveXObject("Microsoft.XMLHTTP") : new XMLHttpRequest();
	};

	/**
	 * creates the responseXML from responseStream when requesting local files
	 * with ajax in IE
	 *
	 * Background: local files lack any MimeType in IE when requested with ajax
	 * Without one of the valid xml MimeTypes IE cannot produce the responseXML
	 * This function patches this behaviour
	 *
	 * do not use this method in any productive environment!
	 * side effects could be possible
	 */

	if (Info.browser.isIE) {
		ExampleNS.ieResponseHook = function(response) {
			if (!response.transport.responseXML.documentElement && response.transport.responseStream) {
				response.transport.responseXML.load(response.transport.responseStream);
			}
		}
	}

}
