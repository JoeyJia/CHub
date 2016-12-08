/****************************** Cummins' new web appearance script *****/
/****************************** Copyright (c) 2007-2010 Cummins AG *****/
/***********************************************************************/
/****************************************** compability component: *****/
/* this file contains the Popup class for opening popups.          *****/
/* This class was never used                  .                    *****/
/*                                                                 *****/
/* this file shouldn't be included per default but could provide   *****/
/* help if custom code relies on these deprecated componentes.     *****/
/***********************************************************************/
/************************************** author virtual identity AG *****/

var Popup = Class.create();

Popup.open = function(url, name, options) {
	options = $H(options).toQueryString().replace(/&/g, ",");
	window.open(url, name, options).focus();
}