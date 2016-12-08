<?xml version="1.0"?>
<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">

	<xsl:output method="html" omit-xml-declaration="yes"/>
	
	<xsl:variable name="filterId"><xsl:value-of select="/ResultSet/additionalInfo/id"/></xsl:variable>
	<xsl:variable name="currentNavigator" select="/ResultSet/Engine[@SubSeName = 'Navigators']/Navigator[@Param = $filterId]" />

	<xsl:template match="/ResultSet">
		<form action="{additionalInfo/action}" id="filter-form-{$filterId}">
			<fieldset class="filter">
				<legend class="access"><xsl:value-of select="additionalInfo/title"/></legend>
				<label class="access" for="filter-{$filterId}"><xsl:value-of select="$currentNavigator/@Name"/></label>
				<select name="{$filterId}" id="filter-{$filterId}" class="gui-select">
					<xsl:for-each select="$currentNavigator/Element">
						<option value="{@MixIn}">
							<xsl:if test="@MixIn = /ResultSet/additionalInfo/selectedFilterMixin">
								<xsl:attribute name="selected">selected</xsl:attribute>
							</xsl:if>
							<xsl:value-of select="@Display"/>
							<xsl:text> (</xsl:text>
							<xsl:value-of select="@Value"/>
							<xsl:text> </xsl:text>
							<xsl:value-of select="/ResultSet/additionalInfo/results"/>
							<xsl:text>)</xsl:text>
						</option>
					</xsl:for-each>
				</select>
				<!-- use hidden fields for additional data that is independent from the <select> -->
				<input type="hidden" name="example" value="foo" />
				<button type="submit" class="generic"><xsl:value-of select="additionalInfo/filter"/></button>
			</fieldset>
		</form>
	</xsl:template>

</xsl:stylesheet>
