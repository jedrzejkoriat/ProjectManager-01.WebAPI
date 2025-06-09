using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager_01.Application.Exceptions;

public sealed class OperationFailedException : Exception
{
    public OperationFailedException(string message) : base(message) { }
}