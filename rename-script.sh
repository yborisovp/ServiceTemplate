#!/bin/bash

# Check the number of parameters
if [ "$#" -eq 0 ] || [ "$#" -gt 2 ]; then
  echo "Usage: $0 <ProjectName> [RootNamespace]"
  exit 1
fi

# Set project name and root namespace based on provided arguments
PROJECT_NAME="$1"
if [ "$#" -eq 2 ]; then
  ROOT_NAMESPACE="$2"
else
  ROOT_NAMESPACE="$1"
fi

# Function to validate input (alphanumeric only)
validate_input() {
  if [[ "$1" =~ [^a-zA-Z0-9] ]]; then
    echo "Error: '$1' contains special characters. Only alphanumeric characters are allowed."
    exit 1
  fi
}

# Validate the inputs
validate_input "$PROJECT_NAME"
validate_input "$ROOT_NAMESPACE"

echo "Project Name: $PROJECT_NAME"
echo "Root Namespace: $ROOT_NAMESPACE"

# 1. Rename directories and files from 'ServiceTemplate' to the new project name
echo "Renaming files and directories..."

find . -depth -name "*ServiceTemplate*" | while read -r NAME; do
  NEW_NAME=$(echo "$NAME" | sed "s/ServiceTemplate/$PROJECT_NAME/g")
  mv "$NAME" "$NEW_NAME"
  echo "Renamed '$NAME' to '$NEW_NAME'"
done

# 2. Update .csproj files
echo "Updating .csproj files..."

find . -type f -name "*.csproj" | while read -r FILE; do
  echo "Processing '$FILE'"

  # Replace <RootNamespace> and <AssemblyName> tags
  sed -i "s#<RootNamespace>ServiceTemplate</RootNamespace>#<RootNamespace>$ROOT_NAMESPACE</RootNamespace>#g" "$FILE"
  sed -i "s#<AssemblyName>ServiceTemplate</AssemblyName>#<AssemblyName>$PROJECT_NAME</AssemblyName>#g" "$FILE"

  # Replace any remaining instances of 'ServiceTemplate' in the file
  sed -i "s#ServiceTemplate#$PROJECT_NAME#g" "$FILE"
done

# 3. Update .sln files
echo "Updating .sln files..."

find . -type f -name "*.sln" | while read -r FILE; do
  echo "Processing '$FILE'"
  
  # Replace project references
  sed -i "s#ServiceTemplate#$PROJECT_NAME#g" "$FILE"
done

# 4. Update namespaces in .cs files
echo "Updating namespaces in .cs files..."

find . -type f -name "*.cs" | while read -r FILE; do
  # Update namespace declarations and using directives
  sed -i "s#namespace ServiceTemplate#namespace $ROOT_NAMESPACE#g" "$FILE"
  sed -i "s#using ServiceTemplate#using $ROOT_NAMESPACE#g" "$FILE"
done

# 5. Update Dockerfile(s) to replace 'ServiceTemplate' with the new project name
echo "Updating Dockerfile(s)..."

find . -type f -name "Dockerfile" | while read -r FILE; do
  echo "Processing '$FILE'"
  
  # Replace 'ServiceTemplate' references
  sed -i "s#ServiceTemplate#$PROJECT_NAME#g" "$FILE"
done

# Git add and commit
echo "Adding changes to Git..."
git add -A
git commit -m "Automated rename of project to $PROJECT_NAME with namespace $ROOT_NAMESPACE"

echo "All operations completed successfully."
