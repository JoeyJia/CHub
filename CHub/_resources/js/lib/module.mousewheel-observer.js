var MouseWheelObserver = Class.create();

MouseWheelObserver.prototype = {

	initialize: function() {
		this.functions = $A([]);

		if (window.addEventListener) { // gecko
			Event.observe(window, "DOMMouseScroll", this.handle.bindAsEventListener(this));
		}
		Event.observe(document, "mousewheel", this.handle.bindAsEventListener(this));
	},

	register: function(fn) {
		this.functions.push(fn);
	},

	fire: function(wheelMovement) {
		var result = this.functions.inject(true, function(acc, fn) {
				return acc && fn(wheelMovement);
			}
		);
		return !!result;
	},

	handle: function(e) {
		var wheelMovement = 0;
		var event = e || window.event;
		if (event.detail) { // gecko
			wheelMovement = event.detail / 3;
		} else if (event.wheelDelta) { // IE and Opera
			wheelMovement = event.wheelDelta / 120;
			if (!window.opera) {
				wheelMovement = -wheelMovement;
			}
		}
		if (wheelMovement) {
			var result = this.fire(wheelMovement);
			if (result == false) {
				Event.stop(e);
			}
		}
	}

};