using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Tables
{    
    public abstract class Entity
    {
        public Guid ID { get; set; } = Guid.NewGuid();        
    }
}