using BlazingTrails.Client.Features.Shared;
using Blazored.LocalStorage;

namespace BlazingTrails.Client.State;

public class FavoriteTrailsState(ILocalStorageService localStorageService)
{
    private const string FavoriteTrailsKey = "favoriteTrails";
    private readonly ILocalStorageService localStorageService = localStorageService;
    private bool isInitialized;
    private List<Trail> favoriteTrails = new();

    public IReadOnlyList<Trail> FavoriteTrails => favoriteTrails.AsReadOnly();
    public event Action? OnChange;


    public async Task Initialize()
    {
        if (!isInitialized)
        {
            favoriteTrails = await localStorageService.GetItemAsync<List<Trail>>(FavoriteTrailsKey) ?? new();
            isInitialized = true;
            NotifyStateHasChanged();
        }
    }

    public async Task AddFavorite(Trail trail)
    {
        if (favoriteTrails.Any(_ => _.Id == trail.Id))
            return;

        favoriteTrails.Add(trail);
        await localStorageService.SetItemAsync(FavoriteTrailsKey, favoriteTrails);
        NotifyStateHasChanged();
    }

    public async Task RemoveFavorite(Trail trail)
    {
        if (favoriteTrails.Remove(trail))
        {
            await localStorageService.SetItemAsync(FavoriteTrailsKey, favoriteTrails);
            NotifyStateHasChanged();
        }
    }

    public bool IsFavorite(Trail trail)
    {
        return favoriteTrails.Any(_ => _.Id == trail.Id);
    }
    private void NotifyStateHasChanged() => OnChange?.Invoke();
}
