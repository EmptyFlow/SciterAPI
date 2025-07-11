param (
	[string]$SciterPath
)

Write-Host "Try to prepare files for $SciterPath ..."

Copy-Item -Path "$SciterPath/bin/windows/x64/sciter.dll" "sciter.dll"
Compress-Archive -Path "sciter.dll" -DestinationPath "sciter_win_x64.zip"
Remove-Item -Path "sciter.dll"

Copy-Item -Path "$SciterPath/bin/windows/x32/sciter.dll" "sciter.dll"
Compress-Archive -Path "sciter.dll" -DestinationPath "sciter_win_x32.zip"
Remove-Item -Path "sciter.dll"

Copy-Item -Path "$SciterPath/bin/windows/arm64/sciter.dll" "sciter.dll"
Compress-Archive -Path "sciter.dll" -DestinationPath "sciter_win_arm64.zip"
Remove-Item -Path "sciter.dll"

Copy-Item -Path "$SciterPath/bin/linux/arm64/libsciter.so" "libsciter.so"
Compress-Archive -Path "libsciter.so" -DestinationPath "sciter_linux_arm64.zip"
Remove-Item -Path "libsciter.so"

Copy-Item -Path "$SciterPath/bin/linux/x64/libsciter.so" "libsciter.so"
Compress-Archive -Path "libsciter.so" -DestinationPath "sciter_linux_x64.zip"
Remove-Item -Path "libsciter.so"

Copy-Item -Path "$SciterPath/bin/macosx/libsciter.dylib" "libsciter.dylib"
Compress-Archive -Path "libsciter.dylib" -DestinationPath "sciter_mac.zip"
Remove-Item -Path "libsciter.dylib"
