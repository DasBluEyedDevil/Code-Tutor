// server/lib/src/services/content_moderator.dart

class ContentModerator {
  // Banned words with severity levels
  static const Map<String, int> _bannedWords = {
    'spam': 3,
    'scam': 3,
    'fake': 1,
    // Severity: 1 = warn, 2 = review, 3 = block
  };
  
  // Leetspeak mappings
  static const Map<String, String> _leetspeak = {
    '0': 'o', '1': 'i', '3': 'e', '4': 'a',
    '5': 's', '7': 't', '@': 'a', '\$': 's',
  };

  /// Check content for moderation issues
  Future<ModerationResult> checkContent(
    String content, {
    ModerationLevel level = ModerationLevel.standard,
  }) async {
    final reasons = <String>[];
    double spamScore = 0.0;
    bool isBlocked = false;
    bool requiresReview = false;
    
    // Normalize content for checking
    final normalized = _normalizeContent(content);
    
    // 1. Check for banned words
    final bannedFound = _findBannedWords(normalized);
    for (final word in bannedFound) {
      final severity = _bannedWords[word] ?? 1;
      if (severity >= 3) {
        isBlocked = true;
        reasons.add('Prohibited word detected: $word');
      } else if (severity >= 2) {
        requiresReview = true;
        reasons.add('Flagged word detected: $word');
      }
      spamScore += severity * 0.1;
    }
    
    // 2. Check for spam patterns
    final spamPatterns = _detectSpamPatterns(content);
    reasons.addAll(spamPatterns);
    spamScore += spamPatterns.length * 0.15;
    
    // 3. Check for excessive caps
    final capsRatio = _calculateCapsRatio(content);
    if (capsRatio > 0.5 && content.length > 10) {
      spamScore += 0.2;
      if (level == ModerationLevel.strict) {
        reasons.add('Excessive use of capital letters');
      }
    }
    
    // 4. Check for repeated characters
    if (_hasExcessiveRepeats(content)) {
      spamScore += 0.15;
      reasons.add('Suspicious repeated characters');
    }
    
    // 5. Check URL count
    final urlCount = _countUrls(content);
    if (urlCount > 2) {
      spamScore += urlCount * 0.1;
      if (urlCount > 5) {
        requiresReview = true;
        reasons.add('Excessive URLs detected');
      }
    }
    
    // Apply level-based thresholds
    spamScore = spamScore.clamp(0.0, 1.0);
    
    final blockThreshold = level == ModerationLevel.strict ? 0.5 :
                           level == ModerationLevel.standard ? 0.7 : 0.9;
    
    if (spamScore >= blockThreshold) {
      isBlocked = true;
    } else if (spamScore >= blockThreshold - 0.2) {
      requiresReview = true;
    }
    
    return ModerationResult(
      isBlocked: isBlocked,
      requiresReview: requiresReview,
      reasons: reasons,
      spamScore: spamScore,
    );
  }
  
  /// Normalize content for comparison
  String _normalizeContent(String content) {
    var normalized = content.toLowerCase();
    
    // Convert leetspeak
    _leetspeak.forEach((leet, letter) {
      normalized = normalized.replaceAll(leet, letter);
    });
    
    // Remove extra spaces
    normalized = normalized.replaceAll(RegExp(r'\s+'), ' ');
    
    return normalized;
  }
  
  /// Check if content contains banned words
  List<String> _findBannedWords(String content) {
    final found = <String>[];
    final words = content.split(RegExp(r'[\s.,!?;:]+'));
    
    for (final word in words) {
      if (_bannedWords.containsKey(word)) {
        found.add(word);
      }
    }
    
    // Also check for banned words as substrings (for evasion attempts)
    for (final banned in _bannedWords.keys) {
      if (content.contains(banned) && !found.contains(banned)) {
        found.add(banned);
      }
    }
    
    return found;
  }
  
  /// Detect spam patterns
  List<String> _detectSpamPatterns(String content) {
    final patterns = <String>[];
    
    // Check for repeated phrases
    final words = content.split(' ');
    if (words.length >= 3) {
      for (var i = 0; i < words.length - 2; i++) {
        final phrase = '${words[i]} ${words[i + 1]} ${words[i + 2]}';
        final count = RegExp(RegExp.escape(phrase)).allMatches(content).length;
        if (count > 2) {
          patterns.add('Repeated phrase detected');
          break;
        }
      }
    }
    
    // Check for phone number patterns (spam indicator)
    if (RegExp(r'\d{3}[-.]?\d{3}[-.]?\d{4}').hasMatch(content)) {
      patterns.add('Phone number detected');
    }
    
    // Check for email patterns (potential spam)
    final emailCount = RegExp(r'[\w.-]+@[\w.-]+\.\w+').allMatches(content).length;
    if (emailCount > 1) {
      patterns.add('Multiple email addresses detected');
    }
    
    // Check for money/crypto patterns
    if (RegExp(r'\$\d+|\d+\s*(btc|eth|crypto|nft)', caseSensitive: false)
        .hasMatch(content)) {
      patterns.add('Financial content detected');
    }
    
    return patterns;
  }
  
  /// Calculate ratio of capital letters
  double _calculateCapsRatio(String content) {
    final letters = content.replaceAll(RegExp(r'[^a-zA-Z]'), '');
    if (letters.isEmpty) return 0.0;
    
    final caps = letters.replaceAll(RegExp(r'[^A-Z]'), '').length;
    return caps / letters.length;
  }
  
  /// Check for excessive repeated characters
  bool _hasExcessiveRepeats(String content) {
    // Check for 4+ same character in a row
    return RegExp(r'(.)\1{3,}').hasMatch(content);
  }
  
  /// Count URLs in content
  int _countUrls(String content) {
    return RegExp(
      r'https?://[^\s]+|www\.[^\s]+',
      caseSensitive: false,
    ).allMatches(content).length;
  }
}

enum ModerationLevel { relaxed, standard, strict }

class ModerationResult {
  final bool isBlocked;
  final bool requiresReview;
  final List<String> reasons;
  final double spamScore;
  
  ModerationResult({
    required this.isBlocked,
    required this.requiresReview,
    required this.reasons,
    required this.spamScore,
  });
  
  bool get isClean => !isBlocked && !requiresReview && reasons.isEmpty;
}