using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemberWindow
{
    public class Storage
    {
        public static DataManager.Context ctx { get; private set; }
        public static DataManager.Entities.Member signedInMember { get; set; }

        public static void Initialize()
        {
            ctx = new DataManager.Context();
        }
    }
}
