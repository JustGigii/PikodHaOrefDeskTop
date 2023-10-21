using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PikodAorfLayout.Class
{
    class Config
    {
        public static Config config;

        public Config()
        {

        }
        public Config(Config other)
        {
            this.ChoiseAlarm = other.ChoiseAlarm;
            this.choise = new Choise(other.choise);
        }
        public string ChoiseAlarm { get; set; }
        public Choise choise { get; set; }
    }
}
