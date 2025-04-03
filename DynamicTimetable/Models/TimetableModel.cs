using System.Collections.Generic;

namespace DynamicTimetable.Models
{
    public class SubjectModel
    {
        public string SubjectName { get; set; }
        public int Hours { get; set; }
        public int TotalHoursForWeek { get; set; }
    }

    public class TimetableRequest
    {
        public int WorkingDays { get; set; }
        public int SubjectsPerDay { get; set; }
        public int TotalSubjects { get; set; }
        public int TotalHoursForWeek { get; set; }
        public List<SubjectModel> Subjects { get; set; }

        public string SubjectName { get; set; }
        public int Hours { get; set; }
    }
}
