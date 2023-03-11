using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace Waterfall.UI.Lang
{
    public class Label : UIComponent
    {
        internal UIComponent? Parent;
        public ObservableCollection<UIComponent> Children { get; set; } = new ObservableCollection<UIComponent>();

        public string Text { get; set; } = "";

        public Label()
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

        public Label(string text)
        {
            Text = text;

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
            Parent = parent;
        }
        
        public UIComponent? GetParent()
        {
            return Parent;
        }
    }
}

