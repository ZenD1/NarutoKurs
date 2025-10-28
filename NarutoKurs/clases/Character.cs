using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging; // Для BitmapImage

namespace NarutoKurs.clases
{

    public class Character
    {
        public class CharacterList
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int? ClanId { get; set; }
            public int? TeamId { get; set; }
            public int? RangId { get; set; }
            public int? VillageId { get; set; }
            public int? CaudateId { get; set; }
            public int? MainTechniqueId { get; set; }
            public int? Age { get; set; }
            public BitmapImage Photo { get; set; } 
            public byte[] Video { get; set; }

            public CharacterList(int id, string name, int? clanId, int? teamId, int? rangId, int? villageId, int? caudateId, int? mainTechniqueId, int? age, byte[] photo, byte[] video)
            {
                Id = id;
                Name = name;
                ClanId = clanId;
                TeamId = teamId;
                RangId = rangId;
                VillageId = villageId;
                CaudateId = caudateId;
                MainTechniqueId = mainTechniqueId;
                Age = age;
                Photo = ByteArrayToBitmapImage(photo); 
                Video = video;
            }

            // Метод конвертации массива байтов в BitmapImage
            private BitmapImage ByteArrayToBitmapImage(byte[] byteArray)
            {
                if (byteArray == null || byteArray.Length == 0) return null;

                BitmapImage bitmapImage = new BitmapImage();
                using (System.IO.MemoryStream memoryStream = new System.IO.MemoryStream(byteArray))
                {
                    memoryStream.Position = 0;
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = memoryStream;
                    bitmapImage.EndInit();
                }

                return bitmapImage;
            }
        }
    }
}