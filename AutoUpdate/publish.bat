@ECHO OFF
cd /d %~dp0

SET BUILDER=C:\Windows\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe
SET UPDATESOLUTION=AutoUpdate\AutoUpdate.sln

@echo ɾ��Deploy�ļ���
rd /s /q Deploy

@echo �����Զ�����Ӧ�ó���...
"%BUILDER%" "%UPDATESOLUTION%" /t:Rebuild /p:Configuration=Release /l:FileLogger,Microsoft.Build.Engine;logfile=UpdateBuildLog.txt /flp2:errorsonly;logfile=msbuildErr.txt

@ECHO ��������ļ���
md Deploy\updateSer

@ECHO ����AppMain�ļ���ָ��DeployĿ¼
XCOPY "AutoUpdate\Aostar.MVP.Main\bin\Release\*.*" "Deploy\" /E /Y /Q
@ECHO ����AppUpdate�ļ���ָ��DeployĿ¼
XCOPY "AutoUpdate\Aostar.MVP.Update\bin\Release\*.*" "Deploy\" /E /Y /Q
@ECHO �������ط����ļ���ָ��updateSerĿ¼
XCOPY "AutoUpdate\Aostar.MVP.DownloadService\bin\Release\*.*" "Deploy\updateSer\" /E /Y /Q
echo �����,�������˵�
pause