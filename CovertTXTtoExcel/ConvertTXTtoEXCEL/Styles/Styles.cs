using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CovertTXTtoExcel.ConvertTXTtoEXCEL.Styles
{
    public class Styles : Control
    {

        public object StilyColumn
        {
            get { return ColumnStyly(); }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                ColumnStyly();
            }
        }
        public object StilyColumnOthet
        {
            get { return StylyColumnOthet(); }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                StylyColumnOthet();
            }
        }

        public object Fon
        {
            get { return FonStyle(); }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                FonStyle();
            }
        }
        public object ColumnStyly()
        {
            var columnStyle = new Style(typeof(GridViewColumnHeader));
            columnStyle.Setters.Add(new Setter{Property = BackgroundProperty, Value = Brushes.Coral});
            columnStyle.Setters.Add(new Setter{Property = FontStyleProperty, Value = FontStyles.Italic});
            columnStyle.Setters.Add(new Setter{Property = FontWeightProperty, Value = FontWeights.Bold});
            columnStyle.Setters.Add(new Setter{Property = HorizontalAlignmentProperty, Value = HorizontalAlignment.Stretch});
            return columnStyle;
        }
        public object StylyColumnOthet()
        {
            var columnStyle = new Style(typeof(GridViewColumnHeader));
            columnStyle.Setters.Add(new Setter { Property = BackgroundProperty, Value = Brushes.Cyan });
            columnStyle.Setters.Add(new Setter { Property = FontStyleProperty, Value = FontStyles.Italic });
            columnStyle.Setters.Add(new Setter { Property = FontWeightProperty, Value = FontWeights.Bold });
            columnStyle.Setters.Add(new Setter { Property = HorizontalAlignmentProperty, Value = HorizontalAlignment.Stretch });
            return columnStyle;
        }
        public object FonStyle()
        {
            var fon = new Style(typeof (ListView));
            fon.Setters.Add(new Setter {Property = BackgroundProperty, Value = Brushes.Yellow });
            fon.Setters.Add(new Setter {Property = ForegroundProperty, Value = Brushes.Red });
            return fon;
        }
    }
}

  