namespace MVCDemoLab.Models
{
    public class Student
    {
        public static List<Student> Students = new List<Student>() {
          new Student(){  Id = 1 , Name = "tamer" , Age = 20 },
          new Student(){  Id = 2 , Name = "tamer2" , Age = 30 },
          new Student(){  Id = 3 , Name = "tamer3" , Age = 40 },
          new Student(){  Id = 4 , Name = "tamer4" , Age = 50 },
          new Student(){  Id = 5 , Name = "tamer5" , Age = 25 }
        };


        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
