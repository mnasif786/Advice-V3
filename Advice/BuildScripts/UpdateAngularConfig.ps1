param(  $filePath,  $publishProfile )

$ErrorActionPreference = "Stop";

# Update angular config in publish profile folder
$publishFolder = $filePath + "\Advice.web\Publish\"

$profileFolder = ""
$configExtension = ""
switch( $publishProfile )
{
    "Advice3_DeployUAT_Jenkins"		
	{ 
		$profileFolder = "Advice3_UAT\"
		$configExtension = ".UAT"
	}	
    
	"Advice3_DeployLive_Jenkins"	
	{ 
		$profileFolder = "Advice3_Live\"
		$configExtension = ".LIVE_OFFLINE"								
	}

	default
	{
		Write-Host "ERROR: Unknown publish profile: "  $publishProfile
		exit -1
	}
}

$baseConfigFile	= $publishFolder + $profileFolder +	"Angular\Modules\configservice.js"
if( !(test-path -path $baseConfigFile) )
{
	Write-host "ERROR: Config File " $baseConfigFile "Not found"
	exit -1
}

$newConfigFile	= $baseConfigFile + $configExtension
if( !(test-path -path $newConfigFile) )
{
	Write-host "ERROR: Config File " $newConfigFile "Not found"
	exit -1
}

Write-host "Replacing " $baseConfigFile " with " $newConfigFile

# replace base configservice.js file with profile specific version
Remove-Item $baseConfigFile
Rename-Item -Path $newConfigFile -NewName "configservice.js"




## Remove all unused configservice.js.* files
#if( $publishProfile -eq "Advice3_DeployLive_Jenkins")
#{
#	Remove-Item ($baseConfigFile + ".*") -exclude $newConfigFile, $baseConfigFile + ".LIVE"	
#}
#else
#{
#	Remove-Item ($baseConfigFile + ".*") -exclude $newConfigFile
#}






