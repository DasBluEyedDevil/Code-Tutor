using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace CodeTutor.Wpf.Services;

public interface IInteractiveSession : IDisposable
{
    event EventHandler<string> OutputReceived;
    event EventHandler<string> ErrorReceived;
    event EventHandler<int> Exited;

    Task InputAsync(string text);
    Task StopAsync();
}

public class InteractiveProcessSession : IInteractiveSession
{
    private readonly Process _process;
    private readonly CancellationTokenSource _cts;
    private bool _isDisposed;

    public event EventHandler<string>? OutputReceived;
    public event EventHandler<string>? ErrorReceived;
    public event EventHandler<int>? Exited;

    public InteractiveProcessSession(Process process)
    {
        _process = process;
        _cts = new CancellationTokenSource();

        _process.OutputDataReceived += (s, e) => {
            if (e.Data != null) OutputReceived?.Invoke(this, e.Data + Environment.NewLine);
        };
        _process.ErrorDataReceived += (s, e) => {
            if (e.Data != null) ErrorReceived?.Invoke(this, e.Data + Environment.NewLine);
        };
        _process.Exited += (s, e) => Exited?.Invoke(this, _process.ExitCode);
        
        // Start reading asynchronously
        _process.BeginOutputReadLine();
        _process.BeginErrorReadLine();
    }

    public async Task InputAsync(string text)
    {
        if (_isDisposed || _process.HasExited) return;
        
        await _process.StandardInput.WriteLineAsync(text);
        await _process.StandardInput.FlushAsync();
    }

    public Task StopAsync()
    {
        if (!_process.HasExited)
        {
            try { _process.Kill(true); } catch { }
        }
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        if (_isDisposed) return;
        _isDisposed = true;
        _cts.Cancel();
        StopAsync();
        _process.Dispose();
        _cts.Dispose();
    }
}
