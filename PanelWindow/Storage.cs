using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PanelWindow
{
    public static class Storage
    {
        public static DataManager.Context ctx { get; private set; }
        /*        public static DataManager.Logic.UnitOfWork uow { get; private set; }*/
        public static DataManager.Entities.Coach signedInCoach { get; set; }

        public static void Initialize()
        {
            ctx = new DataManager.Context();
            /*uow = new DataManager.Logic.UnitOfWork(ctx);*/
        }
    }
}
