namespace ConsumingConsole
{
    public class DepartmentData
    {
        public int departmentId { get; set; }
        public string name { get; set; }
        public string description { get; set; }


        public override string ToString()
        {
            return $"Id = {departmentId} Name : {name} Description {description}";
        }
    }
}
