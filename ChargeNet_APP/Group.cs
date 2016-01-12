﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace ChargeNet_APP
{
   
    public class Group<T> : List<T>
    {
        public string Title
        {
            get;
            set;
        }
        public Group(string name, IEnumerable<T> items): base(items)
        {
            this.Title = name;
        }

    }
}
