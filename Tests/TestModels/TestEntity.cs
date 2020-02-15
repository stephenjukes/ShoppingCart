using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.TestModels
{
    public class TestEntity<TActual>
    {
        public virtual int Id { get; set; }
        public virtual string EntityName { get; set; }
        public virtual TActual ActualEntity { get; set; }
    }
}
