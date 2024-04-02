namespace HRsytem
{
    internal class Department
    {
        private int id;
        public int ID { get { return id; } }
        public string Name { get; set; }

        //array of employees 

        public Department(string name = "No Department", int i = 0)
        {
            this.Name = name;
            this.id = i;
        }
    }
}