using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Science.Models.ModelView;

namespace Science.Models
{
    public class Repository
    {
        ScienceContext db = new ScienceContext();

        public List<DissertationModalView> getListDissertations()
        {
            //return db.Dissertations;
            List<DissertationModalView> lDMV = new List<DissertationModalView>();
            //List<Dissertation> ld = ;
            foreach (Dissertation imet in db.Dissertations.ToList()) {
                DissertationModalView _dmv = new DissertationModalView();
                _dmv.Author = imet.Author;
                _dmv.City = imet.City;
                _dmv.Id = imet.Id;
                _dmv.Index = imet.Index;
                _dmv.Name = imet.Name;
                _dmv.Number = imet.Number;
                _dmv.Rank = imet.Rank;
                _dmv.Type = db.Types.SingleOrDefault(n => n.Id == imet.Type).Name;
                lDMV.Add(_dmv);
            }
            return lDMV;

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