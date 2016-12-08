/****************************** Cummins' new web appearance script *****/
/****************************** Copyright (c) 2007-2010 Cummins AG *****/
/***********************************************************************/
/****************************************** compability component: *****/
/* this file contains the deprecated prototype extension           *****/
/* Position.getWindowSize. It was used only once and is replaced   *****/
/* by custom code.                                                 *****/
/*                                                                 *****/
/* this file shouldn't be included per default but could provide   *****/
/* help if custom code relies on these deprecated componentes.     *****/
/***********************************************************************/
/************************************** author virtual identity AG *****/

Position.getWindowSize = function(w) {
	var width, height;
	var w = w ? w : window;
	width = w.innerWidth || (w.document.documentElement.clientWidth || w.document.body.clientWidth);
	height = w.innerHeight || (w.document.documentElement.clientHeight || w.document.body.clientHeight);
	return { width: width, height: height };
}
