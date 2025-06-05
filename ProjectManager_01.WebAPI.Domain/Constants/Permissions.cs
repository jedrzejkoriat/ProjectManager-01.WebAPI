using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManager_01.Domain.Constants;
public static class Permissions
{
    public const string ReadComment = "ReadComment";
    public const string WriteComment = "WriteComment";
    public const string ReadPriority = "ReadPriority";
    public const string ReadProject = "ReadProject";
    public const string ReadTag = "ReadTag";
    public const string WriteTag = "WriteTag";
    public const string ReadTicket = "ReadTicket";
    public const string WriteTicket = "WriteTicket";
    public const string DeleteTicket = "DeleteTicket";
    public const string ReadTicketRelation = "ReadTicketRelation";
    public const string WriteTicketRelation = "WriteTicketRelation";
    public const string ReadTicketTag = "ReadTicketTag";
    public const string WriteTicketTag = "WriteTicketTag";
}