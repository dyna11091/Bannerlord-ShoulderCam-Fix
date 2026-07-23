# Contributing and release checklist

This repository tracks a maintained compatibility revision of a multi-author mod. Before reusing or redistributing upstream work, verify that you have the necessary permission.

## Local development

1. Install the target Bannerlord version, Bannerlord.Harmony, and Bannerlord.MBOptionScreen.
2. Build with:

   ```powershell
   dotnet build .\src\ShoulderCam\ShoulderCam.csproj -c Release `
     -p:BannerlordDir="C:\Path\To\Mount & Blade II Bannerlord"
   ```

3. Confirm the DLL is written to `ShoulderCam\bin\Win64_Shipping_Client`.
4. Test MCM loading, on-foot and mounted cameras, all ranged modes, all shoulder-switch modes, both included languages, and an existing save.

## Version update

1. Choose the next Semantic Versioning number.
2. Update all of:
   - `ShoulderCam/SubModule.xml`
   - `src/ShoulderCam/Properties/AssemblyInfo.cs`
   - `README.md` and `README.zh-CN.md`
   - `CHANGELOG.md`
3. Rebuild in `Release` configuration.
4. Validate XML and JSON, then compare the DLL version with the manifest.
5. Commit with a concise message such as `Release v1.0.1`.
6. Create and push an annotated tag:

   ```powershell
   git tag -a v1.0.1 -m "Shoulder Cam v1.0.1"
   git push origin main
   git push origin v1.0.1
   ```

The tag triggers the GitHub release workflow, which validates the version and publishes an install-ready ZIP.
