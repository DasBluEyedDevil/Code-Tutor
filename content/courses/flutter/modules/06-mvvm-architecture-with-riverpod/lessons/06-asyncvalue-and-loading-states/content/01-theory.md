---
type: "THEORY"
title: "The Problem with Async Data"
---

In real apps, most data comes from somewhere else: an API, a database, a file. These operations take **time**. While waiting, your app needs to handle three possible outcomes:

1. **Loading**: The data is being fetched (show a spinner)
2. **Success**: The data arrived (show the data)
3. **Error**: Something went wrong (show an error message)

### The Manual Approach Is Tedious

Without Riverpod, you would handle this manually in every widget:

```dart
class UserProfileScreen extends StatefulWidget {
  @override
  State<UserProfileScreen> createState() => _UserProfileScreenState();
}

class _UserProfileScreenState extends State<UserProfileScreen> {
  User? user;
  bool isLoading = true;
  String? errorMessage;

  @override
  void initState() {
    super.initState();
    _loadUser();
  }

  Future<void> _loadUser() async {
    setState(() {
      isLoading = true;
      errorMessage = null;
    });
    
    try {
      final response = await http.get(Uri.parse('https://api.example.com/user'));
      setState(() {
        user = User.fromJson(jsonDecode(response.body));
        isLoading = false;
      });
    } catch (e) {
      setState(() {
        errorMessage = e.toString();
        isLoading = false;
      });
    }
  }

  @override
  Widget build(BuildContext context) {
    if (isLoading) return CircularProgressIndicator();
    if (errorMessage != null) return Text('Error: $errorMessage');
    return Text('Hello, ${user!.name}');
  }
}
```

That is **50 lines** just to load and display one piece of data! And you need to repeat this pattern for every async operation.

### The Problems

1. **Boilerplate**: Three state variables for every async operation
2. **Error-prone**: Easy to forget setting isLoading = false
3. **Not reusable**: Logic is stuck in the widget
4. **Hard to test**: Logic mixed with UI
5. **Repetitive**: Same pattern copied everywhere