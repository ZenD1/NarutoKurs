using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NarutoKurs.clases
{
    public class CharacterInfo
    {

        public string CharacterName { get; set; }
        public string Rang { get; set; }
        public string Clan { get; set; }
        public string Caudate { get; set; }
        public string Teams { get; set; }
        public string RangTech { get; set; }
        public string Techniques { get; set; }
        public string TypeTech { get; set; }
        public string SubTypesTech { get; set; }
        public string Vilages { get; set; }
        public string Age { get; set; }
        public CharacterInfo(string characterName, string rang, string clan, string caudate, string teams, string rangtech, string techniques, string typetech, string subTypesTech, string vilages, string age)
        {
            CharacterName = characterName;
            Rang = rang;
            Clan = clan;
            Caudate = caudate;
            Teams = teams;
            RangTech = rangtech;
            Techniques = techniques;
            TypeTech = typetech;
            SubTypesTech = subTypesTech;
            Vilages = vilages; 
            Age = age; 
        }
    }
}