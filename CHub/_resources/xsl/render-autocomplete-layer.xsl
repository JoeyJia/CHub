<?xml version="1.0"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">

	<xsl:output method="html" omit-xml-declaration="yes"/>

	<xsl:template match="/ResultSet/Engine">
		<xsl:if test="@NumResults &gt; 0">
			<ul>
				<xsl:for-each select="QueryHelp">
					<li>
						<a href="{Recommendations/Recommendation[position() = 1]/Link}"><xsl:value-of select="Entry/text()"/></a>
					</li>
				</xsl:for-each>
			</ul>
		</xsl:if>
	</xsl:template>

</xsl:stylesheet>
