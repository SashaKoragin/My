using System;
using System.Collections.ObjectModel;
using System.IO;
using TestIFNSTools.Detalizacia.WpfUserControl.GenareteFindFile;

namespace TestIFNSTools.Detalizacia.WpfUserControl.Face.UL.File.Files
{
    internal class AddFile
    {
        internal ObservableCollection<FileInfo[]> AddF(string inn, string god,Detalizacia detal)
        {
            var generatefile = new GenerateFindFile();
            var seath = new SeathFile();
            var tableFile = seath.SeathF(inn,god,detal);
            return generatefile.GeheratePath(tableFile,detal, god);
        }
    }
}
