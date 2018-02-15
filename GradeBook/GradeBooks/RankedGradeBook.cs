using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = Enums.GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
            {
                throw new InvalidOperationException("Must be at least five students to return an average grade");
            }

            int threshold = (int)Math.Ceiling(Students.Count * 0.2);
            List<double> grades = Students
                .OrderByDescending(e => e.AverageGrade)
                .Select(s => s.AverageGrade)
                .ToList();

            if (grades[threshold - 1] <= averageGrade)
            {
                return 'A';
            }
            else if (grades[(threshold * 2) - 1] <= averageGrade)
            {
                return 'B';
            }
            else if (grades[(threshold * 3) - 1] <= averageGrade)
            {
                return 'C';
            }
            else if (grades[(threshold * 4) - 1] <= averageGrade)
            {
                return 'D';
            }
            else
            {
                return 'F';
            }

        }

        public override void CalculateStatistics()
        {
            if (Students.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades in order to properly calculate a student's overall grade");
                return;
            }
            base.CalculateStatistics();
        }

        public override void CalculateStudentStatistics(string name)
        {
            var gradeCheck = Students
                .Where(w => w.Grades.Count > 0)
                .Select(s => s.Grades)
                .ToList();
            if (Students.Count < 5 || gradeCheck.Count < 5)
            {
                Console.WriteLine("Ranked grading requires at least 5 students with grades" +
                                  "in order to properly calculate a student's overall grade");
                return;
            }

            base.CalculateStudentStatistics(name);
        }
    }
}
