using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OS_Practice_6
{
    class FixedSections
    {
        public List<Section> Sections { get; set; }
        public FixedSections(int count, int size, params int[] sections)
        {
            Sections = new List<Section>();
            for (int i = 0; i < count; i++)
            {
                Sections.Add(new Section(size));
            }
        }
    }
}
