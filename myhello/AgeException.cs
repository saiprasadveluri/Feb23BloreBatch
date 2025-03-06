using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myhello
{
    public class AgeException : ApplicationException
    {
        public AgeException(string message) : base(message)
        {
            
        }
        
    }

   public class InsufficientBalanceException : ApplicationException
    {
        public InsufficientBalanceException(string message) : base(message)
        {

        }

    }
}
