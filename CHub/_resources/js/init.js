/****************************** Cummins' new web appearance script *****/
/****************************** Copyright (c) 2007-2010 Cummins AG *****/
/***********************************************************************/
/****************************************** inidvidual init script *****/
/***********************************************************************/
/************************************** author virtual identity AG *****/
/* $LastChangedDate: 2010-02-02 12:32:49 +0100 (Di, 02 Feb 2010) $ *****/

var USE_SIFR = false;

setup_sifr();

document.observe('dom:loaded', function() {
	if (!initGlobals()) { return false; }

	Layer.initialize();             // close layer if clicking outside      [lib/core.js]
	HeaderAnimation.initialize();   // animates height                      [lib/core.js]
	initLayout_buttons();           // improves the layout of buttons       [lib/core.js]
	nwa.SifrManager.paint();        // start sifr manager to replace fonts  [lib/core.js]
	init_contentLayers();           // generic layers                       [lib/core.js]
	init_siteIdLayer();             // site id layer                        [lib/core.js]
	init_countryLinks();            // country links                        [lib/core.js]
	init_siteExplorer();            // site explorer | site map             [lib/core.js]
	init_logoLinking();             // logo links in lightbox layer         [lib/core.js]
	init_contentLayer2();           // content layer 2                      [lib/core.js]
	init_languageSwitchLayer();     // language menu (>2 languages)         [lib/module.language-selector.js]
	Magnifier.getInstance();        // magnifies images (CTC)               [lib/module.magnifier.js]
	init_shareLayer();              // share layer with social bookmarking  [lib/core.js]
	init_contextLayer();            // context layers                       [lib/core.js]
	if (typeof init_fontsize == 'function') {
		init_fontsize();              // scalable font-size (CTC)             [lib/module.fontsize.js]
	}
	if (Info.browser.isIEpre7) {
		init_breadcrumb();            // multiline breadcrumbs hover for IE<7 [lib/module.breadcrumb.js]
		initLayout_IEPre7();          // min-width, max-width for IE<7        [lib/core.js]
		Event.observe(window, "resize", initLayout_IEPre7);
	}
});

Event.observe(window, "load", function() {
	HeaderVisual.initialize();      // replaces header images               [lib/module.header-visual.js]
});