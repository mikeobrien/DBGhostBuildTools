<xsl:stylesheet
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">

    <xsl:output method="html"/>

    <xsl:template match="/dbGhost">
    
		<style>
			td 
			{
				font-family: Verdana;
			}
			
			.sectionheader
			{
				font-size: 10pt;
				font-weight: bold;
				color: #ffffff;
			}
			
			.details 
			{
				font-size: 8pt;
			}
			
			.object
			{
				font-size: 8pt;
			}
			
			.error
			{
				font-size: 10pt;
				color: red;
			}
			
		</style>
	
        <table class="section-table" cellpadding="2" cellspacing="0" border="0" width="98%">
    
			<xsl:if test="DBGhost/Scripter and report/scripter">
			
				<tr>
					<td class="sectionheader" colspan="5" style="background-color:#37569A">
						Scripter (<xsl:value-of select="DBGhost/Scripter/DatabaseToScript/Server" />\<xsl:value-of select="DBGhost/Scripter/DatabaseToScript/Database" />)
					</td>
				</tr>        

				<tr>
					<td class="details" colspan="5" style="color:black;background-color:#c0c0c0">
						Duration: <xsl:value-of select="report/scripter/@duration" /><br/>
						Total Scripts: <xsl:value-of select="count(report/scripter/script)" />
					</td>
				</tr>
				
			</xsl:if>        

			<xsl:if test="DBGhost/ChangeManager and report/builder/error">

				<tr>
					<td class="sectionheader" colspan="5" style="background-color:#37569A">
						Builder (<xsl:value-of select="DBGhost/ChangeManager/TargetDB/DBServer" />\<xsl:value-of select="DBGhost/ChangeManager/BuildDBName" />)
					</td>
				</tr>        

				<tr>
					<td class="details" colspan="5" style="color:black;background-color:#c0c0c0">
						Duration: <xsl:value-of select="report/builder/@duration" /><br/>
						Total Objects: <xsl:value-of select="count(report/builder/script)" />
					</td>
				</tr>    

				<xsl:apply-templates select="report/builder/error" />
				
			</xsl:if>    

			<xsl:if test="DBGhost/ChangeManager and report/compare/object">

				<tr>
					<td class="sectionheader" colspan="5" style="background-color:#37569A">
						Compare (<xsl:value-of select="DBGhost/ChangeManager/SourceDB/DBServer" />\<xsl:value-of select="DBGhost/ChangeManager/SourceDB/DBName" /> => <xsl:value-of select="DBGhost/ChangeManager/TargetDB/DBServer" />\<xsl:value-of select="DBGhost/ChangeManager/TargetDB/DBName" />)
					</td>
				</tr>        

				<tr>
					<td class="details" colspan="5" style="color:black;background-color:#c0c0c0">
						Duration: <xsl:value-of select="report/compare/@duration" /><br/>
						Total Objects: <xsl:value-of select="count(report/compare/object)" />
					</td>
				</tr>    
				
				<xsl:apply-templates select="report/compare/object" />

				<xsl:apply-templates select="report/compare/error" />
				
			</xsl:if>  
 
		</table> 

    </xsl:template>
    
   <xsl:template match="object">
    
        <tr>
            <td class="object" colspan="5" style="color:black;background-color:#ffffff;">
                <xsl:value-of select="."></xsl:value-of><br/>
            </td>
        </tr>        

    </xsl:template>
    
   <xsl:template match="error">
    
        <tr>
            <td class="error" colspan="5" style="color:red;background-color:#ffffff;border-color:#000000;border-style:solid;border-top-width:1px;border-left-width:0px;border-right-width:0px;border-bottom-width:0px;">
                <xsl:apply-templates select="message" />
            </td>
        </tr>        

    </xsl:template>
    
   <xsl:template match="message">
    
        <xsl:value-of select="."></xsl:value-of><br/>

    </xsl:template>
    
</xsl:stylesheet>
