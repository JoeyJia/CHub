/*
 * START OF FILE - _resources/js/lib/common.js
 */
/****************************** Cummins' new web appearance script *****/
/****************************** Copyright (c) 2007-2010 Cummins AG *****/
/***********************************************************************/
/****************************************** javascript common lib: *****/
/********************* - Info class for browser and os tests       *****/
/*********************   (Set CSS info classes to DOM root node)   *****/
/********************* - Fix Flickering Background Images          *****/
/********************* - sIFR 2.0.7                                *****/
/********************* - SWFObject v1.5.1                          *****/
/***********************************************************************/
/************************************** author virtual identity AG *****/
/* $LastChangedDate: 2010-02-02 13:30:40 +0100 (Di, 02 Feb 2010) $ *****/
/* START: browser detection                                            */

var Info = new function() {

	var userAgent = navigator.userAgent.toLowerCase();

	// check user agent
	function is(str) { return userAgent.indexOf(str) > -1; }

	// version detection
	function version() {
		var versionString = '';
		var specificOffset = 0;
		var specificLength = 1;

		if(b.isIE)      { versionString = 'msie'; specificOffset = 1; }
		if(b.isFirefox) { versionString = 'firefox/'; specificLength = 3; }
		if(b.isSafari)  { versionString = 'version/'; } // note: detects only safari 3+
		if(b.isChrome)  { versionString = 'chrome/'; }
		if(b.isOpera)   { versionString = 'version/'; specificLength = 2;
			// for Opera version < 10
			if(userAgent.indexOf(versionString) == -1) {
				versionString = 'opera/'; specificLength = 1;
			}
		}

		return parseFloat((userAgent.substr(userAgent.indexOf(versionString) + versionString.length + specificOffset, specificLength)));
	}

	// browser detection
	var b = {};
	b.isOpera      = typeof window.opera != 'undefined';
	b.isIE         = is('msie') && b.isOpera == false;
	b.isWebkit     = is('webkit');
	b.isChrome     = is('chrome');
	b.isSafari     = is('safari') && (b.isChrome == false) && is('version'); // note: detects only safari 3+
	b.isMozilla    = is('gecko') && b.isWebkit == false && b.isSafari == false && b.isChrome == false && b.isOpera == false;
	b.isFirefox    = b.isMozilla == true && is('firefox');
	b.isKonqueror  = navigator.product != null && navigator.product.toLowerCase().indexOf('konqueror') > -1;
	b.isSafari3    = b.isSafari && version() < 4;
	this.browser   = b;

	//browser version initialization
	b.version = version();
	if(b.version == 0 || isNaN(b.version)) { b.version = false; }

	// backward compatibility, use b.version to detect browser version in new scripts
	b.isSafariGte3 = b.isSafari && (b.version >= 3);
	b.isIE7        = b.isIE && (b.version == 7);
	b.isIEpre8     = b.isIE && (b.version < 8);
	b.isIEpre7     = b.isIE && (b.version < 7);
	b.isIEpre6     = b.isIE && (b.version < 6);

	//OS detection
	var os = {};
	os.isLinux     = (navigator.platform.indexOf("Linux") == 0),
	os.isMac       = (navigator.platform.indexOf("Mac") == 0),
	os.isWin       = (navigator.platform.indexOf("Win") == 0)
	this.os        = os;

	// windows version detection >> 5.0=2000 5.1=XP, 5.2=2003, 6.0=Vista, 6.1=Win7
	if(this.os.isWin) {
		this.os.winVersion = parseFloat(userAgent.substr(userAgent.indexOf('nt') + 3, 3));
	}

	//has transparency support detection
	this.hasTransparencySupport = true;
	if(this.os.isLinux || this.browser.isKonqueror || (this.os.isMac && !this.browser.isSafariGte3)) {
		this.hasTransparencySupport = false;
	}

	//CSS classes
	function setCss() {
		var h = document.getElementsByTagName('html')[0],
		classes=[
			(
				b.isIE      ? ('ie ie' + b.version) :
				b.isFirefox ? ('firefox firefox' + (b.version.toString().replace(/\./ig, ''))) :
				b.isChrome  ? ('chrome chrome' + b.version) :
				b.isSafari  ? ('safari safari' + b.version) :
				b.isOpera   ? ('opera opera' + b.version)	: ''
			),
			(
				b.isMozilla ? 'gecko' :
				b.isWebkit  ? 'webkit' : ''
			),
			(
				os.isWin    ? 'win' :
				os.isMac    ? 'mac' :
				os.isLinux  ? 'linux' : ''
			),
			'js'
		];
		h.className += h.className == '' ? classes.join(' ') : ' ' + classes.join(' ');
		return classes;
	}

	setCss();
}

/* END: browser detection                                              */
/***********************************************************************/
/* START: Fix Flickering Background Images                             */

if (window.createPopup && document.compatMode && document.compatMode=="CSS1Compat" && !window.XMLHttpRequest) {
	try{
		document.execCommand("BackgroundImageCache", false, true);
	} catch(e) {}
}

/* END: Fix Flickering Background Images                               */
/***********************************************************************/
/* START sIFR v2.0.7                                                   */
/*
	Copyright 2004 - 2008 Mark Wubben and Mike Davidson. Prior contributions by Shaun Inman and Tomas Jogin.
	This software is licensed under the CC-GNU LGPL <http://creativecommons.org/licenses/LGPL/2.1/>
*/

