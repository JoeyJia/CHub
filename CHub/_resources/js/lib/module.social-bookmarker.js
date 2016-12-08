/****************************** Cummins' new web appearance script *****/
/****************************** Copyright (c) 2007-2010 Cummins AG *****/
/***********************************************************************/
/************************************ module LanguageSelectorLayer *****/
/***********************************************************************/
/************************************** author virtual identity AG *****/
/* $LastChangedDate: 2010-01-12 16:33:21 +0100 (Di, 12 Jan 2010) $ *****/

/***********************************************************************/
/* START: SocialBookmarker class */

// constructor
var SocialBookmarker = Class.create({
  
	initialize: function(node, options) {
		if(!node) { return false; }
		this.settings = Object.extend({}, SocialBookmarker._defaults, options);
		this.node = $(node);

		this.matchRegex = new RegExp('(^' + this.settings.matchPrefix + '|\s' + this.settings.matchPrefix + ')([-a-zA-Z0-9]+)');
		
		var that = this;		
		this.node.observe('click', function(e){
			var linkNode = e.element();
			if(linkNode && linkNode.className){
				that.bookmark(that.extractKey(linkNode.className));
				Event.stop(e);
			}
		});
  },
	
	bookmark: function(serviceKey){
			var service = this.settings.services[serviceKey];
			if (!service) return;

			//build bookmark url and open it in a new window
			var strParamUrl = service.paramUrl ? service.paramUrl + '=' + encodeURIComponent(location.href) : '';
			var strParamTitle = service.paramTitle ? service.paramTitle + '=' + encodeURIComponent(document.title) : '';

			var strUrl = (strParamUrl &&  strParamTitle) ? service.serviceUrl + '?' + strParamUrl + '&' + strParamTitle : service.serviceUrl + '?' + strParamUrl + strParamTitle;

			if (service.preventNewWindow) {
				this.href = strUrl;
			} else {
				window.open(strUrl)
			}

	},
	
  addServices: function(services) {
    if(!services){ return false; }
		Object.extend(this.settings.services, services);
  },
	
	extractKey: function(classNames) {
		return classNames.replace(this.matchRegex, '$2');
	}

});


// static method and properties
Object.extend(SocialBookmarker, {
	_defaults :
	{
		linkSelector: 'a.share',
		
		matchPrefix: 'share-',

		services: {
			delicious: {
				serviceUrl: 'http://delicious.com/post',
				paramUrl: 'url',
				paramTitle: 'title'
			},
			
			digg: {
				serviceUrl: 'http://digg.com/submit',
				paramUrl: 'phase=2&url',
				paramTitle: 'title'
			},
			
			facebook: {
				serviceUrl: 'http://www.facebook.com/share.php',
				paramUrl: 'u',
				paramTitle: 't'
			},
			
			faves: {
				serviceUrl: 'http://faves.com/Authoring.aspx',
				paramUrl: 'u',
				paramTitle: 'title'
			},
			
			friendfeed: {
				serviceUrl: 'http://friendfeed.com/',
				paramUrl: 'url',
				paramTitle: ''
			},
			
			googlebookmarks: {
				serviceUrl: 'http://www.google.com/bookmarks/mark',
				paramUrl: 'op=add&bkmk',
				paramTitle: 'title'
			},
			
			linkagogo: {
				serviceUrl: 'http://www.linkagogo.com/go/AddNoPopup',
				paramUrl: 'url',
				paramTitle: 'title'
			},
			
			linkedin: {
				serviceUrl: 'http://www.linkedin.com/shareArticle',
				paramUrl: 'mini=true&url',
				paramTitle: 'title'
			},
			
			mrwong: {
				serviceUrl: 'http://www.mister-wong.com/index.php',
				paramUrl: 'action=addurl&bm_url',
				paramTitle: 'bm_description'
			},
			
			myspace: {
				serviceUrl: 'http://www.myspace.com/Modules/PostTo/Pages/',
				paramUrl: 'u',
				paramTitle: 't'
			},
			
			newsvine: {
				serviceUrl: 'http://www.newsvine.com/_wine/save',
				paramUrl: 'popoff=0&u',
				paramTitle: 'h'
			},
			
			oneview: {
				serviceUrl: 'http://www.oneview.com/quickadd/neu/addBookmark.jsf',
				paramUrl: 'URL',
				paramTitle: 'title'
			},
			
			propeller: {
				serviceUrl: 'http://www.propeller.com/submit/',
				paramUrl: 'U',
				paramTitle: ''
			},
			
			reddit: {
				serviceUrl: 'http://de.reddit.com/submit',
				paramUrl: 'url',
				paramTitle: ''
			},
			
			simpy: {
				serviceUrl: 'http://simpy.com/simpy/LinkAdd.do',
				paramUrl: 'href',
				paramTitle: 'note'
			},
			
			stumbleupon: {
				serviceUrl: 'http://www.stumbleupon.com/submit',
				paramUrl: 'url',
				paramTitle: 'title'
			},
			
			twitter: {
				serviceUrl: 'http://twitter.com/home/',
				paramUrl: 'status',
				paramTitle: ''
			},
			
			xing: {
				serviceUrl: 'http://www.xing.com',
				paramUrl: '',
				paramTitle: ''
			},
			
			yahoobookmarks: {
				serviceUrl: 'http://bookmarks.yahoo.com/toolbar/savebm',
				paramUrl: 'u',
				paramTitle: 't'
			},
			
			yigg: {
				serviceUrl: 'http://yigg.de/neu',
				paramUrl: 'exturl',
				paramTitle: 'extdesc'
			},
			
			email: {
				serviceUrl: 'mailto:?subject=Cummins%20-%20Recommendation&body=The%20following%20page%20from%20the%20Cummins%20website%20has%20been%20recommended%20to%20you%3A%0B%0D%0A' + location.href,
				paramUrl: '',
				paramTitle: '',
				preventNewWindow: true
			}
		}
	}
});

