using RoomsAndSpacesManagerDataBase.Dto.RoomInfrastructure;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RoomsAndSpacesManagerDesktop.Infrastructure.CustomControls
{
    class CustomTextBlockColumnTemplate : TextBlock
    {
        public string PropertyName
        {
            get
            {
                return (string)GetValue(PropertyNameProperty);
            }
            set
            {
                SetValue(PropertyNameProperty, value);
            }
        }

        public CustomTextBlockColumnTemplate()
        {
            TextWrapping = TextWrapping.Wrap;
        }

        public static readonly DependencyProperty PropertyNameProperty =
           DependencyProperty.Register("PropertyName", typeof(string), typeof(CustomTextBlockColumnTemplate),
            new FrameworkPropertyMetadata("", new PropertyChangedCallback(PropertyNameChanged)));

        private static void PropertyNameChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CustomTextBlockColumnTemplate)d).PropertyName = ((CustomTextBlockColumnTemplate)d).PropertyName;
        }

        public string FirstValue
        {
            get
            {
                return (string)GetValue(FirstValueProperty);
            }
            set
            {
                SetValue(FirstValueProperty, value);


                string propValue = SecondValue?.GetType().GetProperty(PropertyName)?.GetValue(SecondValue)?.ToString();
                if (FirstValue != propValue)
                {
                    Background = Brushes.Pink;
                    
                }
                else
                {
                    Background = Brushes.Transparent;
                }
            }
        }

        public static readonly DependencyProperty FirstValueProperty =
           DependencyProperty.Register("FirstValue", typeof(string), typeof(CustomTextBlockColumnTemplate),
            new FrameworkPropertyMetadata("", new PropertyChangedCallback(FirstValueChanged)));

        private static void FirstValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CustomTextBlockColumnTemplate)d).FirstValue = ((CustomTextBlockColumnTemplate)d).FirstValue;
        }

        public RoomNameDto SecondValue
        {
            get
            {
                return (RoomNameDto)GetValue(SecondValueProperty);
            }
            set
            {
                SetValue(SecondValueProperty, value);
                string propValue = SecondValue?.GetType().GetProperty(PropertyName)?.GetValue(SecondValue)?.ToString();
                if (FirstValue != propValue)
                {
                    Background = Brushes.Pink;
                }
                else
                {
                    Background = Brushes.Transparent;
                }
            }
        }

        public static readonly DependencyProperty SecondValueProperty =
           DependencyProperty.Register("SecondValue", typeof(RoomNameDto), typeof(CustomTextBlockColumnTemplate),
            new FrameworkPropertyMetadata(null, new PropertyChangedCallback(SecondValueChanged)));

        private static void SecondValueChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((CustomTextBlockColumnTemplate)d).SecondValue = ((CustomTextBlockColumnTemplate)d).SecondValue;
        }

    }
}
