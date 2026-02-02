// firebase.json
{
  "hosting": {
    "public": "___",
    "ignore": [
      "firebase.json",
      "**/.*",
      "___"
    ],
    "rewrites": [
      {
        "source": "___",
        "destination": "___"
      }
    ],
    "headers": [
      {
        "source": "**/*.@(js|css)",
        "headers": [
          {
            "key": "Cache-Control",
            "value": "___"
          }
        ]
      },
      {
        "source": "**",
        "headers": [
          {
            "key": "___",
            "value": "nosniff"
          },
          {
            "key": "___",
            "value": "SAMEORIGIN"
          }
        ]
      }
    ]
  }
}