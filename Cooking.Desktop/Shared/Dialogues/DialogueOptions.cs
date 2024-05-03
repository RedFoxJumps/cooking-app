using System;

namespace Cooking.Desktop.Shared;

public class DialogueOptions<TObject>
    where TObject : new()
{
    public TObject Object { get; set; } = new();

    public Action<TObject> ActionResultCallback { get; set; } = delegate { };
}
