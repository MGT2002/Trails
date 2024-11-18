using Blazored.LocalStorage;

namespace BlazingTrails.Client.State;

public class AppState(ILocalStorageService localStorageService)
{
    bool isInitialized;
    public NewTrailState NewTrailState { get; } = new();
    public FavoriteTrailsState FavoriteTrailsState { get; } = new(localStorageService);

    public async Task Initialize()
    {
        if (isInitialized)
            return;

        await FavoriteTrailsState.Initialize();
        isInitialized = true;
    }
}
