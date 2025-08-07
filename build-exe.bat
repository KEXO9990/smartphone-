@echo off
echo Building Mobile Shop Management System...
echo.

:: Clean previous builds
if exist "dist" rmdir /s /q "dist"
if exist "publish" rmdir /s /q "publish"

echo Cleaning previous builds...
dotnet clean -c Release

echo.
echo Building in Release mode...
dotnet build -c Release

if %ERRORLEVEL% NEQ 0 (
    echo Build failed!
    pause
    exit /b %ERRORLEVEL%
)

echo.
echo Publishing as standalone executable...
dotnet publish -c Release --self-contained true -r win-x64 -o "./dist"

if %ERRORLEVEL% NEQ 0 (
    echo Publish failed!
    pause
    exit /b %ERRORLEVEL%
)

echo.
echo SUCCESS! Executable created in ./dist/ folder
echo Main executable: MobileShopApp.exe
echo.
dir /b "dist\*.exe"
echo.
echo You can now run the application by double-clicking MobileShopApp.exe in the dist folder.
pause
