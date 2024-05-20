namespace Api.Entities
{
    public class Person
    {
        public Person()
        {
        }
        public Person (string Name){
            this.Name = Name;
        }
        
        public string Name { get; set; }
    }
}
