using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using DynamicTimetable.Models;

namespace DynamicTimetable.Controllers
{
    public class TimetableController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult _SubjectInputs(int model)
        //{
        //    return PartialView(model);
        //}

        [HttpPost]
        public IActionResult _SubjectInputs(TimetableRequest model)
        {

            return PartialView("_SubjectInputs", model);
        }


        [HttpPost]
        public JsonResult GenerateTimetable([FromBody] TimetableRequest request)
        {
            if (request == null || request.WorkingDays <= 0 || request.SubjectsPerDay <= 0 || request.Subjects == null)
            {
                return Json(new { error = "Invalid input data." });
            }

            int workingDays = request.WorkingDays;
            int subjectsPerDay = request.SubjectsPerDay;
            List<SubjectModel> subjects = request.Subjects;

            int totalHours = workingDays * subjectsPerDay;
            List<List<string>> timetable = new List<List<string>>();

            
            for (int i = 0; i < subjectsPerDay; i++)
            {
                timetable.Add(new List<string>(new string[workingDays]));
            }

            int row = 0, col = 0;

            
            foreach (var subject in subjects)
            {
                int remainingHours = subject.Hours;

                while (remainingHours > 0)
                {
                    if (row >= subjectsPerDay) row = 0;
                    if (col >= workingDays) col = 0;

                    if (string.IsNullOrEmpty(timetable[row][col]))
                    {
                        timetable[row][col] = subject.SubjectName;
                        remainingHours--;

                        col++;
                        if (col >= workingDays)
                        {
                            col = 0;
                            row++;
                        }
                    }
                    else
                    {
                        col++;
                        if (col >= workingDays)
                        {
                            col = 0;
                            row++;
                        }
                    }
                }
            }

            return Json(timetable);
        }
    }


}
