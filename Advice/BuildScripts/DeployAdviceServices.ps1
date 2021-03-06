param(	[string] $deploymentServer="advicev3uat",
		[string] $destinationDirectory="ServiceReviewService",				
		[string] $sourcePath = ".\Advice\Advice.ServiceReviews\bin\Release"
	), 

$destinationPath = "\\" + $deploymentServer + "\c$\AdviceServices\" + $destinationDirectory

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
# 			        																			  #
###################################################################################################
function UpdateAdviceService ([String] $sourceDirectory, [String] $destinationDirectory)
{	
	copyFiles $sourceDirectory $destinationDirectory	
}

###################################################################################################
# 			MAIN LOOP																			  #
###################################################################################################
if ( $deploymentServer -eq $null )
{
	Write-Host "----------------------------------------------------------------"
    Write-Host "ERROR: No deployment servers found. Deployment cancelled. Exiting!"
	Write-Host "----------------------------------------------------------------"
    exit -1
}

Write-Host "Deploying to the following server:-"
Write-Host $deploymentServer

UpdateAdviceService $sourcePath $destinationPath  