/* END: SocialBookmarker class */
/***********************************************************************/



function init_socialBookmarker() {
	if ($('simple-layer-share')) {
		var sb = new SocialBookmarker($('simple-layer-share'));
	}
}

document.observe('dom:loaded', function() {
	init_socialBookmarker();
});


/*

http://del.icio.us/post?url=http://w1.Cummins.com/about/en/"
http://yigg.de/neu?exturl=http://w1.Cummins.com/about/en/&amp;extdesc=Cummins.com"
http://www.linkagogo.com/go/AddNoPopup?title=Cummins.com&amp;url=http://w1.Cummins.com/about/en/"
http://www.mister-wong.com/index.php?action=addurl&amp;bm_url=http://w1.Cummins.com/about/en/&amp;bm_description=Cummins.com"
http://www.oneview.com/quickadd/neu/addBookmark.jsf?URL=http://w1.Cummins.com/about/en/&amp;title=Cummins.com"
http://www.stumbleupon.com/submit?url=http://w1.Cummins.com/about/en/&amp;title=Cummins.com"
http://faves.com/Authoring.aspx?u=http://w1.Cummins.com/about/en/&amp;title=Cummins.com"
http://www.facebook.com/share.php?u=http://w1.Cummins.com/about/en/&amp;t=Cummins.com"
http://www.xing.com"
http://www.myspace.com/Modules/PostTo/Pages/?t=Cummins.com&amp;u=http://w1.Cummins.com/about/en/&amp;l=5"
http://friendfeed.com/?url=http://w1.Cummins.com/about/en/"
http://www.linkedin.com/shareArticle?mini=true&amp;url=http://w1.Cummins.com/about/en/&amp;title=Cummins.com"
http://digg.com/submit?phase=2&amp;url=http://w1.Cummins.com/about/en/&amp;title=Cummins.com"
http://www.newsvine.com/_wine/save?popoff=0&amp;u=http://w1.Cummins.com/about/en/&amp;h=Cummins.com"
http://www.propeller.com/submit/?U=http://w1.Cummins.com/about/en/"
http://de.reddit.com/submit?url=http://w1.Cummins.com/about/en/"
http://twitter.com/home/?status=http://w1.Cummins.com/about/en/"

*/

