# Contributing and reporting bugs

## Issues-only submissions

This is an open-source project with **owner-maintained development**. Use Issues for bug reports and focused feature requests. Pull requests, Discussions, wiki editing and comments on individual commits are disabled; public visitors do not receive repository write, settings or release access.

The project source is available under its applicable licenses, with separate terms and documented exceptions for bundled components. Public repositories and source downloads can be read, cloned and forked; Issues-only submissions do not prohibit those license/platform rights.

## Reporting a problem

Use GitHub Issues for a reproducible bug or focused feature request. Include the version, OS/GPU/driver, display mode, relevant skin/map identity and the minimum steps needed to reproduce the problem.

For assist issues, include cursor mode/strength, AC, Relax, slider controls, mods, runtime rate and generated Map Lab state. For PP issues, include provider identity, actual rated CS/timing, judgments, accuracy, combo and misses.

Do not upload passwords, tokens, personal account/configuration files, entire installations or unreviewed logs. Current-session logs clear on the next launch, so copy/redact the relevant files first if you explicitly recorded a diagnostic session.

## Maintainer development notes

A maintainer source change should preserve qualified gameplay and rewards unless it proves a deterministic defect. Separate UI/presentation, rated-map state, runtime assists, account providers and networking ownership. Avoid synchronizing file I/O, image decoding, PP parsing or large account work with gameplay input/render timing.

Source is supplied as a Release snapshot; a portable contributor build is still incomplete. Include relevant tests and clearly distinguish deterministic fixtures, OS-injected UI tests and real human/device play. Do not claim platforms or input hardware that were not actually tested.
