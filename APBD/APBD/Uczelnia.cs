
using System.Collections.Generic;

namespace APBD
{
    public class Uczelnia<Student, ActiveStudies>
    {
        public string createdAt { get; set; }
        public string author { get; set; }

        public List<Student> studentsList { get; set; }
        public List<ActiveStudies> activeStudiesList { get; set; }
    }
}
