using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RoomAndSpacesOV.Infrastructure.CustomControls
{
    public class CustomTubItem : TabItem
    {
        public CustomTubItem()
        {
            Visibility = Visibility.Hidden;
        }

        public int ListCount
        {
            get
            {
                return (int)GetValue(ListCountProperty);
            }
            set
            {
                SetValue(ListCountProperty, value);
                if (ListCount != 0)
                {
                    Background = Brushes.Pink;
                    Visibility = Visibility.Visible;
                }
            }
        }
        public static readonly DependencyProperty ListCountProperty =
           DependencyProperty.Register("ListCount", typeof(int), typeof(CustomTubItem),
            new FrameworkPropertyMetadata(0, new PropertyChangedCallback(PropertyNameChanged)));
        private static void PropertyNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CustomTubItem)d).ListCount = ((CustomTubItem)d).ListCount;
        }
    }
}
