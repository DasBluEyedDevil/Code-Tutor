---
type: "EXAMPLE"
title: "ARB Files with Placeholders, Plurals, and Selects"
---


Advanced ARB features for dynamic content:



```json
// lib/l10n/app_en.arb
{
  "@@locale": "en",
  
  "appTitle": "Task Manager",
  "@appTitle": {
    "description": "Application title shown in app bar"
  },
  
  "welcomeUser": "Hello, {userName}!",
  "@welcomeUser": {
    "description": "Greeting with user name",
    "placeholders": {
      "userName": {
        "type": "String",
        "example": "John"
      }
    }
  },
  
  "taskCount": "{count, plural, =0{No tasks} =1{1 task} other{{count} tasks}}",
  "@taskCount": {
    "description": "Shows number of tasks with proper pluralization",
    "placeholders": {
      "count": {
        "type": "int"
      }
    }
  },
  
  "itemsRemaining": "{count, plural, =0{No items remaining} =1{1 item remaining} other{{count} items remaining}}",
  "@itemsRemaining": {
    "description": "Shows remaining items count",
    "placeholders": {
      "count": {
        "type": "int"
      }
    }
  },
  
  "gender": "{gender, select, male{He} female{She} other{They}} liked your post",
  "@gender": {
    "description": "Pronoun selection based on gender",
    "placeholders": {
      "gender": {
        "type": "String"
      }
    }
  },
  
  "lastUpdated": "Last updated: {date}",
  "@lastUpdated": {
    "description": "Shows when content was last updated",
    "placeholders": {
      "date": {
        "type": "DateTime",
        "format": "yMMMd"
      }
    }
  },
  
  "price": "Price: {amount}",
  "@price": {
    "description": "Displays a price",
    "placeholders": {
      "amount": {
        "type": "double",
        "format": "currency",
        "optionalParameters": {
          "symbol": "$",
          "decimalDigits": 2
        }
      }
    }
  }
}

// lib/l10n/app_es.arb
{
  "@@locale": "es",
  
  "appTitle": "Gestor de Tareas",
  "welcomeUser": "Hola, {userName}!",
  "taskCount": "{count, plural, =0{Sin tareas} =1{1 tarea} other{{count} tareas}}",
  "itemsRemaining": "{count, plural, =0{No quedan elementos} =1{Queda 1 elemento} other{Quedan {count} elementos}}",
  "gender": "{gender, select, male{A el} female{A ella} other{A ellos}} le gusta tu publicacion",
  "lastUpdated": "Ultima actualizacion: {date}",
  "price": "Precio: {amount}"
}

// lib/l10n/app_ar.arb
{
  "@@locale": "ar",
  
  "appTitle": "مدير المهام",
  "welcomeUser": "مرحبا، {userName}!",
  "taskCount": "{count, plural, =0{لا توجد مهام} =1{مهمة واحدة} =2{مهمتان} few{{count} مهام} many{{count} مهمة} other{{count} مهمة}}",
  "itemsRemaining": "{count, plural, =0{لا توجد عناصر متبقية} =1{عنصر واحد متبقي} =2{عنصران متبقيان} few{{count} عناصر متبقية} many{{count} عنصر متبقي} other{{count} عنصر متبقي}}",
  "gender": "{gender, select, male{هو} female{هي} other{هم}} اعجب بمنشورك",
  "lastUpdated": "اخر تحديث: {date}",
  "price": "السعر: {amount}"
}
```
