using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NarutoKurs.clases
{
    public class Hvost
    {
        public int ID { get; set; }
        public string CaudateName { get; set; }
        public string Description { get; set; }
        public string Abilities { get; set; }

        public Hvost(int id, string caudateName, string description, string abilities)
        {
            ID = id;
            CaudateName = caudateName;
            Description = description;
            Abilities = abilities;
        }
    }
}