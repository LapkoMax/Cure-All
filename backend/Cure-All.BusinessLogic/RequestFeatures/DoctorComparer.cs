using Cure_All.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cure_All.BusinessLogic.RequestFeatures
{
    public class DoctorComparer : EqualityComparer<Doctor>
    {
        public override bool Equals(Doctor doc1, Doctor doc2)
        {
            return doc1.Id.Equals(doc2.Id);
        }

        public override int GetHashCode(Doctor doc)
        {
            return doc.Id.GetHashCode();
        }
    }
}
