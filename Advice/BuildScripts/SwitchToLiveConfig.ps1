param(	[string] $pool="advicev3_pool",
		[string] $forceServers = "")

Import-Module WebAdministration

###################################################################################################
# 			        																			  #
###################################################################################################
function GetOfflineServers( ) 
{ 
	$servers = @()
	
	if( [string]::isnullorempty( $forceServers ) )
	{
		# bodge to handle new query app. don't want to stop using pool as parameter, but new query app 
		# returns for all pools so split them by server name prefix. might be better to switch to passing 
		# in servername prefix, but ideally query app should have pool as a parameter
		if( ($pool -eq "advice_pool") -or ($pool -eq "advice_internal") )
		{
			$prefix = "pbsadviceweb";	
		}
		else
		{
			$prefix = "manadviceweb";
		}

	   $results = & "c:\deployment tools\advicepoolquery.exe" 				
		foreach ($line in $results)
		{
			if ($line.startswith( $prefix, "currentcultureignorecase" ))
			{			         
				$element = $line.split("-");	

				if ( $element[1] -match "disabled" -or $element[1] -match "offline" )
				{							
					$servers += $element[0].trim();                				
				}							
			}
		}
	}
	else	
	{						
		$servers = $forceServers.split(","); 
	}

	return $servers
}

###################################################################################################
# 			        																			  #
###################################################################################################
function switchConfig( [string] $server )
{
	$baseconfig 		= "\\" + $server +  "\c$\inetpub\wwwroot\live\Advice3\Angular\Modules\configservice.js"
	$liveConfig 		= $baseconfig + ".LIVE"

	Write-host "Rename " + $liveConfig + " to " + $baseConfig
	Move-Item -Path $liveConfig -Destination $baseConfig -Force
}


###################################################################################################
# 			MAIN LOOP																			  #
###################################################################################################

$offlineServers = GetOfflineServers

if ( $offlineServers -eq $null )
{
	Write-Host "----------------------------------------------------------------"
    Write-Host "ERROR: No offline servers found. Deployment cancelled. Exiting!"
	Write-Host "----------------------------------------------------------------"
    exit -1
}

if ($offlineServers.Count -gt 3)
{
    Write-Host "----------------------------------------------------------------"
    Write-Host "ERROR: More than 3 servers identified - exiting!"
    Write-Host "----------------------------------------------------------------"
    exit -1
}

Write-Host "\replacing config on the following " $offlineServers.Count " servers:-"
foreach ($server in $offlineServers)
{	
	Write-Host $server    	      
	switchConfig $server
}