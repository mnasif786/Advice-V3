param(	[string] $pool="advicev3_pool",
		[string] $destinationDirectory="live",		
		[string[]] $systems=@("Advice3", "Advice3Internal"),
		[string] $sourcePath = ".\Advice\Advice.Web\Publish\Advice3_Live",
		[string] $forceServers = ""
	), 

[string] $scriptDirectory = Split-Path $script:MyInvocation.MyCommand.Path

$ErrorActionPreference = "Stop";

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
function StopAdviceWebSiteAndAppPool([String] $server, [String] $system)
{	
    Invoke-Command $server { param($system)
        Import-Module WebAdministration;   
		
		$site = "IIS:\sites\" + $system
		$state = Get-WebItemState $site
		if ($state.value -eq "Started")
		{
			Stop-WebSite $system
		}
		
		$pool = "IIS:\apppools\" + $system
		$state = Get-WebItemState $pool
		if ($state.value -eq "Started")
		{
			Stop-WebAppPool $system
		}
		
		#this is required to prevent an access denied message when renaming the existing folder
		Get-WebItemState $site
    } -Args $system
}

###################################################################################################
# 			        																			  #
###################################################################################################
function StartAdviceWebSiteAndAppPool([String] $server, [String] $system)
{
    Invoke-Command $server { param($system)
        Import-Module WebAdministration;   
		
		$site = "IIS:\sites\" + $system
		$state = Get-WebItemState $site
		if ($state.value -eq "Stopped")
		{
			Start-WebSite $system
		}
		
		$pool = "IIS:\apppools\" + $system
		$state = Get-WebItemState $pool
		if ($state.value -eq "Stopped")
		{
			Start-WebAppPool $system
		}
		
		#this is required to prevent an access denied message when renaming the existing folder
		Get-WebItemState $site
    } -Args $system
}

###################################################################################################
# 			        																			  #
###################################################################################################
function copyFiles([String] $sourceDir, [String] $destinationDir)
{
	write-host "Copying files from ${sourceDir} to ${destinationDir}"
	    	
	if(test-path -path $destinationDir)
	{
		Remove-Item $destinationDir -Recurse -force
	}
	
	Copy-Item -Path $sourceDir -Destination $destinationDir -recurse -force
}

###################################################################################################
# To work around the av2reskin pool, in Live offline, we redirect the AV3 installation the	      #
# localhost version of av2reskin 																  #
###################################################################################################
function UpdateConfigFile([String] $server, [String] $destination)
{
	$File = $destination + "\Angular\Modules\configservice.js"
	$NewIdentifier = "advice2Root: 'http://" + $server + ":8022/'" 
	[regex]$regex="advice2Root: .*"   
	(Get-Content ($File))  | Foreach-Object {$_ -replace $regex, "$NewIdentifier"} | Set-Content ($File)	
}


###################################################################################################
# 			        																			  #
###################################################################################################
function UpdateAdvice ([String] $sourceDirectory, [String] $server, [string] $system)
{	
	StopAdviceWebSiteAndAppPool $server $system
	
	$destination = "\\" + $server + "\c$\inetpub\wwwroot\" + $destinationDirectory + "\" + $system
	copyFiles $sourceDirectory $destination	

	UpdateConfigFile $server $destination

	StartAdviceWebSiteAndAppPool $server $system
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

Write-Host "Deploying to the following " $offlineServers.Count "offline servers:-"
foreach ($server in $offlineServers)
{	
	Write-Host $server
}

foreach ($server in $offlineServers)
{  
    foreach($system in $systems)
	{		
       Write-Host "Deploying " $system " to " $server  	
	   UpdateAdvice $sourcePath $server $system        		
	}
}
