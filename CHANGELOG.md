# Changelog

All notable changes to this project are documented here. Versions follow Semantic Versioning.

## [Unreleased]

### Added

- Nothing yet.

## [1.0.0] - 2026-05-23

### Added

- Bannerlord `v1.3.15–v1.4.x` compatibility revision.
- Restored MCM settings menu with full `config.json` field coverage.
- English and Simplified Chinese XML localization.
- Explicit `Bannerlord.Harmony` and `Bannerlord.MBOptionScreen` dependencies.
- Configurable aim-return delay and temporary shoulder-switch duration.

### Fixed

- Ranged mode 1 now reverts to the vanilla camera only when a ranged weapon is equipped and aimed.
- Aim-return delay is applied after ranged aiming stops.
- Temporary shoulder switching now records and uses its switch timestamp.
- Saving through MCM no longer overwrites settings that were absent from the menu.

### Changed

- Removed the bundled `0Harmony.dll`; Harmony is now supplied by the module dependency.

[Unreleased]: https://github.com/dyna11091/Bannerlord-ShoulderCam-Fix/compare/v1.0.0...HEAD
[1.0.0]: https://github.com/dyna11091/Bannerlord-ShoulderCam-Fix/releases/tag/v1.0.0
