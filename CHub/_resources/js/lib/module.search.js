/****************************** Cummins' new web appearance script *****/
/****************************** Copyright (c) 2007-2010 Cummins AG *****/
/***********************************************************************/
/*************************************************** module search *****/
/***********************************************************************/
/************************************** author virtual identity AG *****/
/* $LastChangedDate: 2010-02-02 12:32:49 +0100 (Di, 02 Feb 2010) $ *****/

/********************************************************************/
/* START: Search and filter */

function init_search() {
	if(Info.browser.isIEpre6) return;
	
	// get selectbox when clicking change on active filters
	
	if ($("search-filter-zone")) {
		$("search-filter-zone").setStyle({ left: '0'}); // show filter
		$("search-filter-zone").select('div.active-filter').each(function(element) {
			var name = element.id.replace(/^active-filter-/, '');
			var filter = new ActiveSearchFilter(name);
			$(element).observe("click",
				function(e) {
					filter.load();
					Event.stop(e);
				}
			);
		});
		
		// 1st element after last gui.select hides layer
		var resetButton = $$("fieldset.reset")[0].down('a.generic-button');
		resetButton.observe("focus", function() { Layer.closeCurrent(); });
	}
	
	// init auto complete layer
	new AutoCompleteLayer($('query'));

	// correct minor IE6 layout issues

	if (Info.browser.isIEpre7) {
		var suggestion = $$('div.search-suggestion').first();
		if (suggestion && suggestion.next().hasClassName('recommendations')) {
			suggestion.next().style.marginTop = '-3px';
		}
	}

}

var ActiveSearchFilter = Class.create();

ActiveSearchFilter.prototype = Object.extend(new XmlLoader, {
	
	initialize: function(id) {
		this.id  = id;
	},

	display: function() {
		if (this.insertXslTransformation($('active-filter-' + this.id))) {
			$('active-filter-' + this.id).hide();
			var node = $('filter-' + this.id);
			if (node) {
				GuiSelect.build($A([node]), GuiSelect.searchTransformer, true);
			}
		}
	}
});

/* END: Search and filter */
/********************************************************************/
/* START: Transformer for selectbox replacing */

GuiSelect.searchTransformer = {

	getMatches: function(text) {
		return /^(.+)\s(\([^)]+\))$/.exec(text);
	},

	replaceOption: function(text) {
		var matches = this.getMatches(text);
		var removeByPrefix = function(text) {
			return text.replace(/^by [^:]+:/, '');
		}
		return (matches) ? removeByPrefix(matches[1]) + ' <span class="no">' + matches[2] + '</span>' : removeByPrefix(text);
	},

	replaceTitle: function(text) {
		var matches = this.getMatches(text);
		return (matches) ? matches[1] : text;
	}
}

/* END: Transformer for selectbox replacing */
/********************************************************************/
