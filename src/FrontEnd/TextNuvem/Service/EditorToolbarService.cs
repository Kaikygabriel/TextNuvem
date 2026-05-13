namespace TextNuvem.Service;


public class EditorToolbarService
{
    public event Func<Task>? SaveRequested;

    public bool IsExplorerOpen { get; private set; } = true;
    public event Action? ExplorerChanged;
    
    public event Action? CloseExplorer;
    public event Action? OpenExplorer;
    
    public event Action? HasAlterState;
    
    public async Task RequestSaveAsync()
    {
        if (SaveRequested is not null)
            await SaveRequested.Invoke();
    }

    public void ToggleExplorer()
    {
        if(IsExplorerOpen)
            CloseExplorer?.Invoke();
        else
            OpenExplorer?.Invoke();
        
        IsExplorerOpen = !IsExplorerOpen;
        ExplorerChanged?.Invoke();
        HasAlterState?.Invoke();
    }
}