using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mantis_tests.MantisConnect;

namespace mantis_tests
{
    public class ProjectDataComparator :IComparer<MantisConnect.ProjectData>
    {
        public int Compare(MantisConnect.ProjectData x, MantisConnect.ProjectData y)
        {
            if (Object.ReferenceEquals(x, y))
            {
                return 1;
            }
            return x.name.CompareTo(y.name);

        }

    }
}
