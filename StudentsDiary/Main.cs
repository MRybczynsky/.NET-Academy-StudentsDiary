﻿using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace StudentsDiary
{
    public partial class Main : Form
    {
        private FileHelper<List<Student>> _fileHelper = new FileHelper<List<Student>>(Program.FilePath);
        public Main()
        {
            InitializeComponent();
            RefreshDiary();
            SetColumnHeader();

            var list1 = new List<string>();

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

        private void RefreshDiary()
        {
            var students = _fileHelper.DeserializeFromFile();
            dgvDiary.DataSource = students;

        }

        private void SetColumnHeader()
        {
            dgvDiary.Columns[0].HeaderText = "Numer";
            dgvDiary.Columns[1].HeaderText = "Imię";
            dgvDiary.Columns[2].HeaderText = "Nazwisko";
            dgvDiary.Columns[3].HeaderText = "Uwagi";
            dgvDiary.Columns[4].HeaderText = "Matematyka";
            dgvDiary.Columns[5].HeaderText = "Technologia";
            dgvDiary.Columns[6].HeaderText = "Fizyka";
            dgvDiary.Columns[7].HeaderText = "Język polski";
            dgvDiary.Columns[8].HeaderText = "Język obcy";
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
            if (dgvDiary.SelectedRows.Count == 0)
            {
                MessageBox.Show("Proszę zaznacz ucznia, którego dane chcesz usunąć");
                return;
            }

            var selectedStudent = dgvDiary.SelectedRows[0];

            var confirmDelete = 
                MessageBox.Show($"Czy na pewno chcesz usunąć ucznia {(selectedStudent.Cells[1].Value.ToString() + " " + selectedStudent.Cells[2].Value.ToString()).Trim()}", 
                "Usuwanie ucznia", MessageBoxButtons.OKCancel);

            if(confirmDelete == DialogResult.OK)
            {
                DeleteStudent(Convert.ToInt32(selectedStudent.Cells[0].Value));
                RefreshDiary();
            }
        }

        private void DeleteStudent(int id)
        {
            var students = _fileHelper.DeserializeFromFile();
            students.RemoveAll(x => x.Id == id);
            _fileHelper.SerializeToFile(students);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDiary();
        }
    }
}
