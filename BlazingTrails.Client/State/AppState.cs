using BlazingTrails.Shared.Features.ManageTrails.Shared;

namespace BlazingTrails.Client.State;

public class AppState
{
    private TrailDto unsavedNewTrail = new();
    public TrailDto GetTrail() => unsavedNewTrail;
    public void SaveTrail(TrailDto trail) => unsavedNewTrail = trail;
    public void ClearTrail() => unsavedNewTrail = new();
}
