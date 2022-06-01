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
    public partial class AddEditStudent : Form
    {
        private string _filePath = Path.Combine(Environment.CurrentDirectory, "students.txt");

        public AddEditStudent(int id = 0)
        {
            InitializeComponent();

            if(id!=0)
            {
                var students = DeserializeFromFile();
                var student = students.FirstOrDefault(x => x.Id == id);

                if (student == null)
                    throw new Exception("Brak użytkownika o podanym ID");

                tbID.Text = student.Id.ToString();
                tbFirstname.Text = student.FirstName;
                tbLastname.Text = student.LastName;
                tbMath.Text = student.Math;
                tbPhysics.Text = student.Physics;
                tbTech.Text = student.Tech;
                tbPolish.Text = student.Polish;
                tbFlanguage.Text = student.FLanguage;
                rtbComment.Text = student.Comments;
            }
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

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            var students = DeserializeFromFile();

            var studentWithHighestId = students
                .OrderByDescending(x => x.Id).FirstOrDefault();

            //var studentId = 0;

            //if (studentWithHighestId == null)
            //{
            //    studentId = 1;
            //}
            //else
            //{
            //    studentId = studentWithHighestId.Id + 1;
            //} rownoważne z linijką niżej

            var studentId = studentWithHighestId == null ? 1 : studentWithHighestId.Id + 1;

            var student = new Student
            {
                Id = studentId,
                FirstName = tbFirstname.Text,
                LastName = tbLastname.Text,
                Comments = rtbComment.Text,
                Math = tbMath.Text,
                Tech = tbTech.Text,
                Physics = tbPhysics.Text,
                Polish = tbPolish.Text,
                FLanguage = tbFlanguage.Text
            };

            students.Add(student);

            SerializeToFile(students);

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }
    }
}
