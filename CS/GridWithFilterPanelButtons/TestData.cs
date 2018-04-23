using System;
using System.Linq;

namespace GridWithFilterPanelButtons
{
    class TestData
    {
        public TestData(string name, bool check)
        {
            Name = name;
            Check = check;
        }

        public bool Check { get; set; }
        public string Name { get; set; }
    }
}
