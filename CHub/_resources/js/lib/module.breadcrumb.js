/****************************** Cummins' new web appearance script *****/
/****************************** Copyright (c) 2007-2010 Cummins AG *****/
/***********************************************************************/
/*********************************************** module breadcrumb *****/
/***********************************************************************/
/************************************** author virtual identity AG *****/
/* $LastChangedDate: 2010-01-12 16:33:21 +0100 (Di, 12 Jan 2010) $ *****/

function init_breadcrumb() {
	if ($("breadcrumb")) {

		var item_y = null;
		var isInFollowingLine = false;

		$A($("breadcrumb").getElementsByTagName("dd")).each(function(item) {
			item = $(item);

			//calculate the y-offset position of the current breadcrumb item
			if(item_y == null) {
				item_y = Position.cumulativeOffset(item)[1];
			}
			if(item_y < Position.cumulativeOffset(item)[1]) {
				isInFollowingLine = true;
			}

			//reduce the z-index of the current item
			if(isInFollowingLine) {
				item.setStyle({'zIndex':parseInt(item.getStyle('zIndex')) - 1 });
			}

			//breadcrumb JS hover only needed for IE<7
			if (Info.browser.isIEpre7) {
				var curBreadcrumbLayer = item.down("div");

				if(typeof curBreadcrumbLayer == "undefined") {
					// special treatment IE5.5
					curBreadcrumbLayer = item.getElementsByTagName("DIV")[0];
				}

				if(curBreadcrumbLayer) {
					var iframeLining = new IframeLining(curBreadcrumbLayer);

					item.observe("mouseover", function(e) {
						this.addClassName("active");
						iframeLining.show();
					}.bindAsEventListener(item));

					item.observe("mouseout", function(e) {
						var relatedTarget = $(e.relatedTarget || e.toElement);

						if(relatedTarget!=this && relatedTarget.childOf(this)==false) {
							this.removeClassName("active");
							iframeLining.hide();
						}
					}.bindAsEventListener(item));
				}
			}
		});
	}
}
