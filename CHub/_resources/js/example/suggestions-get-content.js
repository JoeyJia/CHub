// DEVEL NOTE: for adapting this example for a autocomplete implementation
// Remove the following parts:
// - All appearances of ExampleNS.ieResponseHook
// - The method ExampleNS.simulate and its call
// Change the following parts:
// - The URLS of the xml and xsl ressources

/**
 * Has to be implemented to load an xml containing auto suggest data
 */
AutoCompleteXmlLoader.prototype.loadXml = function(callback) {
	var layer = this.layer;
	var query = layer.input.value; // value of the search input field. Not used in this example
	new Ajax.Request(
		'./_resources/xml/autocomplete-data.xml',
		{
			onSuccess: function(response) {
				if ('ieResponseHook' in ExampleNS) ExampleNS.ieResponseHook(response); // hook for IE and USE_LOCAL
				ExampleNS.simulate(layer, response); 
				callback(response);
			}
		}
	);
}


/**
 * Has to be implemented to load an xsl to transform the xml into html
 */
AutoCompleteXmlLoader.prototype.loadXsl = function(callback) {
	new Ajax.Request(
		'./_resources/xsl/render-autocomplete-layer.xsl',
		{
			onSuccess: function(response) {
				if ('ieResponseHook' in ExampleNS) ExampleNS.ieResponseHook(response); // hook for IE and USE_LOCAL
				callback(response);
			}
		}
	);
}


/**
 * Enhances the UX in these frontend templates by manipulating the
 * response xml when USE_LOCAL is true.
 * Since this function works on the raw xml it depends on the xml structure
 * as well as on the xsl transformation.
 */

if (USE_LOCAL) {

	ExampleNS.simulate = function(layer, response) {
	
		var itemList = response.responseXML.getElementsByTagName('ResultSet')[0];
		var items    = response.responseXML.getElementsByTagName('QueryHelp');
		var query    = layer.input.value;

		if (/x/.match(query)) {

			// simulation 1: if the query contains an x, the number of entries is set to 0
			// this shows the handling of empty result sets
		
			itemList.getElementsByTagName('Engine')[0].setAttribute('NumResults', 0);
	
		} else {
	
			// simulation 2: the current input value is inserted before the xml Entry value
	
			for(var i = 0, l = items.length; i < l; ++i) {
				var entry = items[i].getElementsByTagName('Entry')[0].firstChild;
				entry.nodeValue = query + ' ' + entry.nodeValue;
			}
	
		}
	}
	
} else {
	
		ExampleNS.simulate = Prototype.K;

}
