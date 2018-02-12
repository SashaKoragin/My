using System;
using System.Drawing;
using Microsoft.WindowsAPICodePack.Shell;

namespace TestIFNSTools.Detalizacia.WpfUserControl.IconsDetalization
{
   public class Icons
   {
       public static Icon Extracticonfile(String namefile)
       {
            
           var shell = ShellObject.FromParsingName(namefile);
           ShellThumbnail sh = shell.Thumbnail;
           return sh.MediumIcon;
       }
   }
}
