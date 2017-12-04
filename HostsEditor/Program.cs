using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HostsEditor
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            catch(InvalidHostsFileException)
            {
                MessageBox.Show("Invalid hosts file, edit manually");
            }
            catch(Exception e)
            {
                MessageBox.Show($"Uncaught {e.GetType()}: {e.Message}");
            }
        }
    }
}
