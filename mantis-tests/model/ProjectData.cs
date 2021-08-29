using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB.Mapping;

namespace mantis_tests
{
    [Table(Name = "mantis_project_table")]
    public class ProjectData : IEquatable<ProjectData>, IComparable<ProjectData>
    {
        [Column(Name = "name")]
        public string Name { get; set; }

        [Column(Name = "id")]
        public string Id { get; set; }

        public ProjectData()
        {

        }

        public static List<ProjectData> GetAll()
        {
            using (MantisDB db = new MantisDB())
            {
                return (from p in db.Projects select p).ToList();
            }
        }

        public bool Equals(ProjectData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, other))
            {
                return true;
            }

            return Name == other.Name;
        }


        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public int CompareTo(ProjectData other)
        {
            if (Object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return Name.CompareTo(other.Name);
        }
    }
}
