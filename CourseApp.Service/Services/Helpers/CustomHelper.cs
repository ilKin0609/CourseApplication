using CourseApp.Domain.Models;
using System.Text.RegularExpressions;

namespace CourseApp.Service.Services.Helpers;

public static class CustomHelper
{
    public static bool StrCheck(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return false;

        Regex regex = new Regex("^[A-Za-zƏəÖöÜüİıŞşÇçĞğ]+$");
        return regex.IsMatch(value);
    }
}
