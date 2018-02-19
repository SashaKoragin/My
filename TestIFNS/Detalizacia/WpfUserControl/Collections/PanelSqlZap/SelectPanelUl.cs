using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using Prism.Mvvm;

namespace TestIFNSTools.Detalizacia.WpfUserControl.Collections.PanelSqlZap
{
    public class SelectPanelUl : BindableBase, IDataErrorInfo
    {
        private string _innul;
        public string InnUl
        {
            get { return _innul; }
            set { _innul = value; }
        }

        public string Error { get; set; }
        private bool IsValid { get; set; } = true;

        public bool IsValidation()
        {
            IsValid = false;
            RaisePropertyChanged("InnUl");
            return IsValid;
        }
        public string this[string columnName]
        {
            get { return ValidateErrs(columnName); }
        }

        private string ValidateErrs(string columnName)
        {
            Error = null;
            if (!IsValid)
                switch (columnName)
                {
                    case "InnUl":
                            Error = ValidInnUl(Error);
                        break;
                }
            return Error;
        }

        public string ValidInnUl(string error)
        {
            const string expr = @"^(?:\d{10})$";
            const string expr1 = @"^(?:\d{12})$";
            if (string.IsNullOrWhiteSpace(InnUl))
            {
                error = "Не введен ИНН!!!";
                IsValid = true;
                return error;
            }
            else
            {
                if (!Regex.IsMatch(InnUl.Trim(), expr) & !Regex.IsMatch(InnUl.Trim(), expr1))
                {
                    error = "Не соответствует количеству чисел ЮЛ!!!";
                    IsValid = true;
                    return error;
                }
                //if (!KontrSummUl(InnUl.Trim(), ref error))
                //{
                //    IsValid = true;
                //    return error;
                //}
                
        }
            return error;
        }

        //public bool KontrSummUl(string innul, ref string error)
        //{
        //    int sum;
        //    char[] s = innul.ToCharArray();
        //    int[] a = Array.ConvertAll(s, c => (int)char.GetNumericValue(c));
        //    sum = a[0] * 2 + a[1] * 4 + a[2] * 10 + a[3] * 3 + a[4] * 5 + a[5] * 9 + a[6] * 4 + a[7] * 6 + a[8] * 8;
        //    if ((a[9] == sum % 11))
        //    {
        //        return true;
        //    }
        //    else
        //    {
        //        error = "Контрольное число не соответствует последним цифрам " + a[9];
        //        return false;
        //    }
        //}
    }

    public class SelectPanelFl : BindableBase, IDataErrorInfo
    {
        private string _innfl;
        private string _serianomerpasport;
        private string _familia;
        private string _names;
        private string _middlename;

        public string InnFl
        {
            get { return _innfl; }
            set
            {
                _innfl = value; 
                
            }
        }

        public string SeriaNomerPasport
        {
            get { return _serianomerpasport; }
            set
            {
                _serianomerpasport = value;
            }
        }

        public string Familia
        {
            get { return _familia; }
            set
            {
                _familia = value;
            }
        }

        public string Name
        {
            get { return _names; }
            set
            {
                _names = value;
            }
        }

        public string MiddleName
        {
            get { return _middlename; }
            set
            {
                _middlename = value;
            }
        }

        public string Error { get; set; }
        private bool IsValid { get; set; } = true;
        public bool IsValidation()
        {
            IsValid = false;
            RaisePropertyChanged("InnFl");
            RaisePropertyChanged("SeriaNomerPasport");
            RaisePropertyChanged("Name");
            RaisePropertyChanged("Familia");
            RaisePropertyChanged("MiddleName");
            return IsValid;
        }
        public string this[string columnName]
        {
            get { return ValidateErrs(columnName); }
        }

        private string ValidateErrs(string columnName)
        {
            Error = null;
            if (!IsValid)
                switch (columnName)
                {
                    case "InnFl":
                        Error = ValidInn(Error); break;
                    case "SeriaNomerPasport":
                        Error = ValidSeriaNumer(Error); break;
                    case "Name":
                        Error = ValidName(Error); break;
                    case "Familia":
                        Error = ValidFamilia(Error); break;
                    case "MiddleName":
                        Error = ValidMiddleName(Error);break;
                }
            return Error;
        }

