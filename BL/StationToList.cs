using System;
using System.Collections.Generic;
using System.Text;

namespace IBL
{
    namespace BO
    {
        class StationToList
        {
            public int Id { get; set; }
            public String Name { get; set; }
            public int ReadyStandsInStation { get; set; }
            public int OccupiedStandsInStation { get; set; }
        }
    }
}
