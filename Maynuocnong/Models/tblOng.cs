using System;
using System.Collections.Generic;

namespace Maynuocnong.Models
{
    public partial class tblOng
    {
        public int id { get; set; }
        public string Name { get; set; }
        public Nullable<int> Ord { get; set; }
        public Nullable<bool> Active { get; set; }
    }
}
