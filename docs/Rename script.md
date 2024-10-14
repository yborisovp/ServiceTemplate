# Project Rename Scripts

These scripts allow you to rename the root namespace and project name in a .NET project. They automatically update references in `.csproj`, `.sln`, `.cs`, and `Dockerfile` files.

## Requirements

- **Bash Script (`rename-script.sh`)**: Suitable for Unix-based systems (Linux, macOS).
- **PowerShell Script (`rename-script.ps1`)**: Suitable for Windows systems, but can also be used on Unix-based systems with PowerShell Core.

## Features

- **Single Parameter**: When a single parameter is provided, it is used as both the root namespace and the project name.
- **Two Parameters**: When two parameters are provided, the first is used as the project name and the second as the root namespace.
- **File Updates**:
    - Renames all files and directories containing `ServiceTemplate` to the new project name.
    - Updates `<RootNamespace>` and `<AssemblyName>` in `.csproj` files.
    - Replaces `namespace` and `using` directives in `.cs` files.
    - Updates references in `.sln` files.
    - Updates paths and references in `Dockerfile`.

## Usage Instructions

### Bash Script (`update_project.sh`)

### Requirements

- A Unix-based system (Linux, macOS) or Windows with WSL or Git Bash installed.

#### Steps to Use:

1. **Download the Script**:
   Save the script as `update_project.sh`.

2. **Make the Script Executable**:
   ```bash
   chmod +x update_project.sh
   ```

3. **Run the Script**:
    - **Single Parameter**:
      ```bash
      ./update_project.sh MyProj
      ```
        - This will set both the root namespace and project name to `MyProj`.
    - **Two Parameters**:
      ```bash
      ./update_project.sh MyProj RootNamespaceName
      ```
        - This sets the project name to `MyProj` and the root namespace to `RootNamespaceName`.

### PowerShell Script (`update_project.ps1`)

### Requirements

- PowerShell installed (included by default on Windows; available on Linux and macOS via PowerShell Core).

#### Steps to Use:

1. **Download the Script**:
   Save the script as `update_project.ps1`.

2. **Set Execution Policy (if needed)**:
   If you encounter an execution policy error, run:
   ```powershell
   Set-ExecutionPolicy -Scope Process -ExecutionPolicy Bypass
   ```

3. **Run the Script**:
    - **Single Parameter**:
      ```powershell
      .\update_project.ps1 MyProj
      ```
        - This will set both the root namespace and project name to `MyProj`.
    - **Two Parameters**:
      ```powershell
      .\update_project.ps1 MyProj RootNamespaceName
      ```
        - This sets the project name to `MyProj` and the root namespace to `RootNamespaceName`.

## Example Scenarios

1. **Renaming Both Project Name and Root Namespace**:
   If you only provide one parameter, the script will rename all occurrences of `ServiceTemplate` to the provided name in the project files, directories, and namespaces.

   ```bash
   ./update_project.sh NewProjectName
   ```

2. **Renaming Project Name and Using a Different Root Namespace**:
   You can specify separate names for the project and the root namespace.

   ```powershell
   .\update_project.ps1 NewProjectName DifferentNamespace
   ```

3. **Dockerfile Adjustments**:
   The scripts will automatically adjust paths in `Dockerfile` commands such as `COPY` to reflect the new project name.

## Notes

- **Backup**: It is recommended to back up your project before running these scripts.
- **Special Character Restriction**: The scripts enforce that the project name and namespace contain only alphanumeric characters.
- **Cross-Platform Usage**:
    - The bash script is suitable for Unix-like environments.
    - The PowerShell script is suited for Windows but can run on Unix-based systems if PowerShell Core is installed.

## Troubleshooting

If the scripts do not run as expected, ensure:
- **For Bash Script**: Bash is installed and executable permissions are set.
- **For PowerShell Script**: PowerShell execution policies allow the script to run.

For additional help, feel free to reach out or consult further documentation on shell scripting and PowerShell.