using GLIMPSE.Domain.Models;

namespace GLIMPSE.Domain.Helpers;

public class InactiveHandlingHelper
{
    public static bool IsObjectActive(User user)
    {
        if (user.IsActive == false)
        {
            return false;
        }
        return true;
    }
}
