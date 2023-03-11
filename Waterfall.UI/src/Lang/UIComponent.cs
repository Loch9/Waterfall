using System;
using System.Collections.ObjectModel;

namespace Waterfall.UI.Lang
{
    public interface UIComponent
    {
        UIComponent? GetParent();
        void SetParent(UIComponent parent);

        ObservableCollection<UIComponent> Children { get; set; }
    }
}
