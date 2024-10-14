# Check if the script is run with the required arguments
if ($args.Count -eq 0 -or $args.Count -gt 2) {
    Write-Host "Usage: .\script.ps1 <ProjectName> [RootNamespace]"
    exit 1
}

# Define project name and root namespace based on the provided arguments
$ProjectName = $args[0]
$RootNamespace = if ($args.Count -eq 2) { $args[1] } else { $args[0] }

# Validate input for alphanumeric characters only
function Validate-Input($input) {
    if ($input -match '[^a-zA-Z0-9]') {
        Write-Host "Error: '$input' contains special characters. Only alphanumeric characters are allowed."
        exit 1
    }
}

# Validate both ProjectName and RootNamespace
Validate-Input -input $ProjectName
Validate-Input -input $RootNamespace

Write-Host "Project Name: $ProjectName"
Write-Host "Root Namespace: $RootNamespace"

# Rename all files and directories containing 'ServiceTemplate'
Get-ChildItem -Recurse -Filter "*ServiceTemplate*" | ForEach-Object {
    $NewName = $_.FullName -replace "ServiceTemplate", $ProjectName
    Rename-Item -Path $_.FullName -NewName $NewName -Force
    Write-Host "Renamed '$($_.FullName)' to '$NewName'"
}

# Update .csproj files with the new project name and root namespace
Get-ChildItem -Recurse -Filter "*.csproj" | ForEach-Object {
    $Content = Get-Content -Path $_.FullName -Raw
    $Content = $Content -replace "<RootNamespace>ServiceTemplate</RootNamespace>", "<RootNamespace>$RootNamespace</RootNamespace>"
    $Content = $Content -replace "<AssemblyName>ServiceTemplate</AssemblyName>", "<AssemblyName>$ProjectName</AssemblyName>"
    $Content = $Content -replace "ServiceTemplate", $ProjectName
    Set-Content -Path $_.FullName -Value $Content
    Write-Host "Updated .csproj file '$($_.FullName)'"
}

# Update .sln files to replace project references with the new project name
Get-ChildItem -Recurse -Filter "*.sln" | ForEach-Object {
    $Content = Get-Content -Path $_.FullName -Raw
    $Content = $Content -replace "ServiceTemplate", $ProjectName
    Set-Content -Path $_.FullName -Value $Content
    Write-Host "Updated .sln file '$($_.FullName)'"
}

# Update namespace and using directives in .cs files
Get-ChildItem -Recurse -Filter "*.cs" | ForEach-Object {
    $Content = Get-Content -Path $_.FullName -Raw
    $Content = $Content -replace "namespace ServiceTemplate", "namespace $RootNamespace"
    $Content = $Content -replace "using ServiceTemplate", "using $RootNamespace"
    Set-Content -Path $_.FullName -Value $Content
    Write-Host "Updated namespace in file '$($_.FullName)'"
}

# Update Dockerfile(s) to replace 'ServiceTemplate' with the new project name
Get-ChildItem -Recurse -Filter "Dockerfile" | ForEach-Object {
    $Content = Get-Content -Path $_.FullName -Raw
    $Content = $Content -replace "ServiceTemplate", $ProjectName
    Set-Content -Path $_.FullName -Value $Content
    Write-Host "Updated Dockerfile '$($_.FullName)'"
}

Write-Host "All operations completed successfully."
