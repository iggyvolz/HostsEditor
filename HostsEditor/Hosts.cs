using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HostsEditor
{
    /// <summary>
    /// Controls the hosts file
    /// </summary>
    class HostsController
    {
        public class Entry
        {
            public string IP;
            public string Name;
        }
        public List<Entry> Entries;
        private string file;

        public HostsController(string file)
        {
            this.file = file;
            Import();
        }
        public void Import()
        {
            Entries = new List<Entry>();
            StreamReader reader = new StreamReader(File.OpenRead(file));
            string line;
            while((line=reader.ReadLine()) != null) // Read line to end
            {
                if(line.Length == 0 || line[0]=='#' || string.IsNullOrWhiteSpace(line))
                {
                    continue; // Ignore line
                }
                // Split by space and get components
                try
                {
                    // Replace tabs with spaces so we don't have to handle both cases
                    line = line.Replace('\t', ' ');
                    string[] split = line.Split(' ').Except(new string[] { "" }).ToArray(); // Remove empty elements from array
                    Entry ent = new Entry();
                    ent.IP = split[0];
                    ent.Name = split[1];
                    Entries.Add(ent);
                }
                catch(IndexOutOfRangeException)
                {
                    throw new InvalidHostsFileException();
                }
            }
            reader.Close();
        }

        public void Export()
        {
            StreamWriter writer = new StreamWriter(File.Open(file,FileMode.Truncate));
            foreach(Entry entry in Entries)
            {
                writer.WriteLine(entry.IP + "\t" + entry.Name);
            }
            writer.Close();
        }
    }
}
