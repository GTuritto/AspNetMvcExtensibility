@echo off

if "%1%" == "" goto debug
%windir%\Microsoft.NET\Framework\v3.5\MSBuild.exe System.Web.Mvc.Extensibility.build /p:Configuration=%1 /t:Full
goto end

:debug
%windir%\Microsoft.NET\Framework\v3.5\MSBuild.exe System.Web.Mvc.Extensibility.build /p:Configuration=debug /t:Full

:end
pause