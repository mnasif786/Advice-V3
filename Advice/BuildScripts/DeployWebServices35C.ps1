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

	(ROBOCOPY "\\uatmaintci1\c$\Program Files (x86)\Jenkins\jobs\Webservices35c\workspace\Webservices35c" "\\manadviceweb02\c$\inetpub\Webservices35c" /MIR) ^& IF %ERRORLEVEL% LEQ 1 exit 0

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


