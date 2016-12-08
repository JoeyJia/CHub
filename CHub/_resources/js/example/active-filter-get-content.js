// DEVEL NOTE: all occurrences of ExampleNS are only for better local testing
// and not needed in any productive environment

/**
 * has to be implemented to load an xml containing active filter data
 */
ActiveSearchFilter.prototype.loadXml = function(callback) {
	var id = this.id; // id of this filter
	new Ajax.Request(
		'./_resources/xml/' + id + '-filter-data.xml',
		{
			onSuccess: function(response) {
				if ('ieResponseHook' in ExampleNS) ExampleNS.ieResponseHook(response); // hook for IE and USE_LOCAL
				callback(response);
			}
		}
	);
}

/**
 * has to be implemented to load an xsl to transform the xml into html
 */
ActiveSearchFilter.prototype.loadXsl = function(callback) {
	new Ajax.Request(
		'./_resources/xsl/render-active-filter.xsl',
		{
			onSuccess: function(response) {
				if ('ieResponseHook' in ExampleNS) ExampleNS.ieResponseHook(response); // hook for IE and USE_LOCAL
				callback(response);
			}
		}
	);
}
