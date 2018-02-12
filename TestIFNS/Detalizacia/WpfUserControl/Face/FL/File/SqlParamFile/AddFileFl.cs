using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace TestIFNSTools.Detalizacia.WpfUserControl.Face.FL.File.SqlParamFile
{
    public class AddFileFl
    {
        internal ObservableCollection<FileInfo[]> AddF(string god, List<string> param, Detalizacia detal)
        {
            var generatefile = new GenareteFindFile.GenerateFindFile();
            var seath = new ZaprosFileFl();
            var tablefilefl = seath.Zaprosfilefl(god, param, detal);
            return generatefile.GeheratePath(tablefilefl, detal,god);
            //dddddddd
        }
    }
}
