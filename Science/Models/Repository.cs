using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Science.Models
{
    public class Repository
    {
        ScienceContext db = new ScienceContext();

        public IEnumerable<Dissertation> getListDissertations()
        {
            return db.Dissertations;
        }

        public IEnumerable<Type> getListTypes()
        {
            return db.Types;
        }

        public void addType(Type type)
        {
            db.Types.Add(type);
            db.SaveChanges();
        }

        public void addDissertation(Dissertation dissertation)
        {
            db.Dissertations.Add(dissertation);
            db.SaveChanges();
        }

        public Dissertation getDissertationFromId(int id)
        {
            return db.Dissertations.Find(id);
        }

        public bool dellDissertation(int id)
        {
            bool result = false;
            Dissertation b = getDissertationFromId(id);
            if (b != null)
            {
                db.Dissertations.Remove(b);
                db.SaveChanges();
                result = true;
            }
            return result;
        }
    }
}