import 'package:dio/dio.dart';

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

  factory Repository.fromJson(Map<String, dynamic> json) {
    return Repository(
      id: json['id'] as int,
      name: json['name'] as String,
      fullName: json['full_name'] as String,
      description: json['description'] as String?,
      stargazersCount: json['stargazers_count'] as int,
      language: json['language'] as String?,
    );
  }
}

class GitHubException implements Exception {
  final String message;
  final bool isRateLimited;

  GitHubException(this.message, {this.isRateLimited = false});

  @override
  String toString() => message;
}

class GitHubService {
  final Dio _dio;

  GitHubService()
      : _dio = Dio(BaseOptions(
          baseUrl: 'https://api.github.com',
          headers: {
            'Accept': 'application/vnd.github+json',
            'X-GitHub-Api-Version': '2022-11-28',
          },
          connectTimeout: const Duration(seconds: 10),
          receiveTimeout: const Duration(seconds: 10),
        ));

  Future<List<Repository>> searchRepositories(String query) async {
    if (query.trim().isEmpty) {
      return [];
    }

    try {
      final response = await _dio.get(
        '/search/repositories',
        queryParameters: {
          'q': query,
          'sort': 'stars',
          'order': 'desc',
          'per_page': 30,
        },
      );

      final items = response.data['items'] as List<dynamic>;
      return items
          .map((json) => Repository.fromJson(json as Map<String, dynamic>))
          .toList();
    } on DioException catch (e) {
      if (e.response?.statusCode == 403) {
        final message = e.response?.data['message'] ?? '';
        if (message.toString().contains('rate limit')) {
          throw GitHubException(
            'API rate limit exceeded. Please try again later.',
            isRateLimited: true,
          );
        }
      }
      
      if (e.response?.statusCode == 422) {
        throw GitHubException('Invalid search query.');
      }
      
      if (e.type == DioExceptionType.connectionError) {
        throw GitHubException('No internet connection.');
      }
      
      throw GitHubException('Failed to search repositories.');
    }
  }
}