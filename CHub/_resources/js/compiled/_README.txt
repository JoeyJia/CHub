To avoid multiple requests for javascript resources a compiled version could be used:

use for example

	<script type="text/javascript" src="./_resources/js/lib/prototype.js"></script>

	<script type="text/javascript" src="./_resources/js/compiled/script.js"></script>

	<script type="text/javascript" src="./_resources/js/init.js"></script>
	<script type="text/javascript" src="./_resources/js/example/siteexplorer-get-content.js"></script>

instead of

	<script type="text/javascript" src="./_resources/js/lib/prototype.js"></script>

	<script type="text/javascript" src="./_resources/js/lib/common.js"></script>
	<script type="text/javascript" src="./_resources/js/lib/core.js"></script>
	<script type="text/javascript" src="./_resources/js/lib/module.breadcrumb.js"></script>
	<script type="text/javascript" src="./_resources/js/lib/module.header-visual.js"></script>
	<script type="text/javascript" src="./_resources/js/lib/module.language-selector.js"></script>
	<script type="text/javascript" src="./_resources/js/lib/module.magnifier.js"></script>
	<script type="text/javascript" src="./_resources/js/lib/module.fontsize.js"></script>
	<script type="text/javascript" src="./_resources/js/lib/module.social-bookmarker.js"></script>

	<script type="text/javascript" src="./_resources/js/init.js"></script>
	<script type="text/javascript" src="./_resources/js/example/siteexplorer-get-content.js"></script>

in the templates

*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*

within search templates use for example

	<script type="text/javascript" src="./_resources/js/lib/prototype.js"></script>
	<script type="text/javascript" src="./_resources/js/lib/scriptaculous.slider.js"></script>

	<script type="text/javascript" src="./_resources/js/compiled/script.search.js"></script>

	<script type="text/javascript" src="./_resources/js/init.search.js"></script>
	<script type="text/javascript" src="./_resources/js/example/siteexplorer-get-content.js"></script>
	<script type="text/javascript" src="./_resources/js/example/suggestions-get-content.js"></script>

instead of

	<script type="text/javascript" src="./_resources/js/lib/prototype.js"></script>
	<script type="text/javascript" src="./_resources/js/lib/scriptaculous.slider.js"></script>

	<script type="text/javascript" src="./_resources/js/lib/common.js"></script>
	<script type="text/javascript" src="./_resources/js/lib/core.js"></script>
	<script type="text/javascript" src="./_resources/js/lib/module.language-selector.js"></script>
	<script type="text/javascript" src="./_resources/js/lib/module.mousewheel-observer.js"></script>
	<script type="text/javascript" src="./_resources/js/lib/module.gui.select.js"></script>
	<script type="text/javascript" src="./_resources/js/lib/module.autocomplete.js"></script>
	<script type="text/javascript" src="./_resources/js/lib/module.lightbox-layer.js"></script>
	<script type="text/javascript" src="./_resources/js/lib/module.search.js"></script>

	<script type="text/javascript" src="./_resources/js/init.search.js"></script>
	<script type="text/javascript" src="./_resources/js/example/siteexplorer-get-content.js"></script>
	<script type="text/javascript" src="./_resources/js/example/suggestions-get-content.js"></script>

*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*~*

These scripts could be created with bash by:

cd _resources/js/lib
cat common.js core.js module.breadcrumb.js module.header-visual.js module.language-selector.js module.magnifier.js module.fontsize.js > ../compiled/script.js
cat common.js core.js module.language-selector.js module.mousewheel-observer.js module.gui.select.js module.autocomplete.js module.lightbox-layer.js module.search.js > ../compiled/script.search.js 