        public string ValidInn(string error)
        {
            const string expr = @"^(?:\d{12})$";
            if ((string.IsNullOrWhiteSpace(InnFl) && !string.IsNullOrWhiteSpace(SeriaNomerPasport))|| (string.IsNullOrWhiteSpace(InnFl) && (!string.IsNullOrWhiteSpace(Name) || !string.IsNullOrWhiteSpace(Familia) || !string.IsNullOrWhiteSpace(MiddleName))))
            {}
            else
            {
                if (string.IsNullOrWhiteSpace(InnFl))
                { 
                error = "Не введен ИНН!!!";
                IsValid = true;
                return error;
                }
                if (!Regex.IsMatch(InnFl.Trim(), expr))
                {
                    error = "Не соответствует количеству чисел!!!";
                    IsValid = true;
                    return error;
                }
                if (!KontrlSumm(InnFl.Trim(), ref error))
                {
                    IsValid = true;
                    return error;
                }
            }
            return error;
        }

        private bool KontrlSumm(string inn,ref string error)
        {
            int sum;
            int sum2;
            char[] s = inn.ToCharArray();
            int[] a = Array.ConvertAll(s, c => (int) char.GetNumericValue(c));
            sum = a[0] * 7 + a[1] * 2 + a[2] * 4 + a[3] * 10 + a[4] * 3 + a[5] * 5 + a[6] * 9 + a[7] * 4 + a[8] * 6 + a[9] * 8;
            sum2 = a[0] * 3 + a[1] * 7 + a[2] * 2 + a[3] * 4 + a[4] * 10 + a[5] * 3 + a[6] * 5 + a[7] * 9 + a[8] * 4 + a[9] * 6 + a[10] * 8;
            if (a[10] == sum % 11 && a[11] == sum2 % 11)
            {
                return true;
            }
            else
            {
                error = "Контрольное число не соответствует последним цифрам " + a[10] + " и " + a[11];
                return false;
            }
        }

        public string ValidSeriaNumer(string error)
        {
            const string expr = @"^\d{2} \d{2} \d{6}$";
            if ((string.IsNullOrWhiteSpace(SeriaNomerPasport)&&(!string.IsNullOrWhiteSpace(Name)|| !string.IsNullOrWhiteSpace(Familia)|| !string.IsNullOrWhiteSpace(MiddleName)))|| (string.IsNullOrWhiteSpace(SeriaNomerPasport)&& !string.IsNullOrWhiteSpace(InnFl)))
            {}
            else
            {
                if (string.IsNullOrWhiteSpace(SeriaNomerPasport))
                {
                    error = "Не введена серия номер паспорта!!!";
                    IsValid = true;
                    return error;
                }
                if (!Regex.IsMatch(SeriaNomerPasport.Trim(), expr))
                {
                    error = "Не соответствует формату паспорта!!!";
                    IsValid = true;
                    return error;
                }
            }
          return error;
        }

        public string ValidName(string error)
        {
            if (!string.IsNullOrWhiteSpace(Name)||string.IsNullOrWhiteSpace(Name)&& (!string.IsNullOrWhiteSpace(SeriaNomerPasport)|| !string.IsNullOrWhiteSpace(InnFl))|| !string.IsNullOrWhiteSpace(Name) && (string.IsNullOrWhiteSpace(SeriaNomerPasport) || string.IsNullOrWhiteSpace(InnFl)))
            { }
            else
            {
                error = "Не введено Имя!!!";
                IsValid = true;
            }
            return error;
        }

        public string ValidFamilia(string error)
        {
            if (!string.IsNullOrWhiteSpace(Familia) || string.IsNullOrWhiteSpace(Familia) && (!string.IsNullOrWhiteSpace(SeriaNomerPasport) || !string.IsNullOrWhiteSpace(InnFl))|| !string.IsNullOrWhiteSpace(Familia) && (string.IsNullOrWhiteSpace(SeriaNomerPasport) || string.IsNullOrWhiteSpace(InnFl)))
            { }
            else
            {
                error = "Не введена Фамилия";
                IsValid = true;
            }
            return error;
        }

        public string ValidMiddleName(string error)
        {
            if (!string.IsNullOrWhiteSpace(MiddleName) || string.IsNullOrWhiteSpace(MiddleName) && (!string.IsNullOrWhiteSpace(SeriaNomerPasport) || !string.IsNullOrWhiteSpace(InnFl))|| !string.IsNullOrWhiteSpace(MiddleName) && (string.IsNullOrWhiteSpace(SeriaNomerPasport) || string.IsNullOrWhiteSpace(InnFl)))
            { }
            else
            {
                error = "Не введено Отчество";
                IsValid = true;
            }
            return error;
        }
    }
}