---
type: "THEORY"
title: "The Spaghetti Code Problem"
---

Imagine you are building a house. Would you start by randomly placing bricks, wires, and pipes wherever they fit? Of course not! You would follow a blueprint that tells you exactly where everything goes. The foundation comes first, then the frame, then the walls, and finally the finishing touches.

**Software is exactly the same.** Without a plan (architecture), your code becomes a tangled mess that is impossible to maintain. This messy code has a name: **spaghetti code**.

### What Does Spaghetti Code Look Like?

Here is a real example of code that looks simple but hides serious problems. This widget does EVERYTHING in one place: fetches data from an API, manages loading states, handles errors, AND builds the UI.

```dart
// BAD: Everything mixed together in one widget
class UserProfileScreen extends StatefulWidget {
  @override
  State<UserProfileScreen> createState() => _UserProfileScreenState();
}

class _UserProfileScreenState extends State<UserProfileScreen> {
  // State variables mixed with UI logic
  Map<String, dynamic>? userData;
  bool isLoading = true;
  String? errorMessage;
  bool isEditing = false;
  final nameController = TextEditingController();

  @override
  void initState() {
    super.initState();
    _loadUser();  // API call directly in widget!
  }

  // API logic mixed into the widget
  Future<void> _loadUser() async {
    setState(() => isLoading = true);
    try {
      // Direct HTTP call - no separation!
      final response = await http.get(
        Uri.parse('https://api.example.com/users/123'),
      );
      if (response.statusCode == 200) {
        setState(() {
          userData = jsonDecode(response.body);
          isLoading = false;
        });
      } else {
        setState(() {
          errorMessage = 'Failed to load user';
          isLoading = false;
        });
      }
    } catch (e) {
      setState(() {
        errorMessage = e.toString();
        isLoading = false;
      });
    }
  }

  // More API logic in the same file
  Future<void> _saveUser() async {
    setState(() => isLoading = true);
    try {
      await http.put(
        Uri.parse('https://api.example.com/users/123'),
        body: jsonEncode({'name': nameController.text}),
      );
      await _loadUser();  // Reload after save
    } catch (e) {
      setState(() => errorMessage = e.toString());
    }
  }

  @override
  Widget build(BuildContext context) {
    // UI code mixed with all the logic above
    if (isLoading) return CircularProgressIndicator();
    if (errorMessage != null) return Text(errorMessage!);
    
    return Column(
      children: [
        Text(userData?['name'] ?? ''),
        TextField(controller: nameController),
        ElevatedButton(
          onPressed: _saveUser,
          child: Text('Save'),
        ),
      ],
    );
  }
}
```
