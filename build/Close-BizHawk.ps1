param(
    [Parameter(Mandatory = $true)]
    [string]$ExecutablePath
)

$ErrorActionPreference = 'Stop'
$fullExecutablePath = [System.IO.Path]::GetFullPath($ExecutablePath)
$processName = [System.IO.Path]::GetFileNameWithoutExtension($fullExecutablePath)
$processes = @(
    Get-Process -Name $processName -ErrorAction SilentlyContinue |
        Where-Object {
            $_.Path -and [System.String]::Equals(
                [System.IO.Path]::GetFullPath($_.Path),
                $fullExecutablePath,
                [System.StringComparison]::OrdinalIgnoreCase
            )
        }
)

foreach ($process in $processes) {
    if ($process.CloseMainWindow() -and $process.WaitForExit(10000)) {
        continue
    }

    Stop-Process -Id $process.Id -Force
    if (-not $process.WaitForExit(10000)) {
        throw "Could not close BizHawk process $($process.Id)."
    }
}
