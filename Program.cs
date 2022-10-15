public class Program
{
    public static void Main(string[] args)
    {
        Creator creator = new Creator();
        Console.WriteLine("Welcome.");
        bool newList = true;
        while (newList)
        {
            Student[] students = creator.GetList();
            IComparable comparable = creator.GetComparable();
            students[0].SeeSortedList(students, comparable);
            Console.WriteLine("Would you like to see the same list sorted differently?");
            string response = Console.ReadLine();
        }
    }
    public interface IValidate
    {
        public int ValidNumber();
        public string ValidString();
        public string ValidName(string whichName);
        public string ValidDecision();
        public int ValidStudentRange();
        public int ValidScore();
        public int ValidComparable();
    }
    public class Validate : IValidate
    {
        public int ValidNumber()
        {
            int num = 0;
            bool isNum = false;
            while (!isNum)
            {
                Console.WriteLine("\nPlease enter a number.");
                string response = Console.ReadLine();
                isNum = int.TryParse(response, out num);
                if (!isNum)
                {
                    Console.WriteLine("\nSorry, I did not catch that.");
                }
            }
            return num;
        }
        public string ValidString()
        {
            string response = "";
            bool notNull = false;
            while (!notNull)
            {
                response = Console.ReadLine();
                notNull = !string.IsNullOrEmpty(response);
                if (!notNull)
                {
                    Console.WriteLine("\nSorry, I didn't catch that.");
                }
                else
                {
                    bool notWord = decimal.TryParse(response, out decimal word);
                    if (notWord)
                    {
                        notNull = false;
                        Console.WriteLine("\nPlease enter a name.");
                    }
                }
            }
            return response;
        }
        public string ValidDecision()
        {
            bool validDecision = false;
            string response = "";
            while (!validDecision)
            {
                response = ValidString();
                if (response.Equals("Y", StringComparison.CurrentCultureIgnoreCase) || response.Equals("N", StringComparison.CurrentCultureIgnoreCase))
                {
                    validDecision = true;
                }
                else
                {
                    validDecision = false;
                    Console.WriteLine("\nI'm sorry, I didn't catch that.");
                }
            }
            return response;
        }
        public string ValidName(string whichName)
        {
            Console.WriteLine($"\nPlease enter the students {whichName} name.");
            string name = ValidString();
            return name;
        }
        public int ValidStudentRange()
        {
            Console.WriteLine("\nHow big is the list?");
            int range = 0;
            bool isValid = false;
            while (!isValid)
            {
                range = ValidNumber();
                if (range < 1 || range > 10)
                {
                    isValid = false;
                    Console.WriteLine("\nSorry, we can only do lists from 1-10 students");
                }
                else
                {
                    isValid = true;
                }
            }
            return range;
        }
        public int ValidScore()
        {
            int score = 0;
            bool isValid = false;
            while (!isValid)
            {
                score = ValidNumber();
                if (score < 0 || score > 100)
                {
                    isValid = false;
                    Console.WriteLine("\nStudents can only score 0-100.");
                }
                else
                {
                    isValid = true;
                }
            }
            return score;
        }
        public int ValidComparable()
        {
            bool isValid = false;
            int choice = 0;
            Console.WriteLine("\nPlease enter 1 to sort by last name, 2 for first name and 3 for score.");
            while (!isValid)
            {
                choice = ValidNumber();
                if (choice == 1 || choice == 2 || choice == 3)
                {
                    isValid = true;
                }
                else
                {
                    isValid = false;
                    Console.WriteLine("\nSorry, I didn't catch that.");
                }
            }
            return choice;
        }
    }
    public interface ICreator
    {
        public Student GetStudent();
        public Student[] GetList();
        public IComparable GetComparable();
    }
    public class Creator : ICreator
    {
        private Validate validator = new Validate();

        public Student GetStudent()
        {
            Student newStudent = new Student();
            newStudent.firstName = validator.ValidName("first");
            newStudent.lastName = validator.ValidName("last");
            newStudent.score = validator.ValidScore();
            return newStudent;
        }
        public Student[] GetList()
        {
            int range = validator.ValidStudentRange();
            var students = new List<Student>();
            for (int i = 0; i < range; i++)
            {
                students.Add(GetStudent());
            }
            return students.ToArray();
        }
        public IComparable GetComparable()
        {
            IComparable comparable = null;
            int choice = validator.ValidComparable();
            if (choice == 1)
            {
                comparable = new LastComparable();
            }
            else if (choice == 2)
            {
                comparable = new FirstComparable();
            }
            else
            {
                comparable = new ScoreComparable();
            }
            return comparable;
        }
    }
    public interface IComparable
    {
        public Student[] SortStudents(Student[] students);
    }
    public class LastComparable : IComparable
    {
        public Student[] SortStudents(Student[] students)
        {
            var studentsByLastName = from s in students orderby s.lastName select s;
            return studentsByLastName.ToArray();
        }
    }
    public class FirstComparable : IComparable
    {
        public Student[] SortStudents(Student[] students)
        {
            var studentsByFirstName = from s in students orderby s.firstName select s;
            return studentsByFirstName.ToArray();
        }
    }
    public class ScoreComparable : IComparable
    {
        public Student[] SortStudents(Student[] students)
        {
            var studentsByScore = from s in students orderby s.score select s;
            return studentsByScore.ToArray();
        }
    }
    public class Student
    {
        public string lastName { get; set; }
        public string firstName { get; set; }
        public int score { get; set; }
        public Student(string newLast, string newFirst, int newScore)
        {
            lastName = newLast;
            firstName = newFirst;
            score = newScore;
        }
        public Student()
        {

        }
        public void SeeSortedList(Student[] students, IComparable comparable)
        {
            Console.WriteLine("\nHere is your sorted list:");
            foreach (var student in comparable.SortStudents(students))
            {
                Console.WriteLine($"{student.firstName} {student.lastName} scored {student.score}%.");
            }
        }
    }
}