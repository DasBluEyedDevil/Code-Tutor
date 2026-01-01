// Contact Form Challenge
// Create a form with validation

import 'package:flutter/material.dart';

void main() {
  runApp(const ContactFormApp());
}

class ContactFormApp extends StatelessWidget {
  const ContactFormApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      home: Scaffold(
        appBar: AppBar(title: const Text('Contact Form')),
        body: const ContactForm(),
      ),
    );
  }
}

class ContactForm extends StatefulWidget {
  const ContactForm({super.key});

  @override
  State<ContactForm> createState() => _ContactFormState();
}

class _ContactFormState extends State<ContactForm> {
  // TODO: Create GlobalKey<FormState> for form validation
  final _formKey = GlobalKey<FormState>();
  
  final _nameController = TextEditingController();
  final _emailController = TextEditingController();

  @override
  void dispose() {
    _nameController.dispose();
    _emailController.dispose();
    super.dispose();
  }

  // TODO: Create validator functions that return error message or null
  String? _validateName(String? value) {
    if (value == null || value.isEmpty) {
      return 'Please enter your name';
    }
    return null;
  }

  @override
  Widget build(BuildContext context) {
    return SingleChildScrollView(
      padding: const EdgeInsets.all(16),
      child: Form(
        key: _formKey,
        child: Column(
          children: [
            // TODO: Use TextFormField with validator property
            TextFormField(
              controller: _nameController,
              decoration: const InputDecoration(
                labelText: 'Name',
                border: OutlineInputBorder(),
              ),
              validator: _validateName,
            ),
            const SizedBox(height: 16),
            
            // TODO: Add email field with email validation
            
            ElevatedButton(
              onPressed: () {
                // TODO: Call _formKey.currentState!.validate()
              },
              child: const Text('Submit'),
            ),
          ],
        ),
      ),
    );
  }
}