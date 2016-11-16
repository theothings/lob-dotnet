using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lob.Net
{
    public class UnauthorizedException : Exception
    {
    }

    public class ForbiddenException : Exception
    {
    }

    public class NotFoundException : Exception
    {
    }

    public class BadRequestException : Exception
    {
    }

    public class TooManyRequestsException : Exception
    {
    }

    public class ServerErrorException : Exception
    {
    }
}
