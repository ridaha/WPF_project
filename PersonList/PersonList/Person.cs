using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace PersonList
{
    [Serializable]
    public class Person 
    {
        public string name { get; set; }
        public string surname { get; set; }
        public string age { get; set; }
        public string image_way { get; set; }
        public string number { get; set; }
        public int persons_count { get; set; }

        public Person()
        {

        }
        public Person(string name, string surname, string age, string image_way,string number, ObservableCollection<Person> list)
        {
            this.name = name;
            this.surname = surname;
            this.age = age;
            this.image_way = image_way;
            this.number = number;
            if (list.Count != 0)
            {
                this.persons_count = list[list.Count() - 1].persons_count + 1;
            }
            else persons_count = 1;
        }
    }
 
}
