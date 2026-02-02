// Voice message feature
class VoiceMessageService {
  final Record _recorder = Record();
  String? _currentRecordingPath;
  DateTime? _recordingStartTime;
  
  // Check and request microphone permission
  Future<bool> requestPermission() async {
    // TODO: Request microphone permission
    throw UnimplementedError();
  }
  
  // Start recording audio
  Future<void> startRecording() async {
    // TODO: Start recording to temp file
    // Store start time for duration calculation
    throw UnimplementedError();
  }
  
  // Stop recording and return file path
  Future<RecordedAudio?> stopRecording() async {
    // TODO: Stop recording
    // Return recorded audio with path and duration
    throw UnimplementedError();
  }
  
  // Cancel recording (discard file)
  Future<void> cancelRecording() async {
    // TODO: Stop and delete temp file
    throw UnimplementedError();
  }
  
  // Upload audio to storage
  Future<String> uploadAudio(String filePath) async {
    // TODO: Upload to Firebase Storage or backend
    // Return download URL
    throw UnimplementedError();
  }
}

class RecordedAudio {
  final String filePath;
  final Duration duration;
  final List<double>? waveformData;
  
  RecordedAudio({
    required this.filePath,
    required this.duration,
    this.waveformData,
  });
}

// Voice recording button widget
class VoiceRecordButton extends StatefulWidget {
  final Function(RecordedAudio audio) onRecorded;
  final VoidCallback? onCancelled;
  
  const VoiceRecordButton({
    required this.onRecorded,
    this.onCancelled,
    super.key,
  });
  
  @override
  State<VoiceRecordButton> createState() => _VoiceRecordButtonState();
}

class _VoiceRecordButtonState extends State<VoiceRecordButton> {
  bool _isRecording = false;
  Duration _recordingDuration = Duration.zero;
  double _dragOffset = 0;
  
  @override
  Widget build(BuildContext context) {
    // TODO: Build hold-to-record button
    // Show duration while recording
    // Slide to cancel functionality
    throw UnimplementedError();
  }
}

// Voice message bubble widget
class VoiceMessageBubble extends StatefulWidget {
  final String audioUrl;
  final Duration duration;
  final bool isMe;
  
  const VoiceMessageBubble({
    required this.audioUrl,
    required this.duration,
    required this.isMe,
    super.key,
  });
  
  @override
  State<VoiceMessageBubble> createState() => _VoiceMessageBubbleState();
}

class _VoiceMessageBubbleState extends State<VoiceMessageBubble> {
  late AudioPlayer _player;
  bool _isPlaying = false;
  Duration _position = Duration.zero;
  
  @override
  Widget build(BuildContext context) {
    // TODO: Build audio player UI
    // Play/pause button
    // Progress bar
    // Duration display
    throw UnimplementedError();
  }
}