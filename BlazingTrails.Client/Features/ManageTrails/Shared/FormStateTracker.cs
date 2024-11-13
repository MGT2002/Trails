using BlazingTrails.Client.State;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazingTrails.Client.Features.ManageTrails.Shared;

public class FormStateTracker : ComponentBase
{
    [Inject]
    public AppState AppState { get; set; }
    [CascadingParameter]
    private EditContext CascadedEditContext { get; set; }

    protected override void OnInitialized()
    {
        if (CascadedEditContext is null)
        {
            throw new InvalidOperationException($"{nameof(FormStateTracker)} requires a cascading parameter of type {nameof(EditContext)}");
        }
        CascadedEditContext.OnFieldChanged += CascadedEditContext_OnFieldChanged;
    }
}
