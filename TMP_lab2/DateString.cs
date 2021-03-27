using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tmp_lab2
{
    public class DateString
    {
        private int level;
        private string nameUzel;
        private int status;
        private string nameMethod;
        private bool isParent = false; 

        public int Level { get => level; set => level = value; }
        public string NameUzel { get => nameUzel; set => nameUzel = value; }
        public int Status { get => status; set => status = value; }
        public string NameMethod { get => nameMethod; set => nameMethod = value; }
        public bool IsParent { get => isParent; set => isParent = value; }
    }
}