var hasFlash=function(){var a=6;if(navigator.appVersion.indexOf("MSIE")!=-1&&navigator.appVersion.indexOf("Windows")>-1){document.write('<script language="VBScript"\> \non error resume next \nhasFlash = (IsObject(CreateObject("ShockwaveFlash.ShockwaveFlash." & '+a+'))) \n</script\> \n');if(window.hasFlash!=null)return window.hasFlash}if(navigator.mimeTypes&&navigator.mimeTypes["application/x-shockwave-flash"]&&navigator.mimeTypes["application/x-shockwave-flash"].enabledPlugin){var b=(navigator.plugins["Shockwave Flash 2.0"]||navigator.plugins["Shockwave Flash"]).description;return parseInt(b.substr(b.indexOf(".")-2,2),10)>=a}return false}();String.prototype.normalize=function(){return this.replace(/\s+/g," ")};if(Array.prototype.push==null){Array.prototype.push=function(){var i=0,a=this.length,b=arguments.length;while(i<b){this[a++]=arguments[i++]}return this.length}}if(!Function.prototype.apply){Function.prototype.apply=function(a,b){var c=[];var d,e;if(!a)a=window;if(!b)b=[];for(var i=0;i<b.length;i++){c[i]="b["+i+"]"}e="a.__applyTemp__("+c.join(",")+");";a.__applyTemp__=this;d=eval(e);a.__applyTemp__=null;return d}}function named(a){return new named.Arguments(a)}named.Arguments=function(a){this.oArgs=a};named.Arguments.prototype.constructor=named.Arguments;named.extract=function(a,b){var c,d;var i=a.length;while(i--){d=a[i];if(d!=null&&d.constructor!=null&&d.constructor==named.Arguments){c=a[i].oArgs;break}}if(c==null)return;for(e in c)if(b[e]!=null)b[e](c[e]);return};var parseSelector=function(){var a=/^([^#.>`]*)(#|\.|\>|\`)(.+)$/;function r(s,t){var u=s.split(/\s*\,\s*/);var v=[];for(var i=0;i<u.length;i++)v=v.concat(b(u[i],t));return v}function b(c,d,e){c=c.normalize().replace(" ","`");var f=c.match(a);var g,h,i,j,k,n;var l=[];if(f==null)f=[c,c];if(f[1]=="")f[1]="*";if(e==null)e="`";if(d==null)d=document;switch(f[2]){case "#":k=f[3].match(a);if(k==null)k=[null,f[3]];g=document.getElementById(k[1]);if(g==null||(f[1]!="*"&&!o(g,f[1])))return l;if(k.length==2){l.push(g);return l}return b(k[3],g,k[2]);case ".":if(e!=">")h=m(d,f[1]);else h=d.childNodes;for(i=0,n=h.length;i<n;i++){g=h[i];if(g.nodeType!=1)continue;k=f[3].match(a);if(k!=null){if(g.className==null||g.className.match("(\\s|^)"+k[1]+"(\\s|$)")==null)continue;j=b(k[3],g,k[2]);l=l.concat(j)}else if(g.className!=null&&g.className.match("(\\s|^)"+f[3]+"(\\s|$)")!=null)l.push(g)}return l;case ">":if(e!=">")h=m(d,f[1]);else h=d.childNodes;for(i=0,n=h.length;i<n;i++){g=h[i];if(g.nodeType!=1)continue;if(!o(g,f[1]))continue;j=b(f[3],g,">");l=l.concat(j)}return l;case "`":h=m(d,f[1]);for(i=0,n=h.length;i<n;i++){g=h[i];j=b(f[3],g,"`");l=l.concat(j)}return l;default:if(e!=">")h=m(d,f[1]);else h=d.childNodes;for(i=0,n=h.length;i<n;i++){g=h[i];if(g.nodeType!=1)continue;if(!o(g,f[1]))continue;l.push(g)}return l}}function m(d,o){if(o=="*"&&d.all!=null)return d.all;return d.getElementsByTagName(o)}function o(p,q){return q=="*"?true:p.nodeName.toLowerCase().replace("html:", "")==q.toLowerCase()}return r}();var sIFR=function(){var a="http://www.w3.org/1999/xhtml";var b=false;var c=false;var d;var ah=[];var al=document;var ak=al.documentElement;var am=window;var au=al.addEventListener;var av=am.addEventListener;var f=function(){var g=navigator.userAgent.toLowerCase();var f={a:g.indexOf("applewebkit")>-1,b:g.indexOf("safari")>-1,c:navigator.product!=null&&navigator.product.toLowerCase().indexOf("konqueror")>-1,d:g.indexOf("opera")>-1,e:al.contentType!=null&&al.contentType.indexOf("xml")>-1,f:true,g:true,h:null,i:null,j:null,k:null};f.l=f.a||f.c;f.m=!f.a&&navigator.product!=null&&navigator.product.toLowerCase()=="gecko";if(f.m&&g.match(/.*gecko\/(\d{8}).*/))f.j=new Number(g.match(/.*gecko\/(\d{8}).*/)[1]);f.n=g.indexOf("msie")>-1&&!f.d&&!f.l&&!f.m;f.o=f.n&&g.match(/.*mac.*/)!=null;if(f.d&&g.match(/.*opera(\s|\/)(\d+\.\d+)/))f.i=new Number(g.match(/.*opera(\s|\/)(\d+\.\d+)/)[2]);if(f.n||(f.d&&f.i<7.6))f.g=false;if(f.a&&g.match(/.*applewebkit\/(\d+).*/))f.k=new Number(g.match(/.*applewebkit\/(\d+).*/)[1]);if(am.hasFlash&&(!f.n||f.o)){var aj=(navigator.plugins["Shockwave Flash 2.0"]||navigator.plugins["Shockwave Flash"]).description;f.h=parseInt(aj.substr(aj.indexOf(".")-2,2),10)}if(g.match(/.*(windows|mac).*/)==null||f.o||f.c||(f.d&&(g.match(/.*mac.*/)!=null||f.i<7.6))||(f.b&&f.h<7)||(!f.b&&f.a&&f.k<312)||(f.m&&f.j<20020523))f.f=false;if(!f.o&&!f.m&&al.createElementNS)try{al.createElementNS(a,"i").innerHTML=""}catch(e){f.e=true}f.p=f.c||(f.a&&f.k<312);return f}();function at(){return{bIsWebKit:f.a,bIsSafari:f.b,bIsKonq:f.c,bIsOpera:f.d,bIsXML:f.e,bHasTransparencySupport:f.f,bUseDOM:f.g,nFlashVersion:f.h,nOperaVersion:f.i,nGeckoBuildDate:f.j,nWebKitVersion:f.k,bIsKHTML:f.l,bIsGecko:f.m,bIsIE:f.n,bIsIEMac:f.o,bUseInnerHTMLHack:f.p}}if(am.hasFlash==false||!al.getElementsByTagName||!al.getElementById||(f.e&&(f.p||f.n)))return{UA:at()};function af(e){if((!k.bAutoInit&&(am.event||e)!=null)||!l(e))return;b=true;for(var i=0,h=ah.length;i<h;i++)j.apply(null,ah[i]);ah=[]}var k=af;function l(e){if(c==false||k.bIsDisabled==true||((f.e&&f.m||f.l)&&e==null&&b==false)||al.getElementsByTagName("body").length==0)return false;return true}function m(n){if(f.n)return n.replace(new RegExp("%\d{0}","g"),"%25");return n.replace(new RegExp("%(?!\d)","g"),"%25")}function as(p,q){return q=="*"?true:p.nodeName.toLowerCase().replace("html:", "")==q.toLowerCase()}function o(p,q,r,s,t){var u="";var v=p.firstChild;var w,x,y,z;if(s==null)s=0;if(t==null)t="";while(v){if(v.nodeType==3){z=v.nodeValue.replace("<","&lt;");switch(r){case "lower":u+=z.toLowerCase();break;case "upper":u+=z.toUpperCase();break;default:u+=z}}else if(v.nodeType==1){if(as(v,"a")&&!v.getAttribute("href")==false){if(v.getAttribute("target"))t+="&sifr_url_"+s+"_target="+v.getAttribute("target");t+="&sifr_url_"+s+"="+m(v.getAttribute("href")).replace(/&/g,"%26");u+='<a href="asfunction:_root.launchURL,'+s+'">';s++}else if(as(v,"br"))u+="<br/>";if(v.hasChildNodes()){y=o(v,null,r,s,t);u+=y.u;s=y.s;t=y.t}if(as(v,"a"))u+="</a>"}w=v;v=v.nextSibling;if(q!=null){x=w.parentNode.removeChild(w);q.appendChild(x)}}return{"u":u,"s":s,"t":t}}function A(B){if(al.createElementNS&&f.g)return al.createElementNS(a,B);return al.createElement(B)}function C(D,E,z){var p=A("param");p.setAttribute("name",E);p.setAttribute("value",z);D.appendChild(p)}function F(p,G){var H=p.className;if(H==null)H=G;else H=H.normalize()+(H==""?"":" ")+G;p.className=H}function aq(ar){var a=ak;if(k.bHideBrowserText==false)a=al.getElementsByTagName("body")[0];if((k.bHideBrowserText==false||ar)&&a)if(a.className==null||a.className.match(/\bsIFR\-hasFlash\b/)==null)F(a, "sIFR-hasFlash")}function j(I,J,K,L,M,N,O,P,Q,R,S,r,T){if(!l())return ah.push(arguments);aq();named.extract(arguments,{sSelector:function(ap){I=ap},sFlashSrc:function(ap){J=ap},sColor:function(ap){K=ap},sLinkColor:function(ap){L=ap},sHoverColor:function(ap){M=ap},sBgColor:function(ap){N=ap},nPaddingTop:function(ap){O=ap},nPaddingRight:function(ap){P=ap},nPaddingBottom:function(ap){Q=ap},nPaddingLeft:function(ap){R=ap},sFlashVars:function(ap){S=ap},sCase:function(ap){r=ap},sWmode:function(ap){T=ap}});var U=parseSelector(I);if(U.length==0)return false;if(S!=null)S="&"+S.normalize();else S="";if(K!=null)S+="&textcolor="+K;if(M!=null)S+="&hovercolor="+M;if(M!=null||L!=null)S+="&linkcolor="+(L||K);if(O==null)O=0;if(P==null)P=0;if(Q==null)Q=0;if(R==null)R=0;if(N==null)N="#FFFFFF";if(T=="transparent")if(!f.f)T="opaque";else N="transparent";if(T==null)T="";var p,V,W,X,Y,Z,aa,ab,ac;var ad=null;for(var i=0,h=U.length;i<h;i++){p=U[i];if(p.className!=null&&p.className.match(/\bsIFR\-replaced\b/)!=null)continue;V=p.offsetWidth-R-P;W=p.offsetHeight-O-Q;aa=A("span");aa.className="sIFR-alternate";ac=o(p,aa,r);Z="txt="+m(ac.u).replace(/\+/g,"%2B").replace(/&/g,"%26").replace(/\"/g, "%22").normalize() + S + "&w=" + V + "&h=" + W + ac.t;F(p,"sIFR-replaced");if(ad==null||!f.g){if(!f.g){if(!f.n)p.innerHTML=['<embed class="sIFR-flash" type="application/x-shockwave-flash" src="',J,'" quality="best" wmode="',T,'" bgcolor="',N,'" flashvars="',Z,'" width="',V,'" height="',W,'" sifr="true"></embed>'].join("");else p.innerHTML=['<object classid="clsid:D27CDB6E-AE6D-11cf-96B8-444553540000" sifr="true" width="',V,'" height="',W,'" class="sIFR-flash"><param name="movie" value="',J,'"></param><param name="flashvars" value="',Z,'"></param><param name="quality" value="best"></param><param name="wmode" value="',T,'"></param><param name="bgcolor" value="',N,'"></param> </object>'].join('')}else{if(f.d){ab=A("object");ab.setAttribute("data",J);C(ab,"quality","best");C(ab,"wmode",T);C(ab,"bgcolor",N)}else{ab=A("embed");ab.setAttribute("src",J);ab.setAttribute("quality","best");ab.setAttribute("flashvars",Z);ab.setAttribute("wmode",T);ab.setAttribute("bgcolor",N)}ab.setAttribute("sifr","true");ab.setAttribute("type","application/x-shockwave-flash");ab.className="sIFR-flash";if(!f.l||!f.e)ad=ab.cloneNode(true)}}else ab=ad.cloneNode(true);if(f.g){if(f.d)C(ab,"flashvars",Z);else ab.setAttribute("flashvars",Z);ab.setAttribute("width",V);ab.setAttribute("height",W);ab.style.width=V+"px";ab.style.height=W+"px";p.appendChild(ab)}p.appendChild(aa);if(f.p)p.innerHTML+=""}if(f.n&&k.bFixFragIdBug)setTimeout(function(){al.title=d},0)}function ai(){d=al.title}function ae(){if(k.bIsDisabled==true)return;c=true;if(k.bHideBrowserText)aq(true);if(am.attachEvent)am.attachEvent("onload",af);else if(!f.c&&(al.addEventListener||am.addEventListener)){if(f.a&&f.k>=132&&am.addEventListener)am.addEventListener("load",function(){setTimeout("sIFR({})",1)},false);else{if(al.addEventListener)al.addEventListener("load",af,false);if(am.addEventListener)am.addEventListener("load",af,false)}}else if(typeof am.onload=="function"){var ag=am.onload;am.onload=function(){ag();af()}}else am.onload=af;if(!f.n||am.location.hash=="")k.bFixFragIdBug=false;else ai()}k.UA=at();k.bAutoInit=true;k.bFixFragIdBug=true;k.replaceElement=j;k.updateDocumentTitle=ai;k.appendToClassName=F;k.setup=ae;k.debug=function(){aq(true)};k.debug.replaceNow=function(){ae();k()};k.bIsDisabled=false;k.bHideBrowserText=true;return k}();

function setup_sifr() {
	if(typeof sIFR == "function" && !sIFR.UA.bIsIEMac && !sIFR.UA.bIsOpera && (!sIFR.UA.bIsWebKit || sIFR.UA.nWebKitVersion >= 100) && USE_SIFR) {
		sIFR.setup();
	};
}

/* END: sIFR 2.0.7                                                     */
/***********************************************************************/
/*	START: sIFR offical add-ons 1.2	    (Copyright 2005 Mark Wubben)   */
/*
	This software is licensed under the CC-GNU LGPL <http://creativecommons.org/licenses/LGPL/2.1/>
*/
if(typeof sIFR=="function")(function(){var j=document;var h=j.documentElement;sIFR.removeDecoyClasses=function(){function a(b){if(b&&b.className!=null)b.className=b.className.replace(/\bsIFR-hasFlash\b/,"")}return function(){a(h);a(j.getElementsByTagName("body")[0])}}();sIFR.preferenceManager={storage:{sCookieId:"sifr",set:function(a){var b=new Date();b.setFullYear(b.getFullYear()+3);j.cookie=[this.sCookieId,"=",a,";expires=",b.toGMTString(),";path=/"].join("")},get:function(){var a=j.cookie.match(new RegExp(";?"+this.sCookieId+"=([^;]+);?"));if(a!=null&&a[1]=="false")return false;else return true},reset:function(){var a=new Date();a.setFullYear(a.getFullYear()-1);j.cookie=[this.sCookieId,"=true;expires=",a.toGMTString(),";path=/"].join("")}},disable:function(){this.storage.set(false)},enable:function(){this.storage.set(true)},test:function(){return this.storage.get()}};if(sIFR.preferenceManager.test()==false){sIFR.bIsDisabled=true;sIFR.removeDecoyClasses()}sIFR.rollback=function(){function a(b){var c,d,e,f,g,h;var l=parseSelector(b);var i=l.length-1;var m=false;while(i>=0){c=l[i];l.length--;d=c.parentNode;if(c.getAttribute("sifr")=="true"){h=0;while(h<d.childNodes.length){c=d.childNodes[h];if(c.className=="sIFR-alternate"){e=c;h++;continue}d.removeChild(c)}if(e!=null){f=e.firstChild;while(f!=null){g=f.nextSibling;d.appendChild(e.removeChild(f));f=g}d.removeChild(e)}if(!sIFR.UA.bIsXML&&sIFR.UA.bUseInnerHTMLHack)d.innerHTML+="";d.className=d.className.replace(/\bsIFR\-replaced\b/,"")};m=true;i--}return m}return function(k){named.extract(arguments,{sSelector:function(a){k=a}});if(k==null)k="";else k+=">";sIFR.removeDecoyClasses();sIFR.bHideBrowserText=false;if(a(k+"embed")==false)a(k+"object")}}()})()

/* END: sIFR official add-ons 1.2                                      */
/***********************************************************************/
/* START: SWFObject v1.5.1 (http://blog.deconcept.com/swfobject/)      */

if(typeof deconcept=="undefined"){var deconcept={};}if(typeof deconcept.util=="undefined"){deconcept.util={};}if(typeof deconcept.SWFObjectUtil=="undefined"){deconcept.SWFObjectUtil={};}deconcept.SWFObject=function(_1,id,w,h,_5,c,_7,_8,_9,_a){if(!document.getElementById){return;}this.DETECT_KEY=_a?_a:"detectflash";this.skipDetect=deconcept.util.getRequestParameter(this.DETECT_KEY);this.params={};this.variables={};this.attributes=[];if(_1){this.setAttribute("swf",_1);}if(id){this.setAttribute("id",id);}if(w){this.setAttribute("width",w);}if(h){this.setAttribute("height",h);}if(_5){this.setAttribute("version",new deconcept.PlayerVersion(_5.toString().split(".")));}this.installedVer=deconcept.SWFObjectUtil.getPlayerVersion();if(!window.opera&&document.all&&this.installedVer.major>7){if(!deconcept.unloadSet){deconcept.SWFObjectUtil.prepUnload=function(){__flash_unloadHandler=function(){};__flash_savedUnloadHandler=function(){};window.attachEvent("onunload",deconcept.SWFObjectUtil.cleanupSWFs);};window.attachEvent("onbeforeunload",deconcept.SWFObjectUtil.prepUnload);deconcept.unloadSet=true;}}if(c){this.addParam("bgcolor",c);}var q=_7?_7:"high";this.addParam("quality",q);this.setAttribute("useExpressInstall",false);this.setAttribute("doExpressInstall",false);var _c=(_8)?_8:window.location;this.setAttribute("xiRedirectUrl",_c);this.setAttribute("redirectUrl","");if(_9){this.setAttribute("redirectUrl",_9);}};deconcept.SWFObject.prototype={useExpressInstall:function(_d){this.xiSWFPath=!_d?"expressinstall.swf":_d;this.setAttribute("useExpressInstall",true);},setAttribute:function(_e,_f){this.attributes[_e]=_f;},getAttribute:function(_10){return this.attributes[_10]||"";},addParam:function(_11,_12){this.params[_11]=_12;},getParams:function(){return this.params;},addVariable:function(_13,_14){this.variables[_13]=_14;},getVariable:function(_15){return this.variables[_15]||"";},getVariables:function(){return this.variables;},getVariablePairs:function(){var _16=[];var key;var _18=this.getVariables();for(key in _18){_16[_16.length]=key+"="+_18[key];}return _16;},getSWFHTML:function(){var _19="";if(navigator.plugins&&navigator.mimeTypes&&navigator.mimeTypes.length){if(this.getAttribute("doExpressInstall")){this.addVariable("MMplayerType","PlugIn");this.setAttribute("swf",this.xiSWFPath);}_19="<embed type=\"application/x-shockwave-flash\" src=\""+this.getAttribute("swf")+"\" width=\""+this.getAttribute("width")+"\" height=\""+this.getAttribute("height")+"\" style=\""+(this.getAttribute("style")||"")+"\"";_19+=" id=\""+this.getAttribute("id")+"\" name=\""+this.getAttribute("id")+"\" ";var _1a=this.getParams();for(var key in _1a){_19+=[key]+"=\""+_1a[key]+"\" ";}var _1c=this.getVariablePairs().join("&");if(_1c.length>0){_19+="flashvars=\""+_1c+"\"";}_19+="/>";}else{if(this.getAttribute("doExpressInstall")){this.addVariable("MMplayerType","ActiveX");this.setAttribute("swf",this.xiSWFPath);}_19="<object id=\""+this.getAttribute("id")+"\" classid=\"clsid:D27CDB6E-AE6D-11cf-96B8-444553540000\" width=\""+this.getAttribute("width")+"\" height=\""+this.getAttribute("height")+"\" style=\""+(this.getAttribute("style")||"")+"\">";_19+="<param name=\"movie\" value=\""+this.getAttribute("swf")+"\" />";var _1d=this.getParams();for(var key in _1d){_19+="<param name=\""+key+"\" value=\""+_1d[key]+"\" />";}var _1f=this.getVariablePairs().join("&");if(_1f.length>0){_19+="<param name=\"flashvars\" value=\""+_1f+"\" />";}_19+="</object>";}return _19;},write:function(_20){if(this.getAttribute("useExpressInstall")){var _21=new deconcept.PlayerVersion([6,0,65]);if(this.installedVer.versionIsValid(_21)&&!this.installedVer.versionIsValid(this.getAttribute("version"))){this.setAttribute("doExpressInstall",true);this.addVariable("MMredirectURL",escape(this.getAttribute("xiRedirectUrl")));document.title=document.title.slice(0,47)+" - Flash Player Installation";this.addVariable("MMdoctitle",document.title);}}if(this.skipDetect||this.getAttribute("doExpressInstall")||this.installedVer.versionIsValid(this.getAttribute("version"))){var n=(typeof _20=="string")?document.getElementById(_20):_20;n.innerHTML=this.getSWFHTML();return true;}else{if(this.getAttribute("redirectUrl")!=""){document.location.replace(this.getAttribute("redirectUrl"));}}return false;}};deconcept.SWFObjectUtil.getPlayerVersion=function(){var _23=new deconcept.PlayerVersion([0,0,0]);if(navigator.plugins&&navigator.mimeTypes.length){var x=navigator.plugins["Shockwave Flash"];if(x&&x.description){_23=new deconcept.PlayerVersion(x.description.replace(/([a-zA-Z]|\s)+/,"").replace(/(\s+r|\s+b[0-9]+)/,".").split("."));}}else{if(navigator.userAgent&&navigator.userAgent.indexOf("Windows CE")>=0){var axo=1;var _26=3;while(axo){try{_26++;axo=new ActiveXObject("ShockwaveFlash.ShockwaveFlash."+_26);_23=new deconcept.PlayerVersion([_26,0,0]);}catch(e){axo=null;}}}else{try{var axo=new ActiveXObject("ShockwaveFlash.ShockwaveFlash.7");}catch(e){try{var axo=new ActiveXObject("ShockwaveFlash.ShockwaveFlash.6");_23=new deconcept.PlayerVersion([6,0,21]);axo.AllowScriptAccess="always";}catch(e){if(_23.major==6){return _23;}}try{axo=new ActiveXObject("ShockwaveFlash.ShockwaveFlash");}catch(e){}}if(axo!=null){_23=new deconcept.PlayerVersion(axo.GetVariable("$version").split(" ")[1].split(","));}}}return _23;};deconcept.PlayerVersion=function(_29){this.major=_29[0]!=null?parseInt(_29[0]):0;this.minor=_29[1]!=null?parseInt(_29[1]):0;this.rev=_29[2]!=null?parseInt(_29[2]):0;};deconcept.PlayerVersion.prototype.versionIsValid=function(fv){if(this.major<fv.major){return false;}if(this.major>fv.major){return true;}if(this.minor<fv.minor){return false;}if(this.minor>fv.minor){return true;}if(this.rev<fv.rev){return false;}return true;};deconcept.util={getRequestParameter:function(_2b){var q=document.location.search||document.location.hash;if(_2b==null){return q;}if(q){var _2d=q.substring(1).split("&");for(var i=0;i<_2d.length;i++){if(_2d[i].substring(0,_2d[i].indexOf("="))==_2b){return _2d[i].substring((_2d[i].indexOf("=")+1));}}}return "";}};deconcept.SWFObjectUtil.cleanupSWFs=function(){var _2f=document.getElementsByTagName("OBJECT");for(var i=_2f.length-1;i>=0;i--){_2f[i].style.display="none";for(var x in _2f[i]){if(typeof _2f[i][x]=="function"){_2f[i][x]=function(){};}}}};if(!document.getElementById&&document.all){document.getElementById=function(id){return document.all[id];};}var getQueryParamValue=deconcept.util.getRequestParameter;var FlashObject=deconcept.SWFObject;var SWFObject=deconcept.SWFObject;

/* END: SWFObject v1.5.1                                               */
/***********************************************************************/
/*
 * END OF FILE - _resources/js/lib/common.js
 */

/*
 * START OF FILE - _resources/js/lib/core.js
 */
/****************************** Cummins' new web appearance script *****/
/****************************** Copyright (c) 2007-2010 Cummins AG *****/
/***********************************************************************/
/************************************************* javascript core *****/
/***********************************************************************/
/************************************** author virtual identity AG *****/
/* $LastChangedDate: 2010-02-02 13:30:40 +0100 (Di, 02 Feb 2010) $ *****/

// constants

var USE_FLASH_IN_HEADER = false; // could be overwritten in template

var WEBKIT_STYLESHEET_REFERENCE = RESOURCES_PATH + "css/styles-webkit.css";
var MACOS_STYLESHEET_REFERENCE  = RESOURCES_PATH + "css/styles-macos.css";
var SIFR_SLAB_PATH              = RESOURCES_PATH + "sifr/Cumminsslab.swf";

var LINK_REL_REGEX = /^jump-to-(.+)$/;

// global vars

var SIFR_IS_POSSIBLE = (typeof sIFR == "function" && !sIFR.UA.bIsIEMac && !sIFR.UA.bIsOpera && (!sIFR.UA.bIsWebKit || sIFR.UA.nWebKitVersion >= 100));

var zone     = {};   // global hash for standard zones
var pageType = null; // page type, set in initGlobals()

// serve special styles for Webkit and Mac OS

if (Info.browser.isWebkit) { document.write ('<link rel="stylesheet" type="text/css" media="screen,projection" href="' + WEBKIT_STYLESHEET_REFERENCE + '" />'); }
if (Info.os.isMac)         { document.write ('<link rel="stylesheet" type="text/css" media="screen,projection" href="' + MACOS_STYLESHEET_REFERENCE  + '" />'); }

/********************************************************************/
/* START: layout initalisation                                      */

var nwa = {
	version: "1.4.1"
};

// inits global vars as std zones and page type

function initGlobals() {
	var ptReg = /^page-type-/;
	pageType = $(document.body).classNames().find(function(className) {
		return ptReg.test(className);
	}).replace(ptReg, "");

	$A(["content", "header"]).each(function(zoneId) {
		try {
			var _zone = $(zoneId + "-zone");
			if (!_zone) {
				throw ("Implementation Exception: Zone " + zoneId + " is missing.");
			}
			zone[zoneId] = _zone;
		} catch (e) {
			alert(e);
			return false;
		}
	});
	return true;
}

// provides min-width/max-width for IE < 7

function initLayout_IEPre7() {

	var innerWidth = document.documentElement.clientWidth;
	if ($("toolbar-zone")) {
		if (innerWidth <= 996 && innerWidth > 982) {
			$("toolbar-zone").setStyle({"width": (innerWidth - 56) + "px"});
		} else if (innerWidth > 996) {
			$("toolbar-zone").setStyle({"width": "940px"});
		} else {
			$("toolbar-zone").setStyle({"width": "926px"});
		}
	}

	if (innerWidth > 960) {
		$("content-zone").style.width = "auto";
	} else {
		$("content-zone").style.width = "960px";
	}

	if ($("headervisual-zone") && $("fluid-zone") && pageType != "1" && pageType != "entry") {
		var realHeaderWidth = $("headervisual-zone").getWidth() + $("fluid-zone").getWidth();
		if (innerWidth <= realHeaderWidth) {
			var fluidWidth = $("fluid-zone").getWidth();
			var newWidth = innerWidth;
			newWidth = (newWidth - fluidWidth < 364) ? fluidWidth + 364 : newWidth;
			zone.header.setStyle({"width": newWidth + "px"});
		} else {
			zone.header.setStyle({"width": realHeaderWidth + "px"});
		}
	}
}

// inits sifr

function initLayout_sifr() {
	if (typeof sIFR != "function" || Info.browser.isOpera || Info.os.isLinux) { return; }

	sIFR.replaceElement(named({sSelector:"div.link-list span.sifr", sFlashSrc: SIFR_SLAB_PATH, sColor:"#666666", sHoverColor:"#990000", sWmode:"transparent"}));
	sIFR.replaceElement(named({sSelector:"div.sifr h3", sFlashSrc: SIFR_SLAB_PATH, sColor:"#333333", sWmode:"transparent"}));
	sIFR.replaceElement(named({sSelector:"div.sifr-h1 h1", sFlashSrc: SIFR_SLAB_PATH, sColor:"#333333", sWmode:"transparent"}));
	sIFR.replaceElement(named({sSelector:"div.sifr-h2 h2", sFlashSrc: SIFR_SLAB_PATH, sColor:"#999999", sWmode:"transparent"}));
	sIFR.replaceElement(named({sSelector:"div.sifr-header h1", sFlashSrc: SIFR_SLAB_PATH, sColor:"#ffffff", sWmode:"transparent"}));
	sIFR.replaceElement(named({sSelector:"div.sifr-header h2", sFlashSrc: SIFR_SLAB_PATH, sColor:"#ffffff", sWmode:"transparent"}));
	sIFR.replaceElement(named({sSelector:"div.sifr-header h3", sFlashSrc: SIFR_SLAB_PATH, sColor:"#ffffff", sHoverColor:"#990000", sWmode:"transparent"}));
}

nwa.SifrManager = {
	painted: false,
	paint: function(cSelector) {
		if(!SIFR_IS_POSSIBLE || !USE_SIFR) { return false; }
		if (this.painted) {
			if(cSelector){
				sIFR.rollback(named({sSelector:cSelector+" *"}));
			}else{
				sIFR.rollback();
			}
		}
		initLayout_sifr();
		this.painted = true;
	}
};

// replaces search button and generic buttons by a-elements to provide css styled buttons

function initLayout_buttons() {
	$$("button#site-search-button", "button.generic").each(function(elt) {
		if (!elt.form.id) {
			elt.form.id = Helper.getUniqueId();
		}

		linkAsButton = document.createElement('a');
		linkAsButton.className = (elt.id == "site-search-button") ? "search-button" : "generic-button";
		if ($(elt).hasClassName('inactive')) {
			$(linkAsButton).addClassName('inactive-generic-button');
			linkAsButton.observe('click', function(e){
				if($(this).hasClassName('inactive-generic-button')){
					e.stop();
				}
			 });
		}
		linkAsButton.href = "javascript:submitForm('" + elt.form.id + "');";

		if (elt.id == "site-search-button") {
			linkAsButton.innerHTML = $(elt).innerHTML;
		} else {
			linkAsButton.innerHTML = '<span><span>' + $(elt).innerHTML + '</span></span>';
		}

		$(elt).parentNode.replaceChild($(linkAsButton), $(elt));

	});
}
/* END: layout initalisation                                        */
/********************************************************************/
/* START: functional initalisation                                  */

function init_contentLayers() {
	if ($("toolbar-nav")) {
		$A($("toolbar-nav").getElementsByTagName("a")).each(
			function(trigger) {
				trigger = $(trigger);
				if (LINK_REL_REGEX.test(trigger.rel)) { // layer link
					var id = trigger.rel.replace(LINK_REL_REGEX, "$1");
					var node = $("toolbar-layer-" + id);
					new ContentLayer(node, trigger);
				} else { // standard link, probably external
					trigger.observe("click", function() {
						HeaderAnimation.animate = false;
						Layer.closeCurrent();
						HeaderAnimation.animate = true;
					}.bindAsEventListener(this));
				}
			}.bind(this)
		);
	}
}

function init_siteIdLayer(){
	if (Info.browser.isIEpre6) { return; }
	if ($("site-id-layer")) {
		trigger = $($("site-id").getElementsByTagName("a")[0]);
		new SiteIdLayer($("site-id-layer"), trigger);
	}
}

function init_siteExplorer() {
	if ($("site-explorer-layer")) {
		trigger = $($("sitemap-link").getElementsByTagName("a")[0]);
		SiteExplorer.layer = new SiteExplorerLayer($("site-explorer-layer"), trigger);
	}
}

function init_logoLinking(){
	var $logo = $('logo');
	if($logo) init_contentLayer2($logo);
}

function init_shareLayer(){
	var sharetrigger = [];
	if ($("pagetools-zone")) {
		sharetrigger = sharetrigger.concat($("pagetools-zone").select("a.share-trigger"))
	}
	
	if ($("pagetools-footer-zone")) {
		sharetrigger = sharetrigger.concat($("pagetools-footer-zone").select("a.share-trigger"))
	}
	
	sharetrigger.each(
		function(trigger) {
			trigger = $(trigger);
			if (LINK_REL_REGEX.test(trigger.rel)) { // layer link
				var id = trigger.rel.replace(LINK_REL_REGEX, "$1");
				var node = $("simple-layer-" + id);
				new ShareLayer(node, trigger);
			}
		}.bind(this)
	);
}

function init_contextLayer(){
	var contexttrigger = [];
	if ($("content-zone")) {
		contexttrigger = contexttrigger.concat($("content-zone").select("a.context-trigger"))
	}
	
	contexttrigger.each(
		function(trigger) {
			trigger = $(trigger);
			if (LINK_REL_REGEX.test(trigger.rel)) { // layer link
				var id = trigger.rel.replace(LINK_REL_REGEX, "$1");
				var node = $("context-layer-" + id);
				new ContextLayer(node, trigger);
			}
		}.bind(this)
	);
}

function init_countryLinks() {
	if(Info.browser.isIEpre6) { return; }
	var links = [];
	if ($("site-id-layer")) {
		links = links.concat($("site-id-layer").select("div.toolbar-content a"));
	}
	if ($("lightbox-layer-logo")) {
		links = links.concat($("lightbox-layer-logo").select("ul.countries a"));
	}
	
	if(links.length){
		//initalize list element hovers
		links.filter(function(link) {
			return !$(link).hasClassName('c');
		}).each(function(link) {
			link.observe("mouseover", function() {
				$(link).up("li").addClassName("hover");
			});
			link.observe("mouseout", function() {
				$(link).up("li").removeClassName("hover");
			});
		});

		// workaround for missing adjacent sibling combinator
		// ("ul.countries li a.c:hover + a" and "ul.countries li a.worldwide:hover + a") and "ul.countries li a.mobile:hover + a") support in IE6 and Safari3
		if(Info.browser.isIEpre7 || Info.browser.isSafari3) {
			links.filter(function(link) {
				return $(link).hasClassName("c") || $(link).hasClassName("worldwide") || $(link).hasClassName("mobile");
			}).each(function(link) {
				link.observe("mouseover", function() {
					$(link).next("a").addClassName("hover");
				});
				link.observe("mouseout", function() {
					$(link).next("a").removeClassName("hover");
				});
			});
		}
	}
}

/**
 * initialize dynamic lightbox-layer with opaque curtain
 * 1st usage:
 * - use <a class="content-layer-2-trigger">
 *   to trigger the lightbox
 * - use as next (or next of parent) element <div class="content-layer-2"> 
 *   to place the lightbox content
 * 2nd usage:
 * - use <a class="content-layer-2-trigger" rel="my-id-somewhere"> 
 *   to trigger the lightbox
 * - define an element <div class="content-layer-2" id="my-id-somewhere">
 *   to place the lightbox somewhere in the html structure you like
 * 
 * @author astirn
 * @see module.lightbox-layer.js
 */
function init_contentLayer2(_context) {
	var $context = _context || $('content-zone');
	if ($context && typeof LightboxLayer == 'function') {
		$context.select('a.content-layer-2-trigger').each(
			function(trigger) {
				trigger = $(trigger);
				if (trigger.next() && trigger.next('div.content-layer-2')) {
					var node = $(trigger.next());
				}
				if (trigger.up().next() && trigger.up().next('div.content-layer-2')) {
					var node = $(trigger.up().next());
				}
				if (trigger.rel && $(trigger.rel)) {
					var node = $(trigger.rel);
				}
				if (node) {
					new LightboxLayer(node, trigger);
				}
			}.bind(this)
		);
	}
}
/* END: functional initalisation                                    */
/********************************************************************/
/* START: "abstract" layer - do not instantiate                     */

var Layer = Class.create();

Layer.current = null;
Layer.toggle = false;

Layer.initialize = function() {
	var closeEvt = Info.browser.isFirefox && Info.os.isMac ? 'mousedown' : 'mouseup';
	Event.observe(document, closeEvt, function(e) {
		if (Layer.current) {
			var element   = $(Event.element(e));
			var layerNode = Layer.current.node;
			if (element != layerNode && !element.descendantOf(layerNode)) {
				Layer.closeCurrent();
			}
		}
	});

	var escapeKeyEvt = Info.browser.isWebkit ? 'keydown' : 'keypress';

	Event.observe(document, escapeKeyEvt, function(e) {
		if (Layer.current) {
			var code = e.keyCode;
			if (code == Event.KEY_ESC) {
				Layer.closeCurrent();
				Event.stop(e);
			} else {
				if (Layer.current.onkeydown) {
					Layer.current.onkeydown(e);
				}
			}
		}
	});
}

Layer.closeCurrent = function(newLayer) {
	if (Layer.current) {
		return Layer.current.close(newLayer);
		Layer.current = null;
	}
	return true; /* nothing to close, but ok */
}

Layer.prototype = {

	initialize: function() {
	},

	initSuper: function(node, trigger) {
		this.node   = node;
		this.isOpen = false;
		this.trigger = trigger || null;

		if (this.trigger) {
			this.trigger.observe("click", function(e) {
				this.toggle();
				nwa.SifrManager.paint('.lightbox-layer');
				Event.stop(e);
			}.bindAsEventListener(this));
			var closeEvt = Info.browser.isFirefox && Info.os.isMac ? 'mousedown' : 'mouseup';
			this.trigger.observe(closeEvt, function(e) { Event.stop(e); });
		}

		if (Info.browser.isIEpre7) {
			this.iframeLining = new IframeLining(this.node);
			this.correctIframe();
		}

	},

	open: function() {
		Layer.toggle = !!Layer.current;
		if (Layer.closeCurrent(this)) {
			if (this.beforeOpen()) {
				this.show();
				if (this.iframeLining) {
					this.iframeLining.show();
				}
				this.isOpen = true;
				Layer.current = this;
				Layer.toggle = false;
				this.afterOpen();
				return true;
			}
		}

		return false;
	},

	close: function(newLayer) {
		if (this.beforeClose(newLayer)) {
			if (this.iframeLining) {
				this.iframeLining.hide();
			}
			this.hide(newLayer);
			this.isOpen = false;
			Layer.current = null;
			this.afterClose(newLayer);
			return true;
		}
		return false;
	},

	toggle: function() {
		if (this.isOpen) {
			this.close();
		} else {
			this.open();
		}
	},

	afterClose: function() {},

	afterOpen: function() {},

	beforeClose: function() {
		return true;
	},

	beforeOpen: function() {
		return true;
	},

	correctIframe: function() {},

	hide: function(newLayer) {},

	show: function() {},

	superSetOffset: function(offset) {
		this.setOffset(offset);
		if (this.iframeLining) {
			this.iframeLining.refresh();
		}
	},

	setOffset: function(offset) {}

}

/* END: "abstract" layer                                            */
/********************************************************************/
/* START: content layer e. g. contact layer                         */

var ContentLayer = Class.create();

ContentLayer.prototype = Object.extend(new Layer, {

	initialize: function(node, trigger) {
		this.initSuper(node, trigger);

		var closeButton = Helper.getCloseButton(this.node);

		if (!closeButton) alert("Implementation Error: no close button found");

		closeButton.observe("click",
			function() {
				this.close();
			}.bindAsEventListener(this)
		);
	},

	afterClose: function(newLayer) {
		if (!Layer.toggle) {
			HeaderAnimation.augment();
		}
	},

	afterOpen: function() {
		HeaderAnimation.diminish();
	},

	beforeClose: function() {
		HeaderAnimation.unregisterLayer();
		return true;
	},

	beforeOpen: function() {
		HeaderAnimation.registerLayer(this);
		return true;
	},

	hide: function() {
		this.node.removeClassName("active-layer");
		this.trigger.up().removeClassName("active");
	},

	show: function() {
		this.node.addClassName("active-layer");
		this.trigger.up().addClassName("active");
		this.trigger.addClassName("clicked"); // avoids hover effect (only for the first time)
		this.trigger.observe("mouseout",
			function(e) {
				var elm = Event.findElement(e, "a");
				elm.removeClassName("clicked");
				elm.stopObserving("mouseout");
			}
		);
	},

	setOffset:function(offset) {
		this.node.style.top = offset + "px";
	}

});

/* END: content layer                                               */
/********************************************************************/
/* START: site id                                                   */

var SiteIdLayer = Class.create();

SiteIdLayer.prototype = Object.extend(new Layer, {

	initialize: function(node, trigger) {
		this.initSuper(node, trigger);
		var closeButton = Helper.getCloseButton(this.node);
		closeButton.observe("click", function(){this.close();}.bindAsEventListener(this));
	},

	afterClose: function(newLayer) {
		if (!Layer.toggle) {
			HeaderAnimation.augment();
		}
	},

	afterOpen: function() {
		HeaderAnimation.diminish();
	},

	beforeClose: function() {
		HeaderAnimation.unregisterLayer();
		return true;
	},

	beforeOpen: function() {
		HeaderAnimation.registerLayer(this);
		return true;
	},

	correctIframe: function() {
		this.iframeLining.correction.left = -1;
		this.iframeLining.correction.top = -1;
	},

	hide: function() {
		$("site-id-wrapper").removeClassName("active");
	},

	show: function() {
		$("site-id-wrapper").addClassName("active");
		this.trigger.addClassName("clicked"); // avoids hover effect (only for the first time)
		this.trigger.observe("mouseout",
			function(e) {
				this.trigger.removeClassName("clicked");
				this.trigger.stopObserving("mouseout");
			}.bindAsEventListener(this)
		);
	}

});

/* END: site id                                                     */
/********************************************************************/
/* START: site explorer                                             */

var SiteExplorer = Class.create();

SiteExplorer.getContent = function() {
	// overwrite this to implement a custom method
	alert("Implementation Error: SiteExplorer.getContent is missing");
}

SiteExplorer.registerEvents = function(contentNode) {
	$A(contentNode.getElementsByTagName("a")).each(function(link) {
		link = $(link);
		if (link.hasClassName("page")) {
			link.observe("click", function(e) {
				SiteExplorer.followLink(link);
			});
		} else {
			link.observe("click", function(e) {
				SiteExplorer.toggleSubtree(link);
			});
		}
	});
}

SiteExplorer.toggleSubtree = function(node) {
	if (node.up().hasClassName("expanded")) {
		SiteExplorer.collapseSubtree(node);
	} else {
		SiteExplorer.expandSubtree(node);
	}
}

SiteExplorer.followLink = function(linkNode) {
	// not implemented
}

SiteExplorer.expandSubtree = function(linkNode) {
	// not implemented
}

SiteExplorer.collapseSubtree = function(linkNode) {
	// not implemented
}

/* END: site explorer                                               */
/********************************************************************/
/* START: site explorer layer                                       */
var SiteExplorerLayer = Class.create();

SiteExplorerLayer.prototype = Object.extend(new Layer, {

	initialize: function(node, trigger) {
		this.initSuper(node, trigger);

		var closeButton = Helper.getCloseButton(this.node);

		closeButton.observe("click",
			function() {
				this.close();
			}.bindAsEventListener(this)
		);

		this.content     = null;
		this.contentNode = $(document.createElement("div"));
		this.node.appendChild(this.contentNode);
	},

	afterClose: function(newLayer) {
		if (!Layer.toggle) {
			HeaderAnimation.augment();
		}
	},

	afterOpen: function() {
		if (Info.browser.isIE) { // correct float bug in all IE versions
			Helper.getCloseButton(this.node).setStyle(
				{
					position: "absolute",
					left: (this.contentNode.getDimensions().width - 20) + "px"
				}
			);
		}
		HeaderAnimation.diminish();
	},

	beforeClose: function() {
		HeaderAnimation.unregisterLayer();
		return true;
	},

	beforeOpen: function() {
		this.getContent();
		HeaderAnimation.registerLayer(this);
		return true;
	},

	hide: function() {
		$("site-explorer").removeClassName("active");
	},

	show: function() {
		$("site-explorer").addClassName("active");
		this.trigger.addClassName("clicked"); // avoids hover effect (only for the first time)
		this.trigger.observe("mouseout",
			function(e) {
				this.trigger.removeClassName("clicked");
				this.trigger.stopObserving("mouseout");
			}.bindAsEventListener(this)
		);
	},

	getContent: function() {
		if (!this.content) {
			this.content = SiteExplorer.getContent();
			this.contentNode.innerHTML = this.content;
			SiteExplorer.registerEvents(this.contentNode);
		}
	}

});

/* END: site explorer layer                                         */
/********************************************************************/
/* START: simple layer                                              */

var SimpleLayer = Class.create();

SimpleLayer.prototype = Object.extend(new Layer, {

	initialize: function(node, trigger) {
		this.initSuper(node, trigger);

		this.initCloseButton();
	},

	initCloseButton: function(){
		var closeButton = Helper.getCloseButton(this.node);

		if (!closeButton) alert("Implementation Error: no close button found");

		closeButton.observe("click",
			function() {
				this.close();
			}.bindAsEventListener(this)
		);
	},
	
	hide: function() {
		this.node.removeClassName("active-layer");
		this.trigger.up().removeClassName("active");
	},

	show: function() {
		this.node.addClassName("active-layer");
		this.trigger.up().addClassName("active");
	}
	
});

/* END: simple layer                                                */
/********************************************************************/
/* START: context layer                                             */

var ContextLayer = Class.create(SimpleLayer, {

	initialize: function(node, trigger) {
		this.initSuper(node, trigger);
		this.initCloseButton();
	},

	getPosition: function(){
		var pos = $(this.trigger).cumulativeOffset();
		var dim = this.node.getDimensions();
		return {
			left: (pos[0] + $(this.trigger).getWidth() - dim.width - 1) + 'px', // subtract white border width
			top: (pos[1] + $(this.trigger).getHeight()) + 'px'
		}
	},
	
	beforeOpen: function(){
		var pos = this.getPosition();
		this.node.setStyle(pos);
		return true;
	}	
});

/* END: context layer                                               */
/********************************************************************/
/* START: share layer                                              */

var ShareLayer = Class.create(SimpleLayer, {

	initialize: function(node, trigger) {
		this.initSuper(node, trigger);
		this.initCloseButton();
	},

	getPosition: function(){
		if($(this.trigger).up('div#footer-zone')){

			var dim = this.node.getDimensions();
			
			var pos = $(this.trigger).cumulativeOffset();
			return {
				left: (pos[0] - dim.width - 5) + 'px',
				top: (pos[1] - dim.height + $(this.trigger).getHeight()) + 'px'
			}
		} else {
			var pos = $('pagetools-zone').cumulativeOffset();
			return {
				left: (pos[0] - 1) + 'px', // subtract white border width
				top: (pos[1] + $('pagetools-zone').getHeight() + 7) + 'px'
			}
		}
	},
	
	beforeOpen: function(){
		var pos = this.getPosition();
		this.node.setStyle(pos);
		return true;
	}	
});

/* END: simple layer                                                */
/********************************************************************/
/* START: header zone animation                                     */

var HeaderAnimation = Class.create();

HeaderAnimation.initialize = function() {
	this.layer         = null;
	this.slide         = {}
	this.animate       = true;
	this.augmented     = true;
	this.listenerQueue = new ListenerQueue('');

	if (pageType == "1" || pageType == "2" || pageType == "entry") {
		if (USE_FLASH_IN_HEADER && !Info.hasTransparencySupport) {
			// deactivate animation on browsers that have problems with wmode=transparent */
			this.diminishable = false;
		} else {
			this.diminishable = true;
		}
	} else {
		this.diminishable = false;
	}

	this.diminish      = (this.diminishable) ? this.diminish_393 : function() {};
	this.augment       = (this.diminishable) ? this.augment_393 : function() {};

	this.toolbarNode   = $("toolbar-nav");
	this.toolbarHeight = (this.toolbarNode) ? this.toolbarNode.up().getHeight() : 0;
}

HeaderAnimation.augment_393 = function() {
	if (!this.augmented) {
		this.listenerQueue.fire('augmentBegin');
		if (this.animate) {
			this._toggleAnimated([154, 174, 204, 244, 284, 324, 354, 385, 393]);
		} else {
			this._toggle(393);
		}
		this.augmented = true;
	}
}

HeaderAnimation.diminish_393 = function() {
	if (this.augmented) {
		this.listenerQueue.fire('diminishBegin');
		if (this.animate) {
			this._toggleAnimated([363, 313, 263, 213, 183, 163, 152, 144]);
		} else {
			this._toggle(144);
		}
		this.augmented = false;
		
	}
}

HeaderAnimation.registerLayer = function(layer) {
	this.layer = layer;
	if (this.layer) {
		this.layer.superSetOffset(this.toolbarHeight + Position.cumulativeOffset(this.toolbarNode)[1]);
	}
}

HeaderAnimation.unregisterLayer = function() {
	this.layer = null;
}

HeaderAnimation._toggle = function(offset) {
	this.diminished = !this.diminished;
	this._setOffsets(offset);
	if (this.diminished) {
		this.listenerQueue.fire('diminishDone');
	} else {
		this.listenerQueue.fire('augmentDone');
	}
}

HeaderAnimation._toggleAnimated = function(offsets) {
	this.slide.offsets = offsets;
	this.slide.length = offsets.length;
	this.slide.index = 1;

	this._toggle(this.slide.offsets[0]);
	new PeriodicalExecuter(function(pe) {
		if (this.slide.index >= this.slide.length) {
			this.diminished = !this.diminished;
			if (this.diminished) {
				this.listenerQueue.fire('diminishDone');
			} else {
				this.listenerQueue.fire('augmentDone');
			}
			pe.stop();
		} else {
			this._setOffsets(this.slide.offsets[this.slide.index]);
			this.slide.index++;
		}
	}.bind(this), .06);
}

HeaderAnimation._setOffsets = function(offset) {
	$(document.body).style.backgroundPosition = "0 " + (offset - 393) + "px";
	zone.header.style.height = offset + "px";
	if (this.layer) {
		this.layer.superSetOffset(this.toolbarHeight + 1 + offset);
	}
}

/* END: header zone animation                                       */
/********************************************************************/
/* START: Listener queue                                            */

ListenerQueue = Class.create();

ListenerQueue.prototype = {
	initialize: function(wildcardHandlerName) {
		this._listeners = [];
		this._wildcardHandlerName = wildcardHandlerName;
	},

	add: function(listener) {
		this._listeners.push(listener);
	},

	remove: function(listener) {
		var listeners = this._listeners;
		for (var i = 0; i < listeners.length; i++) {
			if (listeners[i] == listener) {
				listeners.splice(i, 1);
				break;
			}
		}
	},

	fire: function(handlerName, args) {
		var listeners = [].concat(this._listeners);
		for (var i = 0; i < listeners.length; i++) {
			var listener = listeners[i];
			try {
				listener[handlerName].call()
			} catch(e) {
				//if there is no handler with handlerName
				if (this._wildcardHandlerName)listener[this._wildcardHandlerName].call();
			}
		}
	}
}

/* END: Listener queue                                              */
/********************************************************************/
/* START: iframe to hide select elements in IE<7                    */

var IframeLining = Class.create();

IframeLining.prototype = {

	initialize: function(layer) {
		this.layer = layer;
		this.active = false;
		this.correction = {
			width:  0,
			height: 0,
			left:   0,
			top:    0
		} // since cumulativeOffset doesn't include borders

		this.div = $(document.createElement("div"));
		this.div.className = 'iframe-lining';
		this.div.innerHTML = '<iframe src="javascript:false" frameborder="0" style="position:absolute; left:0; top:0; width:100%; height:100%; margin:0; padding:0; background:transparent; filter:alpha(opacity=0);"></iframe>';
		this.hide();
		document.body.appendChild(this.div);
	},

	show: function() {
		this.refresh();
		this.div.show();
		this.active = true;
	},

	refresh: function() {
		var dimension = $(this.layer).getDimensions();
		var position  = Position.cumulativeOffset(this.layer);
		this.div.setStyle(
			{
				height: (dimension.height + this.correction.height) + "px",
				left:   (position[0] + this.correction.left)        + "px",
				top:    (position[1] + this.correction.top)         + "px",
				width:  (dimension.width + this.correction.width)   + "px",
				position: "absolute",
				zIndex:   "1"
			}
		);
	},

	setOffset: function(offset) {
		this.div.style.top = offset + "px";
	},

	hide: function() {
		this.div.hide();
		this.active = false;
	}
}

/* END: iframe to hide select elements in IE<7                      */
/********************************************************************/
/* START: xml loader to insert xml (transformed to html by xsl)     */

var XmlLoader = Class.create();

XmlLoader.prototype = {
	
	initialize: function() {},
	
	insertXslTransformation: function(node) {
		if (document.implementation && document.implementation.createDocument) { // code for std browsers
			xsltProcessor = new XSLTProcessor();
			xsltProcessor.importStylesheet(this.xsl);
			content = xsltProcessor.transformToFragment(this.xml, document);
			node.parentNode.insertBefore(content, node);
			return true;
		} else if (window.ActiveXObject) { // code for IE
			var content = this.xml.transformNode(this.xsl);
			node.insert({after: content});
			return true;
		}
		return false;
	},
	
	load: function() {
		that = this;
		that.loadXml(
			function(xml) {
				that.xml = xml.responseXML;
				that.loadXsl(
					function(xsl) {
						that.xsl = xsl.responseXML;
						that.display();
					}
				);
			}
		);
	},
	
	loadXml: function() {
		// overwrite this to implement a custom method
		alert("Implementation Error: XmlLoader.loadXml is missing");
	},
	
	loadXsl: function() {
		// overwrite this to implement a custom method
		alert("Implementation Error: XmlLoader.loadXsl is missing");
	}
}

/* END: xml loader to insert xml                                    */
/********************************************************************/
/* START: misc. helper                                              */

var Helper = Class.create();

Helper._uniqueIdInt = 0;

Helper.getUniqueId = function() {
	Helper._uniqueIdInt++;
	return "unique-" + Helper._uniqueIdInt;
}

Helper.getCloseButton = function(layer) {
	var closeButton = layer.down("div.close");
	if (!closeButton) { // could be IE<6
		$A(layer.getElementsByTagName("div")).each(
			function(div) {
				if ($(div).hasClassName("close")) {
					closeButton = $(div);
				}
			}
		);
	}
	return closeButton;
}

// a wrapper to show a nicer function call in the status bar

function submitForm(id) {
	$(id).submit();
}

function init_newWindow(domChunk) {
	$$(domChunk).each(function(element) {
		element.observe('click', function(e) {
			window.open(this.href);
			Event.stop(e);
		}.bindAsEventListener(element));
	});
}

/* END: misc. helper                                                */
/********************************************************************/

/*
 * END OF FILE - _resources/js/lib/core.js
 */

/*
 * START OF FILE - _resources/js/lib/module.breadcrumb.js
 */
/****************************** Cummins' new web appearance script *****/
/****************************** Copyright (c) 2007-2010 Cummins AG *****/
/***********************************************************************/
/*********************************************** module breadcrumb *****/
/***********************************************************************/
/************************************** author virtual identity AG *****/
/* $LastChangedDate: 2010-02-02 13:30:40 +0100 (Di, 02 Feb 2010) $ *****/

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

/*
 * END OF FILE - _resources/js/lib/module.breadcrumb.js
 */

/*
 * START OF FILE - _resources/js/lib/module.language-selector.js
 */
/****************************** Cummins' new web appearance script *****/
/****************************** Copyright (c) 2007-2010 Cummins AG *****/
/***********************************************************************/
/************************************ module LanguageSelectorLayer *****/
/***********************************************************************/
/************************************** author virtual identity AG *****/
/* $LastChangedDate: 2010-02-02 13:30:40 +0100 (Di, 02 Feb 2010) $ *****/

function init_languageSwitchLayer() {
	if ($("multi-language-switch")) {
		var trigger = $($("language-switch").getElementsByTagName("a")[0]);
		new LanguageSelectorLayer($("language-list"), trigger);
	}
}

var LanguageSelectorLayer = Class.create();

LanguageSelectorLayer.prototype = Object.extend(new Layer, {

	initialize: function(node, trigger) {
		this.initSuper(node, trigger);
	},

	afterClose: function(newLayer) {
		if (!Layer.toggle) {
			HeaderAnimation.augment();
		}
	},

	beforeClose: function() {
		HeaderAnimation.unregisterLayer();
		return true;
	},

	beforeOpen: function() {
		HeaderAnimation.registerLayer(this);
		return true;
	},

	correctIframe: function() {
		this.iframeLining.correction.left = -12;
	},

	hide: function() {
		$("multi-language-switch").removeClassName("active");
	},

	show: function() {
		$("multi-language-switch").addClassName("active");
		this.trigger.addClassName("clicked"); // avoids hover effect (only for the first time)
		this.trigger.observe("mouseout",
			function(e) {
				this.trigger.removeClassName("clicked");
				this.trigger.stopObserving("mouseout");
			}.bindAsEventListener(this)
		);
	}

});

/*
 * END OF FILE - _resources/js/lib/module.language-selector.js
 */

/*
 * START OF FILE - _resources/js/lib/module.mousewheel-observer.js
 */
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
/*
 * END OF FILE - _resources/js/lib/module.mousewheel-observer.js
 */

/*
 * START OF FILE - _resources/js/lib/module.gui.select.js
 */
/****************************** Cummins' new web appearance script *****/
/****************************** Copyright (c) 2007-2010 Cummins AG *****/
/***********************************************************************/
/*********************************************** module gui select *****/
/***********************************************************************/
/************************************** author virtual identity AG *****/
/* $LastChangedDate: 2010-02-02 13:30:40 +0100 (Di, 02 Feb 2010) $ *****/

/********************************************************************/
/* START: functional initalisation                                  */

function init_guiSelect(transformer) {

	// register events for IE<6

	if(Info.browser.isIEpre6) {
		// add onchange handler and abandon initialization
		var selects = document.getElementById('content-zone').getElementsByTagName("select");
		for (var i = 0, l = selects.length; i < l; ++i) {
			var select = selects[i];
			if (select.className.match(/\bgui-select\b/)) {
				select.onchange = function() {
					this.parentNode.parentNode.submit();
				}
			}
		}
		return;
	}
	
	// build and init gui select boxes

	GuiSelect.build($("content-zone").getElementsByTagName("select"), transformer);

	// init mousewheel

	var wheel = new MouseWheelObserver;
	wheel.register(
		function(value) {
			if (Layer.current && Layer.current.onmousescroll && Layer.current.scrollbar) {
				return Layer.current.onmousescroll(value);
			} else {
				return true;
			}
		}
	);
}

/* END: functional initalisation                                    */
/********************************************************************/
/* START: GuiSelect class                                           */

var GuiSelect = Class.create();

GuiSelect.sliderCounter = 0;

GuiSelect.build = function(elements, transformer, openImmediately) {
	transformer = transformer || GuiSelect.defaultTransformer;

	$A(elements).filter(
		function(select) {
			return $(select).hasClassName("gui-select");
		}).each(
		function(select) {
			select = $(select);

			// build html

			var options        = $A(select.getElementsByTagName('option'));
			var index          = select.selectedIndex;
			var titleClassName = (index) ? 'title selected-title' : 'title';
			var text           = transformer.replaceTitle(options[index].text);
			var id             = select.id + '-gui';
			var className      = select.className;
			var wrappedText    = (select.disabled) ? '<span class="a">' + text + '</span>' : '<a href="#">' + text + '</a>';

			var _html = '<div id="' + id + '" class="' + className + '"><p class="' + titleClassName + '">' + wrappedText + '</p>'
				+ '<input type="hidden" name="' + select.name + '" value="' + select.value + '" />'
				+ '<div class="content">';

			if (!select.disabled) {
				var isFirst = true;
				_html += '<ul>';
				options.each(
					function(option) {
						var classes = [];
						if (isFirst) {
							classes.push('first-child');
						}
						if (option.selected) {
							classes.push('selected');
						}
						var classSnippet = (classes.length) ? ' class="' + classes.join(' ') + '"' : '';

						var text = transformer.replaceOption(option.innerHTML);
						_html += '<li' + classSnippet +'><span class="value">' + option.value + '</span><a href="#">' + text + '</a></li>';
						isFirst = false;
					}
				);
				_html += '</ul>';
			}
			_html += '</div></div>';

			// replace html select element with new gui select element

			var wrapper = select.parentNode;
			Element.remove(select);
			wrapper.innerHTML += _html;

			if (select.disabled) {
				// if the select element is disabled, all is done
				return;
			}

			// init layer logic

			// optimized performance for IE6 and Cummins search

			var div        = $(id);
			var form       = $(div.parentNode.parentNode);
			var input      = $(div.childNodes[1]);
			var contentDiv = $(div.childNodes[2]);
			var linkList   = $(contentDiv.firstChild);
			var links      = $A(contentDiv.getElementsByTagName('a'));
			var titleLink  = $(div.firstChild.firstChild);
			this._clicked    = false;

			var that = this;
			var layer   = new GuiSelectLayer(contentDiv, titleLink);

			// add events to title

			titleLink.observe('mousedown', function(e) {
				that._clicked = true;
			});

			titleLink.observe('focus', function(e) {
				if (!that._clicked) {
					layer.open();
				}
			});

			titleLink.observe('mouseup', function(e) {
				that._clicked = false;
			});

			// add events to links to submit form with selected value

			links.each(function(a) {
				a = $(a);

				a.observe('focus', function(e) {
					$(this.parentNode).addClassName('active');
					if (layer != Layer.current) {
						layer.open();
						if (layer.scrollbar) {
							layer.scrollbar.scrollIntoView(a.up('li'));
						}
					}
				}.bindAsEventListener(a));

				a.observe('blur', function(e) {
					$(this.parentNode).removeClassName('active');
				}.bindAsEventListener(a));

				a.observe('mousemove', function(e) {
					if (layer.scrollbar) {
						if (!layer.scrollbar.slider.dragging) {
							a.focus();
						}
					} else {
						a.focus();
					}
				}.bindAsEventListener(a));

				a.observe('click', function(e) {
					if (this.up().hasClassName('selected')) {
						Layer.closeCurrent();
					} else {
						input.value = this.previous('span.value').innerHTML;
						form.submit();
					}
					Event.stop(e);
				}.bindAsEventListener(a));

				// show nicer link on status bar:
				a.href = '?' + input.name + '=' + encodeURIComponent(a.previousSibling.innerHTML);
			});

			// add scrollbar, if necessary

			if (linkList.getHeight() <= contentDiv.getHeight()) { // no slider needed

				contentDiv.style.height = 'auto'; // for IE6

			} else {

				GuiSelect.sliderCounter++;

				linkList.addClassName('has-scrollbar');

				/// add slider gui elements

				var sliderWrapper = document.createElement('div');
				sliderWrapper.className = 'slider-wrapper';
				sliderWrapper.innerHTML = '<div class="arrow-up"></div><div class="slider" id="slider' + GuiSelect.sliderCounter + '"><div class="handle" id="handle' + GuiSelect.sliderCounter + '"></div></div><div class="arrow-down"></div>';
				contentDiv.appendChild(sliderWrapper);

				// init scrollbar and add scrollbar object to layer

				layer.scrollbar = new GuiScrollbar("handle" + GuiSelect.sliderCounter, "slider" + GuiSelect.sliderCounter, layer);

				// add click events

				var elts = $(sliderWrapper).childElements();

				elts[0].observe("click", function() {
					layer.scrollbar.moveUp();
				});
				elts[2].observe("click", function() {
					layer.scrollbar.moveDown();
				});

				// add other events

				var handler = (Info.browser.isIE) ? "activate" : "focus";

				links.each(function(a) {
					$(a).observe(handler, function(e) {
						layer.scrollbar.scrollIntoView($(this).up("li"));
					}.bind(a));
				});

			}

			// open immediately if option is true

			if (openImmediately) {
				layer.open();
			}
		}
	);
}

GuiSelect.defaultTransformer = {
	replaceOption: Prototype.K,
	replaceTitle:  Prototype.K
}

/* END: GuiSelect class                                             */
/********************************************************************/
/* START: layer subclass for gui.select                             */

var GuiSelectLayer = Class.create();

GuiSelectLayer.prototype = Object.extend(new Layer, {

	list: null,
	scrollbar: null,

	initialize: function(node, trigger) {
		this.name = node.up().id.match(/^filter-(.*)-gui$/)[1];
		this.initSuper(node, trigger);
		this.list = node.firstChild;
	},
	
	afterClose: function() {
		this.node.up().removeClassName("active-gui-select");

		var activeFilter = $('active-filter-' + this.name);
		if (activeFilter) {
			activeFilter.show();
			var form = $('filter-form-' + this.name);
			if (form) {
				form.remove();
			}
		}

	},

	beforeOpen: function() {
		this.node.up().addClassName("active-gui-select");
		if (this.scrollbar) {
			this.scrollbar.setValue(0);
		}
		return true;
	},

	hide: function() {
		this.node.removeClassName("active-content");
	},

	onkeydown: function(e) {
		var ITEMS_PER_PAGE = 9;

		if (Info.browser.isOpera) {
			// no key handling, since preventing default actions doesn't work with Opera
			// see: http://www.quirksmode.org/dom/events/keys.html
			return;
		}

		var activeListElement = this.getListElement();

		switch (e.keyCode) {

			case Event.KEY_UP:
				if (activeListElement && activeListElement.previous()) {
					this.setListElement(activeListElement.previous());
				}
				Event.stop(e);
				break;

			case Event.KEY_DOWN:
				if (activeListElement && activeListElement.next()) {
					this.setListElement(activeListElement.next());
				} else if (!activeListElement) {
					this.setListElement(this.node.select('ul li').first());
				}
				Event.stop(e);
				break;

			case Event.KEY_PAGEUP:
				if (activeListElement) {
					if (activeListElement.previous(ITEMS_PER_PAGE - 1)) {
						this.setListElement(activeListElement.previous(ITEMS_PER_PAGE - 1));
					} else {
						this.setListElement(this.node.select('ul li').first());
					}
				} else {
					this.setListElement(this.node.select('ul li').first());
				}
				Event.stop(e);
				break;

			case Event.KEY_PAGEDOWN:
				if (activeListElement) {
					if (activeListElement.next(ITEMS_PER_PAGE - 1)) {
						this.setListElement(activeListElement.next(ITEMS_PER_PAGE - 1));
					} else {
						this.setListElement(this.node.select('ul li').last());
					}
				} else {
					var length = this.node.select('ul li').length;
					this.setListElement(this.node.select('ul li')[Math.min(ITEMS_PER_PAGE, length)]);
				}
				Event.stop(e);
				break;

			case Event.KEY_HOME:
				this.setListElement(this.node.select('ul li').first());
				Event.stop(e);
				break;

			case Event.KEY_END:
				this.setListElement(this.node.select('ul li').last());
				Event.stop(e);
				break;
		}
	},

	getListElement: function() {
		var activeListElement = this.node.select('ul li.active');
		return activeListElement.length ? activeListElement[0]: null;
	},

	setListElement: function(newListElement) {
		if (newListElement) {
			$(newListElement).down('a').focus();
			if (this.scrollbar) {
				this.scrollbar.scrollIntoView(newListElement);
			}
		}
	},

	onmousescroll: function(value) {
		var activeListElement = this.getListElement();
		if (value < 0) {
			this.scrollbar.moveUp();
			if (activeListElement && activeListElement.previous()) {
				this.setListElement(activeListElement.previous());
			}
		} else if (value > 0) {
			this.scrollbar.moveDown();
			if (activeListElement && activeListElement.next()) {
				this.setListElement(activeListElement.next());
			}
		}
		return false;
	},

	scrollTo: function(offsetTop) {
		this.list.style.top = offsetTop + "px";
	},

	show: function() {
		this.node.addClassName("active-content");
	}

});

/* END: layer subclass for gui.select                               */
/********************************************************************/
/* START: scrollbar class                                           */

var GuiScrollbar = Class.create();

GuiScrollbar.prototype = {

	initialize: function(handle, slider, layer) {

		this.outerHeight  = layer.node.getHeight();
		this.innerHeight  = $(layer.node.firstChild).getHeight();
		this.maxScroll    = this.innerHeight - this.outerHeight;
		this.itemHeight   = $(layer.node.firstChild.firstChild.nextSibling).getHeight();
		this.sliderHeight = $(slider).getHeight();

		$(handle).style.height = Math.round(this.sliderHeight * this.outerHeight / this.innerHeight) + "px";

		this.slider = new Control.Slider(handle, slider, {
			axis: 'vertical',
			range: $R(0, this.maxScroll),
			onSlide: function(value) {
				layer.scrollTo(-value);
			},
			onChange: function(value) {
				layer.scrollTo(-value);
			}
		});
	},

	moveDown: function() {
		this.setValue(this.slider.value + this.itemHeight);
	},

	moveUp: function() {
		this.setValue(this.slider.value - this.itemHeight);
	},

	scrollIntoView: function(liNode) {
		var offset = $(liNode).offsetTop;
		var minValue = this.slider.value;
		var maxValue = minValue + this.outerHeight;
		if (offset + this.itemHeight > maxValue) {
			this.setValue(offset + this.itemHeight - this.outerHeight);
		} else if (offset < minValue) {
			this.setValue(offset);
		}
	},

	setValue: function(value) {
		this.slider.setValue(value);
	}

}

/* END: scrollbar class                                             */
/********************************************************************/

/*
 * END OF FILE - _resources/js/lib/module.gui.select.js
 */

/*
 * START OF FILE - _resources/js/lib/module.autocomplete.js
 */
var AutoCompleteXmlLoader = Class.create();

AutoCompleteXmlLoader.prototype = Object.extend(new XmlLoader, {

	initialize: function(layer) {
		this.layer = layer;
	},
	
	display: function() {
		var layer = this.layer;
		this.layer.node.innerHTML = '<span></span>';
		this.insertXslTransformation($(this.layer.node.firstChild));
		var ul = $(this.layer.node.down('ul'));
		if (ul) { // a list exists and the suggest box will be displayed
			var links = $A(ul.getElementsByTagName('a'));
			// add events to links to submit form with selected value
			links.each(function(a) {
				a = $(a);

				a.observe('focus', function(e) {
					$(this.parentNode).addClassName('active');
				}.bindAsEventListener(a));

				a.observe('blur', function(e) {
					$(this.parentNode).removeClassName('active');
				}.bindAsEventListener(a));

				window.setTimeout(function() { // delayed call to prevent focussing instantly after opening
					a.observe('mouseover', function(e) {
						a.focus();
					}.bindAsEventListener(a));
				}, 200);

				a.observe('mousemove', function(e) {
					a.focus();
				}.bindAsEventListener(a));

				a.observe('click', function(e) {
					layer.close();
					layer.setInput(a.innerHTML);
					layer.form.submit();
				}.bindAsEventListener(a));
			});
			this.layer.open();
		} else {
			this.layer.close();
		}
	}
});

var AutoCompleteLayer = Class.create();

AutoCompleteLayer.prototype = Object.extend(new Layer, {
	
	initialize: function(input) {
		
		if(Info.browser.isIEpre6) return;

		this.input     = input;
		this.form      = this.input.up('form');
		this.content   = null;
		this.lastValue = this.input.value;
		this.xmlLoader = new AutoCompleteXmlLoader(this);
		
		this.node = document.createElement('div');
		this.node.className = 'autocomplete-wrapper';
		
		input.insert({after: $(this.node)})
		input.setAttribute('autocomplete', 'off');
		
		this.node.observe('keydown', function(e) {
			this.onkeydown(e);
		}.bindAsEventListener(this));

		input.observe('keydown', function(e) {
			this.onkeydown(e);
		}.bindAsEventListener(this));

		input.observe('keyup', function(e) {
			if (this.input.value != this.lastValue) { // input value changed
				this.lastValue = this.input.value;
				if (this.input.value) {
					this.xmlLoader.load();
				} else {
					this.close();
				}
			}
		}.bindAsEventListener(this));
	},
	
	onkeydown: function(e) {
		if(Info.browser.isOpera) return; // no key handling, since preventing default actions seems to be impossible in Opera

		if(this.isOpen) {
			var activeListElement = this.node.select('ul li.active');
			activeListElement = activeListElement.length ? activeListElement[0]: null;
			var newListElement = null;

			switch (e.keyCode) {
				case Event.KEY_UP:
					if (activeListElement && activeListElement.previous()) {
						newListElement = activeListElement.previous();
					}
					Event.stop(e);
					break;
				
				case Event.KEY_DOWN:
					if (activeListElement && activeListElement.next()) {
						newListElement = activeListElement.next();
					} else if (!activeListElement) {
						newListElement = this.node.select('ul li').first();
					}
					Event.stop(e);
					break;
					
				case Event.KEY_HOME:
					newListElement = this.node.select('ul li').first();
					Event.stop(e);
					break;

				case Event.KEY_END:
					newListElement = this.node.select('ul li').last();
					Event.stop(e);
					break;
					
				case Event.KEY_ESC:
					this.input.focus();
					this.close();
					Event.stop(e);
					break;
			}
			
			if (newListElement) {
				$(newListElement).down('a').focus();
				this.setInput(newListElement.down('a').innerHTML);
			}
		} else {
			if (e.keyCode == Event.KEY_DOWN) {
				if (this.input.value) {
					this.xmlLoader.load();
				}
			}
		}
	},
	
	setInput: function(value) {
		this.input.value = value;
		this.lastValue = value;
	},
	
	show: function() {
		this.node.setStyle({'display':'block'});
	},

	hide: function() {
		this.node.setStyle({'display':'none'});
	}

});

/*
 * END OF FILE - _resources/js/lib/module.autocomplete.js
 */

/*
 * START OF FILE - _resources/js/lib/module.lightbox-layer.js
 */
/****************************** Cummins' new web appearance script *****/
/****************************** Copyright (c) 2007-2010 Cummins AG *****/
/***********************************************************************/
/******************************************** module LightboxLayer *****/
/***********************************************************************/
/************************************** author virtual identity AG *****/
/* $LastChangedDate: 2010-02-02 13:30:40 +0100 (Di, 02 Feb 2010) $ *****/

var LightboxLayer = Class.create();

LightboxLayer.prototype = Object.extend(new Layer, {

	initialize: function(node, trigger) {
		this.node = node;
		this.initSuper(node, trigger);
		var closeButton = Helper.getCloseButton(this.node);

		closeButton.observe("click", function(){this.close();}.bindAsEventListener(this));

		trigger.href = 'javascript:void(0);';

		//create the curtain
		new Insertion.Before($('header-zone'), '<div id="lightbox-curtain">&nbsp;</div>');
		this.curtain = $('lightbox-curtain');
		this.resizeCurtain();

		if (Info.browser.isIEpre7) {
			this.iframeLining = new IframeLining(this.curtain);
		}

		//store the listener so it can be accessed by functions add and remove
		this.listener = {'augmentDone' : this.handleOpen.bind(this) };

		//add an event handler to resize the curtain when window is resized
		Event.observe(window, "resize", function(){this.resizeCurtain();}.bindAsEventListener(this));
	},

	open: function() {
		if(HeaderAnimation.diminishable && !HeaderAnimation.augmented) {
			HeaderAnimation.listenerQueue.add(this.listener);
			HeaderAnimation.augment();
		} else {
			Layer.prototype.open.call(this);
		}
	},

	show: function() {
		this.node.setStyle({'display': 'block'});
	},

	hide: function() {
		this.node.setStyle({'display': 'none'});
	},

	beforeOpen: function() {
		//calculate header height
		if(!this.headerHeight) this.headerHeight = $('header-zone').getDimensions().height + $('toolbar-zone').getDimensions().height;

		//position the curtain
		this.curtain.setStyle({'top': '0px'});

		//calculate layer position
		this.node.setStyle({'display': 'block'});
		if(!this.nodeTop) this.nodeTop = parseInt(this.node.getStyle('top'));
		if(!this.nodeLeft) this.nodeLeft = parseInt(this.node.getStyle('left'));
		if(!this.nodeHeight) this.nodeHeight = this.node.getDimensions().height;
		this.node.setStyle({'display': 'none'});

		//check whether layer is higher than current page
		var wrapper = $('footer-position-wrapper');
		this.diff = parseInt(wrapper.getDimensions().height) - this.nodeTop - this.nodeHeight;
		if(this.diff < 0) {
			//resize the content zone
			var layerOccupation = this.nodeHeight + this.nodeTop;
			layerOccupation = layerOccupation - this.headerHeight;

			//difference between layer height and content height
			var diff2 = $('content-zone').getDimensions().height - layerOccupation;
			$('content-zone').setStyle({'height': $('content-zone').getDimensions().height - diff2 + 'px'});
		}
		this.resizeCurtain();

		this.node.setStyle({'top': this.nodeTop+'px'});
		if(this.iframeLining)
			this.iframeLining.show();
		this.curtain.setStyle({'display': 'block'});
		return true;
	},

	beforeClose: function() {
		this.curtain.setStyle({'display': 'none'});
		if(this.iframeLining)
			this.iframeLining.hide();
		if(this.diff < 0) {
			if(Info.browser.isIE) {
				$('content-zone').setStyle({'height':'1%'});
			} else {
				$('content-zone').setStyle({'height':'auto'});
			}
		}
		return true;
	},

	handleOpen: function() {
		HeaderAnimation.listenerQueue.remove(this.listener);
		Layer.prototype.open.call(this);
	},

	resizeCurtain: function() {
		if(this.curtain) {
			var wrapper = $('footer-position-wrapper').getDimensions();
			this.contentHeight = parseInt(wrapper.height);
			this.contentWidth = $('content-zone').getDimensions().width;
			//982px is toolbar min-width (926px) plus its margins (34px left + 22px right)
			if(this.contentWidth < 982) {
				this.contentWidth = 982;
			}
			this.curtain.setStyle({'height': this.contentHeight+'px','width': this.contentWidth+'px'});
			if(this.iframeLining) {
				this.iframeLining.refresh();
			}
		}
	}
});

/*
 * END OF FILE - _resources/js/lib/module.lightbox-layer.js
 */

/*
 * START OF FILE - _resources/js/lib/module.search.js
 */
/****************************** Cummins' new web appearance script *****/
/****************************** Copyright (c) 2007-2010 Cummins AG *****/
/***********************************************************************/
/*************************************************** module search *****/
/***********************************************************************/
/************************************** author virtual identity AG *****/
/* $LastChangedDate: 2010-02-02 13:30:40 +0100 (Di, 02 Feb 2010) $ *****/

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

/*
 * END OF FILE - _resources/js/lib/module.search.js
 */