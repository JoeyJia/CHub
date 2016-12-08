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
/* $LastChangedDate: 2010-02-02 13:16:56 +0100 (Di, 02 Feb 2010) $ *****/
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