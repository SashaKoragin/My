using System.Windows.Controls;
using System.Windows.Threading;
using CovertTXTtoExcel.ConvertTXTtoEXCEL.AddItem;

namespace CovertTXTtoExcel.ConvertTXTtoEXCEL.Errors
{
    internal class Err
    {
        internal delegate object MyDelegate();
        
        public static bool FileError(int count, ErrTriger error)
        {
            if (count == 0)
            {
                error.Error = Errstext.ErrFile2;
                error.FocusedElement = false;
                return false;
            }
            error.FocusedElement = true;
            return true;
        }

        public static bool FileScalarError(Control control)
        {
            object resul = null;
            resul = control.Dispatcher.Invoke(DispatcherPriority.Background, (MyDelegate)delegate
            {
                var bindingExpression = control.GetBindingExpression(ItemsControl.ItemsSourceProperty);
                bindingExpression?.UpdateSource();
                return resul = !Validation.GetHasError(control);
            });
            return (bool)resul;
        }

        public static bool FileAddError(Control control)
        {
            object obj = null;
            obj = control.Dispatcher.Invoke(DispatcherPriority.Background,(MyDelegate) delegate
                {
                    var bindingExpression = control.GetBindingExpression(ItemsControl.ItemsSourceProperty);
                    bindingExpression?.UpdateSource();
                    return obj = !Validation.GetHasError(control);
                });
            return (bool)obj;
        }
    }
}



