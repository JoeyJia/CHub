/****************************** Cummins' new web appearance script *****/
/****************************** Copyright (c) 2007-2010 Cummins AG *****/
/***********************************************************************/
/******************************** init script for search templates *****/
/***********************************************************************/
/************************************** author virtual identity AG *****/
/* $LastChangedDate: 2010-02-02 12:32:49 +0100 (Di, 02 Feb 2010) $ *****/

var USE_SIFR = false;

setup_sifr();

document.observe('dom:loaded', function() {
	if (!initGlobals()) { return false; }

	Layer.initialize();                           // close layer if clicking outside      [lib/core.js]
	HeaderAnimation.initialize();                 // animates height (#1)                 [lib/core.js] (#1) this class is needed for the layer handling, even if no header is animated on the specific page
	initLayout_buttons();                         // improves the layout of buttons       [lib/core.js]
	initLayout_sifr();                            // replaces some html by flash          [lib/core.js]
	init_contentLayers();                         // generic layers                       [lib/core.js]
	init_siteIdLayer();                           // site id layer                        [lib/core.js]
	init_countryLinks();                          // country links                        [lib/core.js]
	init_siteExplorer();                          // site explorer | site map             [lib/core.js]
	init_logoLinking();                           // logo links in lightbox layer         [lib/core.js]
	init_languageSwitchLayer();                   // language menu (>2 languages)         [lib/module.language-selector.js]
	init_guiSelect(GuiSelect.searchTransformer);  // styled js select boxes               [lib/module.gui.select.js]
	init_search();                                // search                               [lib/module.search.js]
	init_newWindow('a.new-window');               // open new windows unobtrusively       [lib/core.js]
	init_contextLayer();                          // context layers                       [lib/core.js]
	
	if (Info.browser.isIEpre7) {
		initLayout_IEPre7();                        // min-width, max-width for IE<7        [lib/core.js]
		init_breadcrumb();                          // multiline breadcrumbs hover for IE<7 [lib/module.breadcrumb.js]
		Event.observe(window, "resize", initLayout_IEPre7);
	}
});