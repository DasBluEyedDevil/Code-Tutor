---
type: "ANALOGY"
title: "The Import Security Guard"
---

Airport security X-rays your luggage. They're not just checking the label - they verify the contents match what you declared.

Import Attributes are JavaScript's security checkpoint. When you write `with { type: 'json' }`, you're declaring 'this file contains ONLY data, no code.' The runtime verifies this and blocks any attempts to sneak executable code through a JSON import.