# Local replay exchange

Song Select → Map options → Replays, or universal search `replay`, opens the local OSR importer. Standard uses McOsu; Taiko, Catch and Mania route to the supplied Bosh ruleset. The matching beatmap MD5 must be installed. Importing never awards a score, progression or PP and never submits online.

For native Standard Results, F2 exports a standard `.osr` plus two companions: `.osr.ureplay` and `.osr.boshsu.json`. Local score details also offer Export. Share all three for custom CS, reading AR, runtime rate, rotation and flip playback. The JSON stores captured gameplay settings and SHA256 bindings, with no profile ID, local paths, credentials or PP provider identity. The native companion retains exact canonical input; vanilla OSR alone cannot express all custom settings. OSR coordinates include display transforms; the native companion remains canonical.

New UREPLAY4 recordings retain the actual map RNG seed. Existing UREPLAY2/3 recordings remain readable locally. An older recording without a captured seed cannot be exported as a modern OSR by inventing a seed. Historical OSR versions without seeds are imported as seedless native recordings. Target Practice extras are unsupported and rejected visibly.

OSR import limits compressed files to 32 MiB, decoded text to 64 MiB, LZMA dictionary allocation to 64 MiB and frames to one million. Companion metadata is capped at 256 KiB. Malformed, backwards-clock, mismatched or incomplete inputs are rejected. Import/export runs off gameplay timing. Existing export files are preserved; choose a new filename. Imported native files are deduplicated by content hash.

Qualification: 37 new exchange assertions passed, alongside 102 native conversion/submission-ledger assertions. Native seed-format compatibility is also checked in the native fixture. Menu routes, Back and scrolling were exercised at 720p, 4:3 and 900p with 100/150% logical scale. These are automated interaction checks, not physical mouse/tablet certification.

Known limitation: a frozen dense synthetic recording reproduces one 100 as a 50 on both the sealed baseline and candidate. It is not a regression introduced by OSR exchange, but full input/judgment equivalence is not certified. Relax/Spunout forced judgments without recorded input edges remain ineligible for native recording. Do not describe all replay/assist combinations as qualified or enable server replay proof on this evidence.

Actual candidate qualification also completed a 217-hit native play/replay with matching score, judgments and combo. The same recording passed native → OSR with companions → import → real-client playback, again matching all counters and score with no additional awards. The canonical native bytes survived exchange exactly. These passing fixtures do not erase the separate dense-map defect.
