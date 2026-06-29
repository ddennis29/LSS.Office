Write-Host "Restoring LSS Office Suite packages..."
nuget restore .\LSS.Office.sln
Write-Host "Open LSS.Office.sln in Visual Studio 2022 and build Debug|Any CPU."
