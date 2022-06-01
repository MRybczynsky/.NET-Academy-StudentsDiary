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
using System.Xml.Serialization;

namespace StudentsDiary
{
    public partial class Main : Form
    {
        private string _filePath = Path.Combine(Environment.CurrentDirectory, "students.txt");
        public Main()
        {
            InitializeComponent();

            var students = DeserializeFromFile();

            dgvDiary.DataSource = students;



            //var students = new List<Student>();
            //students.Add(new Student { FirstName = "Jan" });
            //students.Add(new Student { FirstName = "Marek" });
            //students.Add(new Student { FirstName = "Małgosia" });

            //SerializeToFile(students);


            //var students = DeserializeFromFile();
            //foreach (var item in students)
            //{
            //    MessageBox.Show(item.FirstName);
            //}


            //var path = $@"{Path.GetDirectoryName(Application.ExecutablePath)}\..\NowyPlik3.txt";

            //System.IO.File.Create(@"C:\Users\Maciek\source\repos\StudentsDiary\NowyPlik.txt");
            //System.IO.File.Create($@"{System.IO.Path.GetDirectoryName(Application.ExecutablePath)}\..\NowyPlik2.txt");

            //if (!File.Exists(path))
            //{
            //    File.Create(path);
            //}

            //File.Delete(path);
            //File.WriteAllText(path, "Zostań programistą .NET");
            //File.AppendAllText(path, "\nAkademia .NET\n"); //automatycznie tworzy plik

            //var text = File.ReadAllText(path);

            //MessageBox.Show(text);



        }

        public void SerializeToFile(List<Student> students)
        {
            var serializer = new XmlSerializer(typeof(List<Student>));

            using (var streamWriter = new StreamWriter(_filePath))
            {
                serializer.Serialize(streamWriter, students);
                streamWriter.Close();
            }
        }

        public List<Student> DeserializeFromFile()
        {
            if (!File.Exists(_filePath))
                return new List<Student>();

            var serializer = new XmlSerializer(typeof(List<Student>));

            using (var streamReader = new StreamReader(_filePath))
            {
                var students = (List<Student>)serializer.Deserialize(streamReader);
                streamReader.Close();
                return students;
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            var addEditStudent = new AddEditStudent();
            addEditStudent.ShowDialog();
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvDiary.SelectedRows.Count == 0)
            {
                MessageBox.Show("Proszę zaznacz ucznia, którego dane chcesz edytować");
                return;//dzięki temu dalszy kod nie zostanie wykonany tylko od razu wyjdziemy z tej metody
            }

            var addEditStudent = new AddEditStudent(
                Convert.ToInt32(dgvDiary.SelectedRows[0].Cells[0].Value));
            addEditStudent.ShowDialog();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            var students = DeserializeFromFile();
            dgvDiary.DataSource = students;
        }
    }
}
