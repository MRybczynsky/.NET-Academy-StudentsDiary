using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentsDiary
{
    public partial class AddEditStudent : Form
    {
        private int _studentId;
        private Student _student;
        private FileHelper<List<Student>> _fileHelper = new FileHelper<List<Student>>(Program.FilePath);


        public AddEditStudent(int id = 0)
        {
            InitializeComponent();
            _studentId = id;
            GetStudentData();
            tbFirstname.Select(); //domyślne zaznaczenie pola
        }

        private void GetStudentData()
        {
        if (_studentId != 0)
        {
            Text = "Edytowanie danych ucznia";

            var students = _fileHelper.DeserializeFromFile();
            _student = students.FirstOrDefault(x => x.Id == _studentId);

            if (_student == null)
                throw new Exception("Brak użytkownika o podanym ID");
            }
        }

        private void FillTextBoxes()
        {
            tbID.Text = _student.Id.ToString();
            tbFirstname.Text = _student.FirstName;
            tbLastname.Text = _student.LastName;
            cmbIdGroup.Text = _student.IdGroup;
            tbMath.Text = _student.Math;
            tbPhysics.Text = _student.Physics;
            tbTech.Text = _student.Tech;
            tbPolish.Text = _student.Polish;
            tbFlanguage.Text = _student.FLanguage;
            cbActivities.Checked = _student.Activities;
            rtbComment.Text = _student.Comments;
            
        }

        private void AssignIdToNewStudent(List<Student> students)
        {
            var studentWithHighestId = students
                    .OrderByDescending(x => x.Id).FirstOrDefault();
            _studentId = studentWithHighestId == null ?
                 1 : studentWithHighestId.Id + 1;
        }
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            var students = _fileHelper.DeserializeFromFile();

            if (_studentId != 0)
                students.RemoveAll(x => x.Id == _studentId);
            else
                AssignIdToNewStudent(students);

            AddStudentToList(students);

            _fileHelper.SerializeToFile(students);

            Close();
        }
        private void AddStudentToList(List<Student> students)
        {
            var student = new Student
            {
                Id = _studentId,
                FirstName = tbFirstname.Text,
                LastName = tbLastname.Text,
                Comments = rtbComment.Text,
                Math = tbMath.Text,
                Tech = tbTech.Text,
                Physics = tbPhysics.Text,
                Polish = tbPolish.Text,
                FLanguage = tbFlanguage.Text,
                Activities = cbActivities.Checked,
                IdGroup = (string)cmbIdGroup.Text
            };

            students.Add(student);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
