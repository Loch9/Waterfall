using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Waterfall.UI.Lang
{
    public class UIWindow : UIComponent
    {
        public ObservableCollection<UIComponent> Children { get; set; } = new ObservableCollection<UIComponent>();

        public UIWindow()
        {
            Children.CollectionChanged += (sender, e) =>
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    if (sender != null)
                    {
                        for (int i = 0; i < ((ObservableCollection<UIComponent>)(sender)).Count; i++)
                        {
                            ((ObservableCollection<UIComponent>)(sender))[i].SetParent(this);
                        }
                    }
                }
            };
        }

        public void SetParent(UIComponent parent)
        {
        }

        public UIComponent? GetParent()
        {
            return null;
        }
    }
}
