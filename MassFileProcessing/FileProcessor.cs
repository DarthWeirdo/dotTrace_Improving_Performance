using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;

namespace MassFileProcessing
{
    internal class FileProcessor
    {
        public List<string> FilePaths;
        private string[] _lines;
        public readonly BackgroundWorker Worker;
        private readonly ProgressUpdater _updater;


        public FileProcessor()
        {
            _updater = new ProgressUpdater();
            Worker = new BackgroundWorker {WorkerReportsProgress = true};
            Worker.DoWork += ProcessFiles;
        }

        private void ProcessFiles(object sender, DoWorkEventArgs evts)
        {
            try
            {

                var reverser = new StringReverser();  

                _updater.TotalFiles = FilePaths.Count;

                for (var i = 0; i < FilePaths.Count; i++)
                {
                    
                    _updater.CurrentFileNmb = i + 1;

                    var path = FilePaths[i];
                    _lines = File.ReadAllLines(path);
                    
                    for (var j = 0; j < _lines.Length; j++)
                    {
                        var line = _lines[j];
                        _lines[j] = reverser.Reverse(line);

                        if (j%1000 != 0) continue;
                        var p = ((float)(j + 1) / _lines.Length) * 100;
                        Worker.ReportProgress((int) p, _updater);
                    }

                    File.WriteAllLines(path, _lines);                    
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
            
        }

    }
}