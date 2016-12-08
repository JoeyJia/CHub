// overwrites the same method in script.js

SiteExplorer.getContent = function() {

	return '<ul>'
		+ '	<li><a href="HomePage.aspx" class="page">Home</a></li>'
		+'	<li class="collapsed">'
		+'		<a href="javascript:void(0);">Item II</a>'
		+'		<ul>'
		+'			<li><a href="javascript:void(0);" class="page">Item II.1</a></li>'
		+'			<li class="collapsed">'
		+'				<a href="javascript:void(0);">Item II.2</a>'
		+'				<ul>'
		+'					<li><a href="javascript:void(0);" class="page">Item II.2.1</a></li>'
		+'					<li class="collapsed">'
		+'						<a href="javascript:void(0);">Item II.2.2</a>'
		+'						<ul>'
		+'							<li><a href="javascript:void(0);" class="page">Item II.2.2.a</a></li>'
		+'							<li><a href="javascript:void(0);" class="page">Item II.2.2.b</a></li>'
		+'							<li><a href="javascript:void(0);" class="page">Item II.2.2.c</a></li>'
		+'						</ul>'
		+'					</li>'
		+'					<li><a href="javascript:void(0);" class="page">Item II.2.3</a></li>'
		+'				</ul>'
		+'			</li>'
		+'			<li><a href="javascript:void(0);" class="page">Item II.3</a></li>'
		+'			<li><a href="javascript:void(0);" class="page">Item II.4</a></li>'
		+'		</ul>'
		+'	</li>'
		+'	<li class="expanded">'
		+'		<a class="expanded" href="javascript:void(0);">Item III</a>'
		+'		<ul>'
		+'			<li><a href="javascript:void(0);" class="page">Item III.1</a></li>'
		+'			<li class="active"><span class="active">Item III.2 is active</span></li>'
		+'			<li><a href="javascript:void(0);" class="page">Item III.3</a></li>'
		+'		</ul>'
		+'	</li>'
		+'	<li class="collapsed">'
		+'		<a href="javascript:void(0);">Item IV</a>'
		+'		<ul>'
		+'			<li><a href="javascript:void(0);" class="page">Item IV.1</a></li>'
		+'			<li><a href="javascript:void(0);" class="page">Item IV.2: A tum byssus digredior. Per Caetera deduco gero pertinaciter.</a></li>'
		+'			<li><a href="javascript:void(0);" class="page">Item IV.3</a></li>'
		+'		</ul>'
		+'	</li>'
		+'</ul>'
	;
}

SiteExplorer.expandSubtree = function(linkNode) {
	linkNode.addClassName("expanded");
	linkNode.up().removeClassName("collapsed");
	linkNode.up().addClassName("expanded");
	SiteExplorer.layer.afterOpen();
}

SiteExplorer.collapseSubtree = function(linkNode) {
	linkNode.removeClassName("expanded");
	linkNode.up().addClassName("collapsed");
	linkNode.up().removeClassName("expanded");
	SiteExplorer.layer.afterOpen();
}

SiteExplorer.followLink = function(linkNode) {
	if (linkNode.innerHTML != "Home") {
		alert("clicked link: " + linkNode.innerHTML + "\nThe Site Explorer will be closed.");
		Layer.closeCurrent();
	}
}
