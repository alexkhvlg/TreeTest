using System;
using System.Collections.Generic;

namespace TreeTest
{
    public class Company
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ParentId { get; set; }
        public virtual List<Company> Childs { get; set; }
    }
}
