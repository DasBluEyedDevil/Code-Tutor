---
type: "ANALOGY"
title: "Workflows as Assembly Lines"
---

Picture a modern automobile manufacturing plant with multiple assembly lines running simultaneously. Each assembly line (workflow) is triggered when raw materials arrive (code is pushed). The line consists of multiple stations (jobs) where specialized work happens: the body shop welds the frame, the paint shop applies coatings, and the final assembly installs the engine and interior. At each station, workers (runners) perform specific tasks (steps) in a precise sequence.

GitHub Actions workflows mirror this industrial process perfectly. When you push code, it triggers a workflow - your digital assembly line. The workflow contains jobs, each representing a major phase of work like building, testing, or deploying. Jobs run on runners, which are virtual machines that serve as your factory workers. Within each job, steps execute sequentially, just as tasks at an assembly station must happen in order: you cannot install the windshield before the frame exists.

The assembly line analogy extends further. Stations can run in parallel when they are independent - the paint shop and the electronics testing facility can work on different cars simultaneously. Similarly, GitHub Actions jobs run in parallel by default, only waiting when one job explicitly depends on another. Quality control checkpoints at each station halt the line if defects are found, just as failing tests stop your pipeline before bad code reaches production.

Services in GitHub Actions are like the utility infrastructure supporting the factory: electricity, water, and compressed air that machines need to function. Your tests might need a database or cache server - these run as services alongside your job, providing the infrastructure your code requires to execute properly. The service containers start before your steps run and remain available throughout the job, just as factory utilities remain on during the entire shift.