using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controller
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private List<Student> studentList = new List<Student>();

        public StudentsController()
        {
            FileInfo info = new FileInfo(@".\Data\dane.csv");
            StreamReader reader = new StreamReader(info.OpenRead());
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                string[] arr = line.Split(",");
                Student student = new Student
                {
                    Name = arr[0],
                    surName = arr[1],
                    studies = arr[2],
                    mode = arr[3],
                    index = arr[4],
                    birthDay = arr[5],
                    eMail = arr[6],
                    fathersName = arr[7],
                    mothersName = arr[8]
                };
                studentList.Add(student);
            }

            reader.Close();
        }

        [HttpGet]
        public IEnumerable<Student> GetStudentsList()
        {
            return studentList;
        }

        [HttpGet("{index}")]
        public ObjectResult getStudentByIndexNumber(string index)
        {
            foreach (var variable in studentList)
            {
                if (variable.index.Equals(index))
                {
                    return Ok(variable);
                }
            }

            return StatusCode((int) HttpStatusCode.BadRequest, index + " was not found in database");
        }

        [HttpPut("{index}")]
        public ObjectResult putStudentByIndexNumber(string index, Student student)
        {
            bool checkIfContains = studentList.Any(p => p.index == index);
            if (checkIfContains == true)
            {
            }
            else
            {
                return StatusCode((int) HttpStatusCode.BadRequest, index + " was not found in database");
            }

            if (string.IsNullOrWhiteSpace(student.Name) || string.IsNullOrWhiteSpace(student.surName) ||
                string.IsNullOrWhiteSpace(student.index) || string.IsNullOrWhiteSpace(student.birthDay) ||
                string.IsNullOrWhiteSpace(student.eMail) || string.IsNullOrWhiteSpace(student.fathersName) ||
                string.IsNullOrWhiteSpace(student.mothersName) || string.IsNullOrWhiteSpace(student.mode) ||
                string.IsNullOrWhiteSpace(student.studies))
            {

                return StatusCode((int) HttpStatusCode.BadRequest, " Some of values was empty or missed");
            }
            else
            {
                if (index.Equals(student.index))
                {
                    for (int i = 0; i < studentList.Count; i++)
                    {
                        if (studentList[i].index.Equals(student.index))
                        {
                            studentList[i] = student;
                        }
                    }

                    StreamWriter writer = new StreamWriter(@".\Data\dane.csv", false);
                    foreach (var variable in studentList)
                    {
                        writer.Write(variable.Name + "," + variable.surName + "," + variable.studies + "," +
                                     variable.mode + "," + variable.index
                                     + "," + variable.birthDay + "," + variable.eMail + "," + variable.mothersName +
                                     "," + variable.fathersName);
                    }

                    writer.Close();
                    return StatusCode((int) HttpStatusCode.OK, " All data was updated");
                }
                else
                {
                    return StatusCode((int) HttpStatusCode.BadRequest,
                        "Were providing 2 different ID " + index + " and " + student.index);
                }
            }

        }

        [HttpPost]
        public ObjectResult postStudentByIndexNumber(Student student)
        {
            if (string.IsNullOrWhiteSpace(student.Name) || string.IsNullOrWhiteSpace(student.surName) ||
                string.IsNullOrWhiteSpace(student.index) || string.IsNullOrWhiteSpace(student.birthDay) ||
                string.IsNullOrWhiteSpace(student.eMail) || string.IsNullOrWhiteSpace(student.fathersName) ||
                string.IsNullOrWhiteSpace(student.mothersName) || string.IsNullOrWhiteSpace(student.mode) ||
                string.IsNullOrWhiteSpace(student.studies)) {

                return StatusCode((int) HttpStatusCode.BadRequest, " Some of values was empty or missed");
            }

            var checkIfUnique = studentList.Any(p => p.index.Contains(student.index));
            if(checkIfUnique==false){
                    return StatusCode((int) HttpStatusCode.BadRequest, student.index + " was not unique");
            }
            else
            {
                StreamWriter writer = new StreamWriter(@".\Data\dane.csv", false);
                foreach (var variable in studentList)
                {
                    writer.Write(variable.Name + "," + variable.surName + "," + variable.studies + "," +
                                 variable.mode + "," + variable.index
                                 + "," + variable.birthDay + "," + variable.eMail + "," + variable.mothersName +
                                 "," + variable.fathersName);
                }

                writer.Close();
            }

            return StatusCode((int) HttpStatusCode.OK, " new Student was successfully added");
        }

        [HttpDelete("{index}")]
        public ObjectResult deleteStudentByIndexNumber(string index)
        {
            bool checkIfExist = studentList.Any(p => p.index.Contains(index));
            if (checkIfExist == true)
            {
                for (int i = 0; i < studentList.Count; i++)
                {
                    if (studentList[i].index.Equals(index))
                    {
                        studentList.RemoveAt(i);
                    }
                }

                StreamWriter writer = new StreamWriter(@".\Data\dane.csv", false);
                foreach (var variable in studentList)
                {
                    writer.Write(variable.Name + "," + variable.surName + "," + variable.studies + "," +
                                 variable.mode + "," + variable.index
                                 + "," + variable.birthDay + "," + variable.eMail + "," + variable.mothersName +
                                 "," + variable.fathersName);
                }

                writer.Close();
            }
            else
            {
                return StatusCode((int) HttpStatusCode.BadRequest, index + " was not found in database");
            }

            return StatusCode((int) HttpStatusCode.OK, "Student with index: " + index + " was successfully deleted");
        }
    }
}