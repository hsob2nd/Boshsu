# Installing and updating Boshsu

## First-time download

Download `Boshsu-1.3-clean.zip` from the official Releases page, extract the **whole ZIP** into a writable folder, then open **Boshsu.exe**. Do not run an executable inside the ZIP. Keep all included DLLs and resource folders together. Windows x64 and .NET Framework 4.8 are required; build tools and a separate PP calculator are not required. The source ZIP is for developers, not for playing.

The client creates a fresh local profile. Maps and custom skins are not bundled; select your library in Options. First launch uses Main Menu, primary display, native resolution and fullscreen; your later changes are saved.

## Manual update, without the client updater or an internet connection

1. Download `Boshsu-1.3-clean.zip` **and `SHA256SUMS.txt` from the same official release**. Keep their original filenames together.
2. Close the Boshsu installation you want to update.
3. Double-click **Boshsu.Updater.exe** in your existing installation. If it predates RC5, extract the new complete ZIP into a separate folder and open its updater instead.
4. Choose **Boshsu.exe in your existing installation**, the downloaded clean ZIP and its checksums file. The adjacent checksums file is selected automatically when available.
5. Click **Verify & install**, review the target folder/version, then confirm. The updater runs a separate copy, replaces application files and opens Boshsu again.

This manual path performs no network requests and requires no GitHub login. Checksums detect corruption; a locally supplied checksum file is **not** an authenticated publisher signature, so obtain both downloads from the official repository. Source ZIPs, missing/mismatched checksums, unsafe files and older versions are rejected. The target client must be closed; the updater does not kill it. Updating an unrelated copy is allowed while your other copy remains open.

## In-client updates

Open **Options → Updates**, or search **updates** with Ctrl K. Check, enable beta releases for RC builds, download, then click **Restart & install** when ready. Startup checking is optional. The client verifies the GitHub SHA-256 asset digest and drains saves before closing. RC1 predates this updater; install a complete current portable package once to obtain it.

## Data preservation and recovery

Both update paths use the same complete-package identity, file-manifest and safe-path validation. They replace application files only. Profiles, scores, progression, avatars, maps, skins, settings, replays, generated Map Lab difficulties and custom PP providers remain in place. One previous application version is retained under `Updates/rollback-*`; failed installation attempts to restore replaced files. `Updates/last-update.txt` records the outcome. Manual helper storage is pruned on later preparation; active/unrecognised folders are retained. Back up personal data before any update or move.

No silent update, forced restart, forced process closure, administrator request or credential upload is performed. Offline play does not require update connectivity. If files are locked, space is insufficient or permission is denied, keep your existing installation and inspect the outcome/recovery folder. Report reproducible issues without uploading private accounts, configurations or unreviewed logs.
