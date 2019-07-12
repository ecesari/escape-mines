using System;
using System.Collections.Generic;
using System.Text;

namespace Service
{
    public interface ICommandable
    {
        void Run(string command);
    }
}
