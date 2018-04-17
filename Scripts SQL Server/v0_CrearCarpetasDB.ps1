$ErrorActionPreference = 'Stop'

$EnvVar = 'SQLServerDB'
$DatabasesPath = 'C:\SQLServer DB'

if (Test-Path env:$EnvVar) { 
    Write-Host 'La variable'$EnvVar' ya existe, ruta: '$DatabasesPath
} else {
    Write-Host 'Creando variable de entorno con la ruta de los ficheros de la base de datos'
    [Environment]::SetEnvironmentVariable($EnvVar, $DatabasesPath, 'User')
    Write-Host 'Variable de entorno creada'
}

New-Item $DatabasesPath -ErrorAction Ignore -ItemType directory
New-Item $DatabasesPath'\Alumno Pair Programming' -ErrorAction Ignore -ItemType directory
New-Item $DatabasesPath'\Alumno Pair Programming\Primary' -ErrorAction Ignore -ItemType directory
New-Item $DatabasesPath'\Alumno Pair Programming\Secondary' -ErrorAction Ignore -ItemType directory
New-Item $DatabasesPath'\Alumno Pair Programming\Logs' -ErrorAction Ignore -ItemType directory