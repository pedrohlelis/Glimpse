using Glimpse.Models;

namespace Glimpse.Helpers;

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
