namespace BlazorAppFin.Client.Services;

public class ThemeState
{
    private bool _isDark = true;

    public bool IsDark
    {
        get => _isDark;
        set
        {
            if (_isDark != value)
            {
                _isDark = value;
                NotifyStateChanged();
            }
        }
    }

    public event Action? OnChange;

    private void NotifyStateChanged() => OnChange?.Invoke();
}
