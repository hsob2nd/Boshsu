# Supplied Bosh ruleset integration — unreleased

The owner approved reusing the supplied Bosh/stable Taiko, Catch and Mania rulesets. Standard continues to use the integrated McOsu runtime. Both paths use the same application process, main window, selected skin and shell. This is not a new implementation of those three rulesets inside McOsu.

The selected conversion applies to Standard maps. A native-mode beatmap selects its own ruleset. An explicit replay selects its recorded ruleset, and a replay cannot silently change a native-mode beatmap to another ruleset. Native Standard keyboard interception and rendering are inactive while a supplied ruleset owns gameplay. Standard-only cursor assistance and CS/reading override paths do not take ownership of the other modes.

Mode scores belong to the existing immutable offline profile ID and selected local world. Versioned metadata and independently compressed replays live under `StableRemasteredAccounts/mode-scores`. One bounded background worker owns compression and atomic writes. Its replay queue has a combined one-million-frame budget. Shutdown drains accepted writes. Corrupt records are retained on disk and reported; valid records continue loading. Renaming a profile updates its displayed score identity without creating another account.

The Song Select records panel shows mode score points and accuracy. It does not show Standard PP against a Taiko, Catch or Mania score. These records never enter Standard's calculator, PP totals, progression ledger or network submission queue. **Mode PP is unavailable** under the owner's instruction to keep the PP system unchanged. Existing PP formulas, rate rewards and historical awards are preserved.

Local replay playback owns a separate score/frame list. It retains its recorded playback rate and does not create another score. The adapter avoids the legacy replay reset that clears the selected recording. A map/replay load failure returns to Song Select with an error, and completion stops updating a disposed Mania input stage.

Qualification is tracked in `GITHUB_ISSUE_WORK_REPORT.md`. CPU/storage tests and automated actual-client lifecycle/replay checks do not certify physical keyboard/drum/tablet feel, every conversion/key count/skin, Linux, mode-specific performance improvements or online compatibility. Full standard `.osr` interchange is a separate issue.
