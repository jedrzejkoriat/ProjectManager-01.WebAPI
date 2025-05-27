namespace ProjectManager_01.WebAPI.Domain.Enums;

public enum Resolution
{
    Unresolved = 0,
    Fixed = 1,
    Duplicate = 2,
    CannotReproduce = 3,
    WontFix = 4,
    Incomplete = 5,
    Invalid = 6,
    Done = 7,
    Cancelled = 8,
    WontDo = 9
}