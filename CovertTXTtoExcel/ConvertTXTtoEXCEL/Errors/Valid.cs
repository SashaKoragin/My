using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace CovertTXTtoExcel.ConvertTXTtoEXCEL.Errors
{
    public class Valid : ValidationRule
    {
        public string Names { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult lResult;
            switch (Names)
            {
                case "ListFile":
                    IEnumerable<object> lCollection = (IEnumerable<object>)value;
                    if (lCollection == null || !lCollection.Any())
                    {
                        lResult = new ValidationResult(false, Errstext.ErrFile1);
                    }
                    else
                    {
                        lResult = new ValidationResult(true, null);
                    }
                    return lResult;
                case "Scalare":
                  var val = System.Convert.ToString(value);
                    const string exp = @"[^\w]";
                    if (val == "")
                    {
                        lResult = new ValidationResult(true, Errstext.OkErrz);
                        return lResult;
                    }
                    if (Regex.IsMatch(val.Trim(), exp))
                    {
                        lResult = new ValidationResult(true, Errstext.OkReg1);
                        return lResult;
                    }
                    lResult = new ValidationResult(false, Errstext.ErrReg1);
                    return lResult;
                default:
                    throw new InvalidCastException();
            }
        }
    }
}
