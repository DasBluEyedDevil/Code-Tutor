interface IPlayable
{
    void Play();
    void Pause();
    void Stop();
}

interface IRecordable
{
    void Record();
    void SaveRecording();
}

class VideoPlayer : IPlayable
{
    public void Play()
    {
        Console.WriteLine("Playing video");
    }
    
    public void Pause()
    {
        Console.WriteLine("Video paused");
    }
    
    public void Stop()
    {
        Console.WriteLine("Video stopped");
    }
}

class AudioRecorder : IPlayable, IRecordable
{
    public void Play()
    {
        Console.WriteLine("Playing audio");
    }
    
    public void Pause()
    {
        Console.WriteLine("Audio paused");
    }
    
    public void Stop()
    {
        Console.WriteLine("Audio stopped");
    }
    
    public void Record()
    {
        Console.WriteLine("Recording audio");
    }
    
    public void SaveRecording()
    {
        Console.WriteLine("Saving recording");
    }
}

IPlayable player = new VideoPlayer();
player.Play();

AudioRecorder recorder = new AudioRecorder();
recorder.Play();
recorder.Record();