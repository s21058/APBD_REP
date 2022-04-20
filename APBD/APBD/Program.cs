 using System.Collections;
 using System.Diagnostics.CodeAnalysis;
 using System.Text.Json;
 using System.Text.Json.Serialization;

 namespace APBD
 {
     public static class Program

     {
         public static string logPath;
         private static void Main(string[] args)
         {
             var path = args[0];
             logPath = @"C:\Users\Sviatoslav\RiderProjects\APBD\APBD\Models\log.txt";
             File.WriteAllText(logPath,"");
             var fileInfo = new FileInfo(path);
             var reader = new StreamReader(fileInfo.OpenRead());
             string line;
             List<Student> studentList = new List<Student>();
             List<ActiveStudies> activeStudiesList = new List<ActiveStudies>();
             while ((line = reader.ReadLine()) != null)
             { 
                 String[] split = line.Split(",");
                 Studies studies = new Studies(split[2], split[3]);

                 Student student = new Student
                 {
                     Name = split[0],
                     surName = split[1],
                     index = split[4],
                     birthDay = split[5],
                     eMail = split[6],
                     mothersName = split[7],
                     fathersName = split[8]
                 };
                 if (args.Length < 9)
                 {
                     
                     File.AppendAllText(logPath,student.Name+","+student.surName+","+student.index+"\n");
                 }
                 else
                 {
                     studentList.Add(student);
                     student.studies = studies;
                 }

                 if (string.IsNullOrWhiteSpace(student.Name) || string.IsNullOrWhiteSpace(student.surName) ||
                     string.IsNullOrWhiteSpace(student.index) || string.IsNullOrWhiteSpace(student.birthDay) ||
                     string.IsNullOrWhiteSpace(student.eMail) || string.IsNullOrWhiteSpace(student.fathersName) ||
                     string.IsNullOrWhiteSpace(student.mothersName))
                 {
                     File.AppendAllText(logPath,student.Name+","+student.surName+","+student.index+"\n");

                 }
                 else
                 {
                     studentList.Add(student);
                     student.studies = studies;
                 }
             }
             studentList = studentList.Distinct(new Comparator()).ToList();
             
             foreach (var item in studentList)
             {
                 ActiveStudies active = new ActiveStudies();
                 active.name = item.studies.studiesName;
                 active.numberOfStudents = 1;
                 activeStudiesList.Add(active);
             }

             activeStudiesList = activeStudiesList.Distinct(new ComparatorStudies()).ToList();
             Uczelnia<Student, ActiveStudies> uczelnia = new Uczelnia<Student, ActiveStudies>
             {
                 author = "Sviatoslav Bozhko",
                 createdAt = DateTime.Now.ToString(),
                 studentsList = studentList,
                 activeStudiesList = activeStudiesList
             };
             var options = new JsonSerializerOptions
             {
                 WriteIndented = true
             };
             var jscon = JsonSerializer.Serialize(uczelnia,options);
        
             File.WriteAllText(@"C:\Users\Sviatoslav\RiderProjects\APBD\APBD\Models\Result.txt",jscon);
             // File.WriteAllLines();
         }
         
     }
     class Comparator : IEqualityComparer<Student>
     {
         public bool Equals(Student x, Student y)
         {
             // if (x.Name.Equals(y.Name)&&x.surName.Equals(y.surName)&&x.index.Equals(y.index))
             // {
             //   File.AppendAllText(Program.logPath,x.Name+","+x.surName+","+x.index+"\n");
             // }
             return x.Name.Equals(y.Name) && x.surName.Equals(y.surName) && x.index.Equals(y.index);
         }

         public int GetHashCode([DisallowNull] Student obj)
         {
             return 0;
         }
     }
     class ComparatorStudies : IEqualityComparer<ActiveStudies>
     {
         public bool Equals(ActiveStudies x, ActiveStudies y)
         {
             if (x.name.Equals(y.name))
             {
                 x.numberOfStudents++;
             }

             return x.name.Equals(y.name);
         }

         public int GetHashCode([DisallowNull] ActiveStudies obj)
         {
             return 0;
         }
     }
 }