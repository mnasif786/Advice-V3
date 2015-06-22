Param(  $filePath )

# update manifest cache with new, unique guid
$File = $filePath + "\Advice.Web\angularManifest.appcache"
$NewIdentifier = "#UniqueIdentifier=" + [Guid]::NewGuid()   
[regex]$regex="#UniqueIdentifier=.*"   
(Get-Content ($File))  | Foreach-Object {$_ -replace $regex, "$NewIdentifier"} | Set-Content ($File)

# add latest jenkins build number to front page
$IndexFile = $filePath + "\Advice.Web\Angular\Views\task.html"
[regex]$regex="#BuildNumber"   

[string]$BuildNumber = $env:BUILD_DISPLAY_NAME
(Get-Content ($IndexFile))  | Foreach-Object {$_ -replace $regex, [string]$BuildNumber} | Set-Content ($IndexFile)


