interface IPlayable
{
    // Define methods
}

interface IRecordable
{
    // Define methods
}

class VideoPlayer : IPlayable
{
    // Implement IPlayable
}

class AudioRecorder : IPlayable, IRecordable
{
    // Implement both interfaces
}