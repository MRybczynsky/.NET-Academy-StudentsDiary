using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentsDiary
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();

            var path = $@"{Path.GetDirectoryName(Application.ExecutablePath)}\..\NowyPlik3.txt";

            //System.IO.File.Create(@"C:\Users\Maciek\source\repos\StudentsDiary\NowyPlik.txt");
            //System.IO.File.Create($@"{System.IO.Path.GetDirectoryName(Application.ExecutablePath)}\..\NowyPlik2.txt");
            if (!File.Exists(path))
            {
                File.Create(path);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {

        }
    }
}
