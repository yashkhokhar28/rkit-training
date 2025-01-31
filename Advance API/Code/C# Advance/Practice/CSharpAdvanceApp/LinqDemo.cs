namespace CSharpAdvanceApp
{
    /// <summary>
    /// This class demonstrates various LINQ operations including filtering, projection, set operations, sorting, quantifiers, partitioning, conversion, grouping, joining, and miscellaneous methods.
    /// </summary>
    public class LinqDemo
    {
        /// <summary>
        /// This method will execute the LINQ demonstration.
        /// </summary>
        public void RunLinqDemo()
        {
            // This method will execute the LINQ demonstration.
            FilteringMethods objFilteringMethods = new FilteringMethods();
            objFilteringMethods.RunFilteringMethods();

            Console.WriteLine("\n");

            ProjectionMethods objProjectionMethods = new ProjectionMethods();
            objProjectionMethods.RunProjectionMethods();

            Console.WriteLine("\n");

            SetOperations objSetOperations = new SetOperations();
            objSetOperations.RunSetOperations();

            Console.WriteLine("\n");

            SortingMethods objSortingMethods = new SortingMethods();
            objSortingMethods.RunSortingMethods();

            Console.WriteLine("\n");

            QuantifierMethods objQuantifierMethods = new QuantifierMethods();
            objQuantifierMethods.RunQuantifierMethods();

            Console.WriteLine("\n");

            PartitioningMethods objPartitioningMethods = new PartitioningMethods();
            objPartitioningMethods.RunPartitioningMethods();

            Console.WriteLine("\n");

            ConversionMethods objConversionMethods = new ConversionMethods();
            objConversionMethods.RunConversionMethods();

            Console.WriteLine("\n");

            GroupingMethods objGroupingMethods = new GroupingMethods();
            objGroupingMethods.RunGroupingMethods();

            Console.WriteLine("\n");

            JoinMethods objJoinMethods = new JoinMethods();
            objJoinMethods.RunJoinMethods();

            Console.WriteLine("\n");

            MiscellaneousMethods objMiscellaneousMethods = new MiscellaneousMethods();
            objMiscellaneousMethods.RunMiscellaneousMethods();
        }
    }

    /// <summary>
    /// This class demonstrates various LINQ filtering methods like Where(), First(), FirstOrDefault(), Single(), SingleOrDefault(), Last(), and LastOrDefault() on a list of students.
    /// </summary>
    public class FilteringMethods
    {
        List<StudentModel> lstStudent = new List<StudentModel>
    {
        new StudentModel { Id = 1, Name = "John", Age = 20 },
        new StudentModel { Id = 2, Name = "Jane", Age = 22 },
        new StudentModel { Id = 3, Name = "Mark", Age = 23 },
        new StudentModel { Id = 4, Name = "Sara", Age = 20 },
        new StudentModel { Id = 5, Name = "Tom", Age = 24 }
    };

        /// <summary>
        /// This method demonstrates the use of LINQ filtering methods on a list of students.
        /// </summary>
        public void RunFilteringMethods()
        {
            // Where() - Filters students with age < 23 (Method Syntax)
            List<StudentModel> youngStudentsMethod = lstStudent.Where(s => s.Age < 23).ToList();
            Console.WriteLine("Students younger than 23 (Method Syntax):");
            foreach (StudentModel student in youngStudentsMethod)
            {
                Console.WriteLine($"{student.Name} ({student.Age})");
            }

            // Where() - Filters students with age < 23 (Query Syntax)
            var youngStudentsQuery = from student in lstStudent
                                     where student.Age < 23
                                     select student;
            Console.WriteLine("\nStudents younger than 23 (Query Syntax):");
            foreach (var student in youngStudentsQuery)
            {
                Console.WriteLine($"{student.Name} ({student.Age})");
            }

            // First() - Retrieves the first student with age < 23 (Method Syntax)
            StudentModel firstYoungStudentMethod = lstStudent.First(s => s.Age < 23);
            Console.WriteLine($"\nFirst student younger than 23 (Method Syntax): {firstYoungStudentMethod.Name}");

            // First() - Retrieves the first student with age < 23 (Query Syntax)
            var firstYoungStudentQuery = (from student in lstStudent
                                          where student.Age < 23
                                          select student).First();
            Console.WriteLine($"\nFirst student younger than 23 (Query Syntax): {firstYoungStudentQuery.Name}");


            // FirstOrDefault() - Retrieves the first student with age > 25 (no match) (Method Syntax)
            StudentModel firstOldStudentMethod = lstStudent.FirstOrDefault(s => s.Age > 25);
            Console.WriteLine($"\nFirst student older than 25 (Method Syntax): {firstOldStudentMethod?.Name ?? "No match"}");

            // FirstOrDefault() - Retrieves the first student with age > 25 (no match) (Query Syntax)
            StudentModel firstOldStudentQuery = (from student in lstStudent
                                        where student.Age > 25
                                        select student).FirstOrDefault();
            Console.WriteLine($"\nFirst student older than 25 (Query Syntax): {firstOldStudentQuery?.Name ?? "No match"}");

            // Single() - Retrieves the student with ID 2 (Method Syntax)
            StudentModel singleStudentMethod = lstStudent.Single(s => s.Id == 2);
            Console.WriteLine($"\nStudent with ID 2 (Method Syntax): {singleStudentMethod.Name}");

            // Single() - Retrieves the student with ID 2 (Query Syntax)
            StudentModel singleStudentQuery = (from student in lstStudent
                                      where student.Id == 2
                                      select student).Single();
            Console.WriteLine($"\nStudent with ID 2 (Query Syntax): {singleStudentQuery.Name}");

            // SingleOrDefault() - Retrieves a student with a name "Michael" (no match) (Method Syntax)
            StudentModel singleOrDefaultStudentMethod = lstStudent.SingleOrDefault(s => s.Name == "Michael");
            Console.WriteLine($"\nStudent named Michael (Method Syntax): {singleOrDefaultStudentMethod?.Name ?? "No match"}");

            // SingleOrDefault() - Retrieves a student with a name "Michael" (no match) (Query Syntax)
            StudentModel singleOrDefaultStudentQuery = (from student in lstStudent
                                               where student.Name == "Michael"
                                               select student).SingleOrDefault();
            Console.WriteLine($"\nStudent named Michael (Query Syntax): {singleOrDefaultStudentQuery?.Name ?? "No match"}");

            // Last() - Retrieves the last student with age < 23 (Method Syntax)
            StudentModel lastYoungStudentMethod = lstStudent.Last(s => s.Age < 23);
            Console.WriteLine($"\nLast student younger than 23 (Method Syntax): {lastYoungStudentMethod.Name}");

            // Last() - Retrieves the last student with age < 23 (Query Syntax)
            StudentModel lastYoungStudentQuery = (from student in lstStudent
                                         where student.Age < 23
                                         select student).Last();
            Console.WriteLine($"\nLast student younger than 23 (Query Syntax): {lastYoungStudentQuery.Name}");

            // LastOrDefault() - Retrieves the last student with age > 25 (no match) (Method Syntax)
            StudentModel lastOlderStudentMethod = lstStudent.LastOrDefault(s => s.Age > 25);
            Console.WriteLine($"\nLast student older than 25 (Method Syntax): {lastOlderStudentMethod?.Name ?? "No match"}");

            // LastOrDefault() - Retrieves the last student with age > 25 (no match) (Query Syntax)
            StudentModel lastOlderStudentQuery = (from student in lstStudent
                                         where student.Age > 25
                                         select student).LastOrDefault();
            Console.WriteLine($"\nLast student older than 25 (Query Syntax): {lastOlderStudentQuery?.Name ?? "No match"}");
        }
    }

    /// <summary>
    /// This class demonstrates various LINQ projection methods such as Select(), SelectMany(), and anonymous types to project and transform data from a list of students.
    /// </summary>
    public class ProjectionMethods
    {
        List<StudentModel> lstStudent = new List<StudentModel>
    {
        new StudentModel
        {
            Id = 1,
            Name = "John",
            Age = 20,
            Subjects = new List<string> { "Math", "Physics" }
        },
        new StudentModel
        {
            Id = 2,
            Name = "Jane",
            Age = 22,
            Subjects = new List<string> { "Biology", "Chemistry" }
        },
        new StudentModel
        {
            Id = 3,
            Name = "Mark",
            Age = 23,
            Subjects = new List<string> { "History", "English", "Arts" }
        }
    };

        /// <summary>
        /// This method demonstrates the use of LINQ projection methods such as Select(), SelectMany(), and anonymous types on a list of students.
        /// </summary>
        public void RunProjectionMethods()
        {
            // Example 1: Select() - Project each student into a new form (e.g., their names)
            List<string> studentNames = lstStudent.Select(s => s.Name).ToList();
            Console.WriteLine("Student Names:");
            foreach (string name in studentNames)
            {
                Console.WriteLine(name);
            }

            // Example 2: Select() - Project into an anonymous type with selected properties
            var studentSummaries = lstStudent.Select(s => new
            {
                FullName = s.Name,
                Age = s.Age
            }).ToList();

            Console.WriteLine("\nStudent Summaries:");
            foreach (var summary in studentSummaries)
            {
                Console.WriteLine($"Name: {summary.FullName}, Age: {summary.Age}");
            }

            // Example 3: SelectMany() - Flatten the list of subjects
            List<string> allSubjects = lstStudent.SelectMany(s => s.Subjects).ToList();
            Console.WriteLine("\nAll Subjects:");
            foreach (string subject in allSubjects)
            {
                Console.WriteLine(subject);
            }

            // Example 4: SelectMany() - Combine with projection
            var studentSubjectPairs = lstStudent.SelectMany(
                s => s.Subjects, // Flatten the Subjects
                (student, subject) => new { student.Name, Subject = subject } // Combine student name with each subject
            ).ToList();

            Console.WriteLine("\nStudent-Subject Pairs:");
            foreach (var pair in studentSubjectPairs)
            {
                Console.WriteLine($"Student: {pair.Name}, Subject: {pair.Subject}");
            }
        }
    }

    /// <summary>
    /// This class demonstrates various LINQ set operations such as Distinct(), Union(), Intersect(), Except(), and Concat() on two lists of integers.
    /// </summary>
    public class SetOperations
    {
        List<int> lstNumbers1 = new List<int> { 1, 2, 3, 4, 5, 5, 6 };
        List<int> lstNumbers2 = new List<int> { 4, 5, 6, 7, 8, 8, 9 };

        /// <summary>
        /// This method demonstrates the use of LINQ set operations on two lists of integers.
        /// </summary>
        public void RunSetOperations()
        {
            // Distinct() - Removes duplicates from a collection
            List<int> distinctList1 = lstNumbers1.Distinct().ToList();
            Console.WriteLine("Distinct elements in list1:");
            foreach (int item in distinctList1)
            {
                Console.WriteLine(item);
            }

            // Union() - Combines two collections, removing duplicates
            List<int> unionList = lstNumbers1.Union(lstNumbers2).ToList();
            Console.WriteLine("\nUnion of list1 and list2:");
            foreach (int item in unionList)
            {
                Console.WriteLine(item);
            }

            // Intersect() - Returns the common elements between two collections
            List<int> intersectList = lstNumbers1.Intersect(lstNumbers2).ToList();
            Console.WriteLine("\nIntersection of list1 and list2:");
            foreach (int item in intersectList)
            {
                Console.WriteLine(item);
            }

            // Except() - Returns elements from the first collection not in the second
            List<int> exceptList = lstNumbers1.Except(lstNumbers2).ToList();
            Console.WriteLine("\nElements in list1 but not in list2:");
            foreach (int item in exceptList)
            {
                Console.WriteLine(item);
            }

            // Concat() - Combines two collections, including duplicates
            List<int> concatList = lstNumbers1.Concat(lstNumbers2).ToList();
            Console.WriteLine("\nConcatenation of list1 and list2:");
            foreach (int item in concatList)
            {
                Console.WriteLine(item);
            }
        }
    }

    /// <summary>
    /// This class demonstrates various LINQ sorting methods such as OrderBy(), OrderByDescending(), ThenBy(), ThenByDescending(), and Reverse() on a list of students.
    /// </summary>
    public class SortingMethods
    {
        List<StudentModel> lstStudents = new List<StudentModel>
    {
        new StudentModel { Id = 1, Name = "John", Age = 20, Grade = 85 },
        new StudentModel { Id = 2, Name = "Jane", Age = 22, Grade = 92 },
        new StudentModel { Id = 3, Name = "Mark", Age = 23, Grade = 85 },
        new StudentModel { Id = 4, Name = "Sara", Age = 20, Grade = 95 },
        new StudentModel { Id = 5, Name = "Tom", Age = 24, Grade = 85 }
    };

        /// <summary>
        /// This method demonstrates the use of LINQ sorting methods such as OrderBy(), OrderByDescending(), ThenBy(), ThenByDescending(), and Reverse() on a list of students.
        /// </summary>
        public void RunSortingMethods()
        {
            // OrderBy() - Sort by Age in ascending order
            List<StudentModel> sortedByAge = lstStudents.OrderBy(s => s.Age).ToList();
            Console.WriteLine("Students sorted by Age (ascending):");
            foreach (StudentModel student in sortedByAge)
            {
                Console.WriteLine($"{student.Name} (Age: {student.Age}, Grade: {student.Grade})");
            }

            // OrderByDescending() - Sort by Grade in descending order
            List<StudentModel> sortedByGradeDesc = lstStudents.OrderByDescending(s => s.Grade).ToList();
            Console.WriteLine("\nStudents sorted by Grade (descending):");
            foreach (StudentModel student in sortedByGradeDesc)
            {
                Console.WriteLine($"{student.Name} (Age: {student.Age}, Grade: {student.Grade})");
            }

            // ThenBy() - Sort by Grade, then by Age (both ascending)
            List<StudentModel> sortedByGradeThenAge = lstStudents.OrderBy(s => s.Grade).ThenBy(s => s.Age).ToList();
            Console.WriteLine("\nStudents sorted by Grade (ascending), then by Age (ascending):");
            foreach (StudentModel student in sortedByGradeThenAge)
            {
                Console.WriteLine($"{student.Name} (Age: {student.Age}, Grade: {student.Grade})");
            }

            // ThenByDescending() - Sort by Grade, then by Age (descending)
            List<StudentModel> sortedByGradeThenAgeDesc = lstStudents.OrderBy(s => s.Grade).ThenByDescending(s => s.Age).ToList();
            Console.WriteLine("\nStudents sorted by Grade (ascending), then by Age (descending):");
            foreach (StudentModel student in sortedByGradeThenAgeDesc)
            {
                Console.WriteLine($"{student.Name} (Age: {student.Age}, Grade: {student.Grade})");
            }

            // Reverse() - Reverse the order of the original collection
            List<StudentModel> reversedStudents = lstStudents.AsEnumerable().Reverse().ToList();
            Console.WriteLine("\nStudents in reversed order:");
            foreach (StudentModel student in reversedStudents)
            {
                Console.WriteLine($"{student.Name} (Age: {student.Age}, Grade: {student.Grade})");
            }
        }
    }

    /// <summary>
    /// This class demonstrates the use of LINQ quantifier methods such as Any(), All(), Count(), LongCount(), Sum(), Average(), Min(), Max(), SequenceEqual(), and Contains() on a list of students.
    /// </summary>
    public class QuantifierMethods
    {
        List<StudentModel> lstStudents = new List<StudentModel>
    {
        new StudentModel { Id = 1, Name = "John", Age = 20, Grade = 85 },
        new StudentModel { Id = 2, Name = "Jane", Age = 22, Grade = 92 },
        new StudentModel { Id = 3, Name = "Mark", Age = 23, Grade = 85 },
        new StudentModel { Id = 4, Name = "Sara", Age = 20, Grade = 95 },
        new StudentModel { Id = 5, Name = "Tom", Age = 24, Grade = 85 }
    };

        /// <summary>
        /// RunQuantifierMethods Demo
        /// </summary>
        public void RunQuantifierMethods()
        {
            // Any() - Check if any student has a grade above 90
            bool hasTopGrades = lstStudents.Any(s => s.Grade > 90);
            Console.WriteLine($"Any student with a grade above 90: {hasTopGrades}");

            // All() - Check if all students are older than 18
            bool allAdults = lstStudents.All(s => s.Age > 18);
            Console.WriteLine($"All students are adults: {allAdults}");

            // Count() - Count students with a grade above 80
            int countTopGrades = lstStudents.Count(s => s.Grade > 80);
            Console.WriteLine($"Number of students with grades above 80: {countTopGrades}");

            // LongCount() - Count students and return long (useful for large collections)
            long totalStudents = lstStudents.LongCount();
            Console.WriteLine($"Total number of students: {totalStudents}");

            // Sum() - Calculate the total grades of all students
            int totalGrades = lstStudents.Sum(s => s.Grade);
            Console.WriteLine($"Sum of all grades: {totalGrades}");

            // Average() - Calculate the average grade of all students
            double averageGrade = lstStudents.Average(s => s.Grade);
            Console.WriteLine($"Average grade: {averageGrade:F2}");

            // Min() - Find the minimum grade
            int minGrade = lstStudents.Min(s => s.Grade);
            Console.WriteLine($"Minimum grade: {minGrade}");

            // Max() - Find the maximum grade
            int maxGrade = lstStudents.Max(s => s.Grade);
            Console.WriteLine($"Maximum grade: {maxGrade}");

            // SequenceEqual() - Check if two sequences are equal
            List<int> lstSequence1 = new List<int> { 1, 2, 3 };
            List<int> lstSequence2 = new List<int> { 1, 2, 3 };
            bool areSequencesEqual = lstSequence1.SequenceEqual(lstSequence2);
            Console.WriteLine($"Sequences are equal: {areSequencesEqual}");

            // Contains() - Check if a student with a specific grade exists
            bool containsGrade = lstStudents.Any(s => s.Grade == 85); // Or use .Select() + .Contains()
            Console.WriteLine($"Contains a student with grade 85: {containsGrade}");
        }
    }

    /// <summary>
    /// This class demonstrates the use of LINQ partitioning methods such as TakeWhile(), SkipWhile(), Take(), Skip(), TakeLast(), and SkipLast() on a list of students.
    /// </summary>
    public class PartitioningMethods
    {
        List<StudentModel> lstStudent = new List<StudentModel>
    {
        new StudentModel { Id = 1, Name = "John", Age = 20 },
        new StudentModel { Id = 2, Name = "Jane", Age = 22 },
        new StudentModel { Id = 3, Name = "Mark", Age = 23 },
        new StudentModel { Id = 4, Name = "Sara", Age = 20 },
        new StudentModel { Id = 5, Name = "Tom", Age = 24 }
    };

        /// <summary>
        /// This method demonstrates the use of LINQ partitioning methods such as TakeWhile(), SkipWhile(), Take(), Skip(), TakeLast(), and SkipLast() on a list of students.
        /// </summary>
        public void RunPartitioningMethods()
        {
            // TakeWhile() - Takes students until age >= 23
            List<StudentModel> studentsUnder23 = lstStudent.TakeWhile(s => s.Age < 23).ToList();
            Console.WriteLine("\nStudents under 23:");
            foreach (StudentModel student in studentsUnder23)
            {
                Console.WriteLine($"{student.Name} ({student.Age})");
            }

            // SkipWhile() - Skips students until age >= 23
            List<StudentModel> studentsAfterAge23 = lstStudent.SkipWhile(s => s.Age < 23).ToList();
            Console.WriteLine("\nStudents after age 23:");
            foreach (StudentModel student in studentsAfterAge23)
            {
                Console.WriteLine($"{student.Name} ({student.Age})");
            }

            // Take() - Takes the first 3 students
            List<StudentModel> top3Students = lstStudent.Take(3).ToList();
            Console.WriteLine("\nTop 3 students:");
            foreach (StudentModel student in top3Students)
            {
                Console.WriteLine($"{student.Name} ({student.Age})");
            }

            // Skip() - Skips the first 2 students
            List<StudentModel> remainingStudents = lstStudent.Skip(2).ToList();
            Console.WriteLine("\nRemaining students after skipping first 2:");
            foreach (StudentModel student in remainingStudents)
            {
                Console.WriteLine($"{student.Name} ({student.Age})");
            }

            // TakeLast() - Take the last 2 students
            List<StudentModel> lastTwoStudents = lstStudent.TakeLast(2).ToList();
            Console.WriteLine("\nLast 2 students (using TakeLast):");
            foreach (StudentModel student in lastTwoStudents)
            {
                Console.WriteLine($"{student.Name} (Age: {student.Age}, Grade: {student.Grade})");
            }

            // SkipLast() - Skip the last 2 students
            List<StudentModel> allButLastTwo = lstStudent.SkipLast(2).ToList();
            Console.WriteLine("\nAll students except the last 2 (using SkipLast):");
            foreach (StudentModel student in allButLastTwo)
            {
                Console.WriteLine($"{student.Name} (Age: {student.Age}, Grade: {student.Grade})");
            }
        }
    }

    /// <summary>
    /// This class demonstrates the use of LINQ conversion methods such as ToList(), ToArray(), ToDictionary(), and OfType<T>().
    /// </summary>
    public class ConversionMethods
    {
        List<StudentModel> lstStudent = new List<StudentModel>
    {
        new StudentModel { Id = 1, Name = "John", Age = 20, Grade = 85 },
        new StudentModel { Id = 2, Name = "Jane", Age = 22, Grade = 92 },
        new StudentModel { Id = 3, Name = "Mark", Age = 23, Grade = 85 },
        new StudentModel { Id = 4, Name = "Sara", Age = 20, Grade = 95 },
        new StudentModel { Id = 5, Name = "Tom", Age = 24, Grade = 85 }
    };

        /// <summary>
        /// This method demonstrates the use of LINQ conversion methods such as ToList(), ToArray(), ToDictionary() and OfType<T>().
        /// </summary>
        public void RunConversionMethods()
        {
            // ToList() - Convert the sequence to a List
            List<StudentModel> studentList = lstStudent.ToList();
            Console.WriteLine("Students as a List:");
            studentList.ForEach(s => Console.WriteLine($"{s.Name} (Age: {s.Age}, Grade: {s.Grade})"));

            // ToArray() - Convert the sequence to an Array
            StudentModel[] studentArray = lstStudent.ToArray();
            Console.WriteLine("\nStudents as an Array:");
            foreach (StudentModel student in studentArray)
            {
                Console.WriteLine($"{student.Name} (Age: {student.Age}, Grade: {student.Grade})");
            }

            // ToDictionary() - Convert the sequence to a Dictionary with Id as the key
            Dictionary<int, StudentModel> studentDictionary = lstStudent.ToDictionary(s => s.Id);
            Console.WriteLine("\nStudents as a Dictionary:");
            foreach (KeyValuePair<int, StudentModel> kvp in studentDictionary)
            {
                Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value.Name} (Age: {kvp.Value.Age}, Grade: {kvp.Value.Grade})");
            }

            // OfType<T>() - Filter elements of a specific type
            List<Object> mixedCollection = new List<object> { 1, "Hello", 2, "World", 3.5, true };
            List<string> stringCollection = mixedCollection.OfType<string>().ToList();
            Console.WriteLine("\nFiltering strings from mixed collection:");
            stringCollection.ForEach(s => Console.WriteLine(s));
        }
    }

    /// <summary>
    /// This class demonstrates the use of LINQ grouping methods such as GroupBy(), GroupJoin(), and ToLookup().
    /// </summary>
    public class GroupingMethods
    {
        /// <summary>
        /// This method demonstrates the use of LINQ grouping methods such as GroupBy(), GroupJoin()
        /// </summary>
        public void RunGroupingMethods()
        {
            List<StudentModel> lstStudents = new List<StudentModel>
        {
            new StudentModel { Id = 1, Name = "John", Age = 20, Grade = 85 },
            new StudentModel { Id = 2, Name = "Jane", Age = 22, Grade = 92 },
            new StudentModel { Id = 3, Name = "Mark", Age = 23, Grade = 70 },
            new StudentModel { Id = 4, Name = "Sara", Age = 20, Grade = 95 },
            new StudentModel { Id = 5, Name = "Tom", Age = 24, Grade = 60 }
        };

            List<CourseModel> lstCourses = new List<CourseModel>
        {
            new CourseModel { CourseId = 1, StudentId = 1, CourseName = "Math" },
            new CourseModel { CourseId = 2, StudentId = 1, CourseName = "Science" },
            new CourseModel { CourseId = 3, StudentId = 2, CourseName = "History" },
            new CourseModel { CourseId = 4, StudentId = 3, CourseName = "Math" },
            new CourseModel { CourseId = 5, StudentId = 5, CourseName = "English" }
        };

            // GroupBy() - Group students by their Age
            IEnumerable<IGrouping<int, StudentModel>> groupedByAge = lstStudents.GroupBy(s => s.Age);
            Console.WriteLine("Students grouped by Age (using GroupBy):");
            foreach (IGrouping<int, StudentModel> group in groupedByAge)
            {
                Console.WriteLine($"Age: {group.Key}");
                foreach (StudentModel student in group)
                {
                    Console.WriteLine($"  {student.Name} (Grade: {student.Grade})");
                }
            }

            // GroupJoin() - Group courses with their corresponding students
            var groupJoinResult = lstStudents.GroupJoin(
                lstCourses,
                student => student.Id,
                course => course.StudentId,
                (student, studentCourses) => new
                {
                    StudentName = student.Name,
                    Courses = studentCourses.Select(c => c.CourseName)
                });

            Console.WriteLine("\nGroupJoin result (Students and their Courses):");
            foreach (var result in groupJoinResult)
            {
                Console.WriteLine($"Student: {result.StudentName}");
                Console.WriteLine($" Courses: {string.Join(", ", result.Courses)}");
            }
        }
    }

    /// <summary>
    /// This class demonstrates the use of LINQ join methods such as Join()
    /// </summary>
    public class JoinMethods
    {
        List<StudentModel> lstStudents = new List<StudentModel>
    {
        new StudentModel { Id = 1, Name = "John", Age = 20, Grade = 85 },
        new StudentModel { Id = 2, Name = "Jane", Age = 22, Grade = 92 },
        new StudentModel { Id = 3, Name = "Mark", Age = 23, Grade = 70 },
        new StudentModel { Id = 4, Name = "Sara", Age = 20, Grade = 95 },
        new StudentModel { Id = 5, Name = "Tom", Age = 24, Grade = 60 }
    };

        List<CourseModel> lstCourses = new List<CourseModel>
    {
        new CourseModel { CourseId = 1, StudentId = 1, CourseName = "Math" },
        new CourseModel { CourseId = 2, StudentId = 1, CourseName = "Science" },
        new CourseModel { CourseId = 3, StudentId = 2, CourseName = "History" },
        new CourseModel { CourseId = 4, StudentId = 3, CourseName = "Math" },
        new CourseModel { CourseId = 5, StudentId = 5, CourseName = "English" }
    };

        /// <summary>
        /// This method demonstrates the use of Join() and Zip() LINQ methods.
        /// </summary>
        public void RunJoinMethods()
        {
            List<int> scores = new List<int> { 85, 92, 70, 95, 60 };

            // Join() - Inner join between students and courses
            var joinResult = lstStudents.Join(
                lstCourses,
                student => student.Id,
                course => course.StudentId,
                (student, course) => new
                {
                    StudentName = student.Name,
                    CourseName = course.CourseName
                });

            Console.WriteLine("Inner Join result (Students and their Courses):");
            foreach (var result in joinResult)
            {
                Console.WriteLine($"Student: {result.StudentName}, Course: {result.CourseName}");
            }
        }
    }

    /// <summary>
    /// This class demonstrates the use of miscellaneous LINQ methods such as AsEnumerable(), ToString()
    /// </summary>
    public class MiscellaneousMethods
    {
        /// <summary>
        /// This method demonstrates the use of AsEnumerable(), ToString(), and DefaultIfEmpty() LINQ methods.
        /// </summary>
        public void RunMiscellaneousMethods()
        {
            List<StudentModel> lstStudents = new List<StudentModel>
        {
            new StudentModel { Id = 1, Name = "John", Age = 20 },
            new StudentModel { Id = 2, Name = "Jane", Age = 22 },
            new StudentModel { Id = 3, Name = "Mark", Age = 23 }
        };

            List<StudentModel> emptyList = new List<StudentModel>();

            // AsEnumerable(): Convert to IEnumerable for LINQ methods
            IEnumerable<StudentModel> enumerableStudents = lstStudents.AsEnumerable();
            Console.WriteLine("Students using AsEnumerable():");
            foreach (StudentModel student in enumerableStudents)
            {
                Console.WriteLine($"  {student.Name} (Age: {student.Age})");
            }

            // ToString(): Convert elements to string representation
            IEnumerable<string> studentNames = lstStudents.Select(s => s.ToString());
            Console.WriteLine("\nStudent string representations (using ToString() override):");
            foreach (string name in studentNames)
            {
                Console.WriteLine($"  {name}");
            }
        }
    }
}