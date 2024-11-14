using BlazingTrails.Shared.Features.ManageTrails.Shared;

namespace BlazingTrails.Client.State
{
    public class NewTrailState
    {
        private TrailDto unsavedNewTrail = new();
        public void ClearTrail() => unsavedNewTrail = new();
        public TrailDto GetTrail() => unsavedNewTrail;
        public void SaveTrail(TrailDto trail) => unsavedNewTrail = trail;
    }
}