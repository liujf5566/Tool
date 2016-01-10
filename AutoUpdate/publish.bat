@ECHO OFF
cd /d %~dp0

SET BUILDER=C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe
SET UPDATESOLUTION=AutoUpdate\AutoUpdate.sln

@echo 删除Deploy文件夹
rd /s /q Deploy

@echo 生成自动更新应用程序...
"%BUILDER%" "%UPDATESOLUTION%" /t:Rebuild /p:Configuration=Release /l:FileLogger,Microsoft.Build.Engine;logfile=UpdateBuildLog.txt /flp2:errorsonly;logfile=msbuildErr.txt

@ECHO 创建打包文件夹
md Deploy\updateSer

@ECHO 复制AppMain文件到指定Deploy目录
XCOPY "AutoUpdate\Aostar.MVP.Main\bin\Release\*.*" "Deploy\" /E /Y /Q
@ECHO 复制AppUpdate文件到指定Deploy目录
XCOPY "AutoUpdate\Aostar.MVP.Update\bin\Release\*.*" "Deploy\" /E /Y /Q
@ECHO 复制下载服务文件到指定updateSer目录
XCOPY "AutoUpdate\Aostar.MVP.DownloadService\bin\Release\*.*" "Deploy\updateSer\" /E /Y /Q
echo 已完成,返回主菜单
pause