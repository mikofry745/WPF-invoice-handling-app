using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ObsługaFaktur.Controls
{
    public class MenuButton : Button
    {
        public static readonly DependencyProperty IsActiveProperty =
            DependencyProperty.Register("IsActive", typeof(bool), typeof(MenuButton),
            new PropertyMetadata(false));

        public bool IsActive
        {
            get { return (bool)GetValue(IsActiveProperty); }
            set { SetValue(IsActiveProperty, value); }
        }

        protected override void OnClick()
        {
            Activate();
            base.OnClick();
        }

        private void Activate()
        {
            Panel parent = GetParent<Panel>();

            if (parent == null)
            {
                return;
            }

            foreach (MenuButton menuButton in GetChildren(parent))
            {
                menuButton.IsActive = false;
            }

            IsActive = true;
        }

        private IEnumerable<MenuButton> GetChildren(DependencyObject parent)
        {
            if (parent == null)
            {
                yield break;
            }

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);

                if (child is MenuButton menuButton)
                {
                    yield return menuButton;
                }

                foreach (MenuButton childButton in GetChildren(child))
                {
                    yield return childButton;
                }
            }
        }

        private T GetParent<T>() where T : DependencyObject
        {
            DependencyObject parent = VisualTreeHelper.GetParent(this);

            while (parent != null && !(parent is Panel))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return (T)parent;
        }
    }
}

