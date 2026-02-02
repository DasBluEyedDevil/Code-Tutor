---
type: "EXAMPLE"
title: "Finding Elements"
---

```html
<h1 id="main-header">Shopping List</h1>

<ul class="item-list">
  <li class="item">Apples</li>
  <li class="item urgent">Milk</li>
  <li class="item">Bread</li>
</ul>

<script>
  // 1. Finding by ID (Use #)
  const header = document.querySelector('#main-header');
  console.log(header.textContent);

  // 2. Finding by Class (Use .)
  // querySelector only finds the FIRST match
  const firstItem = document.querySelector('.item');
  console.log('First item found:', firstItem.textContent);

  // 3. Finding ALL matches
  // querySelectorAll returns a list (NodeList)
  const allItems = document.querySelectorAll('.item');
  console.log(`Found ${allItems.length} items.`);

  // We can loop through them!
  allItems.forEach(item => console.log('List Item:', item.textContent));

  // 4. Advanced Selection (Nesting)
  // "Find an item that also has the 'urgent' class"
  const urgentItem = document.querySelector('.item.urgent');
  
  // "Find the span inside the div"
  const nested = document.querySelector('div p span');
</script>
```