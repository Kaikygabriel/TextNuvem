namespace TextNuvem.Service;


public class EditorToolbarService
{
    public event Func<Task>? SaveRequested;

    public bool IsExplorerOpen { get; private set; } = true;
    public event Action? ExplorerChanged;

    public async Task RequestSaveAsync()
    {
        if (SaveRequested is not null)
            await SaveRequested.Invoke();
    }

    public void ToggleExplorer()
    {
        IsExplorerOpen = !IsExplorerOpen;
        ExplorerChanged?.Invoke();
    }
}