"%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe" -u "%~dp0Aostar.MVP.DownloadService.exe"
"%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\InstallUtil.exe" "%~dp0Aostar.MVP.DownloadService.exe"
sc config "Aostar.MVP.DownloadService" type= interact type= own
net start "Aostar.MVP.DownloadService"
