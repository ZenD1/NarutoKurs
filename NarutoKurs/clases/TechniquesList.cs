using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NarutoKurs.clases
{
    public class TechniquesList
    {
        public int TechniqueID { get; set; }
        public string TechniqueName { get; set; }
        public string RangTechTier { get; set; }
        public string SubtypeName { get; set; }
        public string TypeName { get; set; }

        // Конструктор для инициализации объектов класса TechniquesList
        public TechniquesList(int techniqueID, string techniqueName, string rangTechTier, string subTypeName, string typeName)
        {
            TechniqueID = techniqueID;
            TechniqueName = techniqueName;
            RangTechTier = rangTechTier;
            SubtypeName = subTypeName;
            TypeName = typeName;
        }
    }
}