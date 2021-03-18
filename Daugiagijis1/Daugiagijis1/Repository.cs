using System;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.ObjectModel;

namespace Daugiagijis1
{  
   
    class Repository
    {
        public static event EventHandler<FileInfo> FileFoundEvent ;
        public static event EventHandler<DirectoryInfo> DirectoryFoundEvent;
       // public ObservableCollection<FileInfo> FileFoundCollection;
        static public void WalkDirectoryTree(System.IO.DirectoryInfo root , string searchstring)
        {

            System.IO.FileInfo[] files = null;
            System.IO.DirectoryInfo[] subDirs = null;
            List<DirectoryInfo> foundDirs = new List<DirectoryInfo>();
            List<FileInfo> foundFiles = new List<FileInfo>();
            List<string> log = new List<string>();
            // MessageBox.Show($" folder :{ root.Name}");

            if (Regex.IsMatch(root.Name, searchstring, RegexOptions.IgnoreCase) == true)
            {
               // MessageBox.Show($"Found it biach : {root.FullName}");
                foundDirs.Add(root);
                DirectoryFoundEvent?.Invoke(DirectoryFoundEvent, root);
               // foreach (DirectoryInfo c in foundDirs)
                    //MessageBox.Show($"Found it biach : {c.FullName}");

            }
            // First, process all the files directly under this folder
            try
            {
               
                files = root.GetFiles($"*{searchstring}*.*");
                foreach(FileInfo c in files)
                {
                    foundFiles.Add(c);
                    FileFoundEvent?.Invoke(FileFoundEvent, c);
                   // FileFound
                }
                /*foreach (FileInfo b in foundFiles)
                {
                    MessageBox.Show($"KA AS TURIU LISTE {b.Name} ");

                }*/
                //  listener.Detach();
                //  foreach(FileInfo c in files)
                // MessageBox.Show($"FOund FILE *{c.FullName}");
            }
            // This is thrown if even one of the files requires permissions greater
            // than the application provides.
            catch (UnauthorizedAccessException e)
            {
                // This code just writes out the message and continues to recurse.
                // You may decide to do something different here. For example, you
                // can try to elevate your privileges and access the file again.
                log.Add(e.Message);
            }

            catch (System.IO.DirectoryNotFoundException e)
            {
                log.Add(e.Message);
            }

            if (files != null)
            {
                foreach (System.IO.FileInfo fi in files)
                {
                    // In this example, we only access the existing FileInfo object. If we
                    // want to open, delete or modify the file, then
                    // a try-catch block is required here to handle the case
                    // where the file has been deleted since the call to TraverseTree().
                    // MessageBox.Show(fi.FullName);
                }

                // Now find all the subdirectories under this directory.
                subDirs = root.GetDirectories();

                foreach (System.IO.DirectoryInfo dirInfo in subDirs)
                {
                    // Resursive call for each subdirectory.
                    WalkDirectoryTree(dirInfo,searchstring);
                }
            }
        }
    }
}
