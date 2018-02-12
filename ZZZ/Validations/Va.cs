using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml;
using ZZZ.Pages;
using ZZZ.Znachenie;
using System.ComponentModel;
using System.Globalization;

namespace ZZZ.Validations
{
    public class Va : ValidationRule
    {
        public  String Name { get; set; }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            var val = Convert.ToString(value);
            switch ( Name )
               {
                case "OtdelP2":
                       if (!String.IsNullOrWhiteSpace(val)) return new ValidationResult(true, null);
                       return new ValidationResult(false,"Поле не может быть пустым!!!");
                case "NameUserP2":
                       if (!String.IsNullOrWhiteSpace(val)) return new ValidationResult(true, null);
                       return new ValidationResult(false, "Поле не может быть пустым!!!");
                case "TabelP2":
                       if (!String.IsNullOrWhiteSpace(val)) return new ValidationResult(true, null);
                       return new ValidationResult(false, "Поле не может быть пустым!!!");
                default:
                    throw new InvalidCastException();
               }
        }
    }
}
