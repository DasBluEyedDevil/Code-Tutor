import 'package:dio/dio.dart';

/// Represents a GitHub repository.
class Repository {
  final int id;
  final String name;
  final String fullName;
  final String? description;
  final int stargazersCount;
  final String? language;

  Repository({
    required this.id,
    required this.name,
    required this.fullName,
    this.description,
    required this.stargazersCount,
    this.language,
  });

  // TODO: Implement fromJson factory
  factory Repository.fromJson(Map<String, dynamic> json) {
    throw UnimplementedError();
  }
}

class GitHubService {
  // TODO: Create and configure Dio instance
  
  // TODO: Implement searchRepositories
  // GitHub API: GET /search/repositories?q={query}
  // Response structure:
  // {
  //   "total_count": 123,
  //   "items": [
  //     { "id": 1, "name": "...", "full_name": "...", ... }
  //   ]
  // }
  Future<List<Repository>> searchRepositories(String query) async {
    throw UnimplementedError();
  }
